using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;
using static MainGame;

public struct CellUnitComponent
{
    private EntitiesGeneralManager _eGM;

    private int[] _xy;
    private bool _canShift;
    private bool _canAttack;
    private int _amountSteps;
    private GameObject _pawnGO;
    private GameObject _kingGO;
    private GameObject _rookGO;
    private GameObject _bishopGO;
    private SpriteRenderer _pawnSR;
    private SpriteRenderer _kingSR;

    internal bool IsActiveUnitMaster;
    internal bool IsActiveUnitOther;

    internal bool IsProtected;
    internal bool IsRelaxed;

    internal int AmountHealth;


    internal CellUnitComponent(EntitiesGeneralManager eGM, params int[] xy)
    {
        _eGM = eGM;


        _xy = xy;

        _eGM.CellUnitEnt_OwnerCom(_xy);
        _eGM.CellUnitEnt_UnitTypeCom(_xy);

        _canShift = default;
        _canAttack = default;

        IsActiveUnitMaster = default;
        IsActiveUnitOther = default;
        _amountSteps = default;
        AmountHealth = default;
        IsProtected = default;
        IsRelaxed = default;

        _pawnGO = Instance.GameObjectPool.CellUnitPawnGOs[_xy[_eGM.X], _xy[_eGM.Y]];
        _kingGO = Instance.GameObjectPool.CellUnitKingGOs[_xy[_eGM.X], _xy[_eGM.Y]];
        _rookGO = Instance.GameObjectPool.CellUnitRookGOs[_xy[_eGM.X], _xy[_eGM.Y]];
        _bishopGO = Instance.GameObjectPool.CellUnitBishopGOs[_xy[_eGM.X], _xy[_eGM.Y]];

        _pawnSR = Instance.GameObjectPool.CellUnitPawnGOs[_xy[_eGM.X], _xy[_eGM.Y]].GetComponent<SpriteRenderer>();
        _kingSR = Instance.GameObjectPool.CellUnitKingGOs[_xy[_eGM.X], _xy[_eGM.Y]].GetComponent<SpriteRenderer>();
    }


    #region Properties

    #region Health

    internal int MaxAmountHealth
    {
        get
        {
            switch (_eGM.CellUnitEnt_UnitTypeCom(_xy).UnitType)
            {
                case UnitTypes.None:
                    return default;

                case UnitTypes.King:
                    return Instance.StartValuesGameConfig.AMOUNT_HEALTH_KING;

                case UnitTypes.Pawn:
                    return Instance.StartValuesGameConfig.AMOUNT_HEALTH_PAWN + _eGM.InfoEnt_UpgradeCom.AmountUpgradePawnDict[_eGM.CellUnitEnt_OwnerCom(_xy).IsMasterClient] * Instance.StartValuesGameConfig.HEALTH_UPGRADE_ADDING_PAWN;

                case UnitTypes.Rook:
                    return Instance.StartValuesGameConfig.AMOUNT_HEALTH_ROOK + _eGM.InfoEnt_UpgradeCom.AmountUpgradeRookDict[_eGM.CellUnitEnt_OwnerCom(_xy).IsMasterClient] * Instance.StartValuesGameConfig.HEALTH_UPGRADE_ADDING_ROOK;

                case UnitTypes.Bishop:
                    return Instance.StartValuesGameConfig.AMOUNT_HEALTH_BISHOP + _eGM.InfoEnt_UpgradeCom.AmountUpgradeBishopDict[_eGM.CellUnitEnt_OwnerCom(_xy).IsMasterClient] * Instance.StartValuesGameConfig.HEALTH_UPGRADE_ADDING_BISHOP;

                default:
                    return default;
            }
        }
    }

    #endregion


    #region Damage

    internal int SimplePowerDamage
    {
        get
        {
            switch (_eGM.CellUnitEnt_UnitTypeCom(_xy).UnitType)
            {
                case UnitTypes.None:
                    return default;

                case UnitTypes.King:
                    return Instance.StartValuesGameConfig.SIMPLE_POWER_DAMAGE_KING;

                case UnitTypes.Pawn:
                     return Instance.StartValuesGameConfig.SIMPLE_POWER_DAMAGE_PAWN + _eGM.InfoEnt_UpgradeCom.AmountUpgradePawnDict[_eGM.CellUnitEnt_OwnerCom(_xy).IsMasterClient] * Instance.StartValuesGameConfig.DAMAGE_UPGRADE_ADDING_PAWN;

                case UnitTypes.Rook:
                    return Instance.StartValuesGameConfig.SIMPLE_POWER_DAMAGE_ROOK + _eGM.InfoEnt_UpgradeCom.AmountUpgradeRookDict[_eGM.CellUnitEnt_OwnerCom(_xy).IsMasterClient] * Instance.StartValuesGameConfig.DAMAGE_UPGRADE_ADDING_ROOK;

                case UnitTypes.Bishop:
                    return Instance.StartValuesGameConfig.SIMPLE_POWER_DAMAGE_BISHOP + _eGM.InfoEnt_UpgradeCom.AmountUpgradeBishopDict[_eGM.CellUnitEnt_OwnerCom(_xy).IsMasterClient] * Instance.StartValuesGameConfig.DAMAGE_UPGRADE_ADDING_BISHOP;

                default:
                    return default;
            }
        }
    }

    internal int UniquePowerDamage
    {
        get
        {
            switch (_eGM.CellUnitEnt_UnitTypeCom(_xy).UnitType)
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

    #endregion


    #region Protection

    internal int PowerProtection
    {
        get
        {
            int powerProtection = 0;

            if (IsProtected)
            {
                switch (_eGM.CellUnitEnt_UnitTypeCom(_xy).UnitType)
                {
                    case UnitTypes.King:
                        powerProtection += Instance.StartValuesGameConfig.PROTECTION_KING;
                        break;

                    case UnitTypes.Pawn:
                        powerProtection += Instance.StartValuesGameConfig.PROTECTION_PAWN;
                        break;

                    case UnitTypes.Rook:
                        powerProtection += Instance.StartValuesGameConfig.PROTECTION_ROOK;
                        break;

                    case UnitTypes.Bishop:
                        powerProtection += Instance.StartValuesGameConfig.PROTECTION_BISHOP;
                        break;
                }
            }

            else if (IsRelaxed)
            {
                switch (_eGM.CellUnitEnt_UnitTypeCom(_xy).UnitType)
                {
                    case UnitTypes.King:
                        powerProtection -= Instance.StartValuesGameConfig.PROTECTION_KING;
                        break;

                    case UnitTypes.Pawn:
                        powerProtection -= Instance.StartValuesGameConfig.PROTECTION_PAWN;
                        break;

                    case UnitTypes.Rook:
                        powerProtection -= Instance.StartValuesGameConfig.PROTECTION_ROOK;
                        break;

                    case UnitTypes.Bishop:
                        powerProtection -= Instance.StartValuesGameConfig.PROTECTION_BISHOP;
                        break;
                }
            }

            foreach (var item in _eGM.CellEnvEnt_CellEnvironmentCom(_xy).ListEnvironmentTypes)
            {
                switch (_eGM.CellUnitEnt_UnitTypeCom(_xy).UnitType)
                {
                    case UnitTypes.King:

                        switch (item)
                        {
                            case EnvironmentTypes.Food:
                                powerProtection += Instance.StartValuesGameConfig.PROTECTION_FOOD_FOR_KING;
                                break;

                            case EnvironmentTypes.Tree:
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
                            case EnvironmentTypes.Food:
                                powerProtection += Instance.StartValuesGameConfig.PROTECTION_FOOD_FOR_PAWN;
                                break;

                            case EnvironmentTypes.Tree:
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
                            case EnvironmentTypes.Food:
                                powerProtection += Instance.StartValuesGameConfig.PROTECTION_FOOD_FOR_ROOK;
                                break;

                            case EnvironmentTypes.Tree:
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
                            case EnvironmentTypes.Food:
                                powerProtection += Instance.StartValuesGameConfig.PROTECTION_FOOD_FOR_BISHOP;
                                break;

                            case EnvironmentTypes.Tree:
                                powerProtection += Instance.StartValuesGameConfig.PROTECTION_TREE_FOR_BISHOP;
                                break;

                            case EnvironmentTypes.Hill:
                                powerProtection += Instance.StartValuesGameConfig.PROTECTION_HILL_FOR_BISHOP;
                                break;
                        }

                        break;
                }

            }

            switch (_eGM.CellBuildingEnt_BuildingTypeCom(_xy).BuildingType)
            {
                case BuildingTypes.City:

                    switch (_eGM.CellUnitEnt_UnitTypeCom(_xy).UnitType)
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
                    break;

                case BuildingTypes.Woodcutter:
                    break;

                case BuildingTypes.Mine:
                    break;
            }

            return powerProtection;
        }
    }

    #endregion


    #region Steps

    internal int AmountSteps
    {
        get { return _amountSteps; }
        set { _amountSteps = value; }
    }
    internal bool HaveMaxSteps
    {
        get
        {
            switch (_eGM.CellUnitEnt_UnitTypeCom(_xy).UnitType)
            {
                case UnitTypes.King:
                    return _amountSteps == Instance.StartValuesGameConfig.STANDART_AMOUNT_STEPS_KING;
                case UnitTypes.Pawn:
                    return _amountSteps == Instance.StartValuesGameConfig.STANDART_AMOUNT_STEPS_PAWN;
                case UnitTypes.Rook:
                    return _amountSteps == Instance.StartValuesGameConfig.STANDART_AMOUNT_STEPS_ROOK;
                case UnitTypes.Bishop:
                    return _amountSteps == Instance.StartValuesGameConfig.STANDART_AMOUNT_STEPS_BISHOP;
            }
            return false;
        }
    }
    internal bool MinAmountSteps => _amountSteps >= Instance.StartValuesGameConfig.MIN_AMOUNT_STEPS_FOR_UNIT;

    internal int NeedAmountSteps
    {
        get
        {
            int amountSteps = 1;

            foreach (var item in _eGM.CellEnvEnt_CellEnvironmentCom(_xy).ListEnvironmentTypes)
            {
                switch (item)
                {
                    case EnvironmentTypes.Food:
                        amountSteps += Instance.StartValuesGameConfig.NEED_AMOUNT_STEPS_FOOD;
                        break;

                    case EnvironmentTypes.Tree:
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

    internal void RefreshAmountSteps()
    {
        switch (_eGM.CellUnitEnt_UnitTypeCom(_xy).UnitType)
        {
            case UnitTypes.King:
                _amountSteps = Instance.StartValuesGameConfig.STANDART_AMOUNT_STEPS_KING;
                break;

            case UnitTypes.Pawn:
                _amountSteps = Instance.StartValuesGameConfig.STANDART_AMOUNT_STEPS_PAWN;
                break;

            case UnitTypes.Rook:
                _amountSteps = Instance.StartValuesGameConfig.STANDART_AMOUNT_STEPS_ROOK;
                break;

            case UnitTypes.Bishop:
                _amountSteps = Instance.StartValuesGameConfig.STANDART_AMOUNT_STEPS_BISHOP;
                break;

            default:
                break;
        }
    }

    #endregion

    #endregion


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
        var unitType = _eGM.CellUnitEnt_UnitTypeCom(xyFromUnitTo).UnitType;
        var amountHealth = _eGM.CellUnitEnt_CellUnitCom(xyFromUnitTo).AmountHealth;
        var amountSteps = _eGM.CellUnitEnt_CellUnitCom(xyFromUnitTo).AmountSteps;
        var isProtected = _eGM.CellUnitEnt_CellUnitCom(xyFromUnitTo).IsProtected;
        var isRelaxed = _eGM.CellUnitEnt_CellUnitCom(xyFromUnitTo).IsRelaxed;
        var player = _eGM.CellUnitEnt_OwnerCom(xyFromUnitTo).Owner;

        SetUnit(unitType, amountHealth, amountSteps, isProtected, isRelaxed, player);
    }

    internal void SetUnit(in UnitTypes unitType, in int amountHealth, in int amountSteps, in bool isProtected, in bool isRelaxed, in Player player)
    {
        _eGM.CellUnitEnt_UnitTypeCom(_xy).UnitType = unitType;
        _amountSteps = amountSteps;
        AmountHealth = amountHealth;
        IsProtected = isProtected;
        IsRelaxed = isRelaxed;
        _eGM.CellUnitEnt_OwnerCom(_xy).Owner = player;



        _pawnGO.SetActive(false);
        _kingGO.SetActive(false);
        _rookGO.SetActive(false);
        _bishopGO.SetActive(false);

        switch (_eGM.CellUnitEnt_UnitTypeCom(_xy).UnitType)
        {
            case UnitTypes.King:
                _kingGO.SetActive(true);
                SetColorUnit(_kingSR, _eGM.CellUnitEnt_OwnerCom(_xy).Owner);
                break;

            case UnitTypes.Pawn:
                _pawnGO.SetActive(true);
                SetColorUnit(_pawnSR, _eGM.CellUnitEnt_OwnerCom(_xy).Owner);
                break;

            case UnitTypes.Rook:
                _rookGO.SetActive(true);
                SetColorUnit(_rookGO.GetComponent<SpriteRenderer>(), _eGM.CellUnitEnt_OwnerCom(_xy).Owner);
                break;

            case UnitTypes.Bishop:
                _bishopGO.SetActive(true);
                SetColorUnit(_bishopGO.GetComponent<SpriteRenderer>(), _eGM.CellUnitEnt_OwnerCom(_xy).Owner);
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
                if (player != default) SetColorUnit(_rookGO.GetComponent<SpriteRenderer>(), player);
                break;

            case UnitTypes.Bishop:
                _bishopGO.SetActive(isActive);
                if (player != default) SetColorUnit(_bishopGO.GetComponent<SpriteRenderer>(), player);
                break;

            default:
                break;
        }
    }

    #endregion




    internal List<int[]> GetCellsForShift()
    {
        var listAvailable = TryGetXYAround(_xy);

        var xyAvailableCellsForShift = new List<int[]>();

        foreach (var xy in listAvailable)
        {
            if (!_eGM.CellEnvEnt_CellEnvironmentCom(xy).HaveMountain && !_eGM.CellUnitEnt_UnitTypeCom(xy).HaveUnit)
            {
                if (_eGM.CellUnitEnt_CellUnitCom(_xy).AmountSteps >= _eGM.CellUnitEnt_CellUnitCom(xy).NeedAmountSteps
                    || _eGM.CellUnitEnt_CellUnitCom(_xy).HaveMaxSteps)
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

        bool isMelee = default;

        switch (_eGM.CellUnitEnt_UnitTypeCom(_xy).UnitType)
        {
            case UnitTypes.None:
                break;

            case UnitTypes.King:
                isMelee = true;
                break;

            case UnitTypes.Pawn:
                isMelee = true;
                break;

            case UnitTypes.Rook:
                isMelee = false;
                break;

            case UnitTypes.Bishop:
                isMelee = false;
                break;

            default:
                break;
        }


        if (isMelee)
        {
            for (DirectTypes directType1 = default; directType1 <= DirectTypes.LeftDown; directType1++)
            {
                var xy1 = GetXYCell(_xy, directType1);

                if (_eGM.CellUnitEnt_CellUnitCom(_xy).MinAmountSteps)
                {
                    if (!_eGM.CellEnvEnt_CellEnvironmentCom(xy1).HaveMountain)
                    {
                        if (_eGM.CellUnitEnt_UnitTypeCom(xy1).HaveUnit && !_eGM.CellUnitEnt_OwnerCom(xy1).IsHim(playerFrom))
                        {
                            if (_eGM.CellUnitEnt_UnitTypeCom(_xy).UnitType == UnitTypes.Pawn)
                            {
                                if (directType1 == DirectTypes.Left || directType1 == DirectTypes.Right || directType1 == DirectTypes.Up || directType1 == DirectTypes.Down)
                                {
                                    availableCellsSimpleAttack.Add(xy1);
                                }
                                else availableCellsUniqueAttack.Add(xy1);
                            }

                            else if (_eGM.CellUnitEnt_UnitTypeCom(_xy).UnitType == UnitTypes.King)
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

                if (_eGM.CellUnitEnt_CellUnitCom(_xy).MinAmountSteps)
                {
                    if (!_eGM.CellEnvEnt_CellEnvironmentCom(xy1).HaveMountain)
                    {
                        if (_eGM.CellUnitEnt_UnitTypeCom(xy1).HaveUnit && !_eGM.CellUnitEnt_OwnerCom(xy1).IsHim(playerFrom))
                        {
                            if (_eGM.CellUnitEnt_UnitTypeCom(_xy).UnitType == UnitTypes.Rook)
                            {
                                if (directType1 == DirectTypes.Left || directType1 == DirectTypes.Right || directType1 == DirectTypes.Up || directType1 == DirectTypes.Down)
                                {
                                    availableCellsUniqueAttack.Add(xy1);
                                }
                                else availableCellsSimpleAttack.Add(xy1);
                            }

                            else if (_eGM.CellUnitEnt_UnitTypeCom(_xy).UnitType == UnitTypes.Bishop)
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

                if (_eGM.CellUnitEnt_UnitTypeCom(_xy).UnitType == UnitTypes.Rook)
                {
                    if (directType1 == DirectTypes.Left || directType1 == DirectTypes.Right || directType1 == DirectTypes.Down || directType1 == DirectTypes.Up)
                    {
                        if (!_eGM.CellEnvEnt_CellEnvironmentCom(xy2).HaveMountain && _eGM.CellUnitEnt_UnitTypeCom(xy2).HaveUnit && !_eGM.CellUnitEnt_OwnerCom(xy2).IsHim(playerFrom))
                        {
                            availableCellsUniqueAttack.Add(xy2);
                        }
                    }

                    if (directType1 == DirectTypes.LeftDown || directType1 == DirectTypes.LeftUp || directType1 == DirectTypes.RightDown || directType1 == DirectTypes.RightUp)
                    {
                        if (!_eGM.CellEnvEnt_CellEnvironmentCom(xy2).HaveMountain && _eGM.CellUnitEnt_UnitTypeCom(xy2).HaveUnit && !_eGM.CellUnitEnt_OwnerCom(xy2).IsHim(playerFrom))
                        {
                            availableCellsSimpleAttack.Add(xy2);
                        }
                    }
                }


                else if (_eGM.CellUnitEnt_UnitTypeCom(_xy).UnitType == UnitTypes.Bishop)
                {
                    if (directType1 == DirectTypes.LeftDown || directType1 == DirectTypes.LeftUp || directType1 == DirectTypes.RightDown || directType1 == DirectTypes.RightUp)
                    {
                        if (!_eGM.CellEnvEnt_CellEnvironmentCom(xy2).HaveMountain && _eGM.CellUnitEnt_UnitTypeCom(xy2).HaveUnit && !_eGM.CellUnitEnt_OwnerCom(xy2).IsHim(playerFrom))
                        {
                            availableCellsUniqueAttack.Add(xy2);
                        }
                    }

                    if (directType1 == DirectTypes.Left || directType1 == DirectTypes.Right || directType1 == DirectTypes.Down || directType1 == DirectTypes.Up)
                    {
                        if (!_eGM.CellEnvEnt_CellEnvironmentCom(xy2).HaveMountain && _eGM.CellUnitEnt_UnitTypeCom(xy2).HaveUnit && !_eGM.CellUnitEnt_OwnerCom(xy2).IsHim(playerFrom))
                        {
                            availableCellsSimpleAttack.Add(xy2);
                        }
                    }
                }
            }
        }



































        for (DirectTypes directType1 = default; directType1 <= DirectTypes.LeftDown; directType1++)
        {
            var xy1 = GetXYCell(_xy, directType1);


            switch (_eGM.CellUnitEnt_UnitTypeCom(_xy).UnitType)
            {
                case UnitTypes.Rook:

                    if (directType1 == DirectTypes.Left || directType1 == DirectTypes.Right || directType1 == DirectTypes.Down || directType1 == DirectTypes.Up)
                    {
                        var xy2 = GetXYCell(xy1, directType1);
                        if (!_eGM.CellEnvEnt_CellEnvironmentCom(xy2).HaveMountain && _eGM.CellUnitEnt_UnitTypeCom(xy2).HaveUnit && !_eGM.CellUnitEnt_OwnerCom(xy2).IsHim(playerFrom))
                        {
                            availableCellsUniqueAttack.Add(xy2);
                        }
                    }

                    if (directType1 == DirectTypes.LeftDown || directType1 == DirectTypes.LeftUp || directType1 == DirectTypes.RightDown || directType1 == DirectTypes.RightUp)
                    {
                        var xy2 = GetXYCell(xy1, directType1);
                        if (!_eGM.CellEnvEnt_CellEnvironmentCom(xy2).HaveMountain && _eGM.CellUnitEnt_UnitTypeCom(xy2).HaveUnit && !_eGM.CellUnitEnt_OwnerCom(xy2).IsHim(playerFrom))
                        {
                            availableCellsSimpleAttack.Add(xy2);
                        }
                    }

                    break;


                case UnitTypes.Bishop:

                    if (directType1 == DirectTypes.LeftDown || directType1 == DirectTypes.LeftUp || directType1 == DirectTypes.RightDown || directType1 == DirectTypes.RightUp)
                    {
                        var xy2 = GetXYCell(xy1, directType1);
                        if (!_eGM.CellEnvEnt_CellEnvironmentCom(xy2).HaveMountain && _eGM.CellUnitEnt_UnitTypeCom(xy2).HaveUnit && !_eGM.CellUnitEnt_OwnerCom(xy2).IsHim(playerFrom))
                        {
                            availableCellsUniqueAttack.Add(xy2);
                        }
                    }

                    if (directType1 == DirectTypes.Left || directType1 == DirectTypes.Right || directType1 == DirectTypes.Down || directType1 == DirectTypes.Up)
                    {
                        var xy2 = GetXYCell(xy1, directType1);
                        if (!_eGM.CellEnvEnt_CellEnvironmentCom(xy2).HaveMountain && _eGM.CellUnitEnt_UnitTypeCom(xy2).HaveUnit && !_eGM.CellUnitEnt_OwnerCom(xy2).IsHim(playerFrom))
                        {
                            availableCellsSimpleAttack.Add(xy2);
                        }
                    }

                    break;

                default:
                    break;
            }
        }



    }

    internal List<int[]> TryGetXYAround(int[] xyStartCell)
    {
        var xyAvailableCells = new List<int[]>();
        var xyResultCell = new int[_eGM.XYForArray];

        for (int i = 0; i < (int)DirectTypes.LeftDown + 1; i++)
        {
            var xyDirectCell = GetXYDirect((DirectTypes)i);

            xyResultCell[0] = xyStartCell[0] + xyDirectCell[0];
            xyResultCell[1] = xyStartCell[1] + xyDirectCell[1];

            xyAvailableCells.Add(Instance.CellBaseOperations.CopyXY(xyResultCell));
        }

        return xyAvailableCells;
    }

    internal List<int[]> TryGetXYAround()
    {
        var xyAvailableCells = new List<int[]>();
        var xyResultCell = new int[_eGM.XYForArray];

        for (int i = 0; i < (int)DirectTypes.LeftDown + 1; i++)
        {
            var xyDirectCell = GetXYDirect((DirectTypes)i);

            xyResultCell[0] = _xy[0] + xyDirectCell[0];
            xyResultCell[1] = _xy[1] + xyDirectCell[1];

            xyAvailableCells.Add(Instance.CellBaseOperations.CopyXY(xyResultCell));
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

