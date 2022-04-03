using Chessy.Common.Entity;
using Chessy.Common.Model.System;
using Chessy.Game.Model.Entity;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Pun;
using Photon.Realtime;

namespace Chessy.Game.Model.System
{
    sealed class DestroyBuildingS_M : SystemModelGameAbs
    {
        public DestroyBuildingS_M(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMC, eMC, sMG, eMG)
        {
        }

        internal void Destroy(in byte cell_0, in Player sender)
        {
            if (eMG.UnitStepC(cell_0).HaveAnySteps)
            {
                eMG.RpcPoolEs.SoundToGeneral(RpcTarget.All, ClipTypes.Destroy);

                sMG.DestroyBuildingS.Attack(cell_0, 1f, eMG.UnitPlayerTC(cell_0).PlayerT);

                eMG.UnitStepC(cell_0).Steps -= StepValues.DESTROY_BUILDING;
            }

            else
            {
                eMG.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}