using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Cell;
using Assets.Scripts.Workers.Game.Else.Info.Units;
using Assets.Scripts.Workers.Info;
using Leopotam.Ecs;
using Photon.Pun;
using UnityEngine;

namespace Assets.Scripts.ECS.Systems.Game.Master.PunRPC
{
    internal sealed class CircularAttackKingSystem : IEcsRunSystem
    {
        public void Run()
        {
            if (CellUnitsDataWorker.HaveMaxAmountSteps(RpcWorker.XyCellForCircularAttack))
            {
                foreach (var xy1 in CellSpaceWorker.TryGetXyAround(RpcWorker.XyCellForCircularAttack))
                {
                    if (CellUnitsDataWorker.HaveAnyUnit(xy1))
                    {
                        CellUnitsDataWorker.TakeAmountHealth(xy1, CellUnitsDataWorker.SimplePowerDamage(RpcWorker.XyCellForCircularAttack) / 2);

                        if (!CellUnitsDataWorker.HaveAmountHealth(xy1))
                        {
                            var unitTypeXy1 = CellUnitsDataWorker.UnitType(xy1);
                            var isMasterXy1 = CellUnitsDataWorker.IsMasterClient(xy1);

                            if (CellUnitsDataWorker.IsUnitType(UnitTypes.King, xy1)) PhotonPunRPC.EndGameToMaster(CellUnitsDataWorker.Owner(RpcWorker.XyCellForCircularAttack).ActorNumber);
                            InfoUnitsContainer.RemoveAmountUnitsInGame(unitTypeXy1, isMasterXy1, xy1);
                            CellUnitsDataWorker.ResetUnit(xy1);

                        }

                        PhotonPunRPC.SoundToGeneral(RpcTarget.All, SoundEffectTypes.AttackMelee);
                    }
                }
                CellUnitsDataWorker.TakeAmountSteps(RpcWorker.XyCellForCircularAttack);

                PhotonPunRPC.SoundToGeneral(RpcWorker.InfoFrom.Sender, SoundEffectTypes.AttackMelee);


                var conditionType = CellUnitsDataWorker.ConditionType(RpcWorker.XyCellForCircularAttack);
                if (conditionType == ConditionUnitTypes.Protected || conditionType == ConditionUnitTypes.Relaxed)
                {
                    InfoUnitsContainer.ReplaceCondition(conditionType, ConditionUnitTypes.None, UnitTypes.King, RpcWorker.InfoFrom.Sender.IsMasterClient, RpcWorker.XyCellForCircularAttack);
                    CellUnitsDataWorker.ResetConditionType(RpcWorker.XyCellForCircularAttack);
                }
            }
            else
            {
                PhotonPunRPC.MistakeStepsUnitToGeneral(RpcWorker.InfoFrom.Sender);
                PhotonPunRPC.SoundToGeneral(RpcWorker.InfoFrom.Sender, SoundEffectTypes.Mistake);
            }
        }
    }
}
