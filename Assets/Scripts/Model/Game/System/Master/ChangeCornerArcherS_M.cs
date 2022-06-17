using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Realtime;

namespace Chessy.Game.Model.System
{
    sealed partial class UnitAbilitiesSystems : SystemModel
    {
        internal void Change(in byte cell_0, in AbilityTypes abilityT, in Player sender)
        {
            if (eMG.StepUnitC(cell_0).Steps >= StepValues.Need(abilityT))
            {
                eMG.UnitIsRightArcherC(cell_0).ToggleSide();

                eMG.StepUnitC(cell_0).Steps -= StepValues.CHANGE_CORNER_ARCHER;

                eMG.RpcPoolEs.SoundToGeneral(sender, ClipTypes.PickArcher);
            }

            else
            {
                eMG.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}