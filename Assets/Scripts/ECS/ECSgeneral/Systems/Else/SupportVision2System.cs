using Leopotam.Ecs;
using System.Collections.Generic;
using static Main;

public partial class SupportVision2System : CellReductionSystem, IEcsRunSystem
{
    private EcsComponentRef<SupportVisionComponent> _supportVisionComponentRef = default;


    internal SupportVision2System(ECSmanager eCSmanager, SupportManager supportManager) : base(eCSmanager, supportManager)
    {
        _supportVisionComponentRef = eCSmanager.EntitiesGeneralManager.SupportVisionComponentRef;
    }

    public void Run()
    {
        _supportVisionComponentRef.Unref().Unpack(out SupportVisionTypes supportVisionType, out bool isActive);

        switch (supportVisionType)
        {
            case SupportVisionTypes.Selector:
                //ActiveSelectorVision(isActive);
                break;

            case SupportVisionTypes.Spawn:
                //ActiveSpawnVision(isActive);
                break;

            case SupportVisionTypes.WayOfUnit:
                //ActiveWayOfUnitVision(isActive);
                break;

            case SupportVisionTypes.Enemy:
                //ActiveEnemyVision(isActive);
                break;

            default:
                break;
        }
    }

    private void ActiveSelectorVision(in bool isActive)
    {
        _supportVisionComponentRef.Unref().UnpackSelector(out var xyPreviousCell, out int[] xySelectedCell);

        CellSupportVisionComponent(xyPreviousCell).ActiveVision(false, SupportVisionTypes.Selector);
        CellSupportVisionComponent(xySelectedCell).ActiveVision(isActive, SupportVisionTypes.Selector);
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
                            if (CellComponent(x, y).IsStartMaster)
                            {
                                CellSupportVisionComponent(x, y).ActiveVision(isActive, SupportVisionTypes.Spawn);
                            }
                        }

                        else
                        {
                            if (CellComponent(x, y).IsStartOther)
                            {
                                CellSupportVisionComponent(x, y).ActiveVision(isActive, SupportVisionTypes.Spawn);
                            }
                        }
                    }
                }

                else if (!CellComponent(x, y).IsSelected)
                {
                    CellSupportVisionComponent(x, y).ActiveVision(isActive, SupportVisionTypes.Spawn);
                }
            }
        }
    }

    private void ActiveWayOfUnitVision(bool isActive)
    {
        _supportVisionComponentRef.Unref().UnpackWayUnitVision(out List<int[]> xyAvailableCellsForShift);

        foreach (var xy in xyAvailableCellsForShift)
        {
            CellSupportVisionComponent(xy).ActiveVision(isActive, SupportVisionTypes.WayOfUnit);
        }
    }

    private void ActiveEnemyVision(bool isActive)
    {
        _supportVisionComponentRef.Unref().UnpackEnemyVision(out List<int[]> xyAvailableCellsWithEnemyIN);

        foreach (var xy in xyAvailableCellsWithEnemyIN)
        {
            CellSupportVisionComponent(xy).ActiveVision(isActive, SupportVisionTypes.Enemy);
        }
    }
}
