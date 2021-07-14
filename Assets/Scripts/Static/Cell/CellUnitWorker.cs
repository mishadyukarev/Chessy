using Assets.Scripts.Abstractions;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Static;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using static Assets.Scripts.Main;
using static Assets.Scripts.Static.CellBaseOperations;

namespace Assets.Scripts
{
    public static class CellUnitWorker
    {
        internal static EntitiesGameGeneralManager EGGM => Instance.EntGGM;


        internal static bool IsMelee(UnitTypes unitType)
        {
            switch (unitType)
            {
                case UnitTypes.None:
                    throw new Exception();

                case UnitTypes.King:
                    return true;

                case UnitTypes.Pawn:
                    return true;

                case UnitTypes.PawnSword:
                    return true;

                case UnitTypes.Rook:
                    return false;

                case UnitTypes.RookCrossbow:
                    return false;

                case UnitTypes.Bishop:
                    return false;

                case UnitTypes.BishopCrossbow:
                    return false;

                default:
                    throw new Exception();
            }
        }

        internal static int MaxAmountHealth(UnitTypes unitType)
        {
            switch (unitType)
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

        internal static int SimplePowerDamage(UnitTypes unitType)
        {
            switch (unitType)
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
        internal static int UniquePowerDamage(UnitTypes unitType)
        {
            switch (unitType)
            {
                case UnitTypes.None:
                    return default;

                case UnitTypes.King:
                    return SimplePowerDamage(unitType);

                case UnitTypes.Pawn:
                    return (int)(SimplePowerDamage(unitType) * Instance.StartValuesGameConfig.RATION_UNIQUE_POWER_DAMAGE_PAWN);

                case UnitTypes.PawnSword:
                    return (int)(SimplePowerDamage(unitType) * Instance.StartValuesGameConfig.RATION_UNIQUE_POWER_DAMAGE_PAWN_SWORD);

                case UnitTypes.Rook:
                    return (int)(SimplePowerDamage(unitType) * Instance.StartValuesGameConfig.RATION_UNIQUE_POWER_DAMAGE_ROOK);

                case UnitTypes.RookCrossbow:
                    return (int)(SimplePowerDamage(unitType) * Instance.StartValuesGameConfig.RATION_UNIQUE_POWER_DAMAGE_ROOK_CROSSBOW);

                case UnitTypes.Bishop:
                    return (int)(SimplePowerDamage(unitType) * Instance.StartValuesGameConfig.RATION_UNIQUE_POWER_DAMAGE_BISHOP);

                case UnitTypes.BishopCrossbow:
                    return (int)(SimplePowerDamage(unitType) * Instance.StartValuesGameConfig.RATION_UNIQUE_POWER_DAMAGE_BISHOP_CROSSBOW);

                default:
                    return default;
            }

        }
        internal static int PowerProtection(params int[] xy)
        {
            var unitType = Instance.EntGGM.CellUnitEnt_UnitTypeCom(xy).UnitType;

            int powerProtection = 0;

            if (Instance.EntGGM.CellUnitEnt_ProtectRelaxCom(xy).IsProtected)
            {
                switch (unitType)
                {
                    case UnitTypes.King:
                        powerProtection += (int)(SimplePowerDamage(unitType) * Instance.StartValuesGameConfig.PERCENT_FOR_PROTECTION_KING);
                        break;

                    case UnitTypes.Pawn:
                        powerProtection += (int)(SimplePowerDamage(unitType) * Instance.StartValuesGameConfig.PERCENT_FOR_PROTECTION_PAWN);
                        break;

                    case UnitTypes.PawnSword:
                        powerProtection += (int)(SimplePowerDamage(unitType) * Instance.StartValuesGameConfig.PERCENT_FOR_PROTECTION_PAWN_SWORD);
                        break;

                    case UnitTypes.Rook:
                        powerProtection += (int)(SimplePowerDamage(unitType) * Instance.StartValuesGameConfig.PERCENT_FOR_PROTECTION_ROOK);
                        break;

                    case UnitTypes.RookCrossbow:
                        powerProtection += (int)(SimplePowerDamage(unitType) * Instance.StartValuesGameConfig.PERCENT_FOR_PROTECTION_ROOK_CROSSBOW);
                        break;

                    case UnitTypes.Bishop:
                        powerProtection += (int)(SimplePowerDamage(unitType) * Instance.StartValuesGameConfig.PERCENT_FOR_PROTECTION_BISHOP);
                        break;

                    case UnitTypes.BishopCrossbow:
                        powerProtection += (int)(SimplePowerDamage(unitType) * Instance.StartValuesGameConfig.PERCENT_FOR_PROTECTION_BISHOP_CROSSBOW);
                        break;
                }
            }

            else if (Instance.EntGGM.CellUnitEnt_ProtectRelaxCom(xy).IsRelaxed)
            {
                switch (unitType)
                {
                    case UnitTypes.King:
                        powerProtection -= (int)(SimplePowerDamage(unitType) * Instance.StartValuesGameConfig.PERCENT_FOR_PROTECTION_KING);
                        break;

                    case UnitTypes.Pawn:
                        powerProtection -= (int)(SimplePowerDamage(unitType) * Instance.StartValuesGameConfig.PERCENT_FOR_PROTECTION_PAWN);
                        break;

                    case UnitTypes.PawnSword:
                        powerProtection -= (int)(SimplePowerDamage(unitType) * Instance.StartValuesGameConfig.PERCENT_FOR_PROTECTION_PAWN_SWORD);
                        break;

                    case UnitTypes.Rook:
                        powerProtection -= (int)(SimplePowerDamage(unitType) * Instance.StartValuesGameConfig.PERCENT_FOR_PROTECTION_ROOK);
                        break;

                    case UnitTypes.RookCrossbow:
                        powerProtection -= (int)(SimplePowerDamage(unitType) * Instance.StartValuesGameConfig.PERCENT_FOR_PROTECTION_ROOK_CROSSBOW);
                        break;

                    case UnitTypes.Bishop:
                        powerProtection -= (int)(SimplePowerDamage(unitType) * Instance.StartValuesGameConfig.PERCENT_FOR_PROTECTION_BISHOP);
                        break;

                    case UnitTypes.BishopCrossbow:
                        powerProtection -= (int)(SimplePowerDamage(unitType) * Instance.StartValuesGameConfig.PERCENT_FOR_PROTECTION_BISHOP_CROSSBOW);
                        break;
                }
            }


            switch (unitType)
            {
                case UnitTypes.King:
                    if (Instance.EntGGM.CellEnvEnt_CellEnvCom(xy).HaveEnvironment(EnvironmentTypes.Fertilizer))
                        powerProtection += Instance.StartValuesGameConfig.PROTECTION_FOOD_FOR_KING;

                    if (Instance.EntGGM.CellEnvEnt_CellEnvCom(xy).HaveEnvironment(EnvironmentTypes.AdultForest))
                        powerProtection += Instance.StartValuesGameConfig.PROTECTION_TREE_FOR_KING;

                    if (Instance.EntGGM.CellEnvEnt_CellEnvCom(xy).HaveEnvironment(EnvironmentTypes.Hill))
                        powerProtection += Instance.StartValuesGameConfig.PROTECTION_HILL_FOR_KING;
                    break;

                case UnitTypes.Pawn:
                    if (Instance.EntGGM.CellEnvEnt_CellEnvCom(xy).HaveEnvironment(EnvironmentTypes.Fertilizer))
                        powerProtection += Instance.StartValuesGameConfig.PROTECTION_FOOD_FOR_PAWN;

                    if (Instance.EntGGM.CellEnvEnt_CellEnvCom(xy).HaveEnvironment(EnvironmentTypes.AdultForest))
                        powerProtection += Instance.StartValuesGameConfig.PROTECTION_TREE_FOR_PAWN;

                    if (Instance.EntGGM.CellEnvEnt_CellEnvCom(xy).HaveEnvironment(EnvironmentTypes.Hill))
                        powerProtection += Instance.StartValuesGameConfig.PROTECTION_HILL_FOR_PAWN;
                    break;


                case UnitTypes.PawnSword:
                    if (Instance.EntGGM.CellEnvEnt_CellEnvCom(xy).HaveEnvironment(EnvironmentTypes.Fertilizer))
                        powerProtection += Instance.StartValuesGameConfig.PROTECTION_FOOD_FOR_PAWN_SWORD;

                    if (Instance.EntGGM.CellEnvEnt_CellEnvCom(xy).HaveEnvironment(EnvironmentTypes.AdultForest))
                        powerProtection += Instance.StartValuesGameConfig.PROTECTION_TREE_FOR_PAWN_SWORD;

                    if (Instance.EntGGM.CellEnvEnt_CellEnvCom(xy).HaveEnvironment(EnvironmentTypes.Hill))
                        powerProtection += Instance.StartValuesGameConfig.PROTECTION_HILL_FOR_PAWN_SWORD;
                    break;


                case UnitTypes.Rook:
                    if (Instance.EntGGM.CellEnvEnt_CellEnvCom(xy).HaveEnvironment(EnvironmentTypes.Fertilizer))
                        powerProtection += Instance.StartValuesGameConfig.PROTECTION_FOOD_FOR_ROOK;

                    if (Instance.EntGGM.CellEnvEnt_CellEnvCom(xy).HaveEnvironment(EnvironmentTypes.AdultForest))
                        powerProtection += Instance.StartValuesGameConfig.PROTECTION_TREE_FOR_ROOK;

                    if (Instance.EntGGM.CellEnvEnt_CellEnvCom(xy).HaveEnvironment(EnvironmentTypes.Hill))
                        powerProtection += Instance.StartValuesGameConfig.PROTECTION_HILL_FOR_ROOK;
                    break;


                case UnitTypes.RookCrossbow:
                    if (Instance.EntGGM.CellEnvEnt_CellEnvCom(xy).HaveEnvironment(EnvironmentTypes.Fertilizer))
                        powerProtection += Instance.StartValuesGameConfig.PROTECTION_FOOD_FOR_ROOK_CROSSBOW;

                    if (Instance.EntGGM.CellEnvEnt_CellEnvCom(xy).HaveEnvironment(EnvironmentTypes.AdultForest))
                        powerProtection += Instance.StartValuesGameConfig.PROTECTION_TREE_FOR_ROOK_CROSSBOW;

                    if (Instance.EntGGM.CellEnvEnt_CellEnvCom(xy).HaveEnvironment(EnvironmentTypes.Hill))
                        powerProtection += Instance.StartValuesGameConfig.PROTECTION_HILL_FOR_ROOK_CROSSBOW;
                    break;


                case UnitTypes.Bishop:
                    if (Instance.EntGGM.CellEnvEnt_CellEnvCom(xy).HaveEnvironment(EnvironmentTypes.Fertilizer))
                        powerProtection += Instance.StartValuesGameConfig.PROTECTION_FOOD_FOR_BISHOP;

                    if (Instance.EntGGM.CellEnvEnt_CellEnvCom(xy).HaveEnvironment(EnvironmentTypes.AdultForest))
                        powerProtection += Instance.StartValuesGameConfig.PROTECTION_TREE_FOR_BISHOP;

                    if (Instance.EntGGM.CellEnvEnt_CellEnvCom(xy).HaveEnvironment(EnvironmentTypes.Hill))
                        powerProtection += Instance.StartValuesGameConfig.PROTECTION_HILL_FOR_BISHOP;
                    break;


                case UnitTypes.BishopCrossbow:
                    if (Instance.EntGGM.CellEnvEnt_CellEnvCom(xy).HaveEnvironment(EnvironmentTypes.Fertilizer))
                        powerProtection += Instance.StartValuesGameConfig.PROTECTION_FOOD_FOR_ROOK_CROSSBOW;

                    if (Instance.EntGGM.CellEnvEnt_CellEnvCom(xy).HaveEnvironment(EnvironmentTypes.AdultForest))
                        powerProtection += Instance.StartValuesGameConfig.PROTECTION_TREE_FOR_ROOK_CROSSBOW;

                    if (Instance.EntGGM.CellEnvEnt_CellEnvCom(xy).HaveEnvironment(EnvironmentTypes.Hill))
                        powerProtection += Instance.StartValuesGameConfig.PROTECTION_HILL_FOR_ROOK_CROSSBOW;
                    break;
            }

            switch (Instance.EntGGM.CellBuildEnt_BuilTypeCom(xy).BuildingType)
            {
                case BuildingTypes.City:

                    switch (unitType)
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

        internal static void ShiftUnit(int[] xyFromUnitTo, int[] xyTo)
        {
            var unitType = Instance.EntGGM.CellUnitEnt_UnitTypeCom(xyFromUnitTo).UnitType;
            var amountHealth = Instance.EntGGM.CellUnitEnt_CellUnitCom(xyFromUnitTo).AmountHealth;
            var amountSteps = Instance.EntGGM.CellUnitEnt_CellUnitCom(xyFromUnitTo).AmountSteps;
            var protectRelaxType = Instance.EntGGM.CellUnitEnt_ProtectRelaxCom(xyFromUnitTo).ProtectRelaxType;
            var player = Instance.EntGGM.CellUnitEnt_CellOwnerCom(xyFromUnitTo).Owner;

            SetPlayerUnit(false, unitType, amountHealth, amountSteps, protectRelaxType, player, xyTo);
        }
        internal static void SetPlayerUnit(bool withEconomy, UnitTypes unitType, int amountHealth, int amountSteps, ProtectRelaxTypes protectRelaxType, Player player, params int[] xy)
        {
            if (withEconomy) UnitInfoManager.AddAmountUnitInGame(unitType, player.IsMasterClient);

            Instance.EntGGM.CellUnitEnt_UnitTypeCom(xy).SetUnitType(unitType);
            Instance.EntGGM.CellUnitEnt_CellUnitCom(xy).SetAmountSteps(amountSteps);
            Instance.EntGGM.CellUnitEnt_CellUnitCom(xy).SetAmountHealth(amountHealth);
            Instance.EntGGM.CellUnitEnt_ProtectRelaxCom(xy).SetProtectedRelaxedType(protectRelaxType);
            Instance.EntGGM.CellUnitEnt_CellOwnerCom(xy).SetOwner(player);



            Instance.EntGGM.CellUnitEnt_CellUnitCom(xy).DisableVisionAllSR();

            switch (Instance.EntGGM.CellUnitEnt_UnitTypeCom(xy).UnitType)
            {
                case UnitTypes.None:
                    break;

                case UnitTypes.King:
                    Instance.EntGGM.CellUnitEnt_CellUnitCom(xy).EnablePlayerSRAndSetColor(UnitTypes.King, Instance.EntGGM.CellUnitEnt_CellOwnerCom(xy).Owner);
                    break;

                case UnitTypes.Pawn:
                    Instance.EntGGM.CellUnitEnt_CellUnitCom(xy).EnablePlayerSRAndSetColor(UnitTypes.Pawn, Instance.EntGGM.CellUnitEnt_CellOwnerCom(xy).Owner);
                    break;

                case UnitTypes.PawnSword:
                    Instance.EntGGM.CellUnitEnt_CellUnitCom(xy).EnablePlayerSRAndSetColor(UnitTypes.PawnSword, Instance.EntGGM.CellUnitEnt_CellOwnerCom(xy).Owner);
                    break;

                case UnitTypes.Rook:
                    Instance.EntGGM.CellUnitEnt_CellUnitCom(xy).EnablePlayerSRAndSetColor(UnitTypes.Rook, Instance.EntGGM.CellUnitEnt_CellOwnerCom(xy).Owner);
                    break;

                case UnitTypes.RookCrossbow:
                    Instance.EntGGM.CellUnitEnt_CellUnitCom(xy).EnablePlayerSRAndSetColor(UnitTypes.RookCrossbow, Instance.EntGGM.CellUnitEnt_CellOwnerCom(xy).Owner);
                    break;

                case UnitTypes.Bishop:
                    Instance.EntGGM.CellUnitEnt_CellUnitCom(xy).EnablePlayerSRAndSetColor(UnitTypes.Bishop, Instance.EntGGM.CellUnitEnt_CellOwnerCom(xy).Owner);
                    break;

                case UnitTypes.BishopCrossbow:
                    Instance.EntGGM.CellUnitEnt_CellUnitCom(xy).EnablePlayerSRAndSetColor(UnitTypes.BishopCrossbow, Instance.EntGGM.CellUnitEnt_CellOwnerCom(xy).Owner);
                    break;
            }
        }

        internal static void ResetPlayerUnit(bool withEconomy, params int[] xy)
        {
            var previousUnitType = EGGM.CellUnitEnt_UnitTypeCom(xy).UnitType;
            var previousIsMasterOwner = EGGM.CellUnitEnt_CellOwnerCom(xy).IsMasterClient;

            if (withEconomy) UnitInfoManager.TakeAmountUnitInGame(previousUnitType, previousIsMasterOwner);



            UnitTypes unitType = default;
            int amountHealth = default;
            int amountSteps = default;
            ProtectRelaxTypes protectRelaxType = default;
            Player player = default;

            EGGM.CellUnitEnt_UnitTypeCom(xy).SetUnitType(unitType);
            EGGM.CellUnitEnt_CellUnitCom(xy).SetAmountHealth(amountHealth);
            EGGM.CellUnitEnt_CellUnitCom(xy).SetAmountSteps(amountSteps);
            EGGM.CellUnitEnt_ProtectRelaxCom(xy).SetProtectedRelaxedType(protectRelaxType);
            EGGM.CellUnitEnt_CellOwnerCom(xy).SetOwner(player);

            EGGM.CellUnitEnt_CellUnitCom(xy).DisableVisionAllSR();
        }

        internal static void SetBotUnit(UnitTypes unitType, bool haveBot, int amountHealth, int amountSteps, ProtectRelaxTypes protectRelaxType, params int[] xy)
        {
            Instance.EntGGM.CellUnitEnt_UnitTypeCom(xy).SetUnitType(unitType);
            Instance.EntGGM.CellUnitEnt_CellUnitCom(xy).SetAmountSteps(amountSteps);
            Instance.EntGGM.CellUnitEnt_CellUnitCom(xy).SetAmountHealth(amountHealth);
            Instance.EntGGM.CellUnitEnt_ProtectRelaxCom(xy).SetProtectedRelaxedType(protectRelaxType);
            Instance.EntGGM.CellUnitEnt_CellOwnerBotCom(xy).SetBot(haveBot);


            EGGM.CellUnitEnt_CellUnitCom(xy).DisableVisionAllSR();

            switch (Instance.EntGGM.CellUnitEnt_UnitTypeCom(xy).UnitType)
            {
                case UnitTypes.None:
                    break;

                case UnitTypes.King:
                    Instance.EntGGM.CellUnitEnt_CellUnitCom(xy).EnableBotSRAndSetColor(UnitTypes.King);
                    break;

                case UnitTypes.Pawn:
                    Instance.EntGGM.CellUnitEnt_CellUnitCom(xy).EnableBotSRAndSetColor(UnitTypes.Pawn);
                    break;

                case UnitTypes.PawnSword:
                    Instance.EntGGM.CellUnitEnt_CellUnitCom(xy).EnableBotSRAndSetColor(UnitTypes.PawnSword);
                    break;

                case UnitTypes.Rook:
                    Instance.EntGGM.CellUnitEnt_CellUnitCom(xy).EnableBotSRAndSetColor(UnitTypes.Rook);
                    break;

                case UnitTypes.RookCrossbow:
                    Instance.EntGGM.CellUnitEnt_CellUnitCom(xy).EnableBotSRAndSetColor(UnitTypes.RookCrossbow);
                    break;

                case UnitTypes.Bishop:
                    Instance.EntGGM.CellUnitEnt_CellUnitCom(xy).EnableBotSRAndSetColor(UnitTypes.Bishop);
                    break;

                case UnitTypes.BishopCrossbow:
                    Instance.EntGGM.CellUnitEnt_CellUnitCom(xy).EnableBotSRAndSetColor(UnitTypes.BishopCrossbow);
                    break;
            }
        }

        internal static void ResetBotUnit(params int[] xy)
        {
            UnitTypes unitType = default;
            int amountHealth = default;
            int amountSteps = default;
            ProtectRelaxTypes protectRelaxType = default;
            bool haveBot = default;

            SetBotUnit(unitType, haveBot, amountHealth, amountSteps, protectRelaxType, xy);
        }

        internal static void ChangeUnit(int[] xy, UnitTypes newUnitType)
        {
            Instance.EntGGM.CellUnitEnt_UnitTypeCom(xy).SetUnitType(newUnitType);

            Instance.EntGGM.CellUnitEnt_CellUnitCom(xy).DisableVisionAllSR();

            switch (Instance.EntGGM.CellUnitEnt_UnitTypeCom(xy).UnitType)
            {
                case UnitTypes.None:
                    throw new System.Exception();

                case UnitTypes.King:
                    throw new System.Exception();

                case UnitTypes.Pawn:
                    Instance.EntGGM.CellUnitEnt_CellUnitCom(xy).EnablePlayerSRAndSetColor(UnitTypes.Pawn, Instance.EntGGM.CellUnitEnt_CellOwnerCom(xy).Owner);
                    break;

                case UnitTypes.PawnSword:
                    Instance.EntGGM.CellUnitEnt_CellUnitCom(xy).EnablePlayerSRAndSetColor(UnitTypes.PawnSword, Instance.EntGGM.CellUnitEnt_CellOwnerCom(xy).Owner);
                    Instance.EntGGM.CellUnitEnt_CellUnitCom(xy).AddAmountHealth(Instance.StartValuesGameConfig.AMOUNT_HEALTH_PAWN_SWORD - Instance.StartValuesGameConfig.AMOUNT_HEALTH_PAWN);
                    break;

                case UnitTypes.Rook:
                    Instance.EntGGM.CellUnitEnt_CellUnitCom(xy).EnablePlayerSRAndSetColor(UnitTypes.Rook, Instance.EntGGM.CellUnitEnt_CellOwnerCom(xy).Owner);
                    break;

                case UnitTypes.RookCrossbow:
                    Instance.EntGGM.CellUnitEnt_CellUnitCom(xy).EnablePlayerSRAndSetColor(UnitTypes.RookCrossbow, Instance.EntGGM.CellUnitEnt_CellOwnerCom(xy).Owner);
                    Instance.EntGGM.CellUnitEnt_CellUnitCom(xy).AddAmountHealth(Instance.StartValuesGameConfig.AMOUNT_HEALTH_ROOK_CROSSBOW - Instance.StartValuesGameConfig.AMOUNT_HEALTH_ROOK);
                    break;

                case UnitTypes.Bishop:
                    Instance.EntGGM.CellUnitEnt_CellUnitCom(xy).EnablePlayerSRAndSetColor(UnitTypes.Bishop, Instance.EntGGM.CellUnitEnt_CellOwnerCom(xy).Owner);
                    break;

                case UnitTypes.BishopCrossbow:
                    Instance.EntGGM.CellUnitEnt_CellUnitCom(xy).EnablePlayerSRAndSetColor(UnitTypes.BishopCrossbow, Instance.EntGGM.CellUnitEnt_CellOwnerCom(xy).Owner);
                    Instance.EntGGM.CellUnitEnt_CellUnitCom(xy).AddAmountHealth(Instance.StartValuesGameConfig.AMOUNT_HEALTH_BISHOP_CROSSBOW - Instance.StartValuesGameConfig.AMOUNT_HEALTH_BISHOP);
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
                if (!Instance.EntGGM.CellEnvEnt_CellEnvCom(xy1).HaveEnvironment(EnvironmentTypes.Mountain) && !Instance.EntGGM.CellUnitEnt_UnitTypeCom(xy1).HaveUnit)
                {
                    var unitType = Instance.EntGGM.CellUnitEnt_UnitTypeCom(xy).UnitType;

                    if (Instance.EntGGM.CellUnitEnt_CellUnitCom(xy).AmountSteps >= EGGM.CellEnvEnt_CellEnvCom(xy1).NeedAmountSteps() || Instance.EntGGM.CellUnitEnt_CellUnitCom(xy).HaveMaxSteps(unitType))
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

            if (Instance.EntGGM.CellUnitEnt_UnitTypeCom(xy).IsMelee)
            {
                for (DirectTypes directType1 = default; directType1 <= DirectTypes.LeftDown; directType1++)
                {
                    var xy1 = GetXYCell(xy, directType1);


                    if (!Instance.EntGGM.CellEnvEnt_CellEnvCom(xy1).HaveEnvironment(EnvironmentTypes.Mountain))
                    {
                        var unitType = Instance.EntGGM.CellUnitEnt_UnitTypeCom(xy).UnitType;

                        if (EGGM.CellEnvEnt_CellEnvCom(xy1).NeedAmountSteps() <= Instance.EntGGM.CellUnitEnt_CellUnitCom(xy).AmountSteps || Instance.EntGGM.CellUnitEnt_CellUnitCom(xy).HaveMaxSteps(unitType))
                        {
                            if (Instance.EntGGM.CellUnitEnt_UnitTypeCom(xy1).HaveUnit)
                            {
                                if (Instance.EntGGM.CellUnitEnt_CellOwnerCom(xy1).HaveOwner)
                                {
                                    if (!Instance.EntGGM.CellUnitEnt_CellOwnerCom(xy1).IsHim(playerFrom))
                                    {
                                        if (Instance.EntGGM.CellUnitEnt_UnitTypeCom(xy).UnitType == UnitTypes.Pawn || Instance.EntGGM.CellUnitEnt_UnitTypeCom(xy).UnitType == UnitTypes.PawnSword)
                                        {
                                            if (directType1 == DirectTypes.Left || directType1 == DirectTypes.Right || directType1 == DirectTypes.Up || directType1 == DirectTypes.Down)
                                            {
                                                availableCellsSimpleAttack.Add(xy1);
                                            }
                                            else availableCellsUniqueAttack.Add(xy1);
                                        }

                                        else if (Instance.EntGGM.CellUnitEnt_UnitTypeCom(xy).UnitType == UnitTypes.King)
                                        {
                                            availableCellsSimpleAttack.Add(xy1);
                                        }
                                    }
                                }
                                else if (Instance.EntGGM.CellUnitEnt_CellOwnerBotCom(xy1).HaveBot)
                                {
                                    if (Instance.EntGGM.CellUnitEnt_UnitTypeCom(xy).UnitType == UnitTypes.Pawn || Instance.EntGGM.CellUnitEnt_UnitTypeCom(xy).UnitType == UnitTypes.PawnSword)
                                    {
                                        if (directType1 == DirectTypes.Left || directType1 == DirectTypes.Right || directType1 == DirectTypes.Up || directType1 == DirectTypes.Down)
                                        {
                                            availableCellsSimpleAttack.Add(xy1);
                                        }
                                        else availableCellsUniqueAttack.Add(xy1);
                                    }

                                    else if (Instance.EntGGM.CellUnitEnt_UnitTypeCom(xy).UnitType == UnitTypes.King)
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

                    if (Instance.EntGGM.CellEnt_CellBaseCom(xy1).IsActiveSelfGO)
                    {
                        if (Instance.EntGGM.CellUnitEnt_CellUnitCom(xy).HaveMinAmountSteps)
                        {
                            if (!Instance.EntGGM.CellEnvEnt_CellEnvCom(xy1).HaveEnvironment(EnvironmentTypes.Mountain))
                            {
                                if (Instance.EntGGM.CellUnitEnt_UnitTypeCom(xy1).HaveUnit)
                                {
                                    if (Instance.EntGGM.CellUnitEnt_CellOwnerCom(xy1).HaveOwner)
                                    {
                                        if (!Instance.EntGGM.CellUnitEnt_CellOwnerCom(xy1).IsHim(playerFrom))
                                        {
                                            if (Instance.EntGGM.CellUnitEnt_UnitTypeCom(xy).UnitType == UnitTypes.Rook || Instance.EntGGM.CellUnitEnt_UnitTypeCom(xy).UnitType == UnitTypes.RookCrossbow)
                                            {
                                                if (directType1 == DirectTypes.Left || directType1 == DirectTypes.Right || directType1 == DirectTypes.Up || directType1 == DirectTypes.Down)
                                                {
                                                    availableCellsUniqueAttack.Add(xy1);
                                                }
                                                else availableCellsSimpleAttack.Add(xy1);
                                            }

                                            else if (Instance.EntGGM.CellUnitEnt_UnitTypeCom(xy).UnitType == UnitTypes.Bishop || Instance.EntGGM.CellUnitEnt_UnitTypeCom(xy).UnitType == UnitTypes.BishopCrossbow)
                                            {
                                                if (directType1 == DirectTypes.Left || directType1 == DirectTypes.Right || directType1 == DirectTypes.Up || directType1 == DirectTypes.Down)
                                                {
                                                    availableCellsSimpleAttack.Add(xy1);
                                                }
                                                else availableCellsUniqueAttack.Add(xy1);
                                            }
                                        }
                                    }

                                    else if (Instance.EntGGM.CellUnitEnt_CellOwnerBotCom(xy1).HaveBot)
                                    {
                                        if (Instance.EntGGM.CellUnitEnt_UnitTypeCom(xy).UnitType == UnitTypes.Rook || Instance.EntGGM.CellUnitEnt_UnitTypeCom(xy).UnitType == UnitTypes.RookCrossbow)
                                        {
                                            if (directType1 == DirectTypes.Left || directType1 == DirectTypes.Right || directType1 == DirectTypes.Up || directType1 == DirectTypes.Down)
                                            {
                                                availableCellsUniqueAttack.Add(xy1);
                                            }
                                            else availableCellsSimpleAttack.Add(xy1);
                                        }

                                        else if (Instance.EntGGM.CellUnitEnt_UnitTypeCom(xy).UnitType == UnitTypes.Bishop || Instance.EntGGM.CellUnitEnt_UnitTypeCom(xy).UnitType == UnitTypes.BishopCrossbow)
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

                        if (Instance.EntGGM.CellUnitEnt_ActivatedForPlayersCom(xy2).IsActivated(Instance.IsMasterClient)/*.IsActivatedUnitDict[Instance.IsMasterClient]*/)
                        {
                            if (Instance.EntGGM.CellUnitEnt_UnitTypeCom(xy).UnitType == UnitTypes.Rook || Instance.EntGGM.CellUnitEnt_UnitTypeCom(xy).UnitType == UnitTypes.RookCrossbow)
                            {
                                if (directType1 == DirectTypes.Left || directType1 == DirectTypes.Right || directType1 == DirectTypes.Down || directType1 == DirectTypes.Up)
                                {
                                    if (!Instance.EntGGM.CellEnvEnt_CellEnvCom(xy2).HaveEnvironment(EnvironmentTypes.Mountain))
                                    {
                                        if (Instance.EntGGM.CellUnitEnt_UnitTypeCom(xy2).HaveUnit)
                                        {
                                            if (Instance.EntGGM.CellUnitEnt_CellOwnerCom(xy2).HaveOwner)
                                            {
                                                if (!Instance.EntGGM.CellUnitEnt_CellOwnerCom(xy2).IsHim(playerFrom))
                                                {
                                                    availableCellsUniqueAttack.Add(xy2);
                                                }
                                            }

                                            else if (Instance.EntGGM.CellUnitEnt_CellOwnerBotCom(xy2).HaveBot)
                                            {
                                                availableCellsUniqueAttack.Add(xy2);
                                            }
                                        }
                                    }
                                }

                                if (directType1 == DirectTypes.LeftDown || directType1 == DirectTypes.LeftUp || directType1 == DirectTypes.RightDown || directType1 == DirectTypes.RightUp)
                                {
                                    if (!Instance.EntGGM.CellEnvEnt_CellEnvCom(xy2).HaveEnvironment(EnvironmentTypes.Mountain))
                                    {
                                        if (Instance.EntGGM.CellUnitEnt_UnitTypeCom(xy2).HaveUnit)
                                        {
                                            if (Instance.EntGGM.CellUnitEnt_CellOwnerCom(xy2).HaveOwner)
                                            {
                                                if (!Instance.EntGGM.CellUnitEnt_CellOwnerCom(xy2).IsHim(playerFrom))
                                                {
                                                    availableCellsSimpleAttack.Add(xy2);
                                                }
                                            }

                                            else if (Instance.EntGGM.CellUnitEnt_CellOwnerBotCom(xy2).HaveBot)
                                            {
                                                availableCellsSimpleAttack.Add(xy2);
                                            }
                                        }
                                    }
                                }
                            }


                            else if (Instance.EntGGM.CellUnitEnt_UnitTypeCom(xy).UnitType == UnitTypes.Bishop || Instance.EntGGM.CellUnitEnt_UnitTypeCom(xy).UnitType == UnitTypes.BishopCrossbow)
                            {
                                if (directType1 == DirectTypes.LeftDown || directType1 == DirectTypes.LeftUp || directType1 == DirectTypes.RightDown || directType1 == DirectTypes.RightUp)
                                {
                                    if (!Instance.EntGGM.CellEnvEnt_CellEnvCom(xy2).HaveEnvironment(EnvironmentTypes.Mountain))
                                    {
                                        if (Instance.EntGGM.CellUnitEnt_UnitTypeCom(xy2).HaveUnit)
                                        {
                                            if (Instance.EntGGM.CellUnitEnt_CellOwnerCom(xy2).HaveOwner)
                                            {
                                                if (!Instance.EntGGM.CellUnitEnt_CellOwnerCom(xy2).IsHim(playerFrom))
                                                {
                                                    availableCellsUniqueAttack.Add(xy2);
                                                }
                                            }

                                            else if (Instance.EntGGM.CellUnitEnt_CellOwnerBotCom(xy2).HaveBot)
                                            {
                                                availableCellsUniqueAttack.Add(xy2);
                                            }
                                        }
                                    }
                                }

                                if (directType1 == DirectTypes.Left || directType1 == DirectTypes.Right || directType1 == DirectTypes.Down || directType1 == DirectTypes.Up)
                                {
                                    if (!Instance.EntGGM.CellEnvEnt_CellEnvCom(xy2).HaveEnvironment(EnvironmentTypes.Mountain))
                                    {
                                        if (Instance.EntGGM.CellUnitEnt_UnitTypeCom(xy2).HaveUnit)
                                        {
                                            if (Instance.EntGGM.CellUnitEnt_CellOwnerCom(xy2).HaveOwner)
                                            {
                                                if (!Instance.EntGGM.CellUnitEnt_CellOwnerCom(xy2).IsHim(playerFrom))
                                                {
                                                    availableCellsSimpleAttack.Add(xy2);
                                                }
                                            }

                                            else if (Instance.EntGGM.CellUnitEnt_CellOwnerBotCom(xy2).HaveBot)
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

                if (Instance.EntGGM.CellEnt_CellBaseCom(xyResultCell).IsActiveSelfGO)
                {
                    xyAvailableCells.Add(CopyXy(xyResultCell));
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