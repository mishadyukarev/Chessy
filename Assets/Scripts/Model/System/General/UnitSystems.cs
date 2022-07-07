using Chessy.Model.Entity;
namespace Chessy.Model.System
{
    sealed partial class UnitSystems : SystemModelAbstract
    {
        internal readonly UnitAbilitiesSystems UnitAbilitiesSs;

        internal UnitSystems(SystemsModel s, in EntitiesModel e) : base(s, e)
        {
            UnitAbilitiesSs = new UnitAbilitiesSystems(s, e);
        }

        internal void CopyExtraTW(in byte cell_from, in byte cell_to)
        {
            _e.SetExtraToolWeaponT(cell_to, _e.ExtraToolWeaponT(cell_from));
            _e.SetExtraTWLevelT(cell_to, _e.ExtraTWLevelT(cell_from));
            _e.UnitExtraTWC(cell_to) = _e.UnitExtraTWC(cell_from);
        }
    }

    sealed partial class UnitAbilitiesSystems : SystemModelAbstract
    {
        internal UnitAbilitiesSystems(in SystemsModel sMG, in EntitiesModel eMG) : base(sMG, eMG)
        {
        }
    }
}