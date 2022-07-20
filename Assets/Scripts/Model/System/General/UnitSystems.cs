﻿using Chessy.Model.Entity;
namespace Chessy.Model.System
{
    sealed partial class UnitSystems : SystemModelAbstract
    {
        internal readonly UnitAbilitiesSystems UnitAbilitiesSs;

        internal UnitSystems(SystemsModel s, in EntitiesModel e) : base(s, e)
        {
            UnitAbilitiesSs = new UnitAbilitiesSystems(s, e);
        }
    }

    sealed partial class UnitAbilitiesSystems : SystemModelAbstract
    {
        internal UnitAbilitiesSystems(in SystemsModel sMG, in EntitiesModel eMG) : base(sMG, eMG)
        {
        }
    }
}