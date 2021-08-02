using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Abstractions.ValuesConsts;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Cell;
using Assets.Scripts.Workers.Game.Else;
using Assets.Scripts.Workers.Game.Else.Cell;
using Assets.Scripts.Workers.Game.Else.Fire;
using Assets.Scripts.Workers.Game.Else.Info.Units;
using Leopotam.Ecs;
using Photon.Pun;

internal sealed class SupportVisionSystem : IEcsRunSystem
{
    private int[] XySelectedCell => SelectorWorker.GetXy(SelectorCellTypes.Selected);

    public void Run()
    {
        if (SelectorWorker.HaveAnySelectorUnit)
        {
            for (int x = 0; x < CellValues.CELL_COUNT_X; x++)
                for (int y = 0; y < CellValues.CELL_COUNT_Y; y++)
                {
                    int[] xy = new int[] { x, y };

                    if (!CellUnitsDataContainer.HaveAnyUnit(xy))
                    {
                        if (InfoCellWorker.IsStartedCell(PhotonNetwork.IsMasterClient, xy))
                        {
                            CellSupVisViewContainer.EnableSupVis(SupportVisionTypes.Spawn, xy);
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

                    CellSupVisViewContainer.DisableSupVis(xy);
                }
        }


        if (SelectorWorker.SelectorType == SelectorTypes.UpgradeUnit)
        {
            foreach (var xy in InfoUnitsDataContainer.GetLixtXyUnits(UnitTypes.Pawn, PhotonNetwork.IsMasterClient))
            {
                if (CellUnitsDataContainer.HaveOwner(xy))
                {
                    if (CellUnitsDataContainer.IsMine(xy))
                    {
                        CellSupVisViewContainer.EnableSupVis(SupportVisionTypes.Upgrade, xy);
                    }
                }
            }
            foreach (var xy in InfoUnitsDataContainer.GetLixtXyUnits(UnitTypes.Rook, PhotonNetwork.IsMasterClient))
            {
                if (CellUnitsDataContainer.HaveOwner(xy))
                {
                    if (CellUnitsDataContainer.IsMine(xy))
                    {
                        CellSupVisViewContainer.EnableSupVis(SupportVisionTypes.Upgrade, xy);
                    }
                }
            }
            foreach (var xy in InfoUnitsDataContainer.GetLixtXyUnits(UnitTypes.Bishop, PhotonNetwork.IsMasterClient))
            {
                if (CellUnitsDataContainer.HaveOwner(xy))
                {
                    if (CellUnitsDataContainer.IsMine(xy))
                    {
                        CellSupVisViewContainer.EnableSupVis(SupportVisionTypes.Upgrade, xy);
                    }
                }
            }
        }

        if (SelectorWorker.IsSelectedCell)
        {
            CellSupVisViewContainer.ActiveSupVis(true, XySelectedCell);
            CellSupVisViewContainer.SetColor(SupportVisionTypes.Selector, XySelectedCell);


            if (SelectorWorker.SelectorType == SelectorTypes.PickFire)
            {
                foreach (var xy1 in CellSpaceWorker.TryGetXyAround(XySelectedCell))
                {
                    if (CellEnvirDataContainer.HaveEnvironment(EnvironmentTypes.AdultForest, xy1))
                    {
                        if (!CellFireDataContainer.HaveFire(xy1))
                        {
                            CellSupVisViewContainer.EnableSupVis(SupportVisionTypes.FireSelector, xy1);
                        }
                    }
                }
            }
        }


        AvailableCellsContainer.GetAllCellsCopy(AvailableCellTypes.Shift).ForEach((xy) => CellSupVisViewContainer.EnableSupVis(SupportVisionTypes.Shift, xy));
        AvailableCellsContainer.GetAllCellsCopy(AvailableCellTypes.SimpleAttack).ForEach((xy) => CellSupVisViewContainer.EnableSupVis(SupportVisionTypes.SimpleAttack, xy));
        AvailableCellsContainer.GetAllCellsCopy(AvailableCellTypes.UniqueAttack).ForEach((xy) => CellSupVisViewContainer.EnableSupVis(SupportVisionTypes.UniqueAttack, xy));
    }
}
