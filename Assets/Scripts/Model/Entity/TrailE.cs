using Chessy.Model.Component;

namespace Chessy.Model.Entity
{
    public sealed class TrailE
    {
        public readonly VisibleToOtherPlayerOrNotC VisibleC = new(default);
        public readonly TrailsHealthOnCellC HealthC = new(new float[(byte)DirectTypes.End]);

        internal void Dispose()
        {
            VisibleC.Dispose();
            HealthC.Dispose();
        }
    }
}