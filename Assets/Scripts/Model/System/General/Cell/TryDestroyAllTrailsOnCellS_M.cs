namespace Chessy.Model.Model.System
{
    public sealed partial class SystemsModel : IUpdate
    {
        internal void TryDestroyAllTrailsOnCell(in byte cell)
        {
            for (var dirT = (DirectTypes)1; dirT < DirectTypes.End; dirT++)
            {
                if (_e.HealthTrail(cell).IsAlive(dirT))
                {
                    _e.HealthTrail(cell).Health(dirT) = 0;
                }
            }
        }
    }
}