using Leopotam.Ecs;
using System.Collections.Generic;
using static Main;

public partial class SupportVisionSystem : CellReductionSystem, IEcsRunSystem
{
    private EcsComponentRef<SupportVisionComponent> _supportVisionComponentRef = default;


    internal SupportVisionSystem(ECSmanager eCSmanager, SupportManager supportManager) : base(eCSmanager, supportManager)
    {
        _supportVisionComponentRef = eCSmanager.EntitiesGeneralManager.SupportVisionComponentRef;
    }

    public void Run()
    {
        _supportVisionComponentRef.Unref().Unpack(out SupportVisionTypes supportVisionType, out bool isActive);

        switch (supportVisionType)
        {
            case SupportVisionTypes.SelectorVision:
                ActiveSelectorVision(isActive);
                break;

            case SupportVisionTypes.SpawnVision:
                ActiveSpawnVision(isActive);
                break;

            case SupportVisionTypes.WayOfUnitVision:
                ActiveWayOfUnitVision(isActive);
                break;

            case SupportVisionTypes.EnemyVision:
                ActiveEnemyVision(isActive);
                break;

            default:
                break;
        }
    }

    private void ActiveSelectorVision(in bool isActive)
    {
        _supportVisionComponentRef.Unref().UnpackSelector(out var xyPreviousCell, out int[] xySelectedCell);

        CellSupportVisionComponent(xyPreviousCell).EnableVision(false, SupportVisionTypes.SelectorVision);
        CellSupportVisionComponent(xySelectedCell).EnableVision(isActive, SupportVisionTypes.SelectorVision);
    }

    private void ActiveSpawnVision(in bool isActive)
    {
        for (int x = 0; x < Xcount; x++)
        {
            for (int y = 0; y < Ycount; y++)
            {
                if (isActive)
                {
                    if (!CellComponent(x, y).IsSelected && !CellUnitComponent(x, y).HaveUnit && !CellEnvironmentComponent(x, y).HaveMountain)
                    {
                        if (Instance.IsMasterClient)
                        {
                            if(CellComponent(x, y).IsStartMaster)
                            {
                                CellSupportVisionComponent(x, y).EnableVision(isActive, SupportVisionTypes.SpawnVision);
                            }                
                        }

                        else
                        {
                            if(CellComponent(x, y).IsStartOther)
                            {
                                CellSupportVisionComponent(x, y).EnableVision(isActive, SupportVisionTypes.SpawnVision);
                            }                           
                        }
                    }
                }

                else if (!CellComponent(x, y).IsSelected)
                {
                    CellSupportVisionComponent(x, y).EnableVision(isActive, SupportVisionTypes.SpawnVision);
                }
            }
        }
    }

    private void ActiveWayOfUnitVision(bool isActive)
    {
        _supportVisionComponentRef.Unref().UnpackWayUnitVision(out List<int[]> xyAvailableCellsForShift);

        foreach (var xy in xyAvailableCellsForShift)
        {
            CellSupportVisionComponent(xy).EnableVision(isActive, SupportVisionTypes.WayOfUnitVision);
        }
    }

    private void ActiveEnemyVision(bool isActive)
    {
        _supportVisionComponentRef.Unref().UnpackEnemyVision(out List<int[]> xyAvailableCellsWithEnemyIN);

        foreach (var xy in xyAvailableCellsWithEnemyIN)
        {
            CellSupportVisionComponent(xy).EnableVision(isActive, SupportVisionTypes.EnemyVision);
        }
    }
}
