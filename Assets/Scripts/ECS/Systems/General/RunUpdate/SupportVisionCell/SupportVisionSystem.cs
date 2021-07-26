using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Cell;
using Assets.Scripts.Workers.Game.Else;
using Assets.Scripts.Workers.Game.Else.Cell;
using Assets.Scripts.Workers.Info;
using Leopotam.Ecs;
using Photon.Pun;

internal sealed class SupportVisionSystem : IEcsRunSystem
{
    private int[] XySelectedCell => SelectorWorker.GetXy(SelectorCellTypes.Selected);

    public void Run()
    {

        if (SelectorWorker.HaveAnySelectorUnit)
        {
            foreach (var xy in CellUnitsDataWorker.GetStartCellsForSettingUnit(PhotonNetwork.MasterClient))
            {
                if (InfoCellWorker.IsStartedCell(PhotonNetwork.IsMasterClient, xy))
                {
                    CellSupVisWorker.EnableSupVis(SupportVisionTypes.Spawn, xy);
                }
            }
        }

        else
        {
            for (int x = 0; x < CellGameWorker.Xamount; x++)
                for (int y = 0; y < CellGameWorker.Yamount; y++)
                {
                    int[] xy = new int[] { x, y };

                    CellSupVisWorker.DisableSupVis(xy);
                }
        }


        if (SelectorWorker.IsUpgradeModType(UpgradeModTypes.Unit))
        {
            foreach (var xy in InfoUnitsWorker.GetLixtXyUnits(UnitTypes.Pawn, PhotonNetwork.IsMasterClient))
            {
                if (CellUnitsDataWorker.HaveOwner(xy))
                {
                    if (CellUnitsDataWorker.IsMine(xy))
                    {
                        CellSupVisWorker.EnableSupVis(SupportVisionTypes.Upgrade, xy);
                    }
                }
            }
            foreach (var xy in InfoUnitsWorker.GetLixtXyUnits(UnitTypes.Rook, PhotonNetwork.IsMasterClient))
            {
                if (CellUnitsDataWorker.HaveOwner(xy))
                {
                    if (CellUnitsDataWorker.IsMine(xy))
                    {
                        CellSupVisWorker.EnableSupVis(SupportVisionTypes.Upgrade, xy);
                    }
                }
            }
            foreach (var xy in InfoUnitsWorker.GetLixtXyUnits(UnitTypes.Bishop, PhotonNetwork.IsMasterClient))
            {
                if (CellUnitsDataWorker.HaveOwner(xy))
                {
                    if (CellUnitsDataWorker.IsMine(xy))
                    {
                        CellSupVisWorker.EnableSupVis(SupportVisionTypes.Upgrade, xy);
                    }
                }
            }
        }

        if (SelectorWorker.IsSelectedCell)
        {
            CellSupVisWorker.ActiveSupVis(true, XySelectedCell);
            CellSupVisWorker.SetColor(SupportVisionTypes.Selector, XySelectedCell);
        }


        AvailableCellsEntsWorker.GetAllCellsCopy(AvailableCellTypes.Shift).ForEach((xy) => CellSupVisWorker.EnableSupVis(SupportVisionTypes.Shift, xy));
        AvailableCellsEntsWorker.GetAllCellsCopy(AvailableCellTypes.SimpleAttack).ForEach((xy) => CellSupVisWorker.EnableSupVis(SupportVisionTypes.SimpleAttack, xy));
        AvailableCellsEntsWorker.GetAllCellsCopy(AvailableCellTypes.UniqueAttack).ForEach((xy) => CellSupVisWorker.EnableSupVis(SupportVisionTypes.UniqueAttack, xy));
    }
}
