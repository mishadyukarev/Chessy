using Chessy.Model.Model.Component;
using System.Collections.Generic;

namespace Chessy.Model
{
    public readonly struct TrailE
    {
        public readonly VisibleToOtherPlayerOrNotC VisibleC;
        public readonly HealthTrailC HealthC;

        internal TrailE(in bool b)
        {
            var vis = new Dictionary<PlayerTypes, bool>();

            VisibleC = new VisibleToOtherPlayerOrNotC(default);
            HealthC = new HealthTrailC(new float[(byte)DirectTypes.End - 1]);
        }
    }
}