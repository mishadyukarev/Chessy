using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component;
using Assets.Scripts.ECS.Game.General.Systems.StartFill;
using Assets.Scripts.ECS.System.Data.Game.General.Cell;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Cell;
using Leopotam.Ecs;
using Photon.Pun;

namespace Assets.Scripts.ECS.Systems.Game.Master.PunRPC
{
    internal sealed class CircularAttackKingSystem : IEcsRunSystem
    {
        private EcsFilter<XyUnitsComponent> _xyUnitsFilter;

        public void Run()
        {
            ref var xyUnitsCom = ref _xyUnitsFilter.Get1(0);

            if (CellUnitsDataSystem.HaveMaxAmountSteps(RpcMasterDataContainer.XyCellForCircularAttack))
            {
                foreach (var xy1 in CellSpaceWorker.TryGetXyAround(RpcMasterDataContainer.XyCellForCircularAttack))
                {
                    if (CellUnitsDataSystem.HaveAnyUnit(xy1))
                    {
                        CellUnitsDataSystem.TakeAmountHealth(xy1, CellUnitsDataSystem.SimplePowerDamage(RpcMasterDataContainer.XyCellForCircularAttack) / 2);

                        if (!CellUnitsDataSystem.HaveAmountHealth(xy1))
                        {
                            var unitTypeXy1 = CellUnitsDataSystem.UnitType(xy1);
                            var isMasterXy1 = CellUnitsDataSystem.IsMasterClient(xy1);

                            if (CellUnitsDataSystem.IsUnitType(UnitTypes.King, xy1)) PhotonPunRPC.EndGameToMaster(CellUnitsDataSystem.Owner(RpcMasterDataContainer.XyCellForCircularAttack).ActorNumber);
                            xyUnitsCom.RemoveAmountUnitsInGame(unitTypeXy1, isMasterXy1, xy1);
                            CellUnitsDataSystem.ResetUnit(xy1);

                        }

                        PhotonPunRPC.SoundToGeneral(RpcTarget.All, SoundEffectTypes.AttackMelee);
                    }
                }
                CellUnitsDataSystem.TakeAmountSteps(RpcMasterDataContainer.XyCellForCircularAttack);

                PhotonPunRPC.SoundToGeneral(RpcMasterDataContainer.InfoFrom.Sender, SoundEffectTypes.AttackMelee);


                var conditionType = CellUnitsDataSystem.ConditionType(RpcMasterDataContainer.XyCellForCircularAttack);
                if (conditionType == ConditionUnitTypes.Protected || conditionType == ConditionUnitTypes.Relaxed)
                {
                    InitSystem.XyUnitsContitionCom.ReplaceCondition(conditionType, ConditionUnitTypes.None, UnitTypes.King, RpcMasterDataContainer.InfoFrom.Sender.IsMasterClient, RpcMasterDataContainer.XyCellForCircularAttack);
                    CellUnitsDataSystem.ResetConditionType(RpcMasterDataContainer.XyCellForCircularAttack);
                }
            }
            else
            {
                PhotonPunRPC.MistakeStepsUnitToGeneral(RpcMasterDataContainer.InfoFrom.Sender);
                PhotonPunRPC.SoundToGeneral(RpcMasterDataContainer.InfoFrom.Sender, SoundEffectTypes.Mistake);
            }
        }
    }
}
