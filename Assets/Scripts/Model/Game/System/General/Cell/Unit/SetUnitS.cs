using Chessy.Common;
using Chessy.Common.Entity;
using Chessy.Common.Model.System;
using Chessy.Game.Entity.Model;
using Chessy.Game.Model.System;

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
                eMG.UnitEs(cell_to).Ability(buttonT) = eMG.UnitEs(cell_from).Ability(buttonT);
            }

            for (var abilityT = AbilityTypes.None + 1; abilityT < AbilityTypes.End; abilityT++)
            {
                eMG.UnitEs(cell_to).CoolDownC(abilityT) = eMG.UnitEs(cell_from).CoolDownC(abilityT);
            }

            for (var playerT = PlayerTypes.None + 1; playerT < PlayerTypes.End; playerT++)
            {
                eMG.UnitEs(cell_to).ForPlayer(playerT) = eMG.UnitEs(cell_from).ForPlayer(playerT);
            }
        }
    }
}