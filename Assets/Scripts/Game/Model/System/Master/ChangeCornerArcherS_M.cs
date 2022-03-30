using Chessy.Game.Entity.Model;
using Chessy.Game.System.Model;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Realtime;

namespace Chessy.Game.Model.System
{
    sealed class ChangeCornerArcherS_M : SystemModelGameAbs
    {
        internal ChangeCornerArcherS_M(in SystemsModelGame sMGame, in EntitiesModelGame eMGame) : base(sMGame, eMGame) { }

        internal void Change(in byte cell_0, in AbilityTypes abilityT, in Player sender)
        {
            if (e.UnitStepC(cell_0).Steps >= StepValues.Need(abilityT))
            {
                e.UnitIsRightArcherC(cell_0).ToggleSide();

                e.UnitStepC(cell_0).Steps -= StepValues.CHANGE_CORNER_ARCHER;

                e.RpcPoolEs.SoundToGeneral(sender, ClipTypes.PickArcher);
            }

            else
            {
                e.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}