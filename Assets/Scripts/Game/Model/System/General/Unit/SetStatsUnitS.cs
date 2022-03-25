using Chessy.Game.Entity.Model;

namespace Chessy.Game.Model.System
{
    public sealed class SetStatsUnitS : SystemModelGameAbs
    {
        public SetStatsUnitS(in EntitiesModelGame eMGame) : base(eMGame) { }

        public void Set(in float hp, in float steps, in float water, in byte cell_0)
        {
            eMGame.UnitHpC(cell_0).Health = hp;
            eMGame.UnitStepC(cell_0).Steps = steps;
            eMGame.UnitWaterC(cell_0).Water = water;
        }
    }
}