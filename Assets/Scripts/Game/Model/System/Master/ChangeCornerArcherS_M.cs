using Chessy.Game.Entity.Model;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Realtime;

namespace Chessy.Game.Model.System
{
    public sealed class ChangeCornerArcherS_M : SystemModelGameAbs
    {
        readonly CellEs _cellEs;

        public ChangeCornerArcherS_M(in CellEs cellEs, in EntitiesModelGame eMGame) : base(eMGame)
        {
            _cellEs = cellEs;
        }

        public void Change(in AbilityTypes abilityT, in Player sender)
        {
            if (_cellEs.UnitStatsE.StepC.Steps >= StepValues.Need(abilityT))
            {
                _cellEs.UnitMainE.IsRightArcherC.ToggleSide();

                _cellEs.UnitStatsE.StepC.Steps -= StepValues.CHANGE_CORNER_ARCHER;

                eMGame.RpcPoolEs.SoundToGeneral(sender, ClipTypes.PickArcher);
            }

            else
            {
                eMGame.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}