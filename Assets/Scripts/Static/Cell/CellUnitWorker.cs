using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Abstractions.ValuesConsts;
using Assets.Scripts.Static;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using static Assets.Scripts.Main;
using static Assets.Scripts.Static.CellBaseOperations;
using static Assets.Scripts.Abstractions.ValuesConsts.CellValues;
using static Assets.Scripts.Abstractions.ValuesConsts.UnitValues;
using UnityEngine;

namespace Assets.Scripts
{
    public static class CellUnitWorker
    {
        internal static EntitiesGameGeneralManager EGGM => Instance.EntGGM;
        internal static SpritesData SpritesData => Instance.ECSmanager.EntitiesCommonManager.ResourcesEnt_ResourcesCommonCom.SpritesConfig;

        private static void SetStandartValuesUnit(UnitTypes unitType, int amountHealth, int amountSteps, ProtectRelaxTypes protectRelaxType, params int[] xy)
        {
            EGGM.CellUnitEnt_UnitTypeCom(xy).SetUnitType(unitType);
            EGGM.CellUnitEnt_CellUnitCom(xy).SetAmountHealth(amountHealth);
            EGGM.CellUnitEnt_CellUnitCom(xy).SetAmountSteps(amountSteps);
            EGGM.CellUnitEnt_ProtectRelaxCom(xy).ProtectRelaxType = protectRelaxType;
        }
        private static void SetSprite(UnitTypes unitType, params int[] xy)
        {
            switch (unitType)
            {
                case UnitTypes.None:
                    throw new Exception();

                case UnitTypes.King:
                    EGGM.CellUnitEnt_SpriteRendererCom(xy).SetSprite(SpritesData.KingSprite);
                    break;

                case UnitTypes.Pawn:
                    EGGM.CellUnitEnt_SpriteRendererCom(xy).SetSprite(SpritesData.PawnSprite);
                    break;

                case UnitTypes.PawnSword:
                    EGGM.CellUnitEnt_SpriteRendererCom(xy).SetSprite(SpritesData.PawnSwordSprite);
                    break;

                case UnitTypes.Rook:
                    EGGM.CellUnitEnt_SpriteRendererCom(xy).SetSprite(SpritesData.RookSprite);
                    break;

                case UnitTypes.RookCrossbow:
                    EGGM.CellUnitEnt_SpriteRendererCom(xy).SetSprite(SpritesData.RookCrossbowSprite);
                    break;

                case UnitTypes.Bishop:
                    EGGM.CellUnitEnt_SpriteRendererCom(xy).SetSprite(SpritesData.BishopSprite);
                    break;

                case UnitTypes.BishopCrossbow:
                    EGGM.CellUnitEnt_SpriteRendererCom(xy).SetSprite(SpritesData.BishopCrossbowSprite);
                    break;

                default:
                    throw new Exception();
            }
        }



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
                    return STANDART_AMOUNT_HEALTH_KING;

                case UnitTypes.Pawn:
                    return STANDART_AMOUNT_HEALTH_PAWN;

                case UnitTypes.PawnSword:
                    return STANDART_AMOUNT_HEALTH_PAWN_SWORD;

                case UnitTypes.Rook:
                    return STANDART_AMOUNT_HEALTH_ROOK;

                case UnitTypes.RookCrossbow:
                    return STANDART_AMOUNT_HEALTH_ROOK_CROSSBOW;

                case UnitTypes.Bishop:
                    return STANDART_AMOUNT_HEALTH_BISHOP;

                case UnitTypes.BishopCrossbow:
                    return STANDART_AMOUNT_HEALTH_BISHOP_CROSSBOW;

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
                    return SIMPLE_POWER_DAMAGE_KING;

                case UnitTypes.Pawn:
                    return SIMPLE_POWER_DAMAGE_PAWN;

                case UnitTypes.PawnSword:
                    return SIMPLE_POWER_DAMAGE_PAWN_SWORD;

                case UnitTypes.Rook:
                    return SIMPLE_POWER_DAMAGE_ROOK;

                case UnitTypes.RookCrossbow:
                    return SIMPLE_POWER_DAMAGE_ROOK_CROSSBOW;

                case UnitTypes.Bishop:
                    return SIMPLE_POWER_DAMAGE_BISHOP;

                case UnitTypes.BishopCrossbow:
                    return SIMPLE_POWER_DAMAGE_BISHOP_CROSSBOW;

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
                    return (int)(SimplePowerDamage(unitType) * RATION_UNIQUE_POWER_DAMAGE_PAWN);

                case UnitTypes.PawnSword:
                    return (int)(SimplePowerDamage(unitType) * RATION_UNIQUE_POWER_DAMAGE_PAWN_SWORD);

                case UnitTypes.Rook:
                    return (int)(SimplePowerDamage(unitType) * RATION_UNIQUE_POWER_DAMAGE_ROOK);

                case UnitTypes.RookCrossbow:
                    return (int)(SimplePowerDamage(unitType) * RATION_UNIQUE_POWER_DAMAGE_ROOK_CROSSBOW);

                case UnitTypes.Bishop:
                    return (int)(SimplePowerDamage(unitType) * RATION_UNIQUE_POWER_DAMAGE_BISHOP);

                case UnitTypes.BishopCrossbow:
                    return (int)(SimplePowerDamage(unitType) * RATION_UNIQUE_POWER_DAMAGE_BISHOP_CROSSBOW);

                default:
                    return default;
            }

        }

        internal static int PowerProtection(params int[] xy)
        {
            var unitType = EGGM.CellUnitEnt_UnitTypeCom(xy).UnitType;

            int powerProtection = 0;

            if (EGGM.CellUnitEnt_ProtectRelaxCom(xy).IsType(ProtectRelaxTypes.Protected))
            {
                switch (unitType)
                {
                    case UnitTypes.King:
                        powerProtection += (int)(SimplePowerDamage(unitType) * PERCENT_FOR_PROTECTION_KING);
                        break;

                    case UnitTypes.Pawn:
                        powerProtection += (int)(SimplePowerDamage(unitType) * PERCENT_FOR_PROTECTION_PAWN);
                        break;

                    case UnitTypes.PawnSword:
                        powerProtection += (int)(SimplePowerDamage(unitType) * PERCENT_FOR_PROTECTION_PAWN_SWORD);
                        break;

                    case UnitTypes.Rook:
                        powerProtection += (int)(SimplePowerDamage(unitType) * PERCENT_FOR_PROTECTION_ROOK);
                        break;

                    case UnitTypes.RookCrossbow:
                        powerProtection += (int)(SimplePowerDamage(unitType) * PERCENT_FOR_PROTECTION_ROOK_CROSSBOW);
                        break;

                    case UnitTypes.Bishop:
                        powerProtection += (int)(SimplePowerDamage(unitType) * PERCENT_FOR_PROTECTION_BISHOP);
                        break;

                    case UnitTypes.BishopCrossbow:
                        powerProtection += (int)(SimplePowerDamage(unitType) * PERCENT_FOR_PROTECTION_BISHOP_CROSSBOW);
                        break;
                }
            }

            else if (EGGM.CellUnitEnt_ProtectRelaxCom(xy).IsType(ProtectRelaxTypes.Relaxed))
            {
                switch (unitType)
                {
                    case UnitTypes.King:
                        powerProtection -= (int)(SimplePowerDamage(unitType) * PERCENT_FOR_PROTECTION_KING);
                        break;

                    case UnitTypes.Pawn:
                        powerProtection -= (int)(SimplePowerDamage(unitType) * PERCENT_FOR_PROTECTION_PAWN);
                        break;

                    case UnitTypes.PawnSword:
                        powerProtection -= (int)(SimplePowerDamage(unitType) * PERCENT_FOR_PROTECTION_PAWN_SWORD);
                        break;

                    case UnitTypes.Rook:
                        powerProtection -= (int)(SimplePowerDamage(unitType) * PERCENT_FOR_PROTECTION_ROOK);
                        break;

                    case UnitTypes.RookCrossbow:
                        powerProtection -= (int)(SimplePowerDamage(unitType) * PERCENT_FOR_PROTECTION_ROOK_CROSSBOW);
                        break;

                    case UnitTypes.Bishop:
                        powerProtection -= (int)(SimplePowerDamage(unitType) * PERCENT_FOR_PROTECTION_BISHOP);
                        break;

                    case UnitTypes.BishopCrossbow:
                        powerProtection -= (int)(SimplePowerDamage(unitType) * PERCENT_FOR_PROTECTION_BISHOP_CROSSBOW);
                        break;
                }
            }


            switch (unitType)
            {
                case UnitTypes.King:
                    if (EGGM.CellEnvEnt_CellEnvCom(xy).HaveEnvironment(EnvironmentTypes.Fertilizer))
                        powerProtection -= PROTECTION_FOOD_FOR_KING;

                    if (EGGM.CellEnvEnt_CellEnvCom(xy).HaveEnvironment(EnvironmentTypes.AdultForest))
                        powerProtection += PROTECTION_TREE_FOR_KING;

                    if (EGGM.CellEnvEnt_CellEnvCom(xy).HaveEnvironment(EnvironmentTypes.Hill))
                        powerProtection += PROTECTION_HILL_FOR_KING;
                    break;

                case UnitTypes.Pawn:
                    if (EGGM.CellEnvEnt_CellEnvCom(xy).HaveEnvironment(EnvironmentTypes.Fertilizer))
                        powerProtection -= PROTECTION_FOOD_FOR_PAWN;

                    if (EGGM.CellEnvEnt_CellEnvCom(xy).HaveEnvironment(EnvironmentTypes.AdultForest))
                        powerProtection += PROTECTION_TREE_FOR_PAWN;

                    if (EGGM.CellEnvEnt_CellEnvCom(xy).HaveEnvironment(EnvironmentTypes.Hill))
                        powerProtection += PROTECTION_HILL_FOR_PAWN;
                    break;


                case UnitTypes.PawnSword:
                    if (EGGM.CellEnvEnt_CellEnvCom(xy).HaveEnvironment(EnvironmentTypes.Fertilizer))
                        powerProtection -= PROTECTION_FOOD_FOR_PAWN_SWORD;

                    if (EGGM.CellEnvEnt_CellEnvCom(xy).HaveEnvironment(EnvironmentTypes.AdultForest))
                        powerProtection += PROTECTION_TREE_FOR_PAWN_SWORD;

                    if (EGGM.CellEnvEnt_CellEnvCom(xy).HaveEnvironment(EnvironmentTypes.Hill))
                        powerProtection += PROTECTION_HILL_FOR_PAWN_SWORD;
                    break;


                case UnitTypes.Rook:
                    if (EGGM.CellEnvEnt_CellEnvCom(xy).HaveEnvironment(EnvironmentTypes.Fertilizer))
                        powerProtection -= PROTECTION_FOOD_FOR_ROOK;

                    if (EGGM.CellEnvEnt_CellEnvCom(xy).HaveEnvironment(EnvironmentTypes.AdultForest))
                        powerProtection += PROTECTION_TREE_FOR_ROOK;

                    if (EGGM.CellEnvEnt_CellEnvCom(xy).HaveEnvironment(EnvironmentTypes.Hill))
                        powerProtection += PROTECTION_HILL_FOR_ROOK;
                    break;


                case UnitTypes.RookCrossbow:
                    if (EGGM.CellEnvEnt_CellEnvCom(xy).HaveEnvironment(EnvironmentTypes.Fertilizer))
                        powerProtection -= PROTECTION_FOOD_FOR_ROOK_CROSSBOW;

                    if (EGGM.CellEnvEnt_CellEnvCom(xy).HaveEnvironment(EnvironmentTypes.AdultForest))
                        powerProtection += PROTECTION_TREE_FOR_ROOK_CROSSBOW;

                    if (EGGM.CellEnvEnt_CellEnvCom(xy).HaveEnvironment(EnvironmentTypes.Hill))
                        powerProtection += PROTECTION_HILL_FOR_ROOK_CROSSBOW;
                    break;


                case UnitTypes.Bishop:
                    if (EGGM.CellEnvEnt_CellEnvCom(xy).HaveEnvironment(EnvironmentTypes.Fertilizer))
                        powerProtection -= PROTECTION_FOOD_FOR_BISHOP;

                    if (EGGM.CellEnvEnt_CellEnvCom(xy).HaveEnvironment(EnvironmentTypes.AdultForest))
                        powerProtection += PROTECTION_TREE_FOR_BISHOP;

                    if (EGGM.CellEnvEnt_CellEnvCom(xy).HaveEnvironment(EnvironmentTypes.Hill))
                        powerProtection += PROTECTION_HILL_FOR_BISHOP;
                    break;


                case UnitTypes.BishopCrossbow:
                    if (EGGM.CellEnvEnt_CellEnvCom(xy).HaveEnvironment(EnvironmentTypes.Fertilizer))
                        powerProtection -= PROTECTION_FOOD_FOR_BISHOP_CROSSBOW;

                    if (EGGM.CellEnvEnt_CellEnvCom(xy).HaveEnvironment(EnvironmentTypes.AdultForest))
                        powerProtection += PROTECTION_TREE_FOR_BISHOP_CROSSBOW;

                    if (EGGM.CellEnvEnt_CellEnvCom(xy).HaveEnvironment(EnvironmentTypes.Hill))
                        powerProtection += PROTECTION_HILL_FOR_BISHOP_CROSSBOW;
                    break;
            }

            switch (EGGM.CellBuildEnt_BuilTypeCom(xy).BuildingType)
            {
                case BuildingTypes.City:

                    switch (unitType)
                    {
                        case UnitTypes.King:
                            powerProtection += PROTECTION_CITY_KING;
                            break;

                        case UnitTypes.Pawn:
                            powerProtection += PROTECTION_CITY_PAWN;
                            break;

                        case UnitTypes.PawnSword:
                            powerProtection += PROTECTION_CITY_PAWN_SWORD;
                            break;

                        case UnitTypes.Rook:
                            powerProtection += PROTECTION_CITY_ROOK;
                            break;

                        case UnitTypes.RookCrossbow:
                            powerProtection += PROTECTION_CITY_ROOK_CROSSBOW;
                            break;

                        case UnitTypes.Bishop:
                            powerProtection += PROTECTION_CITY_BISHOP;
                            break;

                        case UnitTypes.BishopCrossbow:
                            powerProtection += PROTECTION_CITY_BISHOP_CROSSBOW;
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


        #region Player

        internal static void ShiftPlayerUnit(int[] fromXy, int[] toXy)
        {
            var unitType = EGGM.CellUnitEnt_UnitTypeCom(fromXy).UnitType;
            var amountHealth = EGGM.CellUnitEnt_CellUnitCom(fromXy).AmountHealth;
            var amountSteps = EGGM.CellUnitEnt_CellUnitCom(fromXy).AmountSteps;
            var protectRelaxType = EGGM.CellUnitEnt_ProtectRelaxCom(fromXy).ProtectRelaxType;
            var player = EGGM.CellUnitEnt_CellOwnerCom(fromXy).Owner;

            SetStandartValuesUnit(unitType, amountHealth, amountSteps, protectRelaxType, toXy);
            EGGM.CellUnitEnt_CellOwnerCom(toXy).SetOwner(player);

            EGGM.CellMaxStepsEnt_SpriteRendererCom(toXy).ActivateSR(true);

            SetSprite(unitType, toXy);

            if (EGGM.CellUnitEnt_CellOwnerCom(toXy).IsMasterClient)
            {
                EGGM.CellMaxStepsEnt_SpriteRendererCom(toXy).SetColorSR(Color.blue);
            }
            else
            {
                EGGM.CellMaxStepsEnt_SpriteRendererCom(toXy).SetColorSR(Color.red);
            }




            EGGM.CellUnitEnt_SpriteRendererCom(fromXy).ActivateSR(false);

            SetStandartValuesUnit(default, default, default, default, fromXy);
            EGGM.CellUnitEnt_CellOwnerCom(fromXy).ResetOwner();

            EGGM.CellMaxStepsEnt_SpriteRendererCom(fromXy).ActivateSR(false);
        }

        internal static void SetNewPlayerUnit(UnitTypes unitType, int amountHealth, int amountSteps, ProtectRelaxTypes protectRelaxType, Player player, params int[] xy)
        {
            UnitInfoManager.AddAmountUnitInGame(unitType, player.IsMasterClient);

            SetStandartValuesUnit(unitType, amountHealth, amountSteps, protectRelaxType, xy);
            EGGM.CellUnitEnt_CellOwnerCom(xy).SetOwner(player);

            EGGM.CellMaxStepsEnt_SpriteRendererCom(xy).ActivateSR(true);
            if (player.IsMasterClient)
                EGGM.CellMaxStepsEnt_SpriteRendererCom(xy).SetColorSR(Color.blue);

            else EGGM.CellMaxStepsEnt_SpriteRendererCom(xy).SetColorSR(Color.red);

            SetSprite(unitType, xy);
        }

        internal static void SetNewPlayerUnit(UnitTypes unitType, Player player, int[] xy)
        {
            int amountHealth;
            int amountSteps;

            switch (unitType)
            {
                case UnitTypes.None:
                    throw new Exception();

                case UnitTypes.King:
                    amountHealth = STANDART_AMOUNT_HEALTH_KING;
                    amountSteps = STANDART_AMOUNT_STEPS_KING;
                    break;

                case UnitTypes.Pawn:
                    amountHealth = STANDART_AMOUNT_HEALTH_PAWN;
                    amountSteps = STANDART_AMOUNT_STEPS_PAWN;
                    break;

                case UnitTypes.PawnSword:
                    amountHealth = STANDART_AMOUNT_HEALTH_PAWN_SWORD;
                    amountSteps = STANDART_AMOUNT_STEPS_PAWN_SWORD;
                    break;

                case UnitTypes.Rook:
                    amountHealth = STANDART_AMOUNT_HEALTH_ROOK;
                    amountSteps = STANDART_AMOUNT_STEPS_ROOK;
                    break;

                case UnitTypes.RookCrossbow:
                    amountHealth = STANDART_AMOUNT_HEALTH_ROOK_CROSSBOW;
                    amountSteps = STANDART_AMOUNT_STEPS_ROOK_CROSSBOW;
                    break;

                case UnitTypes.Bishop:
                    amountHealth = STANDART_AMOUNT_HEALTH_BISHOP;
                    amountSteps = STANDART_AMOUNT_STEPS_BISHOP;
                    break;

                case UnitTypes.BishopCrossbow:
                    amountHealth = STANDART_AMOUNT_HEALTH_BISHOP_CROSSBOW;
                    amountSteps = STANDART_AMOUNT_STEPS_BISHOP_CROSSBOW;
                    break;

                default:
                    throw new Exception();
            }

            SetNewPlayerUnit(unitType, amountHealth, amountSteps, ProtectRelaxTypes.None, player, xy);
        }

        internal static void ResetPlayerUnit(params int[] xy)
        {
            var previousUnitType = EGGM.CellUnitEnt_UnitTypeCom(xy).UnitType;
            var previousIsMasterOwner = EGGM.CellUnitEnt_CellOwnerCom(xy).IsMasterClient;

            EGGM.CellUnitEnt_SpriteRendererCom(xy).ActivateSR(false);

            UnitInfoManager.TakeAmountUnitInGame(previousUnitType, previousIsMasterOwner);


            UnitTypes unitType = default;
            int amountHealth = default;
            int amountSteps = default;
            ProtectRelaxTypes protectRelaxType = default;
            Player player = default;

            SetStandartValuesUnit(unitType, amountHealth, amountSteps, protectRelaxType, xy);
            EGGM.CellUnitEnt_CellOwnerCom(xy).SetOwner(player);
        }

        internal static void SyncPlayerUnit(UnitTypes unitType, int amountHealth, int amountSteps, ProtectRelaxTypes protectRelaxType, Player player, params int[] xy)
        {
            if (EGGM.CellUnitEnt_UnitTypeCom(xy).HaveAnyUnit)
            {
                EGGM.CellUnitEnt_SpriteRendererCom(xy).ActivateSR(false);
            }

            SetStandartValuesUnit(unitType, amountHealth, amountSteps, protectRelaxType, xy);
            EGGM.CellUnitEnt_CellOwnerCom(xy).SetOwner(player);
        }

        internal static void ChangePlayerUnit(int[] xy, UnitTypes newUnitType)
        {
            EGGM.CellUnitEnt_SpriteRendererCom(xy).ActivateSR(false);

            EGGM.CellUnitEnt_UnitTypeCom(xy).SetUnitType(newUnitType);

            EGGM.CellUnitEnt_SpriteRendererCom(xy).ActivateSR(true);

            switch (EGGM.CellUnitEnt_UnitTypeCom(xy).UnitType)
            {
                case UnitTypes.None:
                    throw new Exception();

                case UnitTypes.King:
                    throw new Exception();

                case UnitTypes.Pawn:
                    break;

                case UnitTypes.PawnSword:
                    EGGM.CellUnitEnt_CellUnitCom(xy).AddAmountHealth(STANDART_AMOUNT_HEALTH_PAWN_SWORD - STANDART_AMOUNT_HEALTH_PAWN);
                    break;

                case UnitTypes.Rook:
                    break;

                case UnitTypes.RookCrossbow:
                    EGGM.CellUnitEnt_CellUnitCom(xy).AddAmountHealth(STANDART_AMOUNT_HEALTH_ROOK_CROSSBOW - STANDART_AMOUNT_HEALTH_ROOK);
                    break;

                case UnitTypes.Bishop:
                    break;

                case UnitTypes.BishopCrossbow:
                    EGGM.CellUnitEnt_CellUnitCom(xy).AddAmountHealth(STANDART_AMOUNT_HEALTH_BISHOP_CROSSBOW - STANDART_AMOUNT_HEALTH_BISHOP);
                    break;

                default:
                    throw new Exception();
            }
        }

        internal static void ActiveSelectorVisionUnit(bool isActive, UnitTypes unitType, params int[] xy)
        {
            if (isActive)
            {
                EGGM.CellUnitEnt_SpriteRendererCom(xy).ActivateSR(isActive);
                SetSprite(unitType, xy);
            }

            else
            {
                EGGM.CellUnitEnt_SpriteRendererCom(xy).ActivateSR(isActive);
            }
        }

        #endregion


        #region Bot

        internal static void SetBotUnit(UnitTypes unitType, bool haveBot, int amountHealth, int amountSteps, ProtectRelaxTypes protectRelaxType, params int[] xy)
        {
            SetStandartValuesUnit(unitType, amountHealth, amountSteps, protectRelaxType, xy);
            EGGM.CellUnitEnt_CellOwnerBotCom(xy).SetBot(haveBot);

            EGGM.CellUnitEnt_SpriteRendererCom(xy).ActivateSR(true);
        }

        internal static void ResetBotUnit(params int[] xy)
        {
            if (EGGM.CellUnitEnt_UnitTypeCom(xy).HaveAnyUnit)
                EGGM.CellUnitEnt_SpriteRendererCom(xy).ActivateSR(false);

            UnitTypes unitType = default;
            int amountHealth = default;
            int amountSteps = default;
            ProtectRelaxTypes protectRelaxType = default;
            bool haveBot = default;

            SetStandartValuesUnit(unitType, amountHealth, amountSteps, protectRelaxType, xy);
            EGGM.CellUnitEnt_CellOwnerBotCom(xy).SetBot(haveBot);
        }

        #endregion



        #region ForMoving

        internal static void GetCellsForShift(this List<int[]> list, params int[] xy)
        {
            if (list == default) throw new Exception();

            var listAvailable = TryGetXYAround(xy);

            foreach (var xy1 in listAvailable)
            {
                if (!EGGM.CellEnvEnt_CellEnvCom(xy1).HaveEnvironment(EnvironmentTypes.Mountain) && !EGGM.CellUnitEnt_UnitTypeCom(xy1).HaveAnyUnit)
                {
                    var unitType = EGGM.CellUnitEnt_UnitTypeCom(xy).UnitType;

                    if (EGGM.CellUnitEnt_CellUnitCom(xy).AmountSteps >= EGGM.CellEnvEnt_CellEnvCom(xy1).NeedAmountSteps() || EGGM.CellUnitEnt_CellUnitCom(xy).HaveMaxSteps(unitType))
                    {
                        list.Add(xy1);
                    }
                }
            }
        }
        internal static void GetCellsForAttack(Player playerFrom, out List<int[]> availableCellsSimpleAttack, out List<int[]> availableCellsUniqueAttack, int[] xy)
        {
            availableCellsSimpleAttack = new List<int[]>();
            availableCellsUniqueAttack = new List<int[]>();

            if (EGGM.CellUnitEnt_UnitTypeCom(xy).IsMelee)
            {
                for (DirectTypes directType1 = default; directType1 <= DirectTypes.LeftDown; directType1++)
                {
                    var xy1 = GetXYCell(xy, directType1);


                    if (!EGGM.CellEnvEnt_CellEnvCom(xy1).HaveEnvironment(EnvironmentTypes.Mountain))
                    {
                        var unitType = EGGM.CellUnitEnt_UnitTypeCom(xy).UnitType;

                        if (EGGM.CellEnvEnt_CellEnvCom(xy1).NeedAmountSteps() <= EGGM.CellUnitEnt_CellUnitCom(xy).AmountSteps || EGGM.CellUnitEnt_CellUnitCom(xy).HaveMaxSteps(unitType))
                        {
                            if (EGGM.CellUnitEnt_UnitTypeCom(xy1).HaveAnyUnit)
                            {
                                if (EGGM.CellUnitEnt_CellOwnerCom(xy1).HaveOwner)
                                {
                                    if (!EGGM.CellUnitEnt_CellOwnerCom(xy1).IsHim(playerFrom))
                                    {
                                        if (EGGM.CellUnitEnt_UnitTypeCom(xy).UnitType == UnitTypes.Pawn || EGGM.CellUnitEnt_UnitTypeCom(xy).UnitType == UnitTypes.PawnSword)
                                        {
                                            if (directType1 == DirectTypes.Left || directType1 == DirectTypes.Right || directType1 == DirectTypes.Up || directType1 == DirectTypes.Down)
                                            {
                                                availableCellsSimpleAttack.Add(xy1);
                                            }
                                            else availableCellsUniqueAttack.Add(xy1);
                                        }

                                        else if (EGGM.CellUnitEnt_UnitTypeCom(xy).UnitType == UnitTypes.King)
                                        {
                                            availableCellsSimpleAttack.Add(xy1);
                                        }
                                    }
                                }
                                else if (EGGM.CellUnitEnt_CellOwnerBotCom(xy1).HaveBot)
                                {
                                    if (EGGM.CellUnitEnt_UnitTypeCom(xy).UnitType == UnitTypes.Pawn || EGGM.CellUnitEnt_UnitTypeCom(xy).UnitType == UnitTypes.PawnSword)
                                    {
                                        if (directType1 == DirectTypes.Left || directType1 == DirectTypes.Right || directType1 == DirectTypes.Up || directType1 == DirectTypes.Down)
                                        {
                                            availableCellsSimpleAttack.Add(xy1);
                                        }
                                        else availableCellsUniqueAttack.Add(xy1);
                                    }

                                    else if (EGGM.CellUnitEnt_UnitTypeCom(xy).UnitType == UnitTypes.King)
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

                    if (EGGM.CellEnt_CellBaseCom(xy1).IsActiveSelfGO)
                    {
                        if (EGGM.CellUnitEnt_CellUnitCom(xy).HaveMinAmountSteps)
                        {
                            if (!EGGM.CellEnvEnt_CellEnvCom(xy1).HaveEnvironment(EnvironmentTypes.Mountain))
                            {
                                if (EGGM.CellUnitEnt_UnitTypeCom(xy1).HaveAnyUnit)
                                {
                                    if (EGGM.CellUnitEnt_CellOwnerCom(xy1).HaveOwner)
                                    {
                                        if (!EGGM.CellUnitEnt_CellOwnerCom(xy1).IsHim(playerFrom))
                                        {
                                            if (EGGM.CellUnitEnt_UnitTypeCom(xy).UnitType == UnitTypes.Rook || EGGM.CellUnitEnt_UnitTypeCom(xy).UnitType == UnitTypes.RookCrossbow)
                                            {
                                                if (directType1 == DirectTypes.Left || directType1 == DirectTypes.Right || directType1 == DirectTypes.Up || directType1 == DirectTypes.Down)
                                                {
                                                    availableCellsUniqueAttack.Add(xy1);
                                                }
                                                else availableCellsSimpleAttack.Add(xy1);
                                            }

                                            else if (EGGM.CellUnitEnt_UnitTypeCom(xy).UnitType == UnitTypes.Bishop || EGGM.CellUnitEnt_UnitTypeCom(xy).UnitType == UnitTypes.BishopCrossbow)
                                            {
                                                if (directType1 == DirectTypes.Left || directType1 == DirectTypes.Right || directType1 == DirectTypes.Up || directType1 == DirectTypes.Down)
                                                {
                                                    availableCellsSimpleAttack.Add(xy1);
                                                }
                                                else availableCellsUniqueAttack.Add(xy1);
                                            }
                                        }
                                    }

                                    else if (EGGM.CellUnitEnt_CellOwnerBotCom(xy1).HaveBot)
                                    {
                                        if (EGGM.CellUnitEnt_UnitTypeCom(xy).UnitType == UnitTypes.Rook || EGGM.CellUnitEnt_UnitTypeCom(xy).UnitType == UnitTypes.RookCrossbow)
                                        {
                                            if (directType1 == DirectTypes.Left || directType1 == DirectTypes.Right || directType1 == DirectTypes.Up || directType1 == DirectTypes.Down)
                                            {
                                                availableCellsUniqueAttack.Add(xy1);
                                            }
                                            else availableCellsSimpleAttack.Add(xy1);
                                        }

                                        else if (EGGM.CellUnitEnt_UnitTypeCom(xy).UnitType == UnitTypes.Bishop || EGGM.CellUnitEnt_UnitTypeCom(xy).UnitType == UnitTypes.BishopCrossbow)
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

                        if (EGGM.CellUnitEnt_ActivatedForPlayersCom(xy2).IsActivated(Instance.IsMasterClient)/*.IsActivatedUnitDict[Instance.IsMasterClient]*/)
                        {
                            if (EGGM.CellUnitEnt_UnitTypeCom(xy).UnitType == UnitTypes.Rook || EGGM.CellUnitEnt_UnitTypeCom(xy).UnitType == UnitTypes.RookCrossbow)
                            {
                                if (directType1 == DirectTypes.Left || directType1 == DirectTypes.Right || directType1 == DirectTypes.Down || directType1 == DirectTypes.Up)
                                {
                                    if (!EGGM.CellEnvEnt_CellEnvCom(xy2).HaveEnvironment(EnvironmentTypes.Mountain))
                                    {
                                        if (EGGM.CellUnitEnt_UnitTypeCom(xy2).HaveAnyUnit)
                                        {
                                            if (EGGM.CellUnitEnt_CellOwnerCom(xy2).HaveOwner)
                                            {
                                                if (!EGGM.CellUnitEnt_CellOwnerCom(xy2).IsHim(playerFrom))
                                                {
                                                    availableCellsUniqueAttack.Add(xy2);
                                                }
                                            }

                                            else if (EGGM.CellUnitEnt_CellOwnerBotCom(xy2).HaveBot)
                                            {
                                                availableCellsUniqueAttack.Add(xy2);
                                            }
                                        }
                                    }
                                }

                                if (directType1 == DirectTypes.LeftDown || directType1 == DirectTypes.LeftUp || directType1 == DirectTypes.RightDown || directType1 == DirectTypes.RightUp)
                                {
                                    if (!EGGM.CellEnvEnt_CellEnvCom(xy2).HaveEnvironment(EnvironmentTypes.Mountain))
                                    {
                                        if (EGGM.CellUnitEnt_UnitTypeCom(xy2).HaveAnyUnit)
                                        {
                                            if (EGGM.CellUnitEnt_CellOwnerCom(xy2).HaveOwner)
                                            {
                                                if (!EGGM.CellUnitEnt_CellOwnerCom(xy2).IsHim(playerFrom))
                                                {
                                                    availableCellsSimpleAttack.Add(xy2);
                                                }
                                            }

                                            else if (EGGM.CellUnitEnt_CellOwnerBotCom(xy2).HaveBot)
                                            {
                                                availableCellsSimpleAttack.Add(xy2);
                                            }
                                        }
                                    }
                                }
                            }


                            else if (EGGM.CellUnitEnt_UnitTypeCom(xy).UnitType == UnitTypes.Bishop || EGGM.CellUnitEnt_UnitTypeCom(xy).UnitType == UnitTypes.BishopCrossbow)
                            {
                                if (directType1 == DirectTypes.LeftDown || directType1 == DirectTypes.LeftUp || directType1 == DirectTypes.RightDown || directType1 == DirectTypes.RightUp)
                                {
                                    if (!EGGM.CellEnvEnt_CellEnvCom(xy2).HaveEnvironment(EnvironmentTypes.Mountain))
                                    {
                                        if (EGGM.CellUnitEnt_UnitTypeCom(xy2).HaveAnyUnit)
                                        {
                                            if (EGGM.CellUnitEnt_CellOwnerCom(xy2).HaveOwner)
                                            {
                                                if (!EGGM.CellUnitEnt_CellOwnerCom(xy2).IsHim(playerFrom))
                                                {
                                                    availableCellsUniqueAttack.Add(xy2);
                                                }
                                            }

                                            else if (EGGM.CellUnitEnt_CellOwnerBotCom(xy2).HaveBot)
                                            {
                                                availableCellsUniqueAttack.Add(xy2);
                                            }
                                        }
                                    }
                                }

                                if (directType1 == DirectTypes.Left || directType1 == DirectTypes.Right || directType1 == DirectTypes.Down || directType1 == DirectTypes.Up)
                                {
                                    if (!EGGM.CellEnvEnt_CellEnvCom(xy2).HaveEnvironment(EnvironmentTypes.Mountain))
                                    {
                                        if (EGGM.CellUnitEnt_UnitTypeCom(xy2).HaveAnyUnit)
                                        {
                                            if (EGGM.CellUnitEnt_CellOwnerCom(xy2).HaveOwner)
                                            {
                                                if (!EGGM.CellUnitEnt_CellOwnerCom(xy2).IsHim(playerFrom))
                                                {
                                                    availableCellsSimpleAttack.Add(xy2);
                                                }
                                            }

                                            else if (EGGM.CellUnitEnt_CellOwnerBotCom(xy2).HaveBot)
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
        internal static void GetStartCellsForSettingUnit(this List<int[]> list, Player player)
        {
            if (list == default) throw new Exception();

            for (int x = 0; x < EGGM.Xamount; x++)
                for (int y = 0; y < EGGM.Yamount; y++)
                {
                    if (!EGGM.CellEnvEnt_CellEnvCom(x, y).HaveEnvironment(EnvironmentTypes.Mountain))
                    {
                        if (!EGGM.CellUnitEnt_UnitTypeCom(x, y).HaveAnyUnit)
                        {
                            if (EGGM.CellEnt_CellBaseCom(x, y).IsStartedCell(player.IsMasterClient))
                            {
                                list.Add(new int[] { x, y });
                            }
                        }
                    }
                }
        }
        
        internal static List<int[]> TryGetXYAround(params int[] xyStartCell)
        {
            var xyAvailableCells = new List<int[]>();
            var xyResultCell = new int[XY_FOR_ARRAY];

            for (int i = 0; i < (int)DirectTypes.LeftDown + 1; i++)
            {
                var xyDirectCell = GetXYDirect((DirectTypes)i);

                xyResultCell[X] = xyStartCell[X] + xyDirectCell[X];
                xyResultCell[Y] = xyStartCell[Y] + xyDirectCell[Y];

                if (EGGM.CellEnt_CellBaseCom(xyResultCell).IsActiveSelfGO)
                {
                    xyAvailableCells.Add((int[])xyResultCell.Clone());
                }
            }

            return xyAvailableCells;

        }
        private static int[] GetXYCell(int[] xyStartCell, DirectTypes directType)
        {
            var xyResultCell = new int[XY_FOR_ARRAY];

            var xyDirectCell = GetXYDirect(directType);

            xyResultCell[0] = xyStartCell[0] + xyDirectCell[0];
            xyResultCell[1] = xyStartCell[1] + xyDirectCell[1];

            return xyResultCell;
        }
        private static int[] GetXYDirect(DirectTypes direct)
        {
            var xyDirectCell = new int[XY_FOR_ARRAY];

            switch (direct)
            {
                case DirectTypes.Right:
                    xyDirectCell[X] = 1;
                    xyDirectCell[Y] = 0;
                    break;

                case DirectTypes.Left:
                    xyDirectCell[X] = -1;
                    xyDirectCell[Y] = 0;
                    break;

                case DirectTypes.Up:
                    xyDirectCell[X] = 0;
                    xyDirectCell[Y] = 1;
                    break;

                case DirectTypes.Down:
                    xyDirectCell[X] = 0;
                    xyDirectCell[Y] = -1;
                    break;

                case DirectTypes.RightUp:
                    xyDirectCell[X] = 1;
                    xyDirectCell[Y] = 1;
                    break;

                case DirectTypes.LeftUp:
                    xyDirectCell[X] = -1;
                    xyDirectCell[Y] = 1;
                    break;

                case DirectTypes.RightDown:
                    xyDirectCell[X] = 1;
                    xyDirectCell[Y] = -1;
                    break;

                case DirectTypes.LeftDown:
                    xyDirectCell[X] = -1;
                    xyDirectCell[Y] = -1;
                    break;

                default:
                    break;
            }

            return xyDirectCell;
        }

        #endregion
    }
}