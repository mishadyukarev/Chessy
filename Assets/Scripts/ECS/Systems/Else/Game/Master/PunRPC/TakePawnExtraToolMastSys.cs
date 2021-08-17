//using Assets.Scripts.Abstractions.Enums.Cell;
//using Assets.Scripts.ECS.Component.Data.Else.Game.General;
//using Assets.Scripts.ECS.Component.Data.Else.Game.General.Cell;
//using Assets.Scripts.ECS.Component.Data.Else.Game.Master;
//using Assets.Scripts.ECS.Component.Game.Master;
//using Leopotam.Ecs;

//namespace Assets.Scripts.ECS.Systems.Else.Game.Master.PunRPC
//{
//    internal sealed class TakePawnExtraToolMastSys : IEcsRunSystem
//    {
//        private EcsFilter<InfoMasCom> _infoFilter = default;
//        private EcsFilter<ForTakePawnExtraToolMastCom> _forTakePawnExtraToolFilter = default;

//        private EcsFilter<InventorToolsComp> _inventToolsFilter = default;

//        private EcsFilter<CellUnitDataComponent, OwnerComponent> _cellUnitFilter = default;
//        private EcsFilter<CellPawnComp> _cellPawnFilter = default;

//        public void Run()
//        {
//            ref var infoMasCom = ref _infoFilter.Get1(0);
//            ref var forTakePawnExtraToolCom = ref _forTakePawnExtraToolFilter.Get1(0);

//            var sender = infoMasCom.FromInfo.Sender;

//            ref var cellUnitDataComForTake = ref _cellUnitFilter.Get1(forTakePawnExtraToolCom.IdxCellForTakePawnExtraTool);
//            ref var ownerCellUnitCompForTake = ref _cellUnitFilter.Get2(forTakePawnExtraToolCom.IdxCellForTakePawnExtraTool);
//            ref var cellPawnDataCompForTake = ref _cellPawnFilter.Get1(forTakePawnExtraToolCom.IdxCellForTakePawnExtraTool);


//            }
//        }
//    }
//}
