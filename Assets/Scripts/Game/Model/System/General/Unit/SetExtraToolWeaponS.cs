using Chessy.Game.Entity.Model;

namespace Chessy.Game.Model.System
{
    public sealed class SetExtraToolWeaponS : SystemModelGameAbs
    {
        public SetExtraToolWeaponS(in EntitiesModelGame eMGame) : base(eMGame) { }

        public void Set(in ToolWeaponTypes twT, in LevelTypes levelT, in float protection, in byte cell_0)
        {
            eMGame.UnitExtraTWTC(cell_0).ToolWeapon = twT;
            eMGame.UnitExtraLevelTC(cell_0).Level = levelT;
            eMGame.UnitExtraProtectionTC(cell_0).Protection = protection;
        }
    }
}