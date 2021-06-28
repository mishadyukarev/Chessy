using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using static Assets.Scripts.Main;

internal sealed class SupportVisionSystem : SystemGeneralReduction
{
    private int[] XySelectedCell => _eGM.SelectorEnt_SelectorCom.XySelectedCell;

    public override void Run()
    {
        base.Run();

        _eGM.CellSupVisEnt_CellSupVisCom(_eGM.SelectorEnt_SelectorCom.XyPreviousCell).ActiveVision(false, SupportVisionTypes.Selector);


        if (_eGM.CellEnt_CellBaseCom(XySelectedCell).IsSelected)
            _eGM.CellSupVisEnt_CellSupVisCom(XySelectedCell).ActiveVision(true, SupportVisionTypes.Selector);

        else _eGM.CellSupVisEnt_CellSupVisCom(XySelectedCell).ActiveVision(false, SupportVisionTypes.Selector);


        for (int x = 0; x < _eGM.Xamount; x++)
            for (int y = 0; y < _eGM.Yamount; y++)
            {
                if (_eGM.SelectorEnt_UnitTypeCom.HaveUnit)
                {
                    if (!_eGM.CellEnt_CellBaseCom(x, y).IsSelected && !_eGM.CellUnitEnt_UnitTypeCom(x, y).HaveUnit
                        && !_eGM.CellEnvEnt_CellEnvCom(x, y).HaveMountain)
                    {
                        if (Instance.IsMasterClient)
                        {
                            if (_eGM.CellEnt_CellBaseCom(x, y).IsStartedCell(true))
                            {
                                _eGM.CellSupVisEnt_CellSupVisCom(x, y).ActiveVision(true, SupportVisionTypes.Spawn);
                            }
                        }

                        else
                        {
                            if (_eGM.CellEnt_CellBaseCom(x, y).IsStartedCell(false))
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



                if (_eGM.CellUnitEnt_UnitTypeCom(x, y).HaveUnit && _eGM.CellUnitEnt_UnitTypeCom(x, y).UnitType != UnitTypes.King)
                {
                    if (_eGM.SelectorEnt_SelectorCom.UpgradeModType != UpgradeModTypes.None)
                    {
                        _eGM.CellSupVisEnt_CellSupVisCom(x, y).ActiveVision(true, SupportVisionTypes.Upgrade);
                    }
                    else
                    {
                        _eGM.CellSupVisEnt_CellSupVisCom(x, y).ActiveVision(false, SupportVisionTypes.Upgrade);
                    }
                }
            }



        for (int x = 0; x < _eGM.Xamount; x++)
            for (int y = 0; y < _eGM.Yamount; y++)
            {
                _eGM.CellSupVisEnt_CellSupVisCom(x, y).ActiveVision(false, SupportVisionTypes.WayUnit);
                _eGM.CellSupVisEnt_CellSupVisCom(x, y).ActiveVision(false, SupportVisionTypes.SimpleAttack);
                _eGM.CellSupVisEnt_CellSupVisCom(x, y).ActiveVision(false, SupportVisionTypes.UniqueAttack);



            }


        foreach (var xy in _eGM.SelectorEnt_SelectorCom.AvailableCellsForShift)
            _eGM.CellSupVisEnt_CellSupVisCom(xy).ActiveVision(true, SupportVisionTypes.WayUnit);

        foreach (var xy in _eGM.SelectorEnt_SelectorCom.AvailableCellsSimpleAttack)
            _eGM.CellSupVisEnt_CellSupVisCom(xy).ActiveVision(true, SupportVisionTypes.SimpleAttack);

        foreach (var xy in _eGM.SelectorEnt_SelectorCom.AvailableCellsUniqueAttack)
            _eGM.CellSupVisEnt_CellSupVisCom(xy).ActiveVision(true, SupportVisionTypes.UniqueAttack);
    }
}
