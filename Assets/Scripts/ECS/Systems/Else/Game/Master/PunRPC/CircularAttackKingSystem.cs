using Leopotam.Ecs;

namespace Assets.Scripts.ECS.Systems.Game.Master.PunRPC
{
    internal sealed class CircularAttackKingSystem : IEcsInitSystem, IEcsRunSystem
    {
        //private EcsWorld _currentGameWorld;
        //private EcsFilter<InfoMasCom> _infoMastFilter;
        //private EcsFilter<ForCircularAttackMasCom, XyCellForDoingMasCom> _circularFilter;
        //private EcsFilter<XyUnitsComponent> _xyUnitsFilter;

        //private Player Sender => _infoMastFilter.Get1(0).FromInfo.Sender;
        //private int[] XyCellForCurcularAttack => _circularFilter.Get2(0).XyCellForDoing;

        public void Init()
        {
            //_currentGameWorld.NewEntity()
            //    .Replace(new ForCircularAttackMasCom())
            //    .Replace(new XyCellForDoingMasCom(new int[2]));
        }

        public void Run()
        {
            //ref var xyUnitsCom = ref _xyUnitsFilter.Get1(0);

            //if (CellUnitsDataSystem.HaveMaxAmountSteps(XyCellForCurcularAttack))
            //{
            //    foreach (var xy1 in CellSpaceSupport.TryGetXyAround(XyCellForCurcularAttack))
            //    {
            //        if (CellUnitsDataSystem.HaveAnyUnit(xy1))
            //        {
            //            CellUnitsDataSystem.TakeAmountHealth(xy1, CellUnitsDataSystem.SimplePowerDamage(XyCellForCurcularAttack) / 2);

            //            if (!CellUnitsDataSystem.HaveAmountHealth(xy1))
            //            {
            //                var unitTypeXy1 = CellUnitsDataSystem.UnitType(xy1);
            //                var isMasterXy1 = CellUnitsDataSystem.IsMasterClient(xy1);

            //                if (CellUnitsDataSystem.IsUnitType(UnitTypes.King, xy1)) RPCGameSystem.EndGameToMaster(CellUnitsDataSystem.Owner(XyCellForCurcularAttack).ActorNumber);
            //                xyUnitsCom.RemoveAmountUnitsInGame(unitTypeXy1, isMasterXy1, xy1);
            //                CellUnitsDataSystem.ResetUnit(xy1);

            //            }

            //            RPCGameSystem.SoundToGeneral(RpcTarget.All, SoundEffectTypes.AttackMelee);
            //        }
            //    }
            //    CellUnitsDataSystem.TakeAmountSteps(XyCellForCurcularAttack);

            //    RPCGameSystem.SoundToGeneral(Sender, SoundEffectTypes.AttackMelee);


            //    var conditionType = CellUnitsDataSystem.ConditionType(XyCellForCurcularAttack);
            //    if (conditionType == ConditionUnitTypes.Protected || conditionType == ConditionUnitTypes.Relaxed)
            //    {
            //        MainGameSystem.XyUnitsContitionCom.ReplaceCondition(conditionType, ConditionUnitTypes.None, UnitTypes.King, Sender.IsMasterClient, XyCellForCurcularAttack);
            //        CellUnitsDataSystem.ResetConditionType(XyCellForCurcularAttack);
            //    }
            //}
            //else
            //{
            //    RPCGameSystem.MistakeStepsUnitToGeneral(Sender);
            //    RPCGameSystem.SoundToGeneral(Sender, SoundEffectTypes.Mistake);
            //}
        }
    }
}
