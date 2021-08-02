using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Cell;
using Assets.Scripts.Workers.Game.Else.Info.Units;
using Leopotam.Ecs;
using Photon.Pun;

namespace Assets.Scripts.ECS.Systems.Game.Master.PunRPC
{
    internal sealed class CircularAttackKingSystem : IEcsRunSystem
    {
        public void Run()
        {
            if (CellUnitsDataContainer.HaveMaxAmountSteps(RpcMasterDataContainer.XyCellForCircularAttack))
            {
                foreach (var xy1 in CellSpaceWorker.TryGetXyAround(RpcMasterDataContainer.XyCellForCircularAttack))
                {
                    if (CellUnitsDataContainer.HaveAnyUnit(xy1))
                    {
                        CellUnitsDataContainer.TakeAmountHealth(xy1, CellUnitsDataContainer.SimplePowerDamage(RpcMasterDataContainer.XyCellForCircularAttack) / 2);

                        if (!CellUnitsDataContainer.HaveAmountHealth(xy1))
                        {
                            var unitTypeXy1 = CellUnitsDataContainer.UnitType(xy1);
                            var isMasterXy1 = CellUnitsDataContainer.IsMasterClient(xy1);

                            if (CellUnitsDataContainer.IsUnitType(UnitTypes.King, xy1)) PhotonPunRPC.EndGameToMaster(CellUnitsDataContainer.Owner(RpcMasterDataContainer.XyCellForCircularAttack).ActorNumber);
                            InfoUnitsDataContainer.RemoveAmountUnitsInGame(unitTypeXy1, isMasterXy1, xy1);
                            CellUnitsDataContainer.ResetUnit(xy1);

                        }

                        PhotonPunRPC.SoundToGeneral(RpcTarget.All, SoundEffectTypes.AttackMelee);
                    }
                }
                CellUnitsDataContainer.TakeAmountSteps(RpcMasterDataContainer.XyCellForCircularAttack);

                PhotonPunRPC.SoundToGeneral(RpcMasterDataContainer.InfoFrom.Sender, SoundEffectTypes.AttackMelee);


                var conditionType = CellUnitsDataContainer.ConditionType(RpcMasterDataContainer.XyCellForCircularAttack);
                if (conditionType == ConditionUnitTypes.Protected || conditionType == ConditionUnitTypes.Relaxed)
                {
                    InfoUnitsDataContainer.ReplaceCondition(conditionType, ConditionUnitTypes.None, UnitTypes.King, RpcMasterDataContainer.InfoFrom.Sender.IsMasterClient, RpcMasterDataContainer.XyCellForCircularAttack);
                    CellUnitsDataContainer.ResetConditionType(RpcMasterDataContainer.XyCellForCircularAttack);
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
