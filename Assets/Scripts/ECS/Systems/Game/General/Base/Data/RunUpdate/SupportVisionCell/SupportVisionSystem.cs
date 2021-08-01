using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
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
            for (int x = 0; x < CellViewWorker.Xamount; x++)
                for (int y = 0; y < CellViewWorker.Yamount; y++)
                {
                    int[] xy = new int[] { x, y };

                    if (!CellUnitsDataWorker.HaveAnyUnit(xy))
                    {
                        if (InfoCellWorker.IsStartedCell(PhotonNetwork.IsMasterClient, xy))
                        {
                            CellSupVisViewWorker.EnableSupVis(SupportVisionTypes.Spawn, xy);
                        }
                    }
                }
        }

        else
        {
            for (int x = 0; x < CellViewWorker.Xamount; x++)
                for (int y = 0; y < CellViewWorker.Yamount; y++)
                {
                    int[] xy = new int[] { x, y };

                    CellSupVisViewWorker.DisableSupVis(xy);
                }
        }


        if (SelectorWorker.SelectorType == SelectorTypes.UpgradeUnit)
        {
            foreach (var xy in InfoUnitsContainer.GetLixtXyUnits(UnitTypes.Pawn, PhotonNetwork.IsMasterClient))
            {
                if (CellUnitsDataWorker.HaveOwner(xy))
                {
                    if (CellUnitsDataWorker.IsMine(xy))
                    {
                        CellSupVisViewWorker.EnableSupVis(SupportVisionTypes.Upgrade, xy);
                    }
                }
            }
            foreach (var xy in InfoUnitsContainer.GetLixtXyUnits(UnitTypes.Rook, PhotonNetwork.IsMasterClient))
            {
                if (CellUnitsDataWorker.HaveOwner(xy))
                {
                    if (CellUnitsDataWorker.IsMine(xy))
                    {
                        CellSupVisViewWorker.EnableSupVis(SupportVisionTypes.Upgrade, xy);
                    }
                }
            }
            foreach (var xy in InfoUnitsContainer.GetLixtXyUnits(UnitTypes.Bishop, PhotonNetwork.IsMasterClient))
            {
                if (CellUnitsDataWorker.HaveOwner(xy))
                {
                    if (CellUnitsDataWorker.IsMine(xy))
                    {
                        CellSupVisViewWorker.EnableSupVis(SupportVisionTypes.Upgrade, xy);
                    }
                }
            }
        }

        if (SelectorWorker.IsSelectedCell)
        {
            CellSupVisViewWorker.ActiveSupVis(true, XySelectedCell);
            CellSupVisViewWorker.SetColor(SupportVisionTypes.Selector, XySelectedCell);


            if (SelectorWorker.SelectorType == SelectorTypes.PickFire)
            {
                foreach (var xy1 in CellSpaceWorker.TryGetXyAround(XySelectedCell))
                {
                    if (CellEnvirDataWorker.HaveEnvironment(EnvironmentTypes.AdultForest, xy1))
                    {
                        if (!CellFireDataWorker.HaveFire(xy1))
                        {
                            CellSupVisViewWorker.EnableSupVis(SupportVisionTypes.FireSelector, xy1);
                        }
                    }
                }
            }
        }


        AvailableCellsContainer.GetAllCellsCopy(AvailableCellTypes.Shift).ForEach((xy) => CellSupVisViewWorker.EnableSupVis(SupportVisionTypes.Shift, xy));
        AvailableCellsContainer.GetAllCellsCopy(AvailableCellTypes.SimpleAttack).ForEach((xy) => CellSupVisViewWorker.EnableSupVis(SupportVisionTypes.SimpleAttack, xy));
        AvailableCellsContainer.GetAllCellsCopy(AvailableCellTypes.UniqueAttack).ForEach((xy) => CellSupVisViewWorker.EnableSupVis(SupportVisionTypes.UniqueAttack, xy));
    }
}
