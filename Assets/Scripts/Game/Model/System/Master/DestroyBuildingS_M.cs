using Chessy.Game.Entity.Model;
using Chessy.Game.System.Model;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Pun;
using Photon.Realtime;

namespace Chessy.Game.Model.System
{
    sealed class DestroyBuildingS_M : SystemModelGameAbs
    {
        readonly CellEs _cellEs;
        readonly AttackBuildingS _destroyBuildingS;

        internal DestroyBuildingS_M(in CellEs cellEs, in AttackBuildingS destroyBuildingS, in EntitiesModelGame eMGame) : base(eMGame)
        {
            _cellEs = cellEs;
            _destroyBuildingS = destroyBuildingS;
        }

        internal void Destroy(in Player sender)
        {
            if (_cellEs.UnitStatsE.StepC.HaveAnySteps)
            {
                eMGame.RpcPoolEs.SoundToGeneral(RpcTarget.All, ClipTypes.Destroy);

                _destroyBuildingS.Attack(1f, _cellEs.UnitMainE.PlayerTC.Player);

                _cellEs.UnitStatsE.StepC.Steps -= StepValues.DESTROY_BUILDING;
            }

            else
            {
                eMGame.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}