using Leopotam.Ecs;
using static MainGame;

internal class SupportVisionSystem : CellGeneralReduction, IEcsRunSystem
{
    private bool _isRepeated;

    internal SupportVisionSystem(ECSmanager eCSmanager) : base(eCSmanager)
    {

    }


    public void Run()
    {
        _eGM.CellSupportVisionComponent(_eGM.SelectorESelectorC.XYpreviousCell).ActiveVision(false, SupportVisionTypes.Selector);

        if (_eGM.CellComponent(_eGM.SelectorESelectorC.XYselectedCell).IsSelected)
        {
            _eGM.CellSupportVisionComponent(_eGM.SelectorESelectorC.XYselectedCell).ActiveVision(true, SupportVisionTypes.Selector);
        }
        else
        {
            _eGM.CellSupportVisionComponent(_eGM.SelectorESelectorC.XYselectedCell).ActiveVision(false, SupportVisionTypes.Selector);
        }


        var isSelectedUnit = _eGM.SelectedUnitEUnitTypeC.HaveUnit;

        if (_isRepeated != isSelectedUnit)
        {
            _isRepeated = isSelectedUnit;

            for (int x = 0; x < _eGM.Xcount; x++)
            {
                for (int y = 0; y < _eGM.Ycount; y++)
                {
                    if (isSelectedUnit)
                    {
                        if (!_eGM.CellComponent(x, y).IsSelected && !_eGM.CellUnitComponent(x, y).HaveUnit && !_eGM.CellEnvironmentComponent(x, y).HaveMountain)
                        {
                            if (InstanceGame.IsMasterClient)
                            {
                                if (_eGM.CellComponent(x, y).IsStartMaster)
                                {
                                    _eGM.CellSupportVisionComponent(x, y).ActiveVision(true, SupportVisionTypes.Spawn);
                                }
                            }

                            else
                            {
                                if (_eGM.CellComponent(x, y).IsStartOther)
                                {
                                    _eGM.CellSupportVisionComponent(x, y).ActiveVision(true, SupportVisionTypes.Spawn);
                                }
                            }
                        }
                    }

                    else if (!_eGM.CellComponent(x, y).IsSelected)
                    {
                        _eGM.CellSupportVisionComponent(x, y).ActiveVision(false, SupportVisionTypes.Spawn);
                    }
                }

            }
        }


        for (int x = 0; x < _eGM.Xcount; x++)
        {
            for (int y = 0; y < _eGM.Ycount; y++)
            {
                _eGM.CellSupportVisionComponent(x, y).ActiveVision(false, SupportVisionTypes.WayOfUnit);
                _eGM.CellSupportVisionComponent(x, y).ActiveVision(false, SupportVisionTypes.SimpleAttack);
                _eGM.CellSupportVisionComponent(x, y).ActiveVision(false, SupportVisionTypes.UniqueAttack);
            }
        }

        foreach (var xy in _eGM.SelectorESelectorC.AvailableCellsForShift)
        {
            _eGM.CellSupportVisionComponent(xy).ActiveVision(true, SupportVisionTypes.WayOfUnit);
        }
        foreach (var xy in _eGM.SelectorESelectorC.AvailableCellsSimpleAttack)
        {
            _eGM.CellSupportVisionComponent(xy).ActiveVision(true, SupportVisionTypes.SimpleAttack);
        }
        foreach (var xy in _eGM.SelectorESelectorC.AvailableCellsUniqueAttack)
        {
            _eGM.CellSupportVisionComponent(xy).ActiveVision(true, SupportVisionTypes.UniqueAttack);
        }
    }
}
