﻿using Leopotam.Ecs;
using static MainGame;

internal class SupportVisionSystem : CellGeneralReduction, IEcsRunSystem
{
    private bool _isRepeated;

    internal SupportVisionSystem(ECSmanager eCSmanager) : base(eCSmanager)
    {

    }


    public void Run()
    {
        _eGM.CellSupVisEntCellSupportVisionCom(_eGM.SelectorESelectorC.XYpreviousCell).ActiveVision(false, SupportVisionTypes.Selector);

        if (_eGM.CellEnt_CellCom(_eGM.SelectorESelectorC.XYselectedCell).IsSelected)
        {
            _eGM.CellSupVisEntCellSupportVisionCom(_eGM.SelectorESelectorC.XYselectedCell).ActiveVision(true, SupportVisionTypes.Selector);
        }
        else
        {
            _eGM.CellSupVisEntCellSupportVisionCom(_eGM.SelectorESelectorC.XYselectedCell).ActiveVision(false, SupportVisionTypes.Selector);
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
                        if (!_eGM.CellEnt_CellCom(x, y).IsSelected && !_eGM.CellUnitEnt_UnitTypeCom(x, y).HaveUnit && !_eGM.CellEnvEnt_CellEnvironmentCom(x, y).HaveMountain)
                        {
                            if (Instance.IsMasterClient)
                            {
                                if (_eGM.CellEnt_CellCom(x, y).IsStartMaster)
                                {
                                    _eGM.CellSupVisEntCellSupportVisionCom(x, y).ActiveVision(true, SupportVisionTypes.Spawn);
                                }
                            }

                            else
                            {
                                if (_eGM.CellEnt_CellCom(x, y).IsStartOther)
                                {
                                    _eGM.CellSupVisEntCellSupportVisionCom(x, y).ActiveVision(true, SupportVisionTypes.Spawn);
                                }
                            }
                        }
                    }

                    else if (!_eGM.CellEnt_CellCom(x, y).IsSelected)
                    {
                        _eGM.CellSupVisEntCellSupportVisionCom(x, y).ActiveVision(false, SupportVisionTypes.Spawn);
                    }
                }

            }
        }


        for (int x = 0; x < _eGM.Xamount; x++)
        {
            for (int y = 0; y < _eGM.Yamount; y++)
            {
                _eGM.CellSupVisEntCellSupportVisionCom(x, y).ActiveVision(false, SupportVisionTypes.WayOfUnit);
                _eGM.CellSupVisEntCellSupportVisionCom(x, y).ActiveVision(false, SupportVisionTypes.SimpleAttack);
                _eGM.CellSupVisEntCellSupportVisionCom(x, y).ActiveVision(false, SupportVisionTypes.UniqueAttack);
            }
        }

        foreach (var xy in _eGM.SelectorESelectorC.AvailableCellsForShift)
        {
            _eGM.CellSupVisEntCellSupportVisionCom(xy).ActiveVision(true, SupportVisionTypes.WayOfUnit);
        }
        foreach (var xy in _eGM.SelectorESelectorC.AvailableCellsSimpleAttack)
        {
            _eGM.CellSupVisEntCellSupportVisionCom(xy).ActiveVision(true, SupportVisionTypes.SimpleAttack);
        }
        foreach (var xy in _eGM.SelectorESelectorC.AvailableCellsUniqueAttack)
        {
            _eGM.CellSupVisEntCellSupportVisionCom(xy).ActiveVision(true, SupportVisionTypes.UniqueAttack);
        }
    }
}
