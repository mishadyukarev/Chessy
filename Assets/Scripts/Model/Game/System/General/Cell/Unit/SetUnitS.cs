using Chessy.Common;
using Chessy.Common.Entity;
using Chessy.Common.Model.System;
using Chessy.Game.Model.Entity;

namespace Chessy.Game.Model.System
{
    sealed class SetUnitS : SystemModelGameAbs
    {
        internal SetUnitS(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMC, eMC, sMG, eMG) { }

        public void Set(in byte cell_from, in byte cell_to)
        {
            sMG.UnitSs.SetMainS.Set(cell_from, cell_to);
            sMG.UnitSs.SetEffectsS.Set(cell_from, cell_to);
            sMG.UnitSs.SetStatsS.Set(cell_from, cell_to);
            sMG.UnitSs.SetMainTWS.Set(cell_from, cell_to);
            sMG.UnitSs.SetExtraTWS.Set(cell_from, cell_to);

            for (var buttonT = ButtonTypes.None + 1; buttonT < ButtonTypes.End; buttonT++)
            {
                eMG.UnitButtonAbilitiesC(cell_to).SetAbility(buttonT, eMG.UnitButtonAbilitiesC(cell_from).Ability(buttonT));
            }

            eMG.UnitCooldownAbilitiesC(cell_to).Set(eMG.UnitCooldownAbilitiesC(cell_from));
        }
    }
}