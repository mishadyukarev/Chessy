namespace Chessy.Game.Model.System
{
    public sealed partial class SystemsModelGame : IUpdate
    {
        internal void TryDestroyAllTrailsOnCell(in byte cell)
        {
            for (var dirT = (DirectTypes)1; dirT < DirectTypes.End; dirT++)
            {
                if (_eMG.HealthTrail(cell).IsAlive(dirT))
                {
                    _eMG.HealthTrail(cell).Health(dirT) = 0;
                }
            }
        }
    }
}