using Chessy.Model.Model.Component;
using System.Collections.Generic;

namespace Chessy.Model.Model.Entity
{
    public readonly struct TrailE
    {
        public readonly VisibleC VisibleC;
        public readonly HealthTrailC HealthC;

        internal TrailE(in bool b)
        {
            var vis = new Dictionary<PlayerTypes, bool>();

            VisibleC = new VisibleC(default);
            HealthC = new HealthTrailC(new float[(byte)DirectTypes.End - 1]);
        }
    }
}