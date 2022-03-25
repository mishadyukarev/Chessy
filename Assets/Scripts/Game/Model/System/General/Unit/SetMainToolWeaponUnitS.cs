using Chessy.Game.Entity.Model;

namespace Chessy.Game.Model.System
{
    public sealed class SetMainToolWeaponUnitS : SystemModelGameAbs
    {
        public SetMainToolWeaponUnitS(in EntitiesModelGame eMGame) : base(eMGame) { }

        public void Set(in ToolWeaponTypes twT, in LevelTypes levelT, in byte cell_0)
        {
            eMGame.UnitMainTWTC(cell_0).ToolWeapon = twT;
            eMGame.UnitMainTWLevelTC(cell_0).Level = levelT;
        }
    }
}