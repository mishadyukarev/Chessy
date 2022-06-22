using Chessy.Game.Model.Entity;

namespace Chessy.Game.Model.System
{
    sealed partial class UnitSystems : SystemModel
    {
        internal readonly UnitAbilitiesSystems UnitAbilitiesSs;

        internal UnitSystems(in SystemsModelGame s, in EntitiesModelGame e) : base(s, e)
        {
            UnitAbilitiesSs = new UnitAbilitiesSystems(s, e);
        }

        internal void CopyExtraTW(in byte cell_from, in byte cell_to)
        {
            _e.SetExtraToolWeaponT(cell_to, _e.ExtraToolWeaponT(cell_from));
            _e.SetExtraTWLevelT(cell_to, _e.ExtraTWLevelT(cell_from));
            _e.ExtraTWProtectionC(cell_to) = _e.ExtraTWProtectionC(cell_from);
        }
    }

    sealed partial class UnitAbilitiesSystems : SystemModel
    {
        internal UnitAbilitiesSystems(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG)
        {
        }
    }
}