using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;
using static MainGame;

public struct CellUnitComponent
{
    private EntitiesGeneralManager _eGM;
    private int[] _xy;

    private UnitTypes _unitType;
    private Player _owner;
    private bool _isActiveUnitMaster;
    private bool _isActiveUnitOther;
    private bool _canShift;
    private bool _canAttack;
    private GameObject _pawnGO;
    private GameObject _kingGO;
    private GameObject _rookGO;
    private GameObject _bishopGO;
    private SpriteRenderer _pawnSR;
    private SpriteRenderer _kingSR;
    private SpriteRenderer _rookSR;
    private SpriteRenderer _bishopSR;


    internal UnitTypes UnitType => _unitType;
    internal bool HaveUnit => UnitType != UnitTypes.None;
    internal Player Owner => _owner;

    internal bool IsMelee
    {
        get
        {
            switch (UnitType)
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

    internal int AmountSteps;

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
                    return Instance.StartValuesGameConfig.AMOUNT_HEALTH_KING;

                case UnitTypes.Pawn:
                    return Instance.StartValuesGameConfig.AMOUNT_HEALTH_PAWN + _eGM.InfoEnt_UpgradeCom.AmountUpgradePawnDict[IsMasterClient] * Instance.StartValuesGameConfig.HEALTH_UPGRADE_ADDING_PAWN;

                case UnitTypes.Rook:
                    return Instance.StartValuesGameConfig.AMOUNT_HEALTH_ROOK + _eGM.InfoEnt_UpgradeCom.AmountUpgradeRookDict[IsMasterClient] * Instance.StartValuesGameConfig.HEALTH_UPGRADE_ADDING_ROOK;

                case UnitTypes.Bishop:
                    return Instance.StartValuesGameConfig.AMOUNT_HEALTH_BISHOP + _eGM.InfoEnt_UpgradeCom.AmountUpgradeBishopDict[IsMasterClient] * Instance.StartValuesGameConfig.HEALTH_UPGRADE_ADDING_BISHOP;

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
                    return Instance.StartValuesGameConfig.SIMPLE_POWER_DAMAGE_KING;

                case UnitTypes.Pawn:
                    return Instance.StartValuesGameConfig.SIMPLE_POWER_DAMAGE_PAWN + _eGM.InfoEnt_UpgradeCom.AmountUpgradePawnDict[IsMasterClient] * Instance.StartValuesGameConfig.DAMAGE_UPGRADE_ADDING_PAWN;

                case UnitTypes.Rook:
                    return Instance.StartValuesGameConfig.SIMPLE_POWER_DAMAGE_ROOK + _eGM.InfoEnt_UpgradeCom.AmountUpgradeRookDict[IsMasterClient] * Instance.StartValuesGameConfig.DAMAGE_UPGRADE_ADDING_ROOK;

                case UnitTypes.Bishop:
                    return Instance.StartValuesGameConfig.SIMPLE_POWER_DAMAGE_BISHOP + _eGM.InfoEnt_UpgradeCom.AmountUpgradeBishopDict[IsMasterClient] * Instance.StartValuesGameConfig.DAMAGE_UPGRADE_ADDING_BISHOP;

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
                    return (int)(SimplePowerDamage * Instance.StartValuesGameConfig.RATION_UNIQUE_POWER_DAMAGE_PAWN);

                case UnitTypes.Rook:
                    return (int)(SimplePowerDamage * Instance.StartValuesGameConfig.RATION_UNIQUE_POWER_DAMAGE_ROOK);

                case UnitTypes.Bishop:
                    return (int)(SimplePowerDamage * Instance.StartValuesGameConfig.RATION_UNIQUE_POWER_DAMAGE_BISHOP);


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
                        powerProtection += (int)(SimplePowerDamage * Instance.StartValuesGameConfig.PERCENT_FOR_PROTECTION_KING);
                        break;

                    case UnitTypes.Pawn:
                        powerProtection += (int)(SimplePowerDamage * Instance.StartValuesGameConfig.PERCENT_FOR_PROTECTION_PAWN);
                        break;

                    case UnitTypes.Rook:
                        powerProtection += (int)(SimplePowerDamage * Instance.StartValuesGameConfig.PERCENT_FOR_PROTECTION_ROOK);
                        break;

                    case UnitTypes.Bishop:
                        powerProtection += (int)(SimplePowerDamage * Instance.StartValuesGameConfig.PERCENT_FOR_PROTECTION_BISHOP);
                        break;
                }
            }

            else if (IsRelaxed)
            {
                switch (UnitType)
                {
                    case UnitTypes.King:
                        powerProtection -= (int)(SimplePowerDamage * Instance.StartValuesGameConfig.PERCENT_FOR_PROTECTION_KING);
                        break;

                    case UnitTypes.Pawn:
                        powerProtection -= (int)(SimplePowerDamage * Instance.StartValuesGameConfig.PERCENT_FOR_PROTECTION_PAWN);
                        break;

                    case UnitTypes.Rook:
                        powerProtection -= (int)(SimplePowerDamage * Instance.StartValuesGameConfig.PERCENT_FOR_PROTECTION_ROOK);
                        break;

                    case UnitTypes.Bishop:
                        powerProtection -= (int)(SimplePowerDamage * Instance.StartValuesGameConfig.PERCENT_FOR_PROTECTION_BISHOP);
                        break;
                }
            }

            foreach (var item in _eGM.CellEnt_CellEnvCom(_xy).ListEnvironmentTypes)
            {
                switch (UnitType)
                {
                    case UnitTypes.King:

                        switch (item)
                        {
                            case EnvironmentTypes.Fertilizer:
                                powerProtection += Instance.StartValuesGameConfig.PROTECTION_FOOD_FOR_KING;
                                break;

                            case EnvironmentTypes.AdultForest:
                                powerProtection += Instance.StartValuesGameConfig.PROTECTION_TREE_FOR_KING;
                                break;

                            case EnvironmentTypes.Hill:
                                powerProtection += Instance.StartValuesGameConfig.PROTECTION_HILL_FOR_KING;
                                break;
                        }

                        break;


                    case UnitTypes.Pawn:

                        switch (item)
                        {
                            case EnvironmentTypes.Fertilizer:
                                powerProtection += Instance.StartValuesGameConfig.PROTECTION_FOOD_FOR_PAWN;
                                break;

                            case EnvironmentTypes.AdultForest:
                                powerProtection += Instance.StartValuesGameConfig.PROTECTION_TREE_FOR_PAWN;
                                break;

                            case EnvironmentTypes.Hill:
                                powerProtection += Instance.StartValuesGameConfig.PROTECTION_HILL_FOR_PAWN;
                                break;
                        }

                        break;


                    case UnitTypes.Rook:

                        switch (item)
                        {
                            case EnvironmentTypes.Fertilizer:
                                powerProtection += Instance.StartValuesGameConfig.PROTECTION_FOOD_FOR_ROOK;
                                break;

                            case EnvironmentTypes.AdultForest:
                                powerProtection += Instance.StartValuesGameConfig.PROTECTION_TREE_FOR_ROOK;
                                break;

                            case EnvironmentTypes.Hill:
                                powerProtection += Instance.StartValuesGameConfig.PROTECTION_HILL_FOR_ROOK;
                                break;
                        }

                        break;


                    case UnitTypes.Bishop:

                        switch (item)
                        {
                            case EnvironmentTypes.Fertilizer:
                                powerProtection += Instance.StartValuesGameConfig.PROTECTION_FOOD_FOR_BISHOP;
                                break;

                            case EnvironmentTypes.AdultForest:
                                powerProtection += Instance.StartValuesGameConfig.PROTECTION_TREE_FOR_BISHOP;
                                break;

                            case EnvironmentTypes.Hill:
                                powerProtection += Instance.StartValuesGameConfig.PROTECTION_HILL_FOR_BISHOP;
                                break;
                        }

                        break;
                }

            }

            switch (_eGM.CellEnt_CellBuildingCom(_xy).BuildingType)
            {
                case BuildingTypes.City:

                    switch (UnitType)
                    {
                        case UnitTypes.King:
                            powerProtection += Instance.StartValuesGameConfig.PROTECTION_CITY_KING;
                            break;

                        case UnitTypes.Pawn:
                            powerProtection += Instance.StartValuesGameConfig.PROTECTION_CITY_PAWN;
                            break;

                        case UnitTypes.Rook:
                            powerProtection += Instance.StartValuesGameConfig.PROTECTION_CITY_ROOK;
                            break;

                        case UnitTypes.Bishop:
                            powerProtection += Instance.StartValuesGameConfig.PROTECTION_CITY_BISHOP;
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

    internal bool HaveMaxSteps
    {
        get
        {
            switch (_unitType)
            {
                case UnitTypes.King:
                    return AmountSteps == Instance.StartValuesGameConfig.STANDART_AMOUNT_STEPS_KING;
                case UnitTypes.Pawn:
                    return AmountSteps == Instance.StartValuesGameConfig.STANDART_AMOUNT_STEPS_PAWN;
                case UnitTypes.Rook:
                    return AmountSteps == Instance.StartValuesGameConfig.STANDART_AMOUNT_STEPS_ROOK;
                case UnitTypes.Bishop:
                    return AmountSteps == Instance.StartValuesGameConfig.STANDART_AMOUNT_STEPS_BISHOP;
            }
            return false;
        }
    }
    internal bool MinAmountSteps => AmountSteps >= Instance.StartValuesGameConfig.MIN_AMOUNT_STEPS_FOR_UNIT;
    internal int NeedAmountSteps
    {
        get
        {
            int amountSteps = 1;

            foreach (var item in _eGM.CellEnt_CellEnvCom(_xy).ListEnvironmentTypes)
            {
                switch (item)
                {
                    case EnvironmentTypes.Fertilizer:
                        amountSteps += Instance.StartValuesGameConfig.NEED_AMOUNT_STEPS_FOOD;
                        break;

                    case EnvironmentTypes.YoungForest:
                        amountSteps += 0;
                        break;

                    case EnvironmentTypes.AdultForest:
                        amountSteps += Instance.StartValuesGameConfig.NEED_AMOUNT_STEPS_TREE;
                        break;

                    case EnvironmentTypes.Hill:
                        amountSteps += Instance.StartValuesGameConfig.NEED_AMOUNT_STEPS_HILL;
                        break;
                }
            }

            return amountSteps;
        }
    }

    internal CellUnitComponent(EntitiesGeneralManager eGM, params int[] xy)
    {
        _eGM = eGM;
        _xy = xy;

        _unitType = default;
        _owner = default;
        _canShift = default;
        _canAttack = default;

        _isActiveUnitMaster = default;
        _isActiveUnitOther = default;
        AmountSteps = default;
        AmountHealth = default;
        IsProtected = default;
        IsRelaxed = default;

        _pawnGO = Instance.GameObjectPool.CellUnitPawnGOs[_xy[_eGM.X], _xy[_eGM.Y]];
        _kingGO = Instance.GameObjectPool.CellUnitKingGOs[_xy[_eGM.X], _xy[_eGM.Y]];
        _rookGO = Instance.GameObjectPool.CellUnitRookGOs[_xy[_eGM.X], _xy[_eGM.Y]];
        _bishopGO = Instance.GameObjectPool.CellUnitBishopGOs[_xy[_eGM.X], _xy[_eGM.Y]];

        _pawnSR = _pawnGO.GetComponent<SpriteRenderer>();
        _kingSR = _kingGO.GetComponent<SpriteRenderer>();
        _rookSR = _rookGO.GetComponent<SpriteRenderer>();
        _bishopSR = _bishopGO.GetComponent<SpriteRenderer>();
    }



    internal void RefreshAmountSteps()
    {
        switch (UnitType)
        {
            case UnitTypes.King:
                AmountSteps = Instance.StartValuesGameConfig.STANDART_AMOUNT_STEPS_KING;
                break;

            case UnitTypes.Pawn:
                AmountSteps = Instance.StartValuesGameConfig.STANDART_AMOUNT_STEPS_PAWN;
                break;

            case UnitTypes.Rook:
                AmountSteps = Instance.StartValuesGameConfig.STANDART_AMOUNT_STEPS_ROOK;
                break;

            case UnitTypes.Bishop:
                AmountSteps = Instance.StartValuesGameConfig.STANDART_AMOUNT_STEPS_BISHOP;
                break;

            default:
                break;
        }
    }

    internal void SetActiveUnit(bool isMaster, bool isActive)
    {
        if (isMaster) _isActiveUnitMaster = isActive;
        else _isActiveUnitOther = isActive;
    }
    internal bool GetActiveUnit(bool isMaster)
    {
        if (isMaster)
        {
            return _isActiveUnitMaster;
        }
        else
        {
            return _isActiveUnitOther;
        }
    }



    #region Else

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

    internal void SetUnit(in UnitTypes unitType, in int amountHealth, in int amountSteps, in bool isProtected, in bool isRelaxed, in Player player)
    {
        _unitType = unitType;
        AmountSteps = amountSteps;
        AmountHealth = amountHealth;
        IsProtected = isProtected;
        IsRelaxed = isRelaxed;
        _owner = player;



        _pawnGO.SetActive(false);
        _kingGO.SetActive(false);
        _rookGO.SetActive(false);
        _bishopGO.SetActive(false);

        switch (UnitType)
        {
            case UnitTypes.King:
                _kingGO.SetActive(true);
                SetColorUnit(_kingSR, _owner);
                break;

            case UnitTypes.Pawn:
                _pawnGO.SetActive(true);
                SetColorUnit(_pawnSR, _owner);
                break;

            case UnitTypes.Rook:
                _rookGO.SetActive(true);
                SetColorUnit(_rookSR, _owner);
                break;

            case UnitTypes.Bishop:
                _bishopGO.SetActive(true);
                SetColorUnit(_bishopSR, _owner);
                break;
        }
    }

    internal void ActiveVisionCell(bool isActive, UnitTypes unitType, Player player = default)
    {
        switch (unitType)
        {
            case UnitTypes.King:
                _kingGO.SetActive(isActive);
                if (player != default) SetColorUnit(_kingSR, player);
                break;

            case UnitTypes.Pawn:
                _pawnGO.SetActive(isActive);
                if (player != default) SetColorUnit(_pawnSR, player);
                break;

            case UnitTypes.Rook:
                _rookGO.SetActive(isActive);
                if (player != default) SetColorUnit(_rookSR, player);
                break;

            case UnitTypes.Bishop:
                _bishopGO.SetActive(isActive);
                if (player != default) SetColorUnit(_bishopSR, player);
                break;

            default:
                break;
        }
    }

    #endregion


    #region Owner

    internal int ActorNumber
    {
        get
        {
            if (HaveUnit) return _owner.ActorNumber;
            else return -1;
        }
    }

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

    #endregion





    internal List<int[]> GetCellsForShift()
    {
        var listAvailable = TryGetXYAround(_xy);

        var xyAvailableCellsForShift = new List<int[]>();

        foreach (var xy in listAvailable)
        {
            if (!_eGM.CellEnt_CellEnvCom(xy).HaveMountain && !_eGM.CellEnt_CellUnitCom(xy).HaveUnit)
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


                if (!_eGM.CellEnt_CellEnvCom(xy1).HaveMountain)
                {
                    if (_eGM.CellEnt_CellUnitCom(xy1).NeedAmountSteps >= AmountSteps)
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

                if (_eGM.CellEnt_CellCom(xy1).IsActiveSelf)
                {
                    if (MinAmountSteps)
                    {
                        if (!_eGM.CellEnt_CellEnvCom(xy1).HaveMountain)
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

                    if (_eGM.CellEnt_CellUnitCom(xy2).GetActiveUnit(Instance.IsMasterClient))
                    {
                        if (UnitType == UnitTypes.Rook)
                        {
                            if (directType1 == DirectTypes.Left || directType1 == DirectTypes.Right || directType1 == DirectTypes.Down || directType1 == DirectTypes.Up)
                            {
                                if (!_eGM.CellEnt_CellEnvCom(xy2).HaveMountain && _eGM.CellEnt_CellUnitCom(xy2).HaveUnit && !_eGM.CellEnt_CellUnitCom(xy2).IsHim(playerFrom))
                                {
                                    availableCellsUniqueAttack.Add(xy2);
                                }
                            }

                            if (directType1 == DirectTypes.LeftDown || directType1 == DirectTypes.LeftUp || directType1 == DirectTypes.RightDown || directType1 == DirectTypes.RightUp)
                            {
                                if (!_eGM.CellEnt_CellEnvCom(xy2).HaveMountain && _eGM.CellEnt_CellUnitCom(xy2).HaveUnit && !_eGM.CellEnt_CellUnitCom(xy2).IsHim(playerFrom))
                                {
                                    availableCellsSimpleAttack.Add(xy2);
                                }
                            }
                        }


                        else if (UnitType == UnitTypes.Bishop)
                        {
                            if (directType1 == DirectTypes.LeftDown || directType1 == DirectTypes.LeftUp || directType1 == DirectTypes.RightDown || directType1 == DirectTypes.RightUp)
                            {
                                if (!_eGM.CellEnt_CellEnvCom(xy2).HaveMountain && _eGM.CellEnt_CellUnitCom(xy2).HaveUnit && !_eGM.CellEnt_CellUnitCom(xy2).IsHim(playerFrom))
                                {
                                    availableCellsUniqueAttack.Add(xy2);
                                }
                            }

                            if (directType1 == DirectTypes.Left || directType1 == DirectTypes.Right || directType1 == DirectTypes.Down || directType1 == DirectTypes.Up)
                            {
                                if (!_eGM.CellEnt_CellEnvCom(xy2).HaveMountain && _eGM.CellEnt_CellUnitCom(xy2).HaveUnit && !_eGM.CellEnt_CellUnitCom(xy2).IsHim(playerFrom))
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

            if (_eGM.CellEnt_CellCom(xyResultCell).IsActiveSelf)
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

