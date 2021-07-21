using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Static;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using UnityEngine;
using static Assets.Scripts.Abstractions.ValuesConsts.CellValues;
using static Assets.Scripts.Abstractions.ValuesConsts.UnitValues;
using static Assets.Scripts.CellEnvironmentWorker;
using static Assets.Scripts.Main;
using static Assets.Scripts.Static.Cell.CellSpaceWorker;

namespace Assets.Scripts
{
    public static class CellUnitWorker
    {
        internal static EntitiesGameGeneralManager EGGM => Instance.EntGGM;
        internal static SpritesData SpritesData => Instance.ECSmanager.EntitiesCommonManager.ResourcesEnt_ResourcesCommonCom.SpritesConfig;

        private static void SetStandartValuesUnit(UnitTypes unitType, int amountHealth, int amountSteps, ProtectRelaxTypes protectRelaxType, params int[] xy)
        {
            EGGM.CellUnitEnt_UnitTypeCom(xy).UnitType = unitType;
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
                    EGGM.CellUnitEnt_SpriteRendererCom(xy).Sprite = SpritesData.KingSprite;
                    break;

                case UnitTypes.Pawn:
                    EGGM.CellUnitEnt_SpriteRendererCom(xy).Sprite = SpritesData.PawnSprite;
                    break;

                case UnitTypes.PawnSword:
                    EGGM.CellUnitEnt_SpriteRendererCom(xy).Sprite = SpritesData.PawnSwordSprite;
                    break;

                case UnitTypes.Rook:
                    EGGM.CellUnitEnt_SpriteRendererCom(xy).Sprite = SpritesData.RookSprite;
                    break;

                case UnitTypes.RookCrossbow:
                    EGGM.CellUnitEnt_SpriteRendererCom(xy).Sprite = SpritesData.RookCrossbowSprite;
                    break;

                case UnitTypes.Bishop:
                    EGGM.CellUnitEnt_SpriteRendererCom(xy).Sprite = SpritesData.BishopSprite;
                    break;

                case UnitTypes.BishopCrossbow:
                    EGGM.CellUnitEnt_SpriteRendererCom(xy).Sprite = SpritesData.BishopCrossbowSprite;
                    break;

                default:
                    throw new Exception();
            }
        }

        internal static void SetIsActivated(bool key, bool value, int[] xy) => EGGM.CellUnitEnt_ActivatedForPlayersCom(xy).SetActivated(key, value);
        internal static bool IsActivated(bool key, int[] xy) => EGGM.CellUnitEnt_ActivatedForPlayersCom(xy).IsActivated(key);


        #region UnitType

        internal static bool HaveAnyUnit(int[] xy) => EGGM.CellUnitEnt_UnitTypeCom(xy).HaveAnyUnit;
        internal static UnitTypes UnitType(int[] xy) => EGGM.CellUnitEnt_UnitTypeCom(xy).UnitType;
        internal static bool IsUnitType(UnitTypes unitType, int[] xy) => EGGM.CellUnitEnt_UnitTypeCom(xy).IsUnit(unitType);

        #endregion


        #region Health

        internal static int AmountHealth(int[] xy) => EGGM.CellUnitEnt_CellUnitCom(xy).AmountHealth;
        internal static void SetAmountHealth(int value, int[] xy) => EGGM.CellUnitEnt_CellUnitCom(xy).SetAmountHealth(value);
        internal static bool HaveAmountHealth(int[] xy) => EGGM.CellUnitEnt_CellUnitCom(xy).HaveAmountHealth;
        internal static void AddAmountHealth(int[] xy, int adding = 1) => EGGM.CellUnitEnt_CellUnitCom(xy).AddAmountHealth(adding);
        internal static void TakeAmountHealth(int[] xy, int taking = 1) => EGGM.CellUnitEnt_CellUnitCom(xy).TakeAmountHealth(taking);

        #endregion


        internal static int AmountSteps(int[] xy) => EGGM.CellUnitEnt_CellUnitCom(xy).AmountSteps;
        internal static void RefreshAmountSteps(UnitTypes unitType, int[] xy) => EGGM.CellUnitEnt_CellUnitCom(xy).RefreshAmountSteps(unitType);

        internal static void SetEnabled(bool isEnabled, int[] xy) => EGGM.CellUnitEnt_SpriteRendererCom(xy).Enabled = isEnabled;

        internal static ProtectRelaxTypes ProtectRelaxType(int[] xy) => EGGM.CellUnitEnt_ProtectRelaxCom(xy).ProtectRelaxType;
        internal static void SetProtectRelaxType(ProtectRelaxTypes protectRelaxType, int[] xy) => EGGM.CellUnitEnt_ProtectRelaxCom(xy).ProtectRelaxType = protectRelaxType;
        internal static void ResetAmountStepsInProtectRelax(ProtectRelaxTypes protectRelaxType, int[] xy) => EGGM.CellUnitEnt_CellUnitCom(xy).ResetAmountStepsInProtectRelax(protectRelaxType);
        internal static int AmountStepsInProtectRelax(ProtectRelaxTypes protectRelaxType, int[] xy) => EGGM.CellUnitEnt_CellUnitCom(xy).AmountStepsInProtectRelax(protectRelaxType);
        internal static void AddAmountStepsInProtectRelax(ProtectRelaxTypes protectRelaxType, int[] xy, int adding = 1) => EGGM.CellUnitEnt_CellUnitCom(xy).AddAmountStepsInProtectRelax(protectRelaxType, adding);

        internal static bool IsTypeProtectRelax(ProtectRelaxTypes protectRelaxType, int[] xy) => EGGM.CellUnitEnt_ProtectRelaxCom(xy).IsType(protectRelaxType);
        internal static bool HaveMaxAmountSteps(UnitTypes unitType, int[] xy) => EGGM.CellUnitEnt_CellUnitCom(xy).HaveMaxSteps(unitType);


        #region Owner

        internal static Player Owner(int[] xy) => EGGM.CellUnitEnt_CellOwnerCom(xy).Owner;
        internal static bool HaveOwner(int[] xy) => EGGM.CellUnitEnt_CellOwnerCom(xy).HaveOwner;
        internal static int ActorNumber(int[] xy) => EGGM.CellUnitEnt_CellOwnerCom(xy).ActorNumber;
        internal static bool IsMine(int[] xy) => EGGM.CellUnitEnt_CellOwnerCom(xy).IsMine;
        internal static bool IsMasterClient(int[] xy) => EGGM.CellUnitEnt_CellOwnerCom(xy).IsMasterClient;
        internal static bool IsHim(Player player, int[] xy) => EGGM.CellUnitEnt_CellOwnerCom(xy).IsHim(player);

        internal static bool HaveBot(int[] xy) => EGGM.CellUnitEnt_CellOwnerBotCom(xy).HaveBot;

        #endregion


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

        internal static int PowerProtection(int[] xy)
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
                    if (HaveEnvironment(EnvironmentTypes.Fertilizer, xy))
                        powerProtection -= PROTECTION_FOOD_FOR_KING;

                    if (HaveEnvironment(EnvironmentTypes.AdultForest, xy))
                        powerProtection += PROTECTION_TREE_FOR_KING;

                    if (HaveEnvironment(EnvironmentTypes.Hill, xy))
                        powerProtection += PROTECTION_HILL_FOR_KING;
                    break;

                case UnitTypes.Pawn:
                    if (HaveEnvironment(EnvironmentTypes.Fertilizer, xy))
                        powerProtection -= PROTECTION_FOOD_FOR_PAWN;

                    if (HaveEnvironment(EnvironmentTypes.AdultForest, xy))
                        powerProtection += PROTECTION_TREE_FOR_PAWN;

                    if (HaveEnvironment(EnvironmentTypes.Hill, xy))
                        powerProtection += PROTECTION_HILL_FOR_PAWN;
                    break;


                case UnitTypes.PawnSword:
                    if (HaveEnvironment(EnvironmentTypes.Fertilizer, xy))
                        powerProtection -= PROTECTION_FOOD_FOR_PAWN_SWORD;

                    if (HaveEnvironment(EnvironmentTypes.AdultForest, xy))
                        powerProtection += PROTECTION_TREE_FOR_PAWN_SWORD;

                    if (HaveEnvironment(EnvironmentTypes.Hill, xy))
                        powerProtection += PROTECTION_HILL_FOR_PAWN_SWORD;
                    break;


                case UnitTypes.Rook:
                    if (HaveEnvironment(EnvironmentTypes.Fertilizer, xy))
                        powerProtection -= PROTECTION_FOOD_FOR_ROOK;

                    if (HaveEnvironment(EnvironmentTypes.AdultForest, xy))
                        powerProtection += PROTECTION_TREE_FOR_ROOK;

                    if (HaveEnvironment(EnvironmentTypes.Hill, xy))
                        powerProtection += PROTECTION_HILL_FOR_ROOK;
                    break;


                case UnitTypes.RookCrossbow:
                    if (HaveEnvironment(EnvironmentTypes.Fertilizer, xy))
                        powerProtection -= PROTECTION_FOOD_FOR_ROOK_CROSSBOW;

                    if (HaveEnvironment(EnvironmentTypes.AdultForest, xy))
                        powerProtection += PROTECTION_TREE_FOR_ROOK_CROSSBOW;

                    if (HaveEnvironment(EnvironmentTypes.Hill, xy))
                        powerProtection += PROTECTION_HILL_FOR_ROOK_CROSSBOW;
                    break;


                case UnitTypes.Bishop:
                    if (HaveEnvironment(EnvironmentTypes.Fertilizer, xy))
                        powerProtection -= PROTECTION_FOOD_FOR_BISHOP;

                    if (HaveEnvironment(EnvironmentTypes.AdultForest, xy))
                        powerProtection += PROTECTION_TREE_FOR_BISHOP;

                    if (HaveEnvironment(EnvironmentTypes.Hill, xy))
                        powerProtection += PROTECTION_HILL_FOR_BISHOP;
                    break;


                case UnitTypes.BishopCrossbow:
                    if (HaveEnvironment(EnvironmentTypes.Fertilizer, xy))
                        powerProtection -= PROTECTION_FOOD_FOR_BISHOP_CROSSBOW;

                    if (HaveEnvironment(EnvironmentTypes.AdultForest, xy))
                        powerProtection += PROTECTION_TREE_FOR_BISHOP_CROSSBOW;

                    if (HaveEnvironment(EnvironmentTypes.Hill, xy))
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

            EntitiesGameGeneralManager.CellMaxStepsEnt_SpriteRendererCom(toXy).Enabled = true;

            SetSprite(unitType, toXy);

            if (EGGM.CellUnitEnt_CellOwnerCom(toXy).IsMasterClient)
            {
                EntitiesGameGeneralManager.CellMaxStepsEnt_SpriteRendererCom(toXy).Color = Color.blue;
            }
            else
            {
                EntitiesGameGeneralManager.CellMaxStepsEnt_SpriteRendererCom(toXy).Color = Color.red;
            }




            EGGM.CellUnitEnt_SpriteRendererCom(fromXy).Enabled = false;

            SetStandartValuesUnit(default, default, default, default, fromXy);
            EGGM.CellUnitEnt_CellOwnerCom(fromXy).ResetOwner();

            EntitiesGameGeneralManager.CellMaxStepsEnt_SpriteRendererCom(fromXy).Enabled = false;
        }

        internal static void SetNewPlayerUnit(UnitTypes unitType, int amountHealth, int amountSteps, ProtectRelaxTypes protectRelaxType, Player player, int[] xy)
        {
            UnitInfoManager.AddAmountUnitInGame(unitType, player.IsMasterClient);

            SetStandartValuesUnit(unitType, amountHealth, amountSteps, protectRelaxType, xy);
            EGGM.CellUnitEnt_CellOwnerCom(xy).SetOwner(player);

            EntitiesGameGeneralManager.CellMaxStepsEnt_SpriteRendererCom(xy).Enabled = true;
            if (player.IsMasterClient)
                EntitiesGameGeneralManager.CellMaxStepsEnt_SpriteRendererCom(xy).Color = Color.blue;

            else EntitiesGameGeneralManager.CellMaxStepsEnt_SpriteRendererCom(xy).Color = Color.red;

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

        internal static void ResetPlayerUnit(int[] xy)
        {
            var previousUnitType = EGGM.CellUnitEnt_UnitTypeCom(xy).UnitType;
            var previousIsMasterOwner = EGGM.CellUnitEnt_CellOwnerCom(xy).IsMasterClient;

            EGGM.CellUnitEnt_SpriteRendererCom(xy).Enabled = false;

            UnitInfoManager.TakeAmountUnitInGame(previousUnitType, previousIsMasterOwner);


            UnitTypes unitType = default;
            int amountHealth = default;
            int amountSteps = default;
            ProtectRelaxTypes protectRelaxType = default;
            Player player = default;

            SetStandartValuesUnit(unitType, amountHealth, amountSteps, protectRelaxType, xy);
            EGGM.CellUnitEnt_CellOwnerCom(xy).SetOwner(player);
        }

        internal static void SyncPlayerUnit(UnitTypes unitType, int amountHealth, int amountSteps, ProtectRelaxTypes protectRelaxType, Player player, int[] xy)
        {
            if (EGGM.CellUnitEnt_UnitTypeCom(xy).HaveAnyUnit)
            {
                EGGM.CellUnitEnt_SpriteRendererCom(xy).Enabled = false;
            }

            SetStandartValuesUnit(unitType, amountHealth, amountSteps, protectRelaxType, xy);
            EGGM.CellUnitEnt_CellOwnerCom(xy).SetOwner(player);
        }

        internal static void ChangePlayerUnit(int[] xy, UnitTypes newUnitType)
        {
            EGGM.CellUnitEnt_SpriteRendererCom(xy).Enabled = false;

            EGGM.CellUnitEnt_UnitTypeCom(xy).UnitType = newUnitType;

            EGGM.CellUnitEnt_SpriteRendererCom(xy).Enabled = true;

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

        internal static void ActiveSelectorVisionUnit(bool isActive, UnitTypes unitType, int[] xy)
        {
            if (isActive)
            {
                EGGM.CellUnitEnt_SpriteRendererCom(xy).Enabled = isActive;
                SetSprite(unitType, xy);
            }

            else
            {
                EGGM.CellUnitEnt_SpriteRendererCom(xy).Enabled = isActive;
            }
        }

        #endregion


        #region Bot

        internal static void SetBotUnit(UnitTypes unitType, bool haveBot, int amountHealth, int amountSteps, ProtectRelaxTypes protectRelaxType, params int[] xy)
        {
            SetStandartValuesUnit(unitType, amountHealth, amountSteps, protectRelaxType, xy);
            EGGM.CellUnitEnt_CellOwnerBotCom(xy).SetBot(haveBot);

            EGGM.CellUnitEnt_SpriteRendererCom(xy).Enabled = true;
        }

        internal static void ResetBotUnit(params int[] xy)
        {
            if (EGGM.CellUnitEnt_UnitTypeCom(xy).HaveAnyUnit)
                EGGM.CellUnitEnt_SpriteRendererCom(xy).Enabled = false;

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

        internal static void GetCellsForShift(this List<int[]> list, int[] xy)
        {
            if (list == default) throw new Exception();

            var listAvailable = TryGetXYAround(xy);

            foreach (var xy1 in listAvailable)
            {
                if (!HaveEnvironment(EnvironmentTypes.Mountain, xy1) && !EGGM.CellUnitEnt_UnitTypeCom(xy1).HaveAnyUnit)
                {
                    var unitType = EGGM.CellUnitEnt_UnitTypeCom(xy).UnitType;

                    if (EGGM.CellUnitEnt_CellUnitCom(xy).AmountSteps >= NeedAmountSteps(xy1) || EGGM.CellUnitEnt_CellUnitCom(xy).HaveMaxSteps(unitType))
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


                    if (!HaveEnvironment(EnvironmentTypes.Mountain, xy1))
                    {
                        var unitType = EGGM.CellUnitEnt_UnitTypeCom(xy).UnitType;

                        if (NeedAmountSteps(xy1) <= EGGM.CellUnitEnt_CellUnitCom(xy).AmountSteps || EGGM.CellUnitEnt_CellUnitCom(xy).HaveMaxSteps(unitType))
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
                            if (!HaveEnvironment(EnvironmentTypes.Mountain, xy1))
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
                                    if (!HaveEnvironment(EnvironmentTypes.Mountain, xy2))
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
                                    if (!HaveEnvironment(EnvironmentTypes.Mountain, xy2))
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
                                    if (!HaveEnvironment(EnvironmentTypes.Mountain, xy2))
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
                                    if (!HaveEnvironment(EnvironmentTypes.Mountain, xy2))
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
                    var xy = new int[] { x, y };

                    if (!HaveEnvironment(EnvironmentTypes.Mountain, xy))
                    {
                        if (!HaveAnyUnit(xy))
                        {
                            if (CellWorker.IsStartedCell(player.IsMasterClient, xy))
                            {
                                list.Add(new int[] { x, y });
                            }
                        }
                    }
                }
        }

        #endregion
    }
}