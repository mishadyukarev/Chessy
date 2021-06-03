using static Main;

internal sealed class SupportVisionSystem : SystemGeneralReduction
{
    private bool _isRepeated;

    public override void Run()
    {
        base.Run();

        _eGM.CellSupVisEnt_CellSupVisCom(_eGM.SelectorEntSelectorCom.XYpreviousCell).ActiveVision(false, SupportVisionTypes.Selector);

        if (_eGM.CellEnt_CellBaseCom(_eGM.SelectorEntSelectorCom.XYselectedCell).IsSelected)
        {
            _eGM.CellSupVisEnt_CellSupVisCom(_eGM.SelectorEntSelectorCom.XYselectedCell).ActiveVision(true, SupportVisionTypes.Selector);
        }
        else
        {
            _eGM.CellSupVisEnt_CellSupVisCom(_eGM.SelectorEntSelectorCom.XYselectedCell).ActiveVision(false, SupportVisionTypes.Selector);
        }


        var isSelectedUnit = _eGM.SelectorEnt_UnitTypeCom.HaveUnit;

        if (_isRepeated != isSelectedUnit)
        {
            _isRepeated = isSelectedUnit;

            for (int x = 0; x < _eGM.Xamount; x++)
            {
                for (int y = 0; y < _eGM.Yamount; y++)
                {
                    if (isSelectedUnit)
                    {
                        if (!_eGM.CellEnt_CellBaseCom(x, y).IsSelected && !_eGM.CellUnitEnt_UnitTypeCom(x, y).HaveUnit && !_eGM.CellEnvEnt_CellEnvCom(x, y).HaveMountain)
                        {
                            if (Instance.IsMasterClient)
                            {
                                if (_eGM.CellEnt_CellBaseCom(x, y).IsStarted(true))
                                {
                                    _eGM.CellSupVisEnt_CellSupVisCom(x, y).ActiveVision(true, SupportVisionTypes.Spawn);
                                }
                            }

                            else
                            {
                                if (_eGM.CellEnt_CellBaseCom(x, y).IsStarted(false))
                                {
                                    _eGM.CellSupVisEnt_CellSupVisCom(x, y).ActiveVision(true, SupportVisionTypes.Spawn);
                                }
                            }
                        }
                    }

                    else if (!_eGM.CellEnt_CellBaseCom(x, y).IsSelected)
                    {
                        _eGM.CellSupVisEnt_CellSupVisCom(x, y).ActiveVision(false, SupportVisionTypes.Spawn);
                    }
                }

            }
        }


        for (int x = 0; x < _eGM.Xamount; x++)
        {
            for (int y = 0; y < _eGM.Yamount; y++)
            {
                _eGM.CellSupVisEnt_CellSupVisCom(x, y).ActiveVision(false, SupportVisionTypes.WayUnit);
                _eGM.CellSupVisEnt_CellSupVisCom(x, y).ActiveVision(false, SupportVisionTypes.SimpleAttack);
                _eGM.CellSupVisEnt_CellSupVisCom(x, y).ActiveVision(false, SupportVisionTypes.UniqueAttack);
            }
        }

        foreach (var xy in _eGM.SelectorEntSelectorCom.AvailableCellsForShift)
        {
            _eGM.CellSupVisEnt_CellSupVisCom(xy).ActiveVision(true, SupportVisionTypes.WayUnit);
        }
        foreach (var xy in _eGM.SelectorEntSelectorCom.AvailableCellsSimpleAttack)
        {
            _eGM.CellSupVisEnt_CellSupVisCom(xy).ActiveVision(true, SupportVisionTypes.SimpleAttack);
        }
        foreach (var xy in _eGM.SelectorEntSelectorCom.AvailableCellsUniqueAttack)
        {
            _eGM.CellSupVisEnt_CellSupVisCom(xy).ActiveVision(true, SupportVisionTypes.UniqueAttack);
        }
    }
}
