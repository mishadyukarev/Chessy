using Chessy.Game.Entity.Model.Cell.Unit;

namespace Chessy.Game.System.Model
{
    public struct SetLastDiedS
    {
        public void Set(in UnitMainE unitMainE, ref WhoLastDiedHereE e)
        {
            e.UnitTC = unitMainE.UnitTC;
            e.LevelTC = unitMainE.LevelTC;
            e.PlayerTC = unitMainE.PlayerTC;
        }
    }
}