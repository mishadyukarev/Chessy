using Chessy.Game.Entity.Model;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Realtime;

namespace Chessy.Game.Model.System
{
    public sealed class ChangeCornerArcherS_M : SystemModelGameAbs
    {
        public ChangeCornerArcherS_M(in EntitiesModelGame eMGame) : base(eMGame)
        {
        }

        public void Change(in byte cell_0, in AbilityTypes abilityT, in Player sender)
        {
            if (eMGame.UnitStepC(cell_0).Steps >= StepValues.Need(abilityT))
            {
                eMGame.UnitIsRightArcherC(cell_0).ToggleSide();

                eMGame.UnitStepC(cell_0).Steps -= StepValues.CHANGE_CORNER_ARCHER;

                eMGame.RpcPoolEs.SoundToGeneral(sender, ClipTypes.PickArcher);
            }

            else
            {
                eMGame.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}