﻿using Chessy.Model;

namespace Chessy.Model
{
    sealed partial class UnitSystems : SystemModel
    {
        internal readonly UnitAbilitiesSystems UnitAbilitiesSs;

        internal UnitSystems(in SystemsModel s, in EntitiesModel e) : base(s, e)
        {
            UnitAbilitiesSs = new UnitAbilitiesSystems(s, e);
        }

        internal void CopyExtraTW(in byte cell_from, in byte cell_to)
        {
            _e.SetExtraToolWeaponT(cell_to, _e.ExtraToolWeaponT(cell_from));
            _e.SetExtraTWLevelT(cell_to, _e.ExtraTWLevelT(cell_from));
            _e.UnitExtraTWE(cell_to) = _e.UnitExtraTWE(cell_from);
        }
    }

    sealed partial class UnitAbilitiesSystems : SystemModel
    {
        internal UnitAbilitiesSystems(in SystemsModel sMG, in EntitiesModel eMG) : base(sMG, eMG)
        {
        }
    }
}