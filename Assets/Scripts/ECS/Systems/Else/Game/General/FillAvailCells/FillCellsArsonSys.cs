using Assets.Scripts.ECS.Component.Data.Else.Game.General.Cell;
using Assets.Scripts.ECS.Components.Data.Else.Game.General;
using Assets.Scripts.ECS.Components.Data.Else.Game.General.AvailCells;
using Assets.Scripts.ECS.Game.General.Components;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Cell;
using Leopotam.Ecs;

namespace Assets.Scripts.ECS.Systems.Else.Game.General.FillAvailCells
{
    internal sealed class FillCellsArsonSys : IEcsRunSystem
    {
        private EcsFilter<CellsArsonArcherComp> _cellsArsonFilter = default;

        private EcsFilter<XyCellComponent> _xyCellFilter = default;
        private EcsFilter<CellUnitDataCom, OwnerOnlineComp, OwnerOfflineCom, OwnerBotComponent> _cellUnitFilter = default;
        private EcsFilter<CellEnvironDataCom> _cellEnvFilter = default;
        private EcsFilter<CellFireDataComponent> _cellFireFilter = default;

        public void Run()
        {
            ref var cellsArsonCom = ref _cellsArsonFilter.Get1(0);

            foreach (byte curIdxCell in _cellEnvFilter)
            {
                var curXy = _xyCellFilter.GetXyCell(curIdxCell);

                ref var curUnitDatCom = ref _cellUnitFilter.Get1(curIdxCell);
                ref var curOwnUnitCom = ref _cellUnitFilter.Get2(curIdxCell);
                ref var curOffUnitCom = ref _cellUnitFilter.Get3(curIdxCell);

                if (curUnitDatCom.Is(new[] { UnitTypes.Rook, UnitTypes.Bishop }))
                {
                    if (curOwnUnitCom.HaveOwner)
                    {
                        foreach (var arouXy in CellSpaceSupport.TryGetXyAround(curXy))
                        {
                            var arouIdx = _xyCellFilter.GetIdxCell(arouXy);

                            ref var arounEnvDatCom = ref _cellEnvFilter.Get1(arouIdx);

                            if (!_cellFireFilter.Get1(arouIdx).HaveFire)
                            {
                                if (arounEnvDatCom.HaveEnvir(EnvirTypes.AdultForest))
                                {
                                    cellsArsonCom.Add(curOwnUnitCom.IsMasterClient, curIdxCell, arouIdx);
                                }
                            }
                        }
                    }
                    else if (curOffUnitCom.HaveLocalPlayer)
                    {
                        foreach (var arouXy in CellSpaceSupport.TryGetXyAround(curXy))
                        {
                            var arouIdx = _xyCellFilter.GetIdxCell(arouXy);

                            ref var arounEnvDatCom = ref _cellEnvFilter.Get1(arouIdx);

                            if (!_cellFireFilter.Get1(arouIdx).HaveFire)
                            {
                                if (arounEnvDatCom.HaveEnvir(EnvirTypes.AdultForest))
                                {
                                    cellsArsonCom.Add(curOffUnitCom.IsMainMaster, curIdxCell, arouIdx);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
