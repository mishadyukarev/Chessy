using Chessy.Game.Model.Component;
using System.Collections.Generic;

namespace Chessy.Game.Model.Entity
{
    public readonly struct TrailE
    {
        public readonly VisibleC VisibleC;
        public readonly HealthTrailC HealthC;

        internal TrailE(in bool b)
        {
            var vis = new Dictionary<PlayerTypes, bool>();

            for (var playerT = (PlayerTypes)1; playerT < PlayerTypes.End; playerT++) 
                vis.Add(playerT, default);

            VisibleC = new VisibleC(vis);
            HealthC = new HealthTrailC(new float[(byte)DirectTypes.End - 1]);
        }
    }
}