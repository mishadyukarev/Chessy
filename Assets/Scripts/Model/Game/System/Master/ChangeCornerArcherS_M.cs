using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Realtime;

namespace Chessy.Game.Model.System
{
    sealed partial class UnitAbilitiesSystems : SystemModel
    {
        internal void TryChangeCornerArcher(in byte cell_0, in AbilityTypes abilityT, in Player sender)
        {
            if (_eMG.StepUnitC(cell_0).Steps >= StepValues.Need(abilityT))
            {
                _eMG.UnitIsRightArcherC(cell_0).ToggleSide();

                _eMG.StepUnitC(cell_0).Steps -= StepValues.CHANGE_CORNER_ARCHER;

                _sMG.ExecuteSoundActionToGeneral(sender, ClipTypes.PickArcher);
            }

            else
            {
                _eMG.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}