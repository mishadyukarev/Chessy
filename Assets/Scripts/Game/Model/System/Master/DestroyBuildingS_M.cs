using Chessy.Game.Entity.Model;
using Chessy.Game.System.Model;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Pun;
using Photon.Realtime;

namespace Chessy.Game.Model.System
{
    sealed class DestroyBuildingS_M : SystemModelGameAbs
    {
        internal DestroyBuildingS_M(in SystemsModelGame sMGame, in EntitiesModelGame eMGame) : base(sMGame, eMGame) { }

        internal void Destroy(in byte cell_0, in Player sender)
        {
            if (e.UnitStepC(cell_0).HaveAnySteps)
            {
                e.RpcPoolEs.SoundToGeneral(RpcTarget.All, ClipTypes.Destroy);

                s.DestroyBuildingS.Attack(cell_0, 1f, e.UnitPlayerTC(cell_0).Player);

                e.UnitStepC(cell_0).Steps -= StepValues.DESTROY_BUILDING;
            }

            else
            {
                e.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}