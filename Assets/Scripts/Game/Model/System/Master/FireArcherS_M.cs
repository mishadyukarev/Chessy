using Chessy.Game.Entity.Model;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Pun;
using Photon.Realtime;

namespace Chessy.Game.Model.System
{
    public sealed class FireArcherS_M : SystemModelGameAbs
    {
        readonly CellEs _cellEs;

        public FireArcherS_M(in CellEs cellEs, in EntitiesModelGame eMGame) : base(eMGame)
        {
            _cellEs = cellEs;
        }

        public void Fire(in byte idx_to, in Player sender)
        {
            if (_cellEs.UnitEs.ForArson.Contains(idx_to))
            {
                if (_cellEs.UnitStatsE.StepC.Steps >= StepValues.ARCHER_FIRE)
                {
                    eMGame.RpcPoolEs.SoundToGeneral(RpcTarget.All, AbilityTypes.FireArcher);

                    _cellEs.UnitStatsE.StepC.Steps -= StepValues.ARCHER_FIRE;
                    eMGame.HaveFire(idx_to) = true;

                }

                else
                {
                    eMGame.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedMoreSteps, sender);
                }
            }
        }
    }
}