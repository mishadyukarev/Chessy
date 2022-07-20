using System;
namespace Chessy.Model
{
    public sealed class AdC
    {
        public DateTime LastTimeAd { get; internal set; }

        internal AdC(in DateTime startGameTime) => LastTimeAd = startGameTime;
    }
}