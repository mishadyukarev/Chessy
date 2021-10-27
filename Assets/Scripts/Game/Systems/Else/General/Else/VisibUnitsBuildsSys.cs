﻿using Leopotam.Ecs;

namespace Scripts.Game
{
    internal sealed class VisibUnitsBuildsSys : IEcsRunSystem
    {
        private EcsFilter<XyCellComponent> _xyCellFilter = default;
        private EcsFilter<CellEnvironmentDataC> _cellEnvFilter = default;
        private EcsFilter<CellUnitDataCom, OwnerCom, VisibleC> _cellUnitFilter = default;
        private EcsFilter<CellBuildDataCom, OwnerCom, VisibleC> _cellBuildFilt = default;

        public void Run()
        {
            foreach (byte idxCurCell in _cellUnitFilter)
            {
                var xy = _xyCellFilter.GetXyCell(idxCurCell);

                ref var curEnvDataCom = ref _cellEnvFilter.Get1(idxCurCell);



                ref var curUnitCom = ref _cellUnitFilter.Get1(idxCurCell);

                if (curUnitCom.HaveUnit)
                {
                    ref var curOwnUnitCom = ref _cellUnitFilter.Get2(idxCurCell);
                    ref var curVisUnitCom = ref _cellUnitFilter.Get3(idxCurCell);

                    curVisUnitCom.SetVisibled(curOwnUnitCom.PlayerType, true);

                    if (curEnvDataCom.Have(EnvirTypes.AdultForest))
                    {
                        var isVisibledNextPlayer = false;

                        var list = CellSpaceSupport.TryGetXyAround(xy);

                        foreach (var xy_1 in list)
                        {
                            var idxCell_1 = _xyCellFilter.GetIdxCell(xy_1);

                            ref var unitCom_1 = ref _cellUnitFilter.Get1(idxCell_1);
                            ref var ownUnitCom_1 = ref _cellUnitFilter.Get2(idxCell_1);

                            if (unitCom_1.HaveUnit)
                            {
                                if (!ownUnitCom_1.Is(curOwnUnitCom.PlayerType))
                                {
                                    isVisibledNextPlayer = true;
                                    break;
                                }
                            }
                        }

                        curVisUnitCom.SetVisibled(WhoseMoveC.NextPlayerFrom(curOwnUnitCom.PlayerType), isVisibledNextPlayer);
                    }
                    else
                    {
                        curVisUnitCom.SetVisibled(WhoseMoveC.NextPlayerFrom(curOwnUnitCom.PlayerType), true);
                    }

                }


                ref var curBuildCom = ref _cellBuildFilt.Get1(idxCurCell);

                if (curBuildCom.HaveBuild)
                {
                    ref var curOwnBuildCom = ref _cellBuildFilt.Get2(idxCurCell);
                    ref var curVisBuildCom = ref _cellBuildFilt.Get3(idxCurCell);

                    curVisBuildCom.SetVisibled(curOwnBuildCom.PlayerType, true);

                    if (curEnvDataCom.Have(EnvirTypes.AdultForest))
                    {
                        var isVisibledNextPlayer = false;

                        var list = CellSpaceSupport.TryGetXyAround(xy);

                        foreach (var xy_1 in list)
                        {
                            var idxCell_1 = _xyCellFilter.GetIdxCell(xy_1);

                            ref var aroUnitDataCom = ref _cellUnitFilter.Get1(idxCell_1);
                            ref var arouOnUnitCom = ref _cellUnitFilter.Get2(idxCell_1);

                            if (aroUnitDataCom.HaveUnit)
                            {
                                if (!arouOnUnitCom.Is(curOwnBuildCom.PlayerType))
                                {
                                    isVisibledNextPlayer = true;
                                    break;
                                }
                            }
                        }

                        curVisBuildCom.SetVisibled(WhoseMoveC.NextPlayerFrom(curOwnBuildCom.PlayerType), isVisibledNextPlayer);
                    }
                    else curVisBuildCom.SetVisibled(WhoseMoveC.NextPlayerFrom(curOwnBuildCom.PlayerType), true);
                }

            }
        }
    }
}