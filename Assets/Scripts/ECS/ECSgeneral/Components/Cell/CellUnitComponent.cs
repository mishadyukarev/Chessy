using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;
using System;
using static MainGame;

internal struct CellUnitComponent
{
    private int[] _xy;
    private EntitiesGeneralManager _eGM;

    private UnitTypes _unitType;
    private Player _owner;
    private GameObject _pawnGO;
    private GameObject _kingGO;
    private GameObject _rookGO;
    private GameObject _bishopGO;
    private SpriteRenderer _pawnSR;
    private SpriteRenderer _kingSR;
    private SpriteRenderer _rookSR;
    private SpriteRenderer _bishopSR;
    private StartValuesGameConfig _startValuesGameConfig;


    internal UnitTypes UnitType => _unitType;
    internal bool HaveUnit => UnitType != UnitTypes.None;
    internal bool HaveOwner => _owner != default;
    internal bool HaveUnitAndOwner => HaveUnit && HaveOwner;
    internal int ActorNumberOwner => _owner.ActorNumber;
    internal bool IsMine
    {
        get
        {
            if (HaveUnit) return _owner.IsLocal;
            else return default;
        }
    }
    internal bool IsMasterClient
    {
        get
        {
            if (HaveUnit) return _owner.IsMasterClient;
            else return default;
        }
    }
    internal bool IsHim(Player player)
    {
        if (player == default) return default;
        return player.ActorNumber == _owner.ActorNumber;
    }

    internal Transform CurrentUnitTransform
    {
        get
        {
            switch (_unitType)
            {
                case UnitTypes.None:
                    throw new Exception();

                case UnitTypes.King:
                    return _kingGO.transform;

                case UnitTypes.Pawn:
                    return _pawnGO.transform;

                case UnitTypes.Rook:
                    return _rookGO.transform;

                case UnitTypes.Bishop:
                    return _bishopGO.transform;

                default:
                    throw new Exception();
            }
        }
    }

    internal Dictionary<bool, bool> IsActivatedUnitDict;

    internal bool IsMelee
    {
        get
        {
            switch (_unitType)
            {
                case UnitTypes.None:
                    return false;

                case UnitTypes.King:
                    return true;

                case UnitTypes.Pawn:
                    return true;

                case UnitTypes.Rook:
                    return false;

                case UnitTypes.Bishop:
                    return false;

                default:
                    return false;
            }
        }
    }

    internal bool IsProtected;
    internal bool IsRelaxed;

    internal int AmountHealth;
    internal bool HaveHealth => AmountHealth > 0;
    internal int MaxAmountHealth
    {
        get
        {
            switch (_unitType)
            {
                case UnitTypes.None:
                    return default;

                case UnitTypes.King:
                    return _startValuesGameConfig.AMOUNT_HEALTH_KING;

                case UnitTypes.Pawn:
                    return _startValuesGameConfig.AMOUNT_HEALTH_PAWN + _eGM.InfoEnt_UpgradeInfoCom.AmountUpgradePawnDict[IsMasterClient] * _startValuesGameConfig.HEALTH_UPGRADE_ADDING_PAWN;

                case UnitTypes.Rook:
                    return _startValuesGameConfig.AMOUNT_HEALTH_ROOK + _eGM.InfoEnt_UpgradeInfoCom.AmountUpgradeRookDict[IsMasterClient] * _startValuesGameConfig.HEALTH_UPGRADE_ADDING_ROOK;

                case UnitTypes.Bishop:
                    return _startValuesGameConfig.AMOUNT_HEALTH_BISHOP + _eGM.InfoEnt_UpgradeInfoCom.AmountUpgradeBishopDict[IsMasterClient] * _startValuesGameConfig.HEALTH_UPGRADE_ADDING_BISHOP;

                default:
                    return default;
            }
        }
    }

    internal int SimplePowerDamage
    {
        get
        {
            switch (_unitType)
            {
                case UnitTypes.None:
                    return default;

                case UnitTypes.King:
                    return _startValuesGameConfig.SIMPLE_POWER_DAMAGE_KING;

                case UnitTypes.Pawn:
                    return _startValuesGameConfig.SIMPLE_POWER_DAMAGE_PAWN + _eGM.InfoEnt_UpgradeInfoCom.AmountUpgradePawnDict[IsMasterClient] * _startValuesGameConfig.DAMAGE_UPGRADE_ADDING_PAWN;

                case UnitTypes.Rook:
                    return _startValuesGameConfig.SIMPLE_POWER_DAMAGE_ROOK + _eGM.InfoEnt_UpgradeInfoCom.AmountUpgradeRookDict[IsMasterClient] * _startValuesGameConfig.DAMAGE_UPGRADE_ADDING_ROOK;

                case UnitTypes.Bishop:
                    return _startValuesGameConfig.SIMPLE_POWER_DAMAGE_BISHOP + _eGM.InfoEnt_UpgradeInfoCom.AmountUpgradeBishopDict[IsMasterClient] * _startValuesGameConfig.DAMAGE_UPGRADE_ADDING_BISHOP;

                default:
                    return default;
            }
        }
    }
    internal int UniquePowerDamage
    {
        get
        {
            switch (_unitType)
            {
                case UnitTypes.None:
                    return default;

                case UnitTypes.King:
                    return SimplePowerDamage;

                case UnitTypes.Pawn:
                    return (int)(SimplePowerDamage * _startValuesGameConfig.RATION_UNIQUE_POWER_DAMAGE_PAWN);

                case UnitTypes.Rook:
                    return (int)(SimplePowerDamage * _startValuesGameConfig.RATION_UNIQUE_POWER_DAMAGE_ROOK);

                case UnitTypes.Bishop:
                    return (int)(SimplePowerDamage * _startValuesGameConfig.RATION_UNIQUE_POWER_DAMAGE_BISHOP);


                default:
                    return default;
            }
        }
    }
    internal int PowerProtection
    {
        get
        {
            int powerProtection = 0;

            if (IsProtected)
            {
                switch (UnitType)
                {
                    case UnitTypes.King:
                        powerProtection += (int)(SimplePowerDamage * _startValuesGameConfig.PERCENT_FOR_PROTECTION_KING);
                        break;

                    case UnitTypes.Pawn:
                        powerProtection += (int)(SimplePowerDamage * _startValuesGameConfig.PERCENT_FOR_PROTECTION_PAWN);
                        break;

                    case UnitTypes.Rook:
                        powerProtection += (int)(SimplePowerDamage * _startValuesGameConfig.PERCENT_FOR_PROTECTION_ROOK);
                        break;

                    case UnitTypes.Bishop:
                        powerProtection += (int)(SimplePowerDamage * _startValuesGameConfig.PERCENT_FOR_PROTECTION_BISHOP);
                        break;
                }
            }

            else if (IsRelaxed)
            {
                switch (UnitType)
                {
                    case UnitTypes.King:
                        powerProtection -= (int)(SimplePowerDamage * _startValuesGameConfig.PERCENT_FOR_PROTECTION_KING);
                        break;

                    case UnitTypes.Pawn:
                        powerProtection -= (int)(SimplePowerDamage * _startValuesGameConfig.PERCENT_FOR_PROTECTION_PAWN);
                        break;

                    case UnitTypes.Rook:
                        powerProtection -= (int)(SimplePowerDamage * _startValuesGameConfig.PERCENT_FOR_PROTECTION_ROOK);
                        break;

                    case UnitTypes.Bishop:
                        powerProtection -= (int)(SimplePowerDamage * _startValuesGameConfig.PERCENT_FOR_PROTECTION_BISHOP);
                        break;
                }
            }

            foreach (var item in _eGM.CellEnvEnt_CellEnvCom(_xy).ListEnvironmentTypes)
            {
                switch (UnitType)
                {
                    case UnitTypes.King:

                        switch (item)
                        {
                            case EnvironmentTypes.Fertilizer:
                                powerProtection += _startValuesGameConfig.PROTECTION_FOOD_FOR_KING;
                                break;

                            case EnvironmentTypes.AdultForest:
                                powerProtection += _startValuesGameConfig.PROTECTION_TREE_FOR_KING;
                                break;

                            case EnvironmentTypes.Hill:
                                powerProtection += _startValuesGameConfig.PROTECTION_HILL_FOR_KING;
                                break;
                        }

                        break;


                    case UnitTypes.Pawn:

                        switch (item)
                        {
                            case EnvironmentTypes.Fertilizer:
                                powerProtection += _startValuesGameConfig.PROTECTION_FOOD_FOR_PAWN;
                                break;

                            case EnvironmentTypes.AdultForest:
                                powerProtection += _startValuesGameConfig.PROTECTION_TREE_FOR_PAWN;
                                break;

                            case EnvironmentTypes.Hill:
                                powerProtection += _startValuesGameConfig.PROTECTION_HILL_FOR_PAWN;
                                break;
                        }

                        break;


                    case UnitTypes.Rook:

                        switch (item)
                        {
                            case EnvironmentTypes.Fertilizer:
                                powerProtection += _startValuesGameConfig.PROTECTION_FOOD_FOR_ROOK;
                                break;

                            case EnvironmentTypes.AdultForest:
                                powerProtection += _startValuesGameConfig.PROTECTION_TREE_FOR_ROOK;
                                break;

                            case EnvironmentTypes.Hill:
                                powerProtection += _startValuesGameConfig.PROTECTION_HILL_FOR_ROOK;
                                break;
                        }

                        break;


                    case UnitTypes.Bishop:

                        switch (item)
                        {
                            case EnvironmentTypes.Fertilizer:
                                powerProtection += _startValuesGameConfig.PROTECTION_FOOD_FOR_BISHOP;
                                break;

                            case EnvironmentTypes.AdultForest:
                                powerProtection += _startValuesGameConfig.PROTECTION_TREE_FOR_BISHOP;
                                break;

                            case EnvironmentTypes.Hill:
                                powerProtection += _startValuesGameConfig.PROTECTION_HILL_FOR_BISHOP;
                                break;
                        }

                        break;
                }

            }

            switch (_eGM.CellBuildingEnt_BuildingTypeCom(_xy).BuildingType)
            {
                case BuildingTypes.City:

                    switch (UnitType)
                    {
                        case UnitTypes.King:
                            powerProtection += _startValuesGameConfig.PROTECTION_CITY_KING;
                            break;

                        case UnitTypes.Pawn:
                            powerProtection += _startValuesGameConfig.PROTECTION_CITY_PAWN;
                            break;

                        case UnitTypes.Rook:
                            powerProtection += _startValuesGameConfig.PROTECTION_CITY_ROOK;
                            break;

                        case UnitTypes.Bishop:
                            powerProtection += _startValuesGameConfig.PROTECTION_CITY_BISHOP;
                            break;
                    }

                    break;

                case BuildingTypes.Farm:
                    powerProtection += 5;
                    break;

                case BuildingTypes.Woodcutter:
                    powerProtection += 5;
                    break;

                case BuildingTypes.Mine:
                    break;
            }

            return powerProtection;
        }
    }

    internal int AmountSteps;
    internal bool HaveMaxSteps
    {
        get
        {
            switch (_unitType)
            {
                case UnitTypes.King:
                    return AmountSteps == _startValuesGameConfig.STANDART_AMOUNT_STEPS_KING;
                case UnitTypes.Pawn:
                    return AmountSteps == _startValuesGameConfig.STANDART_AMOUNT_STEPS_PAWN;
                case UnitTypes.Rook:
                    return AmountSteps == _startValuesGameConfig.STANDART_AMOUNT_STEPS_ROOK;
                case UnitTypes.Bishop:
                    return AmountSteps == _startValuesGameConfig.STANDART_AMOUNT_STEPS_BISHOP;
            }
            return false;
        }
    }
    internal bool HaveMinAmountSteps => AmountSteps >= _startValuesGameConfig.MIN_AMOUNT_STEPS_FOR_UNIT;
    internal int NeedAmountSteps
    {
        get
        {
            int amountSteps = 1;

            foreach (var item in _eGM.CellEnvEnt_CellEnvCom(_xy).ListEnvironmentTypes)
            {
                switch (item)
                {
                    case EnvironmentTypes.Fertilizer:
                        amountSteps += _startValuesGameConfig.NEED_AMOUNT_STEPS_FOOD;
                        break;

                    case EnvironmentTypes.YoungForest:
                        amountSteps += 0;
                        break;

                    case EnvironmentTypes.AdultForest:
                        amountSteps += _startValuesGameConfig.NEED_AMOUNT_STEPS_TREE;
                        break;

                    case EnvironmentTypes.Hill:
                        amountSteps += _startValuesGameConfig.NEED_AMOUNT_STEPS_HILL;
                        break;
                }
            }

            return amountSteps;
        }
    }


    internal CellUnitComponent(EntitiesGeneralManager eGM, StartValuesGameConfig startValuesGameConfig, ObjectPool gameObjectPool, int x, int y)
    {
        _eGM = eGM;
        _xy = new int[] { x, y};

        _unitType = default;
        _owner = default;

        IsActivatedUnitDict = new Dictionary<bool, bool>();
        AmountSteps = default;
        AmountHealth = default;
        IsProtected = default;
        IsRelaxed = default;

        _pawnGO = gameObjectPool.CellUnitPawnGOs[x, y];
        _kingGO = gameObjectPool.CellUnitKingGOs[x, y];
        _rookGO = gameObjectPool.CellUnitRookGOs[x, y];
        _bishopGO = gameObjectPool.CellUnitBishopGOs[x, y];

        _kingSR = gameObjectPool.CellUnitKingSRs[x, y];
        _pawnSR = gameObjectPool.CellUnitPawnSRs[x, y];
        _rookSR = gameObjectPool.CellUnitRookSRs[x, y];
        _bishopSR = gameObjectPool.CellUnitBishopSRs[x, y];

        _startValuesGameConfig = startValuesGameConfig;
    }

    internal void RefreshAmountSteps()
    {
        switch (UnitType)
        {
            case UnitTypes.King:
                AmountSteps = _startValuesGameConfig.STANDART_AMOUNT_STEPS_KING;
                break;

            case UnitTypes.Pawn:
                AmountSteps = _startValuesGameConfig.STANDART_AMOUNT_STEPS_PAWN;
                break;

            case UnitTypes.Rook:
                AmountSteps = _startValuesGameConfig.STANDART_AMOUNT_STEPS_ROOK;
                break;

            case UnitTypes.Bishop:
                AmountSteps = _startValuesGameConfig.STANDART_AMOUNT_STEPS_BISHOP;
                break;

            default:
                break;
        }
    }

    private void SetColorUnit(in SpriteRenderer unitSpriteRender, in Player player)
    {
        if (player.IsMasterClient) unitSpriteRender.color = Color.blue;
        else unitSpriteRender.color = Color.red;
    }

    internal void ResetUnit()
    {
        UnitTypes unitType = default;
        int amountHealth = default;
        int amountSteps = default;
        bool isProtected = default;
        bool isRelaxed = default;
        Player player = default;

        SetUnit(unitType, amountHealth, amountSteps, isProtected, isRelaxed, player);
    }
    internal void SetUnit(int[] xyFromUnitTo)
    {
        var unitType = _eGM.CellEnt_CellUnitCom(xyFromUnitTo).UnitType;
        var amountHealth = _eGM.CellEnt_CellUnitCom(xyFromUnitTo).AmountHealth;
        var amountSteps = _eGM.CellEnt_CellUnitCom(xyFromUnitTo).AmountSteps;
        var isProtected = _eGM.CellEnt_CellUnitCom(xyFromUnitTo).IsProtected;
        var isRelaxed = _eGM.CellEnt_CellUnitCom(xyFromUnitTo).IsRelaxed;
        var player = _eGM.CellEnt_CellUnitCom(xyFromUnitTo)._owner;

        SetUnit(unitType, amountHealth, amountSteps, isProtected, isRelaxed, player);
    }
    internal void SetUnit(UnitTypes unitType, int amountHealth, int amountSteps, bool isProtected, bool isRelaxed, Player player)
    {
        _unitType = unitType;
        AmountSteps = amountSteps;
        AmountHealth = amountHealth;
        IsProtected = isProtected;
        IsRelaxed = isRelaxed;
        _owner = player;


        switch (UnitType)
        {
            case UnitTypes.None:
                _pawnSR.enabled = false; 
                _kingSR.enabled = false;
                _rookSR.enabled = false;
                _bishopSR.enabled = false;
                break;

            case UnitTypes.King:
                _kingSR.enabled = true;
                SetColorUnit(_kingSR, _owner);
                break;

            case UnitTypes.Pawn:
                _pawnSR.enabled = true;
                SetColorUnit(_pawnSR, _owner);
                break;

            case UnitTypes.Rook:
                _rookSR.enabled = true;
                SetColorUnit(_rookSR, _owner);
                break;

            case UnitTypes.Bishop:
                _bishopSR.enabled = true;
                SetColorUnit(_bishopSR, _owner);
                break;
        }
    }

    internal void ActiveVisionCell(bool isActive, UnitTypes unitType, Player player = default)
    {
        switch (unitType)
        {
            case UnitTypes.King:
                _kingSR.enabled = isActive;
                if (player != default) SetColorUnit(_kingSR, player);
                break;

            case UnitTypes.Pawn:
                _pawnSR.enabled = isActive;
                if (player != default) SetColorUnit(_pawnSR, player);
                break;

            case UnitTypes.Rook:
                _rookSR.enabled = isActive;
                if (player != default) SetColorUnit(_rookSR, player);
                break;

            case UnitTypes.Bishop:
                _bishopSR.enabled = isActive;
                if (player != default) SetColorUnit(_bishopSR, player);
                break;

            default:
                break;
        }
    }



    internal List<int[]> GetCellsForShift()
    {
        var listAvailable = TryGetXYAround(_xy);

        var xyAvailableCellsForShift = new List<int[]>();

        foreach (var xy in listAvailable)
        {
            if (!_eGM.CellEnvEnt_CellEnvCom(xy).HaveMountain && !_eGM.CellEnt_CellUnitCom(xy).HaveUnit)
            {
                if (AmountSteps >= _eGM.CellEnt_CellUnitCom(xy).NeedAmountSteps || HaveMaxSteps)
                {
                    xyAvailableCellsForShift.Add(xy);
                }
            }
        }
        return xyAvailableCellsForShift;
    }

    internal void GetCellsForAttack(Player playerFrom, out List<int[]> availableCellsSimpleAttack, out List<int[]> availableCellsUniqueAttack)
    {
        availableCellsSimpleAttack = new List<int[]>();
        availableCellsUniqueAttack = new List<int[]>();

        if (IsMelee)
        {
            for (DirectTypes directType1 = default; directType1 <= DirectTypes.LeftDown; directType1++)
            {
                var xy1 = GetXYCell(_xy, directType1);


                if (!_eGM.CellEnvEnt_CellEnvCom(xy1).HaveMountain)
                {
                    if (_eGM.CellEnt_CellUnitCom(xy1).NeedAmountSteps <= AmountSteps || _eGM.CellEnt_CellUnitCom(_xy).HaveMaxSteps)
                    {
                        if (_eGM.CellEnt_CellUnitCom(xy1).HaveUnit && !_eGM.CellEnt_CellUnitCom(xy1).IsHim(playerFrom))
                        {
                            if (UnitType == UnitTypes.Pawn)
                            {
                                if (directType1 == DirectTypes.Left || directType1 == DirectTypes.Right || directType1 == DirectTypes.Up || directType1 == DirectTypes.Down)
                                {
                                    availableCellsSimpleAttack.Add(xy1);
                                }
                                else availableCellsUniqueAttack.Add(xy1);
                            }

                            else if (_eGM.CellEnt_CellUnitCom(_xy).UnitType == UnitTypes.King)
                            {
                                availableCellsSimpleAttack.Add(xy1);
                            }
                        }
                    }
                }
            }
        }






        else
        {
            for (DirectTypes directType1 = default; directType1 <= DirectTypes.LeftDown; directType1++)
            {
                var xy1 = GetXYCell(_xy, directType1);

                if (_eGM.CellEnt_CellBaseCom(xy1).IsActiveSelfGO)
                {
                    if (HaveMinAmountSteps)
                    {
                        if (!_eGM.CellEnvEnt_CellEnvCom(xy1).HaveMountain)
                        {
                            if (_eGM.CellEnt_CellUnitCom(xy1).HaveUnit && !_eGM.CellEnt_CellUnitCom(xy1).IsHim(playerFrom))
                            {
                                if (UnitType == UnitTypes.Rook)
                                {
                                    if (directType1 == DirectTypes.Left || directType1 == DirectTypes.Right || directType1 == DirectTypes.Up || directType1 == DirectTypes.Down)
                                    {
                                        availableCellsUniqueAttack.Add(xy1);
                                    }
                                    else availableCellsSimpleAttack.Add(xy1);
                                }

                                else if (UnitType == UnitTypes.Bishop)
                                {
                                    if (directType1 == DirectTypes.Left || directType1 == DirectTypes.Right || directType1 == DirectTypes.Up || directType1 == DirectTypes.Down)
                                    {
                                        availableCellsSimpleAttack.Add(xy1);
                                    }
                                    else availableCellsUniqueAttack.Add(xy1);
                                }
                            }
                        }
                    }


                    var xy2 = GetXYCell(xy1, directType1);

                    if (_eGM.CellEnt_CellUnitCom(xy2).IsActivatedUnitDict[Instance.IsMasterClient])
                    {
                        if (UnitType == UnitTypes.Rook)
                        {
                            if (directType1 == DirectTypes.Left || directType1 == DirectTypes.Right || directType1 == DirectTypes.Down || directType1 == DirectTypes.Up)
                            {
                                if (!_eGM.CellEnvEnt_CellEnvCom(xy2).HaveMountain && _eGM.CellEnt_CellUnitCom(xy2).HaveUnit && !_eGM.CellEnt_CellUnitCom(xy2).IsHim(playerFrom))
                                {
                                    availableCellsUniqueAttack.Add(xy2);
                                }
                            }

                            if (directType1 == DirectTypes.LeftDown || directType1 == DirectTypes.LeftUp || directType1 == DirectTypes.RightDown || directType1 == DirectTypes.RightUp)
                            {
                                if (!_eGM.CellEnvEnt_CellEnvCom(xy2).HaveMountain && _eGM.CellEnt_CellUnitCom(xy2).HaveUnit && !_eGM.CellEnt_CellUnitCom(xy2).IsHim(playerFrom))
                                {
                                    availableCellsSimpleAttack.Add(xy2);
                                }
                            }
                        }


                        else if (UnitType == UnitTypes.Bishop)
                        {
                            if (directType1 == DirectTypes.LeftDown || directType1 == DirectTypes.LeftUp || directType1 == DirectTypes.RightDown || directType1 == DirectTypes.RightUp)
                            {
                                if (!_eGM.CellEnvEnt_CellEnvCom(xy2).HaveMountain && _eGM.CellEnt_CellUnitCom(xy2).HaveUnit && !_eGM.CellEnt_CellUnitCom(xy2).IsHim(playerFrom))
                                {
                                    availableCellsUniqueAttack.Add(xy2);
                                }
                            }

                            if (directType1 == DirectTypes.Left || directType1 == DirectTypes.Right || directType1 == DirectTypes.Down || directType1 == DirectTypes.Up)
                            {
                                if (!_eGM.CellEnvEnt_CellEnvCom(xy2).HaveMountain && _eGM.CellEnt_CellUnitCom(xy2).HaveUnit && !_eGM.CellEnt_CellUnitCom(xy2).IsHim(playerFrom))
                                {
                                    availableCellsSimpleAttack.Add(xy2);
                                }
                            }
                        }
                    }
                }
            }
        } 
    }

    internal List<int[]> TryGetXYAround(int[] xyStartCell = default)
    {
        int[] xy;
        if(xyStartCell == default) xy = _xy;
        else xy = xyStartCell;

        var xyAvailableCells = new List<int[]>();
        var xyResultCell = new int[_eGM.XYForArray];

        for (int i = 0; i < (int)DirectTypes.LeftDown + 1; i++)
        {
            var xyDirectCell = GetXYDirect((DirectTypes)i);

            xyResultCell[0] = xy[0] + xyDirectCell[0];
            xyResultCell[1] = xy[1] + xyDirectCell[1];

            if (_eGM.CellEnt_CellBaseCom(xyResultCell).IsActiveSelfGO)
            {
                xyAvailableCells.Add(_eGM.CellBaseOperEnt_CellBaseOperCom.CopyXY(xyResultCell));
            }
        }

        return xyAvailableCells;

    }


    private int[] GetXYCell(int[] xyStartCell, DirectTypes directType)
    {
        var xyResultCell = new int[_eGM.XYForArray];

        var xyDirectCell = GetXYDirect(directType);

        xyResultCell[0] = xyStartCell[0] + xyDirectCell[0];
        xyResultCell[1] = xyStartCell[1] + xyDirectCell[1];

        return xyResultCell;
    }

    private int[] GetXYDirect(DirectTypes direct)
    {
        var xyDirectCell = new int[_eGM.XYForArray];

        switch (direct)
        {
            case DirectTypes.Right:
                xyDirectCell[0] = 1;
                xyDirectCell[1] = 0;
                break;

            case DirectTypes.Left:
                xyDirectCell[0] = -1;
                xyDirectCell[1] = 0;
                break;

            case DirectTypes.Up:
                xyDirectCell[0] = 0;
                xyDirectCell[1] = 1;
                break;

            case DirectTypes.Down:
                xyDirectCell[0] = 0;
                xyDirectCell[1] = -1;
                break;

            case DirectTypes.RightUp:
                xyDirectCell[0] = 1;
                xyDirectCell[1] = 1;
                break;

            case DirectTypes.LeftUp:
                xyDirectCell[0] = -1;
                xyDirectCell[1] = 1;
                break;

            case DirectTypes.RightDown:
                xyDirectCell[0] = 1;
                xyDirectCell[1] = -1;
                break;

            case DirectTypes.LeftDown:
                xyDirectCell[0] = -1;
                xyDirectCell[1] = -1;
                break;

            default:
                break;
        }

        return xyDirectCell;
    }
}

