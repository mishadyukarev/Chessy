using Chessy.Game.Entity.Model;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Realtime;

namespace Chessy.Game.Model.System
{
    public sealed class PutOutFirePawnS_M : SystemModelGameAbs
    {
        readonly CellEs _cellEs;

        public PutOutFirePawnS_M(in CellEs cellEs, in EntitiesModelGame eMGame) : base(eMGame)
        {
            _cellEs = cellEs;
        }

        public void PutOut(in Player sender)
        {
            if (_cellEs.UnitStatsE.StepC.Steps >= StepValues.PUT_OUT_FIRE_PAWN)
            {
                _cellEs.EffectEs.HaveFire = false;

                _cellEs.UnitStatsE.StepC.Steps -= StepValues.PUT_OUT_FIRE_PAWN;
            }

            else
            {
                eMGame.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}