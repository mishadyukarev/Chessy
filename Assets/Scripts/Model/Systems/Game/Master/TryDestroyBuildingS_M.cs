using Chessy.Game.Model.Entity;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Pun;
using Photon.Realtime;

namespace Chessy.Game.Model.System
{
    sealed class TryDestroyBuildingS_M : SystemModel
    {
        public TryDestroyBuildingS_M(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG)
        {
        }

        internal void Destroy(in byte cell_0, in Player sender)
        {
            if (eMG.StepUnitC(cell_0).HaveAnySteps)
            {
                eMG.RpcPoolEs.SoundToGeneral(RpcTarget.All, ClipTypes.Destroy);

                sMG.BuildingSs.DestroyS.Attack(cell_0, 1f, eMG.UnitPlayerTC(cell_0).PlayerT);

                eMG.StepUnitC(cell_0).Steps -= StepValues.DESTROY_BUILDING;
            }

            else
            {
                eMG.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}