using Chessy.Game.Entity.Model;
using Chessy.Game.System.Model;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Pun;
using Photon.Realtime;

namespace Chessy.Game.Model.System
{
    public sealed class DestroyBuildingS_M : SystemModelGameAbs
    {
        public DestroyBuildingS_M(in EntitiesModelGame eMGame) : base(eMGame)
        {
        }

        public void Destroy(in byte cell_0, in Player sender)
        {
            if (eMGame.UnitStepC(cell_0).HaveAnySteps)
            {
                eMGame.RpcPoolEs.SoundToGeneral(RpcTarget.All, ClipTypes.Destroy);

                new DestroyBuildingS(1f, eMGame.UnitPlayerTC(cell_0).Player, cell_0, eMGame);

                eMGame.UnitStepC(cell_0).Steps -= StepValues.DESTROY_BUILDING;
            }

            else
            {
                eMGame.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}