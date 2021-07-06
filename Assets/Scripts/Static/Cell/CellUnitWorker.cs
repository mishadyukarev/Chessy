using Assets.Scripts.Abstractions;
using Assets.Scripts.Abstractions.Enums;
using Photon.Realtime;
using System.Collections.Generic;
using static Assets.Scripts.Main;
using static Assets.Scripts.Static.CellBaseOperations;

namespace Assets.Scripts
{
    public static class CellUnitWorker
    {
        internal static int MaxAmountHealth(params int[] xy)
        {
            switch (Instance.EGM.CellUnitEnt_UnitTypeCom(xy).UnitType)
            {
                case UnitTypes.None:
                    return default;

                case UnitTypes.King:
                    return Instance.StartValuesGameConfig.AMOUNT_HEALTH_KING;

                case UnitTypes.Pawn:
                    return Instance.StartValuesGameConfig.AMOUNT_HEALTH_PAWN;

                case UnitTypes.PawnSword:
                    return Instance.StartValuesGameConfig.AMOUNT_HEALTH_PAWN_SWORD;

                case UnitTypes.Rook:
                    return Instance.StartValuesGameConfig.AMOUNT_HEALTH_ROOK;

                case UnitTypes.RookCrossbow:
                    return Instance.StartValuesGameConfig.AMOUNT_HEALTH_ROOK_CROSSBOW;

                case UnitTypes.Bishop:
                    return Instance.StartValuesGameConfig.AMOUNT_HEALTH_BISHOP;

                case UnitTypes.BishopCrossbow:
                    return Instance.StartValuesGameConfig.AMOUNT_HEALTH_BISHOP_CROSSBOW;

                default:
                    return default;
            }
        }
        internal static int SimplePowerDamage(params int[] xy)
        {
            switch (Instance.EGM.CellUnitEnt_UnitTypeCom(xy).UnitType)
            {
                case UnitTypes.None:
                    return default;

                case UnitTypes.King:
                    return Instance.StartValuesGameConfig.SIMPLE_POWER_DAMAGE_KING;

                case UnitTypes.Pawn:
                    return Instance.StartValuesGameConfig.SIMPLE_POWER_DAMAGE_PAWN;

                case UnitTypes.PawnSword:
                    return Instance.StartValuesGameConfig.SIMPLE_POWER_DAMAGE_PAWN_SWORD;

                case UnitTypes.Rook:
                    return Instance.StartValuesGameConfig.SIMPLE_POWER_DAMAGE_ROOK;

                case UnitTypes.RookCrossbow:
                    return Instance.StartValuesGameConfig.SIMPLE_POWER_DAMAGE_ROOK_CROSSBOW;

                case UnitTypes.Bishop:
                    return Instance.StartValuesGameConfig.SIMPLE_POWER_DAMAGE_BISHOP;

                case UnitTypes.BishopCrossbow:
                    return Instance.StartValuesGameConfig.SIMPLE_POWER_DAMAGE_BISHOP_CROSSBOW;

                default:
                    return default;

            }
        }
        internal static int UniquePowerDamage(params int[] xy)
        {

            switch (Instance.EGM.CellUnitEnt_UnitTypeCom(xy).UnitType)
            {
                case UnitTypes.None:
                    return default;

                case UnitTypes.King:
                    return SimplePowerDamage(xy);

                case UnitTypes.Pawn:
                    return (int)(SimplePowerDamage(xy) * Instance.StartValuesGameConfig.RATION_UNIQUE_POWER_DAMAGE_PAWN);

                case UnitTypes.PawnSword:
                    return (int)(SimplePowerDamage(xy) * Instance.StartValuesGameConfig.RATION_UNIQUE_POWER_DAMAGE_PAWN_SWORD);

                case UnitTypes.Rook:
                    return (int)(SimplePowerDamage(xy) * Instance.StartValuesGameConfig.RATION_UNIQUE_POWER_DAMAGE_ROOK);

                case UnitTypes.RookCrossbow:
                    return (int)(SimplePowerDamage(xy) * Instance.StartValuesGameConfig.RATION_UNIQUE_POWER_DAMAGE_ROOK_CROSSBOW);

                case UnitTypes.Bishop:
                    return (int)(SimplePowerDamage(xy) * Instance.StartValuesGameConfig.RATION_UNIQUE_POWER_DAMAGE_BISHOP);

                case UnitTypes.BishopCrossbow:
                    return (int)(SimplePowerDamage(xy) * Instance.StartValuesGameConfig.RATION_UNIQUE_POWER_DAMAGE_BISHOP_CROSSBOW);

                default:
                    return default;
            }

        }
        internal static int PowerProtection(params int[] xy)
        {

            int powerProtection = 0;

            if (Instance.EGM.CellUnitEnt_CellUnitCom(xy).IsProtected)
            {
                switch (Instance.EGM.CellUnitEnt_UnitTypeCom(xy).UnitType)
                {
                    case UnitTypes.King:
                        powerProtection += (int)(SimplePowerDamage(xy) * Instance.StartValuesGameConfig.PERCENT_FOR_PROTECTION_KING);
                        break;

                    case UnitTypes.Pawn:
                        powerProtection += (int)(SimplePowerDamage(xy) * Instance.StartValuesGameConfig.PERCENT_FOR_PROTECTION_PAWN);
                        break;

                    case UnitTypes.PawnSword:
                        powerProtection += (int)(SimplePowerDamage(xy) * Instance.StartValuesGameConfig.PERCENT_FOR_PROTECTION_PAWN_SWORD);
                        break;

                    case UnitTypes.Rook:
                        powerProtection += (int)(SimplePowerDamage(xy) * Instance.StartValuesGameConfig.PERCENT_FOR_PROTECTION_ROOK);
                        break;

                    case UnitTypes.RookCrossbow:
                        powerProtection += (int)(SimplePowerDamage(xy) * Instance.StartValuesGameConfig.PERCENT_FOR_PROTECTION_ROOK_CROSSBOW);
                        break;

                    case UnitTypes.Bishop:
                        powerProtection += (int)(SimplePowerDamage(xy) * Instance.StartValuesGameConfig.PERCENT_FOR_PROTECTION_BISHOP);
                        break;

                    case UnitTypes.BishopCrossbow:
                        powerProtection += (int)(SimplePowerDamage(xy) * Instance.StartValuesGameConfig.PERCENT_FOR_PROTECTION_BISHOP_CROSSBOW);
                        break;
                }
            }

            else if (Instance.EGM.CellUnitEnt_CellUnitCom(xy).IsRelaxed)
            {
                switch (Instance.EGM.CellUnitEnt_UnitTypeCom(xy).UnitType)
                {
                    case UnitTypes.King:
                        powerProtection -= (int)(SimplePowerDamage(xy) * Instance.StartValuesGameConfig.PERCENT_FOR_PROTECTION_KING);
                        break;

                    case UnitTypes.Pawn:
                        powerProtection -= (int)(SimplePowerDamage(xy) * Instance.StartValuesGameConfig.PERCENT_FOR_PROTECTION_PAWN);
                        break;

                    case UnitTypes.PawnSword:
                        powerProtection -= (int)(SimplePowerDamage(xy) * Instance.StartValuesGameConfig.PERCENT_FOR_PROTECTION_PAWN_SWORD);
                        break;

                    case UnitTypes.Rook:
                        powerProtection -= (int)(SimplePowerDamage(xy) * Instance.StartValuesGameConfig.PERCENT_FOR_PROTECTION_ROOK);
                        break;

                    case UnitTypes.RookCrossbow:
                        powerProtection -= (int)(SimplePowerDamage(xy) * Instance.StartValuesGameConfig.PERCENT_FOR_PROTECTION_ROOK_CROSSBOW);
                        break;

                    case UnitTypes.Bishop:
                        powerProtection -= (int)(SimplePowerDamage(xy) * Instance.StartValuesGameConfig.PERCENT_FOR_PROTECTION_BISHOP);
                        break;

                    case UnitTypes.BishopCrossbow:
                        powerProtection -= (int)(SimplePowerDamage(xy) * Instance.StartValuesGameConfig.PERCENT_FOR_PROTECTION_BISHOP_CROSSBOW);
                        break;
                }
            }

            foreach (var item in Instance.EGM.CellEnvEnt_CellEnvCom(xy).ListEnvironmentTypes)
            {
                switch (Instance.EGM.CellUnitEnt_UnitTypeCom(xy).UnitType)
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


                    case UnitTypes.PawnSword:
                        switch (item)
                        {
                            case EnvironmentTypes.Fertilizer:
                                powerProtection += Instance.StartValuesGameConfig.PROTECTION_FOOD_FOR_PAWN_SWORD;
                                break;

                            case EnvironmentTypes.AdultForest:
                                powerProtection += Instance.StartValuesGameConfig.PROTECTION_TREE_FOR_PAWN_SWORD;
                                break;

                            case EnvironmentTypes.Hill:
                                powerProtection += Instance.StartValuesGameConfig.PROTECTION_HILL_FOR_PAWN_SWORD;
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


                    case UnitTypes.RookCrossbow:
                        switch (item)
                        {
                            case EnvironmentTypes.Fertilizer:
                                powerProtection += Instance.StartValuesGameConfig.PROTECTION_FOOD_FOR_ROOK_CROSSBOW;
                                break;

                            case EnvironmentTypes.AdultForest:
                                powerProtection += Instance.StartValuesGameConfig.PROTECTION_TREE_FOR_ROOK_CROSSBOW;
                                break;

                            case EnvironmentTypes.Hill:
                                powerProtection += Instance.StartValuesGameConfig.PROTECTION_HILL_FOR_ROOK_CROSSBOW;
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


                    case UnitTypes.BishopCrossbow:
                        switch (item)
                        {
                            case EnvironmentTypes.Fertilizer:
                                powerProtection += Instance.StartValuesGameConfig.PROTECTION_FOOD_FOR_BISHOP_CROSSBOW;
                                break;

                            case EnvironmentTypes.AdultForest:
                                powerProtection += Instance.StartValuesGameConfig.PROTECTION_TREE_FOR_BISHOP_CROSSBOW;
                                break;

                            case EnvironmentTypes.Hill:
                                powerProtection += Instance.StartValuesGameConfig.PROTECTION_HILL_FOR_BISHOP_CROSSBOW;
                                break;
                        }
                        break;
                }

            }

            switch (Instance.EGM.CellBuildEnt_BuilTypeCom(xy).BuildingType)
            {
                case BuildingTypes.City:

                    switch (Instance.EGM.CellUnitEnt_UnitTypeCom(xy).UnitType)
                    {
                        case UnitTypes.King:
                            powerProtection += Instance.StartValuesGameConfig.PROTECTION_CITY_KING;
                            break;

                        case UnitTypes.Pawn:
                            powerProtection += Instance.StartValuesGameConfig.PROTECTION_CITY_PAWN;
                            break;

                        case UnitTypes.PawnSword:
                            powerProtection += Instance.StartValuesGameConfig.PROTECTION_CITY_PAWN_SWORD;
                            break;

                        case UnitTypes.Rook:
                            powerProtection += Instance.StartValuesGameConfig.PROTECTION_CITY_ROOK;
                            break;

                        case UnitTypes.RookCrossbow:
                            powerProtection += Instance.StartValuesGameConfig.PROTECTION_CITY_ROOK_CROSSBOW;
                            break;

                        case UnitTypes.Bishop:
                            powerProtection += Instance.StartValuesGameConfig.PROTECTION_CITY_BISHOP;
                            break;

                        case UnitTypes.BishopCrossbow:
                            powerProtection += Instance.StartValuesGameConfig.PROTECTION_CITY_BISHOP_CROSSBOW;
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
        internal static bool HaveMaxSteps(params int[] xy)
        {
            switch (Instance.EGM.CellUnitEnt_UnitTypeCom(xy).UnitType)
            {
                case UnitTypes.King:
                    return Instance.EGM.CellUnitEnt_CellUnitCom(xy).AmountSteps == Instance.StartValuesGameConfig.STANDART_AMOUNT_STEPS_KING;

                case UnitTypes.Pawn:
                    return Instance.EGM.CellUnitEnt_CellUnitCom(xy).AmountSteps == Instance.StartValuesGameConfig.STANDART_AMOUNT_STEPS_PAWN;

                case UnitTypes.PawnSword:
                    return Instance.EGM.CellUnitEnt_CellUnitCom(xy).AmountSteps == Instance.StartValuesGameConfig.STANDART_AMOUNT_STEPS_PAWN_SWORD;

                case UnitTypes.Rook:
                    return Instance.EGM.CellUnitEnt_CellUnitCom(xy).AmountSteps == Instance.StartValuesGameConfig.STANDART_AMOUNT_STEPS_ROOK;

                case UnitTypes.RookCrossbow:
                    return Instance.EGM.CellUnitEnt_CellUnitCom(xy).AmountSteps == Instance.StartValuesGameConfig.STANDART_AMOUNT_STEPS_ROOK_CROSSBOW;

                case UnitTypes.Bishop:
                    return Instance.EGM.CellUnitEnt_CellUnitCom(xy).AmountSteps == Instance.StartValuesGameConfig.STANDART_AMOUNT_STEPS_BISHOP;

                case UnitTypes.BishopCrossbow:
                    return Instance.EGM.CellUnitEnt_CellUnitCom(xy).AmountSteps == Instance.StartValuesGameConfig.STANDART_AMOUNT_STEPS_BISHOP_CROSSBOW;
            }
            return false;

        }
        internal static int NeedAmountSteps(params int[] xy)
        {
            int amountSteps = 1;

            foreach (var item in Instance.EGM.CellEnvEnt_CellEnvCom(xy).ListEnvironmentTypes)
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
        internal static void RefreshAmountSteps(params int[] xy)
        {
            switch (Instance.EGM.CellUnitEnt_UnitTypeCom(xy).UnitType)
            {
                case UnitTypes.King:
                    Instance.EGM.CellUnitEnt_CellUnitCom(xy).AmountSteps = Instance.StartValuesGameConfig.STANDART_AMOUNT_STEPS_KING;
                    break;

                case UnitTypes.Pawn:
                    Instance.EGM.CellUnitEnt_CellUnitCom(xy).AmountSteps = Instance.StartValuesGameConfig.STANDART_AMOUNT_STEPS_PAWN;
                    break;

                case UnitTypes.PawnSword:
                    Instance.EGM.CellUnitEnt_CellUnitCom(xy).AmountSteps = Instance.StartValuesGameConfig.STANDART_AMOUNT_STEPS_PAWN_SWORD;
                    break;

                case UnitTypes.Rook:
                    Instance.EGM.CellUnitEnt_CellUnitCom(xy).AmountSteps = Instance.StartValuesGameConfig.STANDART_AMOUNT_STEPS_ROOK;
                    break;

                case UnitTypes.RookCrossbow:
                    Instance.EGM.CellUnitEnt_CellUnitCom(xy).AmountSteps = Instance.StartValuesGameConfig.STANDART_AMOUNT_STEPS_ROOK_CROSSBOW;
                    break;

                case UnitTypes.Bishop:
                    Instance.EGM.CellUnitEnt_CellUnitCom(xy).AmountSteps = Instance.StartValuesGameConfig.STANDART_AMOUNT_STEPS_BISHOP;
                    break;

                case UnitTypes.BishopCrossbow:
                    Instance.EGM.CellUnitEnt_CellUnitCom(xy).AmountSteps = Instance.StartValuesGameConfig.STANDART_AMOUNT_STEPS_BISHOP_CROSSBOW;
                    break;

                default:
                    break;
            }
        }


        internal static void ResetUnit(params int[] xy)
        {
            UnitTypes unitType = default;
            int amountHealth = default;
            int amountSteps = default;
            bool isProtected = default;
            bool isRelaxed = default;
            Player player = default;
            bool haveBot = default;

            SetPlayerUnit(unitType, amountHealth, amountSteps, isProtected, isRelaxed, player, xy);
            SetBotUnit(unitType, haveBot, amountHealth, amountSteps, isProtected, isRelaxed, xy);
        }
        internal static void ShiftUnit(int[] xyFromUnitTo, int[] xyTo)
        {
            var unitType = Instance.EGM.CellUnitEnt_UnitTypeCom(xyFromUnitTo).UnitType;
            var amountHealth = Instance.EGM.CellUnitEnt_CellUnitCom(xyFromUnitTo).AmountHealth;
            var amountSteps = Instance.EGM.CellUnitEnt_CellUnitCom(xyFromUnitTo).AmountSteps;
            var isProtected = Instance.EGM.CellUnitEnt_CellUnitCom(xyFromUnitTo).IsProtected;
            var isRelaxed = Instance.EGM.CellUnitEnt_CellUnitCom(xyFromUnitTo).IsRelaxed;
            var player = Instance.EGM.CellUnitEnt_CellOwnerCom(xyFromUnitTo).Owner;

            SetPlayerUnit(unitType, amountHealth, amountSteps, isProtected, isRelaxed, player, xyTo);
        }
        internal static void SetPlayerUnit(UnitTypes unitType, int amountHealth, int amountSteps, bool isProtected, bool isRelaxed, Player player, params int[] xy)
        {
            Instance.EGM.CellUnitEnt_UnitTypeCom(xy).UnitType = unitType;
            Instance.EGM.CellUnitEnt_CellUnitCom(xy).AmountSteps = amountSteps;
            Instance.EGM.CellUnitEnt_CellUnitCom(xy).AmountHealth = amountHealth;
            Instance.EGM.CellUnitEnt_CellUnitCom(xy).IsProtected = isProtected;
            Instance.EGM.CellUnitEnt_CellUnitCom(xy).IsRelaxed = isRelaxed;
            Instance.EGM.CellUnitEnt_CellOwnerCom(xy).SetOwner(player);


            Instance.EGM.CellUnitEnt_CellUnitCom(xy).EnablePlayerSR(false, UnitTypes.King, player);
            Instance.EGM.CellUnitEnt_CellUnitCom(xy).EnablePlayerSR(false, UnitTypes.Pawn, player);
            Instance.EGM.CellUnitEnt_CellUnitCom(xy).EnablePlayerSR(false, UnitTypes.PawnSword, player);
            Instance.EGM.CellUnitEnt_CellUnitCom(xy).EnablePlayerSR(false, UnitTypes.Rook, player);
            Instance.EGM.CellUnitEnt_CellUnitCom(xy).EnablePlayerSR(false, UnitTypes.RookCrossbow, player);
            Instance.EGM.CellUnitEnt_CellUnitCom(xy).EnablePlayerSR(false, UnitTypes.Bishop, player);
            Instance.EGM.CellUnitEnt_CellUnitCom(xy).EnablePlayerSR(false, UnitTypes.BishopCrossbow, player);

            switch (Instance.EGM.CellUnitEnt_UnitTypeCom(xy).UnitType)
            {
                case UnitTypes.None:
                    break;

                case UnitTypes.King:
                    Instance.EGM.CellUnitEnt_CellUnitCom(xy).EnablePlayerSR(true, UnitTypes.King, Instance.EGM.CellUnitEnt_CellOwnerCom(xy).Owner);
                    break;

                case UnitTypes.Pawn:
                    Instance.EGM.CellUnitEnt_CellUnitCom(xy).EnablePlayerSR(true, UnitTypes.Pawn, Instance.EGM.CellUnitEnt_CellOwnerCom(xy).Owner);
                    break;

                case UnitTypes.PawnSword:
                    Instance.EGM.CellUnitEnt_CellUnitCom(xy).EnablePlayerSR(true, UnitTypes.PawnSword, Instance.EGM.CellUnitEnt_CellOwnerCom(xy).Owner);
                    break;

                case UnitTypes.Rook:
                    Instance.EGM.CellUnitEnt_CellUnitCom(xy).EnablePlayerSR(true, UnitTypes.Rook, Instance.EGM.CellUnitEnt_CellOwnerCom(xy).Owner);
                    break;

                case UnitTypes.RookCrossbow:
                    Instance.EGM.CellUnitEnt_CellUnitCom(xy).EnablePlayerSR(true, UnitTypes.RookCrossbow, Instance.EGM.CellUnitEnt_CellOwnerCom(xy).Owner);
                    break;

                case UnitTypes.Bishop:
                    Instance.EGM.CellUnitEnt_CellUnitCom(xy).EnablePlayerSR(true, UnitTypes.Bishop, Instance.EGM.CellUnitEnt_CellOwnerCom(xy).Owner);
                    break;

                case UnitTypes.BishopCrossbow:
                    Instance.EGM.CellUnitEnt_CellUnitCom(xy).EnablePlayerSR(true, UnitTypes.BishopCrossbow, Instance.EGM.CellUnitEnt_CellOwnerCom(xy).Owner);
                    break;
            }
        }

        internal static void SetBotUnit(UnitTypes unitType, bool haveBot, int amountHealth, int amountSteps, bool isProtected, bool isRelaxed, params int[] xy)
        {
            Instance.EGM.CellUnitEnt_UnitTypeCom(xy).UnitType = unitType;
            Instance.EGM.CellUnitEnt_CellUnitCom(xy).AmountSteps = amountSteps;
            Instance.EGM.CellUnitEnt_CellUnitCom(xy).AmountHealth = amountHealth;
            Instance.EGM.CellUnitEnt_CellUnitCom(xy).IsProtected = isProtected;
            Instance.EGM.CellUnitEnt_CellUnitCom(xy).IsRelaxed = isRelaxed;
            Instance.EGM.CellUnitEnt_CellOwnerBotCom(xy).HaveBot = haveBot;


            Instance.EGM.CellUnitEnt_CellUnitCom(xy).EnableBotSR(false, UnitTypes.King);
            Instance.EGM.CellUnitEnt_CellUnitCom(xy).EnableBotSR(false, UnitTypes.Pawn);
            Instance.EGM.CellUnitEnt_CellUnitCom(xy).EnableBotSR(false, UnitTypes.PawnSword);
            Instance.EGM.CellUnitEnt_CellUnitCom(xy).EnableBotSR(false, UnitTypes.Rook);
            Instance.EGM.CellUnitEnt_CellUnitCom(xy).EnableBotSR(false, UnitTypes.RookCrossbow);
            Instance.EGM.CellUnitEnt_CellUnitCom(xy).EnableBotSR(false, UnitTypes.Bishop);
            Instance.EGM.CellUnitEnt_CellUnitCom(xy).EnableBotSR(false, UnitTypes.BishopCrossbow);

            switch (Instance.EGM.CellUnitEnt_UnitTypeCom(xy).UnitType)
            {
                case UnitTypes.None:
                    break;

                case UnitTypes.King:
                    Instance.EGM.CellUnitEnt_CellUnitCom(xy).EnableBotSR(true, UnitTypes.King);
                    break;

                case UnitTypes.Pawn:
                    Instance.EGM.CellUnitEnt_CellUnitCom(xy).EnableBotSR(true, UnitTypes.Pawn);
                    break;

                case UnitTypes.PawnSword:
                    Instance.EGM.CellUnitEnt_CellUnitCom(xy).EnableBotSR(true, UnitTypes.PawnSword);
                    break;

                case UnitTypes.Rook:
                    Instance.EGM.CellUnitEnt_CellUnitCom(xy).EnableBotSR(true, UnitTypes.Rook);
                    break;

                case UnitTypes.RookCrossbow:
                    Instance.EGM.CellUnitEnt_CellUnitCom(xy).EnableBotSR(true, UnitTypes.RookCrossbow);
                    break;

                case UnitTypes.Bishop:
                    Instance.EGM.CellUnitEnt_CellUnitCom(xy).EnableBotSR(true, UnitTypes.Bishop);
                    break;

                case UnitTypes.BishopCrossbow:
                    Instance.EGM.CellUnitEnt_CellUnitCom(xy).EnableBotSR(true, UnitTypes.BishopCrossbow);
                    break;
            }
        }

        internal static void ChangeUnitType(int[] xy, UnitTypes newUnitType)
        {
            Instance.EGM.CellUnitEnt_UnitTypeCom(xy).UnitType = newUnitType;

            Instance.EGM.CellUnitEnt_CellUnitCom(xy).EnableBotSR(false, UnitTypes.King);
            Instance.EGM.CellUnitEnt_CellUnitCom(xy).EnableBotSR(false, UnitTypes.Pawn);
            Instance.EGM.CellUnitEnt_CellUnitCom(xy).EnableBotSR(false, UnitTypes.PawnSword);
            Instance.EGM.CellUnitEnt_CellUnitCom(xy).EnableBotSR(false, UnitTypes.Rook);
            Instance.EGM.CellUnitEnt_CellUnitCom(xy).EnableBotSR(false, UnitTypes.RookCrossbow);
            Instance.EGM.CellUnitEnt_CellUnitCom(xy).EnableBotSR(false, UnitTypes.Bishop);
            Instance.EGM.CellUnitEnt_CellUnitCom(xy).EnableBotSR(false, UnitTypes.BishopCrossbow);

            switch (Instance.EGM.CellUnitEnt_UnitTypeCom(xy).UnitType)
            {
                case UnitTypes.None:
                    throw new System.Exception();

                case UnitTypes.King:
                    Instance.EGM.CellUnitEnt_CellUnitCom(xy).EnablePlayerSR(true, UnitTypes.King, Instance.EGM.CellUnitEnt_CellOwnerCom(xy).Owner);
                    break;

                case UnitTypes.Pawn:
                    Instance.EGM.CellUnitEnt_CellUnitCom(xy).EnablePlayerSR(true, UnitTypes.Pawn, Instance.EGM.CellUnitEnt_CellOwnerCom(xy).Owner);
                    break;

                case UnitTypes.PawnSword:
                    Instance.EGM.CellUnitEnt_CellUnitCom(xy).EnablePlayerSR(true, UnitTypes.PawnSword, Instance.EGM.CellUnitEnt_CellOwnerCom(xy).Owner);
                    Instance.EGM.CellUnitEnt_CellUnitCom(xy).AmountHealth += Instance.StartValuesGameConfig.AMOUNT_HEALTH_PAWN_SWORD - Instance.StartValuesGameConfig.AMOUNT_HEALTH_PAWN;
                    break;

                case UnitTypes.Rook:
                    Instance.EGM.CellUnitEnt_CellUnitCom(xy).EnablePlayerSR(true, UnitTypes.Rook, Instance.EGM.CellUnitEnt_CellOwnerCom(xy).Owner);
                    break;

                case UnitTypes.RookCrossbow:
                    Instance.EGM.CellUnitEnt_CellUnitCom(xy).EnablePlayerSR(true, UnitTypes.RookCrossbow, Instance.EGM.CellUnitEnt_CellOwnerCom(xy).Owner);
                    Instance.EGM.CellUnitEnt_CellUnitCom(xy).AmountHealth += Instance.StartValuesGameConfig.AMOUNT_HEALTH_ROOK_CROSSBOW - Instance.StartValuesGameConfig.AMOUNT_HEALTH_ROOK;
                    break;

                case UnitTypes.Bishop:
                    Instance.EGM.CellUnitEnt_CellUnitCom(xy).EnablePlayerSR(true, UnitTypes.Bishop, Instance.EGM.CellUnitEnt_CellOwnerCom(xy).Owner);
                    break;

                case UnitTypes.BishopCrossbow:
                    Instance.EGM.CellUnitEnt_CellUnitCom(xy).EnablePlayerSR(true, UnitTypes.BishopCrossbow, Instance.EGM.CellUnitEnt_CellOwnerCom(xy).Owner);
                    Instance.EGM.CellUnitEnt_CellUnitCom(xy).AmountHealth += Instance.StartValuesGameConfig.AMOUNT_HEALTH_BISHOP_CROSSBOW - Instance.StartValuesGameConfig.AMOUNT_HEALTH_BISHOP;
                    break;

                default:
                    throw new System.Exception();
            }
        }


        internal static List<int[]> GetCellsForShift(params int[] xy)
        {
            var listAvailable = TryGetXYAround(xy);

            var xyAvailableCellsForShift = new List<int[]>();

            foreach (var xy1 in listAvailable)
            {
                if (!Instance.EGM.CellEnvEnt_CellEnvCom(xy1).HaveMountain && !Instance.EGM.CellUnitEnt_UnitTypeCom(xy1).HaveUnit)
                {
                    if (Instance.EGM.CellUnitEnt_CellUnitCom(xy).AmountSteps >= NeedAmountSteps(xy1) || HaveMaxSteps(xy))
                    {
                        xyAvailableCellsForShift.Add(xy1);
                    }
                }
            }
            return xyAvailableCellsForShift;
        }
        internal static void GetCellsForAttack(Player playerFrom, out List<int[]> availableCellsSimpleAttack, out List<int[]> availableCellsUniqueAttack, int[] xy)
        {
            availableCellsSimpleAttack = new List<int[]>();
            availableCellsUniqueAttack = new List<int[]>();

            if (Instance.EGM.CellUnitEnt_UnitTypeCom(xy).IsMelee)
            {
                for (DirectTypes directType1 = default; directType1 <= DirectTypes.LeftDown; directType1++)
                {
                    var xy1 = GetXYCell(xy, directType1);


                    if (!Instance.EGM.CellEnvEnt_CellEnvCom(xy1).HaveMountain)
                    {
                        if (NeedAmountSteps(xy1) <= Instance.EGM.CellUnitEnt_CellUnitCom(xy).AmountSteps || HaveMaxSteps(xy))
                        {
                            if (Instance.EGM.CellUnitEnt_UnitTypeCom(xy1).HaveUnit)
                            {
                                if (Instance.EGM.CellUnitEnt_CellOwnerCom(xy1).HaveOwner)
                                {
                                    if (!Instance.EGM.CellUnitEnt_CellOwnerCom(xy1).IsHim(playerFrom))
                                    {
                                        if (Instance.EGM.CellUnitEnt_UnitTypeCom(xy).UnitType == UnitTypes.Pawn || Instance.EGM.CellUnitEnt_UnitTypeCom(xy).UnitType == UnitTypes.PawnSword)
                                        {
                                            if (directType1 == DirectTypes.Left || directType1 == DirectTypes.Right || directType1 == DirectTypes.Up || directType1 == DirectTypes.Down)
                                            {
                                                availableCellsSimpleAttack.Add(xy1);
                                            }
                                            else availableCellsUniqueAttack.Add(xy1);
                                        }

                                        else if (Instance.EGM.CellUnitEnt_UnitTypeCom(xy).UnitType == UnitTypes.King)
                                        {
                                            availableCellsSimpleAttack.Add(xy1);
                                        }
                                    }
                                }
                                else if (Instance.EGM.CellUnitEnt_CellOwnerBotCom(xy1).HaveBot)
                                {
                                    if (Instance.EGM.CellUnitEnt_UnitTypeCom(xy).UnitType == UnitTypes.Pawn || Instance.EGM.CellUnitEnt_UnitTypeCom(xy).UnitType == UnitTypes.PawnSword)
                                    {
                                        if (directType1 == DirectTypes.Left || directType1 == DirectTypes.Right || directType1 == DirectTypes.Up || directType1 == DirectTypes.Down)
                                        {
                                            availableCellsSimpleAttack.Add(xy1);
                                        }
                                        else availableCellsUniqueAttack.Add(xy1);
                                    }

                                    else if (Instance.EGM.CellUnitEnt_UnitTypeCom(xy).UnitType == UnitTypes.King)
                                    {
                                        availableCellsSimpleAttack.Add(xy1);
                                    }
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

                    if (Instance.EGM.CellEnt_CellBaseCom(xy1).IsActiveSelfGO)
                    {
                        if (Instance.EGM.CellUnitEnt_CellUnitCom(xy).HaveMinAmountSteps)
                        {
                            if (!Instance.EGM.CellEnvEnt_CellEnvCom(xy1).HaveMountain)
                            {
                                if (Instance.EGM.CellUnitEnt_UnitTypeCom(xy1).HaveUnit)
                                {
                                    if (Instance.EGM.CellUnitEnt_CellOwnerCom(xy1).HaveOwner)
                                    {
                                        if (!Instance.EGM.CellUnitEnt_CellOwnerCom(xy1).IsHim(playerFrom))
                                        {
                                            if (Instance.EGM.CellUnitEnt_UnitTypeCom(xy).UnitType == UnitTypes.Rook || Instance.EGM.CellUnitEnt_UnitTypeCom(xy).UnitType == UnitTypes.RookCrossbow)
                                            {
                                                if (directType1 == DirectTypes.Left || directType1 == DirectTypes.Right || directType1 == DirectTypes.Up || directType1 == DirectTypes.Down)
                                                {
                                                    availableCellsUniqueAttack.Add(xy1);
                                                }
                                                else availableCellsSimpleAttack.Add(xy1);
                                            }

                                            else if (Instance.EGM.CellUnitEnt_UnitTypeCom(xy).UnitType == UnitTypes.Bishop || Instance.EGM.CellUnitEnt_UnitTypeCom(xy).UnitType == UnitTypes.BishopCrossbow)
                                            {
                                                if (directType1 == DirectTypes.Left || directType1 == DirectTypes.Right || directType1 == DirectTypes.Up || directType1 == DirectTypes.Down)
                                                {
                                                    availableCellsSimpleAttack.Add(xy1);
                                                }
                                                else availableCellsUniqueAttack.Add(xy1);
                                            }
                                        }
                                    }

                                    else if (Instance.EGM.CellUnitEnt_CellOwnerBotCom(xy1).HaveBot)
                                    {
                                        if (Instance.EGM.CellUnitEnt_UnitTypeCom(xy).UnitType == UnitTypes.Rook || Instance.EGM.CellUnitEnt_UnitTypeCom(xy).UnitType == UnitTypes.RookCrossbow)
                                        {
                                            if (directType1 == DirectTypes.Left || directType1 == DirectTypes.Right || directType1 == DirectTypes.Up || directType1 == DirectTypes.Down)
                                            {
                                                availableCellsUniqueAttack.Add(xy1);
                                            }
                                            else availableCellsSimpleAttack.Add(xy1);
                                        }

                                        else if (Instance.EGM.CellUnitEnt_UnitTypeCom(xy).UnitType == UnitTypes.Bishop || Instance.EGM.CellUnitEnt_UnitTypeCom(xy).UnitType == UnitTypes.BishopCrossbow)
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
                        }


                        var xy2 = GetXYCell(xy1, directType1);

                        if (Instance.EGM.CellUnitEnt_CellUnitCom(xy2).IsActivatedUnitDict[Instance.IsMasterClient])
                        {
                            if (Instance.EGM.CellUnitEnt_UnitTypeCom(xy).UnitType == UnitTypes.Rook || Instance.EGM.CellUnitEnt_UnitTypeCom(xy).UnitType == UnitTypes.RookCrossbow)
                            {
                                if (directType1 == DirectTypes.Left || directType1 == DirectTypes.Right || directType1 == DirectTypes.Down || directType1 == DirectTypes.Up)
                                {
                                    if (!Instance.EGM.CellEnvEnt_CellEnvCom(xy2).HaveMountain)
                                    {
                                        if (Instance.EGM.CellUnitEnt_UnitTypeCom(xy2).HaveUnit)
                                        {
                                            if (Instance.EGM.CellUnitEnt_CellOwnerCom(xy2).HaveOwner)
                                            {
                                                if (!Instance.EGM.CellUnitEnt_CellOwnerCom(xy2).IsHim(playerFrom))
                                                {
                                                    availableCellsUniqueAttack.Add(xy2);
                                                }
                                            }

                                            else if (Instance.EGM.CellUnitEnt_CellOwnerBotCom(xy2).HaveBot)
                                            {
                                                availableCellsUniqueAttack.Add(xy2);
                                            }
                                        }
                                    }
                                }

                                if (directType1 == DirectTypes.LeftDown || directType1 == DirectTypes.LeftUp || directType1 == DirectTypes.RightDown || directType1 == DirectTypes.RightUp)
                                {
                                    if (!Instance.EGM.CellEnvEnt_CellEnvCom(xy2).HaveMountain)
                                    {
                                        if (Instance.EGM.CellUnitEnt_UnitTypeCom(xy2).HaveUnit)
                                        {
                                            if (Instance.EGM.CellUnitEnt_CellOwnerCom(xy2).HaveOwner)
                                            {
                                                if (!Instance.EGM.CellUnitEnt_CellOwnerCom(xy2).IsHim(playerFrom))
                                                {
                                                    availableCellsSimpleAttack.Add(xy2);
                                                }
                                            }

                                            else if (Instance.EGM.CellUnitEnt_CellOwnerBotCom(xy2).HaveBot)
                                            {
                                                availableCellsSimpleAttack.Add(xy2);
                                            }
                                        }
                                    }
                                }
                            }


                            else if (Instance.EGM.CellUnitEnt_UnitTypeCom(xy).UnitType == UnitTypes.Bishop || Instance.EGM.CellUnitEnt_UnitTypeCom(xy).UnitType == UnitTypes.BishopCrossbow)
                            {
                                if (directType1 == DirectTypes.LeftDown || directType1 == DirectTypes.LeftUp || directType1 == DirectTypes.RightDown || directType1 == DirectTypes.RightUp)
                                {
                                    if (!Instance.EGM.CellEnvEnt_CellEnvCom(xy2).HaveMountain)
                                    {
                                        if (Instance.EGM.CellUnitEnt_UnitTypeCom(xy2).HaveUnit)
                                        {
                                            if (Instance.EGM.CellUnitEnt_CellOwnerCom(xy2).HaveOwner)
                                            {
                                                if (!Instance.EGM.CellUnitEnt_CellOwnerCom(xy2).IsHim(playerFrom))
                                                {
                                                    availableCellsUniqueAttack.Add(xy2);
                                                }
                                            }

                                            else if (Instance.EGM.CellUnitEnt_CellOwnerBotCom(xy2).HaveBot)
                                            {
                                                availableCellsUniqueAttack.Add(xy2);
                                            }
                                        }
                                    }
                                }

                                if (directType1 == DirectTypes.Left || directType1 == DirectTypes.Right || directType1 == DirectTypes.Down || directType1 == DirectTypes.Up)
                                {
                                    if (!Instance.EGM.CellEnvEnt_CellEnvCom(xy2).HaveMountain)
                                    {
                                        if (Instance.EGM.CellUnitEnt_UnitTypeCom(xy2).HaveUnit)
                                        {
                                            if (Instance.EGM.CellUnitEnt_CellOwnerCom(xy2).HaveOwner)
                                            {
                                                if (!Instance.EGM.CellUnitEnt_CellOwnerCom(xy2).IsHim(playerFrom))
                                                {
                                                    availableCellsSimpleAttack.Add(xy2);
                                                }
                                            }

                                            else if (Instance.EGM.CellUnitEnt_CellOwnerBotCom(xy2).HaveBot)
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
            }
        }
        internal static List<int[]> TryGetXYAround(params int[] xyStartCell)
        {
            var xyAvailableCells = new List<int[]>();
            var xyResultCell = new int[ValuesConst.XY_FOR_ARRAY];

            for (int i = 0; i < (int)DirectTypes.LeftDown + 1; i++)
            {
                var xyDirectCell = GetXYDirect((DirectTypes)i);

                xyResultCell[0] = xyStartCell[0] + xyDirectCell[0];
                xyResultCell[1] = xyStartCell[1] + xyDirectCell[1];

                if (Instance.EGM.CellEnt_CellBaseCom(xyResultCell).IsActiveSelfGO)
                {
                    xyAvailableCells.Add(CopyXY(xyResultCell));
                }
            }

            return xyAvailableCells;

        }
        private static int[] GetXYCell(int[] xyStartCell, DirectTypes directType)
        {
            var xyResultCell = new int[ValuesConst.XY_FOR_ARRAY];

            var xyDirectCell = GetXYDirect(directType);

            xyResultCell[0] = xyStartCell[0] + xyDirectCell[0];
            xyResultCell[1] = xyStartCell[1] + xyDirectCell[1];

            return xyResultCell;
        }
        private static int[] GetXYDirect(DirectTypes direct)
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