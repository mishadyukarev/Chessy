using Chessy.Common;
using Chessy.Game.Entity.Model;
using Chessy.Game.System.Model;

namespace Chessy.Game.Model.System
{
    sealed class SetUnitS : SystemModelGameAbs
    {
        internal SetUnitS(in SystemsModelGame sMGame, in EntitiesModelGame eMGame) : base(sMGame, eMGame) { }

        public void Set(in byte cell_from, in byte cell_to)
        {
            s.SetMainS.Set(cell_from, cell_to);
            s.SetEffectsS.Set(cell_from, cell_to);
            s.SetStatsS.Set(cell_from, cell_to);
            s.SetMainTWS.Set(cell_from, cell_to);
            s.SetExtraTWS.Set(cell_from, cell_to);

            for (var buttonT = ButtonTypes.None + 1; buttonT < ButtonTypes.End; buttonT++)
            {
                e.UnitEs(cell_to).Ability(buttonT) = e.UnitEs(cell_from).Ability(buttonT);
            }

            for (var abilityT = AbilityTypes.None + 1; abilityT < AbilityTypes.End; abilityT++)
            {
                e.UnitEs(cell_to).CoolDownC(abilityT) = e.UnitEs(cell_from).CoolDownC(abilityT);
            }

            for (var playerT = PlayerTypes.None + 1; playerT < PlayerTypes.End; playerT++)
            {
                e.UnitEs(cell_to).ForPlayer(playerT) = e.UnitEs(cell_from).ForPlayer(playerT);
            }
        }
    }
}