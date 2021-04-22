using Leopotam.Ecs;
using static MainGame;

internal class SupportVisionSystem : CellReduction, IEcsRunSystem
{
    private EcsComponentRef<SelectorComponent> _selectorComponentRef = default;
    private EcsComponentRef<SelectedUnitComponent> _selectedUnitComponent = default;

    private bool _isRepeated;

    internal SupportVisionSystem(ECSmanager eCSmanager, SupportManager supportManager) : base(eCSmanager, supportManager)
    {
        _selectorComponentRef = eCSmanager.EntitiesGeneralManager.SelectorComponentRef;
        _selectedUnitComponent = eCSmanager.EntitiesGeneralManager.SelectedUnitComponentRef;
    }


    public void Run()
    {
        CellSupportVisionComponent(_selectorComponentRef.Unref().XYpreviousCell).ActiveVision(false, SupportVisionTypes.Selector);

        if (CellComponent(_selectorComponentRef.Unref().XYselectedCell).IsSelected)
        {
            CellSupportVisionComponent(_selectorComponentRef.Unref().XYselectedCell).ActiveVision(true, SupportVisionTypes.Selector);
        }
        else
        {
            CellSupportVisionComponent(_selectorComponentRef.Unref().XYselectedCell).ActiveVision(false, SupportVisionTypes.Selector);
        }


        var isSelectedUnit = _selectedUnitComponent.Unref().IsSelectedUnit;

        if (_isRepeated != isSelectedUnit)
        {
            _isRepeated = isSelectedUnit;

            for (int x = 0; x < Xcount; x++)
            {
                for (int y = 0; y < Ycount; y++)
                {
                    if (isSelectedUnit)
                    {
                        if (!CellComponent(x, y).IsSelected && !CellUnitComponent(x, y).HaveUnit && !CellEnvironmentComponent(x, y).HaveMountain)
                        {
                            if (Instance.IsMasterClient)
                            {
                                if (CellComponent(x, y).IsStartMaster)
                                {
                                    CellSupportVisionComponent(x, y).ActiveVision(true, SupportVisionTypes.Spawn);
                                }
                            }

                            else
                            {
                                if (CellComponent(x, y).IsStartOther)
                                {
                                    CellSupportVisionComponent(x, y).ActiveVision(true, SupportVisionTypes.Spawn);
                                }
                            }
                        }
                    }

                    else if (!CellComponent(x, y).IsSelected)
                    {
                        CellSupportVisionComponent(x, y).ActiveVision(false, SupportVisionTypes.Spawn);
                    }
                }

            }
        }


        for (int x = 0; x < Xcount; x++)
        {
            for (int y = 0; y < Ycount; y++)
            {
                CellSupportVisionComponent(x, y).ActiveVision(false, SupportVisionTypes.WayOfUnit);
                CellSupportVisionComponent(x, y).ActiveVision(false, SupportVisionTypes.Enemy);
            }
        }

        foreach (var xy in _selectorComponentRef.Unref().XYavailableCellsForShift)
        {
            CellSupportVisionComponent(xy).ActiveVision(true, SupportVisionTypes.WayOfUnit);
        }
        foreach (var xy in _selectorComponentRef.Unref().XYavailableCellsForAttack)
        {
            CellSupportVisionComponent(xy).ActiveVision(true, SupportVisionTypes.Enemy);
        }
    }
}
