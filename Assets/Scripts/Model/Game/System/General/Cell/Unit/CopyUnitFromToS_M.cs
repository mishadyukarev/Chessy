using Chessy.Common;

namespace Chessy.Game.Model.System
{
    sealed partial class UnitSystems
    {
        internal void CopyUnitFromTo(in byte cell_from, in byte cell_to)
        {
            _unitSimpleSs[cell_to].SetMainS.CopyFrom(_eMG.UnitMainE(cell_from));
            CopyEffects(cell_from, cell_to);
            Set(cell_from, cell_to);
            CopyMainTW(cell_from, cell_to);
            CopyExtraTW(cell_from, cell_to);

            for (var buttonT = ButtonTypes.None + 1; buttonT < ButtonTypes.End; buttonT++)
            {
                _eMG.UnitButtonAbilitiesC(cell_to).SetAbility(buttonT, _eMG.UnitButtonAbilitiesC(cell_from).Ability(buttonT));
            }

            _eMG.UnitCooldownAbilitiesC(cell_to).Set(_eMG.UnitCooldownAbilitiesC(cell_from));
        }
    }
}