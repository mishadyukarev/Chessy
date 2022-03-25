using Chessy.Game.Entity.Model;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Pun;
using Photon.Realtime;

namespace Chessy.Game.Model.System
{
    public sealed class FireArcherS_M : SystemModelGameAbs
    {
        public FireArcherS_M(in EntitiesModelGame eMGame) : base(eMGame)
        {
        }

        public void Fire(in byte idx_from, in byte idx_to, in Player sender)
        {
            if (eMGame.UnitEs(idx_from).ForArson.Contains(idx_to))
            {
                if (eMGame.UnitStepC(idx_from).Steps >= StepValues.ARCHER_FIRE)
                {
                    eMGame.RpcPoolEs.SoundToGeneral(RpcTarget.All, AbilityTypes.FireArcher);

                    eMGame.UnitStepC(idx_from).Steps -= StepValues.ARCHER_FIRE;
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