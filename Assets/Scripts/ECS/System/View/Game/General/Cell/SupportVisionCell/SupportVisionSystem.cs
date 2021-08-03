using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Abstractions.ValuesConsts;
using Assets.Scripts.ECS.Component;
using Assets.Scripts.ECS.System.Data.Game.General.Cell;
using Assets.Scripts.ECS.System.View.Game.General.Cell;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Cell;
using Assets.Scripts.Workers.Game.Else;
using Assets.Scripts.Workers.Game.Else.Cell;
using Assets.Scripts.Workers.Game.Else.Info.Units;
using Leopotam.Ecs;
using Photon.Pun;

internal sealed class SupportVisionSystem : IEcsRunSystem
{
    private EcsFilter<SelectorComponent> _selectorFilter;
    private EcsFilter<XyUnitsComponent> _xyUnitsFilter;

    public void Run()
    {
        ref var xyUnitsCom = ref _xyUnitsFilter.Get1(0);
        ref var selCom = ref _selectorFilter.Get1(0);

        if (selCom.HaveAnySelectorUnit)
        {
            for (int x = 0; x < CellValues.CELL_COUNT_X; x++)
                for (int y = 0; y < CellValues.CELL_COUNT_Y; y++)
                {
                    int[] xy = new int[] { x, y };

                    if (!CellUnitsDataSystem.HaveAnyUnit(xy))
                    {
                        if (InfoCellWorker.IsStartedCell(PhotonNetwork.IsMasterClient, xy))
                        {
                            CellSupViewSystem.EnableSupVis(SupportVisionTypes.Spawn, xy);
                        }
                    }
                }
        }

        else
        {
            for (int x = 0; x < CellValues.CELL_COUNT_X; x++)
                for (int y = 0; y < CellValues.CELL_COUNT_Y; y++)
                {
                    int[] xy = new int[] { x, y };

                    CellSupViewSystem.DisableSupVis(xy);
                }
        }


        if (selCom.SelectorType == SelectorTypes.UpgradeUnit)
        {
            foreach (var xy in xyUnitsCom.GetLixtXyUnits(UnitTypes.Pawn, PhotonNetwork.IsMasterClient))
            {
                if (CellUnitsDataSystem.HaveOwner(xy))
                {
                    if (CellUnitsDataSystem.IsMine(xy))
                    {
                        CellSupViewSystem.EnableSupVis(SupportVisionTypes.Upgrade, xy);
                    }
                }
            }
            foreach (var xy in xyUnitsCom.GetLixtXyUnits(UnitTypes.Rook, PhotonNetwork.IsMasterClient))
            {
                if (CellUnitsDataSystem.HaveOwner(xy))
                {
                    if (CellUnitsDataSystem.IsMine(xy))
                    {
                        CellSupViewSystem.EnableSupVis(SupportVisionTypes.Upgrade, xy);
                    }
                }
            }
            foreach (var xy in xyUnitsCom.GetLixtXyUnits(UnitTypes.Bishop, PhotonNetwork.IsMasterClient))
            {
                if (CellUnitsDataSystem.HaveOwner(xy))
                {
                    if (CellUnitsDataSystem.IsMine(xy))
                    {
                        CellSupViewSystem.EnableSupVis(SupportVisionTypes.Upgrade, xy);
                    }
                }
            }
        }

        if (selCom.IsSelectedCell)
        {
            CellSupViewSystem.ActiveSupVis(true, selCom.XySelectedCell);
            CellSupViewSystem.SetColor(SupportVisionTypes.Selector, selCom.XySelectedCell);


            if (selCom.SelectorType == SelectorTypes.PickFire)
            {
                foreach (var xy1 in CellSpaceWorker.TryGetXyAround(selCom.XySelectedCell))
                {
                    if (CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.AdultForest, xy1))
                    {
                        if (!CellFireDataSystem.HaveFireCom(xy1).HaveFire)
                        {
                            CellSupViewSystem.EnableSupVis(SupportVisionTypes.FireSelector, xy1);
                        }
                    }
                }
            }
        }


        AvailableCellsContainer.GetAllCellsCopy(AvailableCellTypes.Shift).ForEach((xy) => CellSupViewSystem.EnableSupVis(SupportVisionTypes.Shift, xy));
        AvailableCellsContainer.GetAllCellsCopy(AvailableCellTypes.SimpleAttack).ForEach((xy) => CellSupViewSystem.EnableSupVis(SupportVisionTypes.SimpleAttack, xy));
        AvailableCellsContainer.GetAllCellsCopy(AvailableCellTypes.UniqueAttack).ForEach((xy) => CellSupViewSystem.EnableSupVis(SupportVisionTypes.UniqueAttack, xy));
    }
}
