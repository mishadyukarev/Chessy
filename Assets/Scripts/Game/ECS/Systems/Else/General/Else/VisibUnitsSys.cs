using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component.Data.Else.Game.General.Cell;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Cell;
using Leopotam.Ecs;

internal sealed class VisibUnitsSys : IEcsRunSystem
{
    private EcsFilter<XyCellComponent> _xyCellFilter = default;
    private EcsFilter<CellEnvironDataCom> _cellEnvFilter = default;
    private EcsFilter<CellUnitDataCom, OwnerCom> _cellUnitFilter = default;

    public void Run()
    {
        foreach (byte idxCurCell in _cellUnitFilter)
        {
            var xy = _xyCellFilter.GetXyCell(idxCurCell);

            ref var curUnitDataCom = ref _cellUnitFilter.Get1(idxCurCell);
            ref var curOwnUnitCom = ref _cellUnitFilter.Get2(idxCurCell);

            ref var curEnvDataCom = ref _cellEnvFilter.Get1(idxCurCell);


            curUnitDataCom.SetIsVisibleUnit(PlayerTypes.First, true);
            curUnitDataCom.SetIsVisibleUnit(PlayerTypes.Second, true);


            if (curUnitDataCom.HaveUnit)
            {
                if (curEnvDataCom.HaveEnvir(EnvirTypes.AdultForest))
                {
                    PlayerTypes nextPlayer = default;
                    if (curOwnUnitCom.IsPlayerType(PlayerTypes.First)) nextPlayer = PlayerTypes.Second;
                    else nextPlayer = PlayerTypes.First;

                    curUnitDataCom.SetIsVisibleUnit(nextPlayer, false);

                    var list = CellSpaceSupport.TryGetXyAround(xy);

                    foreach (var xy_1 in list)
                    {
                        var idxCell_1 = _xyCellFilter.GetIdxCell(xy_1);

                        ref var aroUnitDataCom = ref _cellUnitFilter.Get1(idxCell_1);
                        ref var arouOnUnitCom = ref _cellUnitFilter.Get2(idxCell_1);

                        if (aroUnitDataCom.HaveUnit)
                        {
                            if (!arouOnUnitCom.IsPlayerType(curOwnUnitCom.PlayerType))
                            {
                                curUnitDataCom.SetIsVisibleUnit(nextPlayer, true);
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}