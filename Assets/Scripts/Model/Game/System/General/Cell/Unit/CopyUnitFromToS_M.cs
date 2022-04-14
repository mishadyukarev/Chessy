using Chessy.Common;
using Chessy.Game.Model.Entity;

namespace Chessy.Game.Model.System
{
    sealed class CopyUnitFromToS_M : SystemModel
    {
        internal CopyUnitFromToS_M(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG) { }

        internal void Copy(in byte cell_from, in byte cell_to)
        {
            sMG.UnitSs.SetMain(cell_from, cell_to);
            sMG.UnitSs.CopyEffects(cell_from, cell_to);
            sMG.UnitSs.Set(cell_from, cell_to);
            sMG.UnitSs.CopyMainTW(cell_from, cell_to);
            sMG.UnitSs.CopyExtraTW(cell_from, cell_to);

            for (var buttonT = ButtonTypes.None + 1; buttonT < ButtonTypes.End; buttonT++)
            {
                eMG.UnitButtonAbilitiesC(cell_to).SetAbility(buttonT, eMG.UnitButtonAbilitiesC(cell_from).Ability(buttonT));
            }

            eMG.UnitCooldownAbilitiesC(cell_to).Set(eMG.UnitCooldownAbilitiesC(cell_from));
        }
    }
}