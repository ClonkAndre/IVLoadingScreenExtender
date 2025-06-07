using System;
using System.Threading;

using IVSDKDotNet;
using IVSDKDotNet.Attributes;

namespace IVLoadingScreenExtender
{
    public class Main : Script
    {

        #region Variables
        [Range(0f, 1_000_000f)]
        public double ExtendBySeconds; // Public to make it editable in the Public Fields window
        #endregion

        #region Constructor
        public Main()
        {
            Initialized += Main_Initialized;
            GameLoad += Main_GameLoad;
        }
        #endregion

        private void Main_Initialized(object sender, EventArgs e)
        {
            ExtendBySeconds = Settings.GetDouble("Main", "ExtendBySeconds", 0.0d);

            // Check if value is not negative
            if (ExtendBySeconds < 0.0d)
                ExtendBySeconds = 0.0d;
        }

        private void Main_GameLoad(object sender, EventArgs e)
        {
            DateTime waitUntil = DateTime.UtcNow.AddSeconds(ExtendBySeconds);

            do
            {
                IVGrcWindow.ProcessWindowMessage();
                Thread.Sleep(1);
            }
            while (DateTime.UtcNow < waitUntil);
        }

    }
}
