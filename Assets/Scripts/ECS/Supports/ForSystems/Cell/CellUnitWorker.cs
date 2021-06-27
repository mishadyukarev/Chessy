using Assets.Scripts.Abstractions;
using Photon.Realtime;
using System.Collections.Generic;
using static Assets.Scripts.Main;

namespace Assets.Scripts
{
    internal sealed class CellUnitWorker : CellWorker
    {
        private CellBaseOperations _cellBaseOperations;

        internal CellUnitWorker(ECSManager eCSmanager, CellBaseOperations cellBaseOperations) : base(eCSmanager)
        {
            _cellBaseOperations = cellBaseOperations;
        }

        internal int MaxAmountHealth(params int[] xy)
        {
            switch (_eGM.CellUnitEnt_UnitTypeCom(xy).UnitType)
            {
                case UnitTypes.None:
                    return default;

                case UnitTypes.King:
                    return Instance.StartValuesGameConfig.AMOUNT_HEALTH_KING;

                case UnitTypes.Pawn:
                    return Instance.StartValuesGameConfig.AMOUNT_HEALTH_PAWN + _eGM.UnitInventorEnt_UpgradeUnitCom.AmountUpgrades(UnitTypes.Pawn, _eGM.CellUnitEnt_CellOwnerCom(xy).IsMasterClient) * Instance.StartValuesGameConfig.HEALTH_UPGRADE_ADDING_PAWN;

                case UnitTypes.Rook:
                    return Instance.StartValuesGameConfig.AMOUNT_HEALTH_ROOK + _eGM.UnitInventorEnt_UpgradeUnitCom.AmountUpgrades(UnitTypes.Rook, _eGM.CellUnitEnt_CellOwnerCom(xy).IsMasterClient) * Instance.StartValuesGameConfig.HEALTH_UPGRADE_ADDING_ROOK;

                case UnitTypes.Bishop:
                    return Instance.StartValuesGameConfig.AMOUNT_HEALTH_BISHOP + _eGM.UnitInventorEnt_UpgradeUnitCom.AmountUpgrades(UnitTypes.Bishop, _eGM.CellUnitEnt_CellOwnerCom(xy).IsMasterClient) * Instance.StartValuesGameConfig.HEALTH_UPGRADE_ADDING_BISHOP;

                default:
                    return default;
            }
        }
        internal int SimplePowerDamage(params int[] xy)
        {

            switch (_eGM.CellUnitEnt_UnitTypeCom(xy).UnitType)
            {
                case UnitTypes.None:
                    return default;

                case UnitTypes.King:
                    return Instance.StartValuesGameConfig.SIMPLE_POWER_DAMAGE_KING;

                case UnitTypes.Pawn:
                    return Instance.StartValuesGameConfig.SIMPLE_POWER_DAMAGE_PAWN + _eGM.UnitInventorEnt_UpgradeUnitCom.AmountUpgrades(UnitTypes.Pawn, _eGM.CellUnitEnt_CellOwnerCom(xy).IsMasterClient) * Instance.StartValuesGameConfig.DAMAGE_UPGRADE_ADDING_PAWN;

                case UnitTypes.Rook:
                    return Instance.StartValuesGameConfig.SIMPLE_POWER_DAMAGE_ROOK + _eGM.UnitInventorEnt_UpgradeUnitCom.AmountUpgrades(UnitTypes.Rook, _eGM.CellUnitEnt_CellOwnerCom(xy).IsMasterClient) * Instance.StartValuesGameConfig.DAMAGE_UPGRADE_ADDING_ROOK;

                case UnitTypes.Bishop:
                    return Instance.StartValuesGameConfig.SIMPLE_POWER_DAMAGE_BISHOP + _eGM.UnitInventorEnt_UpgradeUnitCom.AmountUpgrades(UnitTypes.Bishop, _eGM.CellUnitEnt_CellOwnerCom(xy).IsMasterClient) * Instance.StartValuesGameConfig.DAMAGE_UPGRADE_ADDING_BISHOP;

                default:
                    return default;

            }
        }
        internal int UniquePowerDamage(params int[] xy)
        {

            switch (_eGM.CellUnitEnt_UnitTypeCom(xy).UnitType)
            {
                case UnitTypes.None:
                    return default;

                case UnitTypes.King:
                    return SimplePowerDamage(xy);

                case UnitTypes.Pawn:
                    return (int)(SimplePowerDamage(xy) * Instance.StartValuesGameConfig.RATION_UNIQUE_POWER_DAMAGE_PAWN);

                case UnitTypes.Rook:
                    return (int)(SimplePowerDamage(xy) * Instance.StartValuesGameConfig.RATION_UNIQUE_POWER_DAMAGE_ROOK);

                case UnitTypes.Bishop:
                    return (int)(SimplePowerDamage(xy) * Instance.StartValuesGameConfig.RATION_UNIQUE_POWER_DAMAGE_BISHOP);


                default:
                    return default;
            }

        }
        internal int PowerProtection(params int[] xy)
        {

            int powerProtection = 0;

            if (_eGM.CellUnitEnt_CellUnitCom(xy).IsProtected)
            {
                switch (_eGM.CellUnitEnt_UnitTypeCom(xy).UnitType)
                {
                    case UnitTypes.King:
                        powerProtection += (int)(SimplePowerDamage(xy) * Instance.StartValuesGameConfig.PERCENT_FOR_PROTECTION_KING);
                        break;

                    case UnitTypes.Pawn:
                        powerProtection += (int)(SimplePowerDamage(xy) * Instance.StartValuesGameConfig.PERCENT_FOR_PROTECTION_PAWN);
                        break;

                    case UnitTypes.Rook:
                        powerProtection += (int)(SimplePowerDamage(xy) * Instance.StartValuesGameConfig.PERCENT_FOR_PROTECTION_ROOK);
                        break;

                    case UnitTypes.Bishop:
                        powerProtection += (int)(SimplePowerDamage(xy) * Instance.StartValuesGameConfig.PERCENT_FOR_PROTECTION_BISHOP);
                        break;
                }
            }

            else if (_eGM.CellUnitEnt_CellUnitCom(xy).IsRelaxed)
            {
                switch (_eGM.CellUnitEnt_UnitTypeCom(xy).UnitType)
                {
                    case UnitTypes.King:
                        powerProtection -= (int)(SimplePowerDamage(xy) * Instance.StartValuesGameConfig.PERCENT_FOR_PROTECTION_KING);
                        break;

                    case UnitTypes.Pawn:
                        powerProtection -= (int)(SimplePowerDamage(xy) * Instance.StartValuesGameConfig.PERCENT_FOR_PROTECTION_PAWN);
                        break;

                    case UnitTypes.Rook:
                        powerProtection -= (int)(SimplePowerDamage(xy) * Instance.StartValuesGameConfig.PERCENT_FOR_PROTECTION_ROOK);
                        break;

                    case UnitTypes.Bishop:
                        powerProtection -= (int)(SimplePowerDamage(xy) * Instance.StartValuesGameConfig.PERCENT_FOR_PROTECTION_BISHOP);
                        break;
                }
            }

            foreach (var item in _eGM.CellEnvEnt_CellEnvCom(xy).ListEnvironmentTypes)
            {
                switch (_eGM.CellUnitEnt_UnitTypeCom(xy).UnitType)
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

            switch (_eGM.CellBuildEnt_BuilTypeCom(xy).BuildingType)
            {
                case BuildingTypes.City:

                    switch (_eGM.CellUnitEnt_UnitTypeCom(xy).UnitType)
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
        internal bool HaveMaxSteps(params int[] xy)
        {
            switch (_eGM.CellUnitEnt_UnitTypeCom(xy).UnitType)
            {
                case UnitTypes.King:
                    return _eGM.CellUnitEnt_CellUnitCom(xy).AmountSteps == Instance.StartValuesGameConfig.STANDART_AMOUNT_STEPS_KING;

                case UnitTypes.Pawn:
                    return _eGM.CellUnitEnt_CellUnitCom(xy).AmountSteps == Instance.StartValuesGameConfig.STANDART_AMOUNT_STEPS_PAWN;

                case UnitTypes.Rook:
                    return _eGM.CellUnitEnt_CellUnitCom(xy).AmountSteps == Instance.StartValuesGameConfig.STANDART_AMOUNT_STEPS_ROOK;

                case UnitTypes.Bishop:
                    return _eGM.CellUnitEnt_CellUnitCom(xy).AmountSteps == Instance.StartValuesGameConfig.STANDART_AMOUNT_STEPS_BISHOP;
            }
            return false;

        }
        internal int NeedAmountSteps(params int[] xy)
        {
            int amountSteps = 1;

            foreach (var item in _eGM.CellEnvEnt_CellEnvCom(xy).ListEnvironmentTypes)
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
        internal void RefreshAmountSteps(params int[] xy)
        {
            switch (_eGM.CellUnitEnt_UnitTypeCom(xy).UnitType)
            {
                case UnitTypes.King:
                    _eGM.CellUnitEnt_CellUnitCom(xy).AmountSteps = Instance.StartValuesGameConfig.STANDART_AMOUNT_STEPS_KING;
                    break;

                case UnitTypes.Pawn:
                    _eGM.CellUnitEnt_CellUnitCom(xy).AmountSteps = Instance.StartValuesGameConfig.STANDART_AMOUNT_STEPS_PAWN;
                    break;

                case UnitTypes.Rook:
                    _eGM.CellUnitEnt_CellUnitCom(xy).AmountSteps = Instance.StartValuesGameConfig.STANDART_AMOUNT_STEPS_ROOK;
                    break;

                case UnitTypes.Bishop:
                    _eGM.CellUnitEnt_CellUnitCom(xy).AmountSteps = Instance.StartValuesGameConfig.STANDART_AMOUNT_STEPS_BISHOP;
                    break;

                default:
                    break;
            }
        }
        internal void ResetUnit(params int[] xy)
        {
            UnitTypes unitType = default;
            int amountHealth = default;
            int amountSteps = default;
            bool isProtected = default;
            bool isRelaxed = default;
            Player player = default;

            SetUnit(unitType, amountHealth, amountSteps, isProtected, isRelaxed, player, xy);
        }
        internal void SetUnit(int[] xyFromUnitTo, int[] xyTo)
        {
            var unitType = _eGM.CellUnitEnt_UnitTypeCom(xyFromUnitTo).UnitType;
            var amountHealth = _eGM.CellUnitEnt_CellUnitCom(xyFromUnitTo).AmountHealth;
            var amountSteps = _eGM.CellUnitEnt_CellUnitCom(xyFromUnitTo).AmountSteps;
            var isProtected = _eGM.CellUnitEnt_CellUnitCom(xyFromUnitTo).IsProtected;
            var isRelaxed = _eGM.CellUnitEnt_CellUnitCom(xyFromUnitTo).IsRelaxed;
            var player = _eGM.CellUnitEnt_CellOwnerCom(xyFromUnitTo).Owner;

            SetUnit(unitType, amountHealth, amountSteps, isProtected, isRelaxed, player, xyTo);
        }
        internal void SetUnit(UnitTypes unitType, int amountHealth, int amountSteps, bool isProtected, bool isRelaxed, Player player, params int[] xy)
        {
            _eGM.CellUnitEnt_UnitTypeCom(xy).UnitType = unitType;
            _eGM.CellUnitEnt_CellUnitCom(xy).AmountSteps = amountSteps;
            _eGM.CellUnitEnt_CellUnitCom(xy).AmountHealth = amountHealth;
            _eGM.CellUnitEnt_CellUnitCom(xy).IsProtected = isProtected;
            _eGM.CellUnitEnt_CellUnitCom(xy).IsRelaxed = isRelaxed;
            _eGM.CellUnitEnt_CellOwnerCom(xy).SetOwner(player);


            switch (_eGM.CellUnitEnt_UnitTypeCom(xy).UnitType)
            {
                case UnitTypes.None:
                    _eGM.CellUnitEnt_CellUnitCom(xy).EnableSR(false, UnitTypes.King);
                    _eGM.CellUnitEnt_CellUnitCom(xy).EnableSR(false, UnitTypes.Pawn);
                    _eGM.CellUnitEnt_CellUnitCom(xy).EnableSR(false, UnitTypes.Rook);
                    _eGM.CellUnitEnt_CellUnitCom(xy).EnableSR(false, UnitTypes.Bishop);
                    break;

                case UnitTypes.King:
                    _eGM.CellUnitEnt_CellUnitCom(xy).EnableSR(true, UnitTypes.King, _eGM.CellUnitEnt_CellOwnerCom(xy).Owner);
                    break;

                case UnitTypes.Pawn:
                    _eGM.CellUnitEnt_CellUnitCom(xy).EnableSR(true, UnitTypes.Pawn, _eGM.CellUnitEnt_CellOwnerCom(xy).Owner);
                    break;

                case UnitTypes.Rook:
                    _eGM.CellUnitEnt_CellUnitCom(xy).EnableSR(true, UnitTypes.Rook, _eGM.CellUnitEnt_CellOwnerCom(xy).Owner);
                    break;

                case UnitTypes.Bishop:
                    _eGM.CellUnitEnt_CellUnitCom(xy).EnableSR(true, UnitTypes.Bishop, _eGM.CellUnitEnt_CellOwnerCom(xy).Owner);
                    break;
            }
        }


        internal List<int[]> GetCellsForShift(params int[] xy)
        {
            var listAvailable = TryGetXYAround(xy);

            var xyAvailableCellsForShift = new List<int[]>();

            foreach (var xy1 in listAvailable)
            {
                if (!_eGM.CellEnvEnt_CellEnvCom(xy1).HaveMountain && !_eGM.CellUnitEnt_UnitTypeCom(xy1).HaveUnit)
                {
                    if (_eGM.CellUnitEnt_CellUnitCom(xy).AmountSteps >= NeedAmountSteps(xy1) || HaveMaxSteps(xy))
                    {
                        xyAvailableCellsForShift.Add(xy1);
                    }
                }
            }
            return xyAvailableCellsForShift;
        }
        internal void GetCellsForAttack(Player playerFrom, out List<int[]> availableCellsSimpleAttack, out List<int[]> availableCellsUniqueAttack, int[] xy)
        {
            availableCellsSimpleAttack = new List<int[]>();
            availableCellsUniqueAttack = new List<int[]>();

            if (_eGM.CellUnitEnt_UnitTypeCom(xy).IsMelee)
            {
                for (DirectTypes directType1 = default; directType1 <= DirectTypes.LeftDown; directType1++)
                {
                    var xy1 = GetXYCell(xy, directType1);


                    if (!_eGM.CellEnvEnt_CellEnvCom(xy1).HaveMountain)
                    {
                        if (NeedAmountSteps(xy1) <= _eGM.CellUnitEnt_CellUnitCom(xy).AmountSteps || HaveMaxSteps(xy))
                        {
                            if (_eGM.CellUnitEnt_UnitTypeCom(xy1).HaveUnit && !_eGM.CellUnitEnt_CellOwnerCom(xy1).IsHim(playerFrom))
                            {
                                if (_eGM.CellUnitEnt_UnitTypeCom(xy).UnitType == UnitTypes.Pawn)
                                {
                                    if (directType1 == DirectTypes.Left || directType1 == DirectTypes.Right || directType1 == DirectTypes.Up || directType1 == DirectTypes.Down)
                                    {
                                        availableCellsSimpleAttack.Add(xy1);
                                    }
                                    else availableCellsUniqueAttack.Add(xy1);
                                }

                                else if (_eGM.CellUnitEnt_UnitTypeCom(xy).UnitType == UnitTypes.King)
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
                    var xy1 = GetXYCell(xy, directType1);

                    if (_eGM.CellEnt_CellBaseCom(xy1).IsActiveSelfGO)
                    {
                        if (_eGM.CellUnitEnt_CellUnitCom(xy).HaveMinAmountSteps)
                        {
                            if (!_eGM.CellEnvEnt_CellEnvCom(xy1).HaveMountain)
                            {
                                if (_eGM.CellUnitEnt_UnitTypeCom(xy1).HaveUnit && !_eGM.CellUnitEnt_CellOwnerCom(xy1).IsHim(playerFrom))
                                {
                                    if (_eGM.CellUnitEnt_UnitTypeCom(xy).UnitType == UnitTypes.Rook)
                                    {
                                        if (directType1 == DirectTypes.Left || directType1 == DirectTypes.Right || directType1 == DirectTypes.Up || directType1 == DirectTypes.Down)
                                        {
                                            availableCellsUniqueAttack.Add(xy1);
                                        }
                                        else availableCellsSimpleAttack.Add(xy1);
                                    }

                                    else if (_eGM.CellUnitEnt_UnitTypeCom(xy).UnitType == UnitTypes.Bishop)
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

                        if (_eGM.CellUnitEnt_CellUnitCom(xy2).IsActivatedUnitDict[Instance.IsMasterClient])
                        {
                            if (_eGM.CellUnitEnt_UnitTypeCom(xy).UnitType == UnitTypes.Rook)
                            {
                                if (directType1 == DirectTypes.Left || directType1 == DirectTypes.Right || directType1 == DirectTypes.Down || directType1 == DirectTypes.Up)
                                {
                                    if (!_eGM.CellEnvEnt_CellEnvCom(xy2).HaveMountain && _eGM.CellUnitEnt_UnitTypeCom(xy2).HaveUnit && !_eGM.CellUnitEnt_CellOwnerCom(xy2).IsHim(playerFrom))
                                    {
                                        availableCellsUniqueAttack.Add(xy2);
                                    }
                                }

                                if (directType1 == DirectTypes.LeftDown || directType1 == DirectTypes.LeftUp || directType1 == DirectTypes.RightDown || directType1 == DirectTypes.RightUp)
                                {
                                    if (!_eGM.CellEnvEnt_CellEnvCom(xy2).HaveMountain && _eGM.CellUnitEnt_UnitTypeCom(xy2).HaveUnit && !_eGM.CellUnitEnt_CellOwnerCom(xy2).IsHim(playerFrom))
                                    {
                                        availableCellsSimpleAttack.Add(xy2);
                                    }
                                }
                            }


                            else if (_eGM.CellUnitEnt_UnitTypeCom(xy).UnitType == UnitTypes.Bishop)
                            {
                                if (directType1 == DirectTypes.LeftDown || directType1 == DirectTypes.LeftUp || directType1 == DirectTypes.RightDown || directType1 == DirectTypes.RightUp)
                                {
                                    if (!_eGM.CellEnvEnt_CellEnvCom(xy2).HaveMountain && _eGM.CellUnitEnt_UnitTypeCom(xy2).HaveUnit && !_eGM.CellUnitEnt_CellOwnerCom(xy2).IsHim(playerFrom))
                                    {
                                        availableCellsUniqueAttack.Add(xy2);
                                    }
                                }

                                if (directType1 == DirectTypes.Left || directType1 == DirectTypes.Right || directType1 == DirectTypes.Down || directType1 == DirectTypes.Up)
                                {
                                    if (!_eGM.CellEnvEnt_CellEnvCom(xy2).HaveMountain && _eGM.CellUnitEnt_UnitTypeCom(xy2).HaveUnit && !_eGM.CellUnitEnt_CellOwnerCom(xy2).IsHim(playerFrom))
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
        internal List<int[]> TryGetXYAround(params int[] xyStartCell)
        {
            var xyAvailableCells = new List<int[]>();
            var xyResultCell = new int[ValuesConst.XY_FOR_ARRAY];

            for (int i = 0; i < (int)DirectTypes.LeftDown + 1; i++)
            {
                var xyDirectCell = GetXYDirect((DirectTypes)i);

                xyResultCell[0] = xyStartCell[0] + xyDirectCell[0];
                xyResultCell[1] = xyStartCell[1] + xyDirectCell[1];

                if (_eGM.CellEnt_CellBaseCom(xyResultCell).IsActiveSelfGO)
                {
                    xyAvailableCells.Add(_cellBaseOperations.CopyXY(xyResultCell));
                }
            }

            return xyAvailableCells;

        }
        private int[] GetXYCell(int[] xyStartCell, DirectTypes directType)
        {
            var xyResultCell = new int[ValuesConst.XY_FOR_ARRAY];

            var xyDirectCell = GetXYDirect(directType);

            xyResultCell[0] = xyStartCell[0] + xyDirectCell[0];
            xyResultCell[1] = xyStartCell[1] + xyDirectCell[1];

            return xyResultCell;
        }
        private int[] GetXYDirect(DirectTypes direct)
        {
            var xyDirectCell = new int[ValuesConst.XY_FOR_ARRAY];

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
}