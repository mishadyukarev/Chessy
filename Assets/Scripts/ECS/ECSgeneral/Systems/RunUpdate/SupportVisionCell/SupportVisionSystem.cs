using Leopotam.Ecs;
using static MainGame;

internal class SupportVisionSystem : SystemGeneralReduction, IEcsRunSystem
{
    private bool _isRepeated;

    internal SupportVisionSystem(ECSmanager eCSmanager) : base(eCSmanager)
    {

    }


    public void Run()
    {
        _eGM.CellEnt_CellSupVisCom(_eGM.SelectorEntSelectorCom.XYpreviousCell).ActiveVision(false, SupportVisionTypes.Selector);

        if (_eGM.CellEnt_CellCom(_eGM.SelectorEntSelectorCom.XYselectedCell).IsSelected)
        {
            _eGM.CellEnt_CellSupVisCom(_eGM.SelectorEntSelectorCom.XYselectedCell).ActiveVision(true, SupportVisionTypes.Selector);
        }
        else
        {
            _eGM.CellEnt_CellSupVisCom(_eGM.SelectorEntSelectorCom.XYselectedCell).ActiveVision(false, SupportVisionTypes.Selector);
        }


        var isSelectedUnit = _eGM.SelectedUnitEntUnitTypeCom.HaveUnit;

        if (_isRepeated != isSelectedUnit)
        {
            _isRepeated = isSelectedUnit;

            for (int x = 0; x < _eGM.Xamount; x++)
            {
                for (int y = 0; y < _eGM.Yamount; y++)
                {
                    if (isSelectedUnit)
                    {
                        if (!_eGM.CellEnt_CellCom(x, y).IsSelected && !_eGM.CellEnt_CellUnitCom(x, y).HaveUnit && !_eGM.CellEnt_CellEnvCom(x, y).HaveMountain)
                        {
                            if (Instance.IsMasterClient)
                            {
                                if (_eGM.CellEnt_CellCom(x, y).IsStartMaster)
                                {
                                    _eGM.CellEnt_CellSupVisCom(x, y).ActiveVision(true, SupportVisionTypes.Spawn);
                                }
                            }

                            else
                            {
                                if (_eGM.CellEnt_CellCom(x, y).IsStartOther)
                                {
                                    _eGM.CellEnt_CellSupVisCom(x, y).ActiveVision(true, SupportVisionTypes.Spawn);
                                }
                            }
                        }
                    }

                    else if (!_eGM.CellEnt_CellCom(x, y).IsSelected)
                    {
                        _eGM.CellEnt_CellSupVisCom(x, y).ActiveVision(false, SupportVisionTypes.Spawn);
                    }
                }

            }
        }


        for (int x = 0; x < _eGM.Xamount; x++)
        {
            for (int y = 0; y < _eGM.Yamount; y++)
            {
                _eGM.CellEnt_CellSupVisCom(x, y).ActiveVision(false, SupportVisionTypes.WayOfUnit);
                _eGM.CellEnt_CellSupVisCom(x, y).ActiveVision(false, SupportVisionTypes.SimpleAttack);
                _eGM.CellEnt_CellSupVisCom(x, y).ActiveVision(false, SupportVisionTypes.UniqueAttack);
            }
        }

        foreach (var xy in _eGM.SelectorEntSelectorCom.AvailableCellsForShift)
        {
            _eGM.CellEnt_CellSupVisCom(xy).ActiveVision(true, SupportVisionTypes.WayOfUnit);
        }
        foreach (var xy in _eGM.SelectorEntSelectorCom.AvailableCellsSimpleAttack)
        {
            _eGM.CellEnt_CellSupVisCom(xy).ActiveVision(true, SupportVisionTypes.SimpleAttack);
        }
        foreach (var xy in _eGM.SelectorEntSelectorCom.AvailableCellsUniqueAttack)
        {
            _eGM.CellEnt_CellSupVisCom(xy).ActiveVision(true, SupportVisionTypes.UniqueAttack);
        }
    }
}
