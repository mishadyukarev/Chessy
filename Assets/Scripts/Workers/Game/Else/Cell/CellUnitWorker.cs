using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Cell;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using UnityEngine;
using static Assets.Scripts.Abstractions.ValuesConsts.UnitValues;
using static Assets.Scripts.CellEnvironmentWorker;
using static Assets.Scripts.Main;
using static Assets.Scripts.Workers.Cell.CellSpaceWorker;

namespace Assets.Scripts
{
    public abstract class CellUnitWorker : MainGeneralWorker
    {
        internal static SpritesData SpritesData => Instance.ECSmanager.EntitiesCommonManager.ResourcesEnt_ResourcesCommonCom.SpritesConfig;

        private static void SetStandartValuesUnit(UnitTypes unitType, int amountHealth, int amountSteps, ProtectRelaxTypes protectRelaxType, params int[] xy)
        {
            SetUnitType(unitType, xy);
            SetAmountHealth(amountHealth, xy);
            SetAmountSteps(amountSteps, xy);
            SetProtectRelaxType(protectRelaxType, xy);
        }
        private static void SetSprite(UnitTypes unitType, params int[] xy)
        {
            switch (unitType)
            {
                case UnitTypes.None:
                    throw new Exception();

                case UnitTypes.King:
                    EGGM.CellUnitEnt_SpriteRendererCom(xy).SpriteRenderer.sprite = SpritesData.KingSprite;
                    break;

                case UnitTypes.Pawn:
                    EGGM.CellUnitEnt_SpriteRendererCom(xy).SpriteRenderer.sprite = SpritesData.PawnSprite;
                    break;

                case UnitTypes.PawnSword:
                    EGGM.CellUnitEnt_SpriteRendererCom(xy).SpriteRenderer.sprite = SpritesData.PawnSwordSprite;
                    break;

                case UnitTypes.Rook:
                    EGGM.CellUnitEnt_SpriteRendererCom(xy).SpriteRenderer.sprite = SpritesData.RookSprite;
                    break;

                case UnitTypes.RookCrossbow:
                    EGGM.CellUnitEnt_SpriteRendererCom(xy).SpriteRenderer.sprite = SpritesData.RookCrossbowSprite;
                    break;

                case UnitTypes.Bishop:
                    EGGM.CellUnitEnt_SpriteRendererCom(xy).SpriteRenderer.sprite = SpritesData.BishopSprite;
                    break;

                case UnitTypes.BishopCrossbow:
                    EGGM.CellUnitEnt_SpriteRendererCom(xy).SpriteRenderer.sprite = SpritesData.BishopCrossbowSprite;
                    break;

                default:
                    throw new Exception();
            }
        }

        internal static void SetIsActivated(bool key, bool value, int[] xy) => EGGM.CellUnitEnt_ActivatedForPlayersCom(xy).SetActivated(key, value);
        internal static bool IsActivated(bool key, int[] xy) => EGGM.CellUnitEnt_ActivatedForPlayersCom(xy).IsActivated(key);

        internal static void SetEnabledUnit(bool isEnabled, int[] xy) => EGGM.CellUnitEnt_SpriteRendererCom(xy).SpriteRenderer.enabled = isEnabled;


        #region UnitType

        internal static bool HaveAnyUnit(int[] xy) => EGGM.CellUnitEnt_UnitTypeCom(xy).UnitType != UnitTypes.None;
        internal static UnitTypes UnitType(int[] xy) => EGGM.CellUnitEnt_UnitTypeCom(xy).UnitType;
        internal static void SetUnitType(UnitTypes unitType, int[] xy) => EGGM.CellUnitEnt_UnitTypeCom(xy).UnitType = unitType;
        internal static bool IsUnitType(UnitTypes unitType, int[] xy) => EGGM.CellUnitEnt_UnitTypeCom(xy).UnitType == unitType;
        internal static bool IsMelee(int[] xy) => IsMelee(EGGM.CellUnitEnt_UnitTypeCom(xy).UnitType);

        #endregion


        #region Health

        private const int STANDART_AMOUNT_HEALTH_KING = 300;
        private const int STANDART_AMOUNT_HEALTH_PAWN = 100;
        private const int STANDART_AMOUNT_HEALTH_PAWN_SWORD = 150;
        private const int STANDART_AMOUNT_HEALTH_ROOK = 100;
        private const int STANDART_AMOUNT_HEALTH_ROOK_CROSSBOW = 100;
        private const int STANDART_AMOUNT_HEALTH_BISHOP = 100;
        private const int STANDART_AMOUNT_HEALTH_BISHOP_CROSSBOW = 100;

        private const float PERCENT_FOR_HEALTH_KING = 0.2f;
        private const float PERCENT_FOR_HEALTH_PAWN = 0.3f;
        private const float PERCENT_FOR_HEALTH_PAWN_SWORD = 0.2f;
        private const float PERCENT_FOR_HEALTH_ROOK = 0.3f;
        private const float PERCENT_FOR_HEALTH_ROOK_CROSSBOW = 0.3f;
        private const float PERCENT_FOR_HEALTH_BISHOP = 0.3f;
        private const float PERCENT_FOR_HEALTH_BISHOP_CROSSBOW = 0.3f;

        internal static int AmountHealth(int[] xy) => EGGM.CellUnitEnt_CellUnitCom(xy).AmountHealth;
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
        internal static int MaxAmountHealth(int[] xy) => MaxAmountHealth(EGGM.CellUnitEnt_UnitTypeCom(xy).UnitType);
        internal static bool HaveMaxAmountHealth(int[] xy) => AmountHealth(xy) >= MaxAmountHealth(xy);
        internal static bool HaveAmountHealth(int[] xy) => EGGM.CellUnitEnt_CellUnitCom(xy).AmountHealth > 0;
        internal static void SetAmountHealth(int value, int[] xy) => EGGM.CellUnitEnt_CellUnitCom(xy).AmountHealth = value;
        internal static void AddAmountHealth(int[] xy, int adding = 1) => EGGM.CellUnitEnt_CellUnitCom(xy).AmountHealth += adding;
        internal static void TakeAmountHealth(int[] xy, int taking = 1) => EGGM.CellUnitEnt_CellUnitCom(xy).AmountHealth -= taking;
        internal static void AddStandartHeal(int[] xy)
        {
            switch (UnitType(xy))
            {
                case UnitTypes.None:
                    throw new Exception();

                case UnitTypes.King:
                    AddAmountHealth(xy, (int)(STANDART_AMOUNT_HEALTH_KING * PERCENT_FOR_HEALTH_KING));
                    break;

                case UnitTypes.Pawn:
                    AddAmountHealth(xy, (int)(STANDART_AMOUNT_HEALTH_PAWN * PERCENT_FOR_HEALTH_PAWN));
                    break;

                case UnitTypes.PawnSword:
                    AddAmountHealth(xy, (int)(STANDART_AMOUNT_HEALTH_PAWN_SWORD * PERCENT_FOR_HEALTH_PAWN_SWORD));
                    break;

                case UnitTypes.Rook:
                    AddAmountHealth(xy, (int)(STANDART_AMOUNT_HEALTH_ROOK * PERCENT_FOR_HEALTH_ROOK));
                    break;

                case UnitTypes.RookCrossbow:
                    AddAmountHealth(xy, (int)(STANDART_AMOUNT_HEALTH_ROOK_CROSSBOW * PERCENT_FOR_HEALTH_ROOK_CROSSBOW));
                    break;

                case UnitTypes.Bishop:
                    AddAmountHealth(xy, (int)(STANDART_AMOUNT_HEALTH_BISHOP * PERCENT_FOR_HEALTH_BISHOP));
                    break;

                case UnitTypes.BishopCrossbow:
                    AddAmountHealth(xy, (int)(STANDART_AMOUNT_HEALTH_BISHOP_CROSSBOW * PERCENT_FOR_HEALTH_BISHOP_CROSSBOW));
                    break;

                default:
                    throw new Exception();
            }
        }

        #endregion


        #region Steps

        internal static int AmountSteps(int[] xy) => EGGM.CellUnitEnt_CellUnitCom(xy).AmountSteps;
        internal static bool HaveMaxAmountSteps(int[] xy)
        {
            switch (EGGM.CellUnitEnt_UnitTypeCom(xy).UnitType)
            {
                case UnitTypes.None:
                    throw new Exception();

                case UnitTypes.King:
                    return EGGM.CellUnitEnt_CellUnitCom(xy).AmountSteps == STANDART_AMOUNT_STEPS_KING;

                case UnitTypes.Pawn:
                    return EGGM.CellUnitEnt_CellUnitCom(xy).AmountSteps == STANDART_AMOUNT_STEPS_PAWN;

                case UnitTypes.PawnSword:
                    return EGGM.CellUnitEnt_CellUnitCom(xy).AmountSteps == STANDART_AMOUNT_STEPS_PAWN_SWORD;

                case UnitTypes.Rook:
                    return EGGM.CellUnitEnt_CellUnitCom(xy).AmountSteps == STANDART_AMOUNT_STEPS_ROOK;

                case UnitTypes.RookCrossbow:
                    return EGGM.CellUnitEnt_CellUnitCom(xy).AmountSteps == STANDART_AMOUNT_STEPS_ROOK_CROSSBOW;

                case UnitTypes.Bishop:
                    return EGGM.CellUnitEnt_CellUnitCom(xy).AmountSteps == STANDART_AMOUNT_STEPS_BISHOP;

                case UnitTypes.BishopCrossbow:
                    return EGGM.CellUnitEnt_CellUnitCom(xy).AmountSteps == STANDART_AMOUNT_STEPS_BISHOP_CROSSBOW;

                default:
                    throw new Exception();
            }
        }
        internal static bool HaveMinAmountSteps(int[] xy) => EGGM.CellUnitEnt_CellUnitCom(xy).AmountSteps > 0;
        internal static void SetAmountSteps(int value, int[] xy) => EGGM.CellUnitEnt_CellUnitCom(xy).AmountSteps = value;
        internal static void ResetAmountSteps(int[] xy) => EGGM.CellUnitEnt_CellUnitCom(xy).AmountSteps = default;
        internal static void AddAmountSteps(int[] xy, int adding = 1) => EGGM.CellUnitEnt_CellUnitCom(xy).AmountSteps += adding;
        internal static void TakeAmountSteps(int[] xy, int taking = 1) => EGGM.CellUnitEnt_CellUnitCom(xy).AmountSteps -= taking;
        internal static void RefreshAmountSteps(int[] xy)
        {
            switch (EGGM.CellUnitEnt_UnitTypeCom(xy).UnitType)
            {
                case UnitTypes.None:
                    throw new Exception();

                case UnitTypes.King:
                    SetAmountSteps(STANDART_AMOUNT_STEPS_KING, xy);
                    break;

                case UnitTypes.Pawn:
                    SetAmountSteps(STANDART_AMOUNT_STEPS_PAWN, xy);
                    break;

                case UnitTypes.PawnSword:
                    SetAmountSteps(STANDART_AMOUNT_STEPS_PAWN_SWORD, xy);
                    break;

                case UnitTypes.Rook:
                    SetAmountSteps(STANDART_AMOUNT_STEPS_ROOK, xy);
                    break;

                case UnitTypes.RookCrossbow:
                    SetAmountSteps(STANDART_AMOUNT_STEPS_ROOK_CROSSBOW, xy);
                    break;

                case UnitTypes.Bishop:
                    SetAmountSteps(STANDART_AMOUNT_STEPS_BISHOP, xy);
                    break;

                case UnitTypes.BishopCrossbow:
                    SetAmountSteps(STANDART_AMOUNT_STEPS_BISHOP_CROSSBOW, xy);
                    break;

                default:
                    throw new Exception();
            }
        }

        #endregion


        #region ProtRelax

        internal static ProtectRelaxTypes ProtectRelaxType(int[] xy) => EGGM.CellUnitEnt_ProtectRelaxCom(xy).ProtectRelaxType;
        internal static void SetProtectRelaxType(ProtectRelaxTypes protectRelaxType, int[] xy) => EGGM.CellUnitEnt_ProtectRelaxCom(xy).ProtectRelaxType = protectRelaxType;
        internal static void ResetProtectedRelaxType(int[] xy) => SetProtectRelaxType(ProtectRelaxTypes.None, xy);
        internal static void ResetAmountStepsInProtectRelax(ProtectRelaxTypes protectRelaxType, int[] xy) => EGGM.CellUnitEnt_CellUnitCom(xy).ResetAmountStepsInProtectRelax(protectRelaxType);
        internal static int AmountStepsInProtectRelax(ProtectRelaxTypes protectRelaxType, int[] xy) => EGGM.CellUnitEnt_CellUnitCom(xy).AmountStepsInProtectRelax(protectRelaxType);
        internal static void AddAmountStepsInProtectRelax(ProtectRelaxTypes protectRelaxType, int[] xy, int adding = 1) => EGGM.CellUnitEnt_CellUnitCom(xy).AddAmountStepsInProtectRelax(protectRelaxType, adding);
        internal static bool IsUnitProtectRelaxType(ProtectRelaxTypes protectRelaxType, int[] xy) => EGGM.CellUnitEnt_ProtectRelaxCom(xy).ProtectRelaxType == protectRelaxType;

        #endregion


        #region Owner

        internal static Player Owner(int[] xy) => EGGM.CellUnitEnt_CellOwnerCom(xy).Owner;
        internal static void SetOwner(Player newOwner, int[] xy) => EGGM.CellUnitEnt_CellOwnerCom(xy).Owner = newOwner;
        internal static bool HaveOwner(int[] xy) => EGGM.CellUnitEnt_CellOwnerCom(xy).Owner != default;
        internal static int ActorNumber(int[] xy) => EGGM.CellUnitEnt_CellOwnerCom(xy).Owner.ActorNumber;
        internal static bool IsMine(int[] xy) => EGGM.CellUnitEnt_CellOwnerCom(xy).Owner.IsLocal;
        internal static bool IsMasterClient(int[] xy) => EGGM.CellUnitEnt_CellOwnerCom(xy).Owner.IsMasterClient;
        internal static bool IsHim(Player player, int[] xy) => ActorNumber(xy) == player.ActorNumber;
        internal static void ResetOwner(int[] xy) => EGGM.CellUnitEnt_CellOwnerCom(xy).Owner = default;

        internal static bool IsBot(int[] xy) => EGGM.CellUnitEnt_CellOwnerBotCom(xy).IsBot;
        internal static void SetIsBot(bool isBot, int[] xy) => EGGM.CellUnitEnt_CellOwnerBotCom(xy).IsBot = isBot;

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
            var unitType = UnitType(xy);

            int powerProtection = 0;

            if (IsUnitProtectRelaxType(ProtectRelaxTypes.Protected, xy))
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

            else if (IsUnitProtectRelaxType(ProtectRelaxTypes.Relaxed, xy))
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
            var unitType = UnitType(fromXy);
            var amountHealth = AmountHealth(fromXy);
            var amountSteps = AmountSteps(fromXy);
            var protectRelaxType = ProtectRelaxType(fromXy);
            var player = Owner(fromXy);

            SetStandartValuesUnit(unitType, amountHealth, amountSteps, protectRelaxType, toXy);
            SetOwner(player, toXy);

            EGGM.CellMaxStepsEnt_SpriteRendererCom(toXy).SpriteRenderer.enabled = true;

            SetSprite(unitType, toXy);

            if (IsMasterClient(toXy))
            {
                EGGM.CellMaxStepsEnt_SpriteRendererCom(toXy).SpriteRenderer.color = Color.blue;
            }
            else
            {
                EGGM.CellMaxStepsEnt_SpriteRendererCom(toXy).SpriteRenderer.color = Color.red;
            }




            EGGM.CellUnitEnt_SpriteRendererCom(fromXy).SpriteRenderer.enabled = false;

            SetStandartValuesUnit(default, default, default, default, fromXy);
            CellUnitWorker.ResetOwner(fromXy);

            EGGM.CellMaxStepsEnt_SpriteRendererCom(fromXy).SpriteRenderer.enabled = false;
        }

        internal static void SetNewPlayerUnit(UnitTypes unitType, int amountHealth, int amountSteps, ProtectRelaxTypes protectRelaxType, Player player, int[] xy)
        {
            UnitInfoManager.AddAmountUnitInGame(unitType, player.IsMasterClient);

            SetStandartValuesUnit(unitType, amountHealth, amountSteps, protectRelaxType, xy);
            SetOwner(player, xy);

            EGGM.CellMaxStepsEnt_SpriteRendererCom(xy).SpriteRenderer.enabled = true;
            if (player.IsMasterClient)
                EGGM.CellMaxStepsEnt_SpriteRendererCom(xy).SpriteRenderer.color = Color.blue;

            else EGGM.CellMaxStepsEnt_SpriteRendererCom(xy).SpriteRenderer.color = Color.red;

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
            var previousUnitType = UnitType(xy);
            var previousIsMasterOwner = IsMasterClient(xy);

            EGGM.CellUnitEnt_SpriteRendererCom(xy).SpriteRenderer.enabled = false;

            UnitInfoManager.TakeAmountUnitInGame(previousUnitType, previousIsMasterOwner);


            UnitTypes unitType = default;
            int amountHealth = default;
            int amountSteps = default;
            ProtectRelaxTypes protectRelaxType = default;
            Player player = default;

            SetStandartValuesUnit(unitType, amountHealth, amountSteps, protectRelaxType, xy);
            SetOwner(player, xy);
        }

        internal static void SyncPlayerUnit(UnitTypes unitType, int amountHealth, int amountSteps, ProtectRelaxTypes protectRelaxType, Player player, int[] xy)
        {
            if (HaveAnyUnit(xy))
            {
                EGGM.CellUnitEnt_SpriteRendererCom(xy).SpriteRenderer.enabled = false;
            }

            SetStandartValuesUnit(unitType, amountHealth, amountSteps, protectRelaxType, xy);
            SetOwner(player, xy);
        }

        internal static void ChangePlayerUnit(int[] xy, UnitTypes newUnitType)
        {
            EGGM.CellUnitEnt_SpriteRendererCom(xy).SpriteRenderer.enabled = false;

            SetUnitType(newUnitType, xy);

            EGGM.CellUnitEnt_SpriteRendererCom(xy).SpriteRenderer.enabled = true;

            switch (UnitType(xy))
            {
                case UnitTypes.None:
                    throw new Exception();

                case UnitTypes.King:
                    throw new Exception();

                case UnitTypes.Pawn:
                    break;

                case UnitTypes.PawnSword:
                    AddAmountHealth(xy, STANDART_AMOUNT_HEALTH_PAWN_SWORD - STANDART_AMOUNT_HEALTH_PAWN);
                    break;

                case UnitTypes.Rook:
                    break;

                case UnitTypes.RookCrossbow:
                    AddAmountHealth(xy, STANDART_AMOUNT_HEALTH_ROOK_CROSSBOW - STANDART_AMOUNT_HEALTH_ROOK);
                    break;

                case UnitTypes.Bishop:
                    break;

                case UnitTypes.BishopCrossbow:
                    AddAmountHealth(xy, STANDART_AMOUNT_HEALTH_BISHOP_CROSSBOW - STANDART_AMOUNT_HEALTH_BISHOP);
                    break;

                default:
                    throw new Exception();
            }
        }

        internal static void ActiveSelectorVisionUnit(bool isActive, UnitTypes unitType, int[] xy)
        {
            if (isActive)
            {
                EGGM.CellUnitEnt_SpriteRendererCom(xy).SpriteRenderer.enabled = isActive;
                SetSprite(unitType, xy);
            }

            else
            {
                EGGM.CellUnitEnt_SpriteRendererCom(xy).SpriteRenderer.enabled = isActive;
            }
        }

        #endregion


        #region Bot

        internal static void SetBotUnit(UnitTypes unitType, bool haveBot, int amountHealth, int amountSteps, ProtectRelaxTypes protectRelaxType, params int[] xy)
        {
            SetStandartValuesUnit(unitType, amountHealth, amountSteps, protectRelaxType, xy);
            SetIsBot(haveBot, xy);

            EGGM.CellUnitEnt_SpriteRendererCom(xy).SpriteRenderer.enabled = true;
        }

        internal static void ResetBotUnit(params int[] xy)
        {
            if (HaveAnyUnit(xy))
                EGGM.CellUnitEnt_SpriteRendererCom(xy).SpriteRenderer.enabled = false;

            UnitTypes unitType = default;
            int amountHealth = default;
            int amountSteps = default;
            ProtectRelaxTypes protectRelaxType = default;
            bool haveBot = default;

            SetStandartValuesUnit(unitType, amountHealth, amountSteps, protectRelaxType, xy);
            SetIsBot(haveBot, xy);
        }

        #endregion



        #region ForMoving

        internal static List<int[]> GetCellsForShift(int[] xy)
        {
            var list = new List<int[]>();

            var listAvailable = TryGetXYAround(xy);

            foreach (var xy1 in listAvailable)
            {
                if (!HaveEnvironment(EnvironmentTypes.Mountain, xy1) && !HaveAnyUnit(xy1))
                {
                    if (AmountSteps(xy) >= NeedAmountSteps(xy1) || HaveMaxAmountSteps(xy))
                    {
                        list.Add(xy1);
                    }
                }
            }

            return list;
        }
        internal static void GetCellsForAttack(Player playerFrom, out List<int[]> availableCellsSimpleAttack, out List<int[]> availableCellsUniqueAttack, int[] xy)
        {
            availableCellsSimpleAttack = new List<int[]>();
            availableCellsUniqueAttack = new List<int[]>();

            if (IsMelee(xy))
            {
                for (DirectTypes directType1 = default; directType1 <= DirectTypes.LeftDown; directType1++)
                {
                    var xy1 = GetXYCell(xy, directType1);


                    if (!HaveEnvironment(EnvironmentTypes.Mountain, xy1))
                    {
                        if (NeedAmountSteps(xy1) <= AmountSteps(xy) || HaveMaxAmountSteps(xy))
                        {
                            if (HaveAnyUnit(xy1))
                            {
                                if (HaveOwner(xy1))
                                {
                                    if (!IsHim(playerFrom, xy1))
                                    {
                                        if (UnitType(xy) == UnitTypes.Pawn || UnitType(xy) == UnitTypes.PawnSword)
                                        {
                                            if (directType1 == DirectTypes.Left || directType1 == DirectTypes.Right || directType1 == DirectTypes.Up || directType1 == DirectTypes.Down)
                                            {
                                                availableCellsSimpleAttack.Add(xy1);
                                            }
                                            else availableCellsUniqueAttack.Add(xy1);
                                        }

                                        else if (UnitType(xy) == UnitTypes.King)
                                        {
                                            availableCellsSimpleAttack.Add(xy1);
                                        }
                                    }
                                }
                                else if (IsBot(xy1))
                                {
                                    if (UnitType(xy) == UnitTypes.Pawn || UnitType(xy) == UnitTypes.PawnSword)
                                    {
                                        if (directType1 == DirectTypes.Left || directType1 == DirectTypes.Right || directType1 == DirectTypes.Up || directType1 == DirectTypes.Down)
                                        {
                                            availableCellsSimpleAttack.Add(xy1);
                                        }
                                        else availableCellsUniqueAttack.Add(xy1);
                                    }

                                    else if (UnitType(xy) == UnitTypes.King)
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
                        if (HaveMinAmountSteps(xy))
                        {
                            if (!HaveEnvironment(EnvironmentTypes.Mountain, xy1))
                            {
                                if (HaveAnyUnit(xy1))
                                {
                                    if (HaveOwner(xy1))
                                    {
                                        if (!IsHim(playerFrom, xy1))
                                        {
                                            if (UnitType(xy) == UnitTypes.Rook || UnitType(xy) == UnitTypes.RookCrossbow)
                                            {
                                                if (directType1 == DirectTypes.Left || directType1 == DirectTypes.Right || directType1 == DirectTypes.Up || directType1 == DirectTypes.Down)
                                                {
                                                    availableCellsUniqueAttack.Add(xy1);
                                                }
                                                else availableCellsSimpleAttack.Add(xy1);
                                            }

                                            else if (UnitType(xy) == UnitTypes.Bishop || UnitType(xy) == UnitTypes.BishopCrossbow)
                                            {
                                                if (directType1 == DirectTypes.Left || directType1 == DirectTypes.Right || directType1 == DirectTypes.Up || directType1 == DirectTypes.Down)
                                                {
                                                    availableCellsSimpleAttack.Add(xy1);
                                                }
                                                else availableCellsUniqueAttack.Add(xy1);
                                            }
                                        }
                                    }

                                    else if (IsBot(xy1))
                                    {
                                        if (UnitType(xy) == UnitTypes.Rook || UnitType(xy) == UnitTypes.RookCrossbow)
                                        {
                                            if (directType1 == DirectTypes.Left || directType1 == DirectTypes.Right || directType1 == DirectTypes.Up || directType1 == DirectTypes.Down)
                                            {
                                                availableCellsUniqueAttack.Add(xy1);
                                            }
                                            else availableCellsSimpleAttack.Add(xy1);
                                        }

                                        else if (UnitType(xy) == UnitTypes.Bishop || UnitType(xy) == UnitTypes.BishopCrossbow)
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

                        if (EGGM.CellUnitEnt_ActivatedForPlayersCom(xy2).IsActivated(Instance.IsMasterClient))
                        {
                            if (UnitType(xy) == UnitTypes.Rook || UnitType(xy) == UnitTypes.RookCrossbow)
                            {
                                if (directType1 == DirectTypes.Left || directType1 == DirectTypes.Right || directType1 == DirectTypes.Down || directType1 == DirectTypes.Up)
                                {
                                    if (!HaveEnvironment(EnvironmentTypes.Mountain, xy2))
                                    {
                                        if (HaveAnyUnit(xy2))
                                        {
                                            if (HaveOwner(xy2))
                                            {
                                                if (!IsHim(playerFrom, xy2))
                                                {
                                                    availableCellsUniqueAttack.Add(xy2);
                                                }
                                            }

                                            else if (EGGM.CellUnitEnt_CellOwnerBotCom(xy2).IsBot)
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
                                        if (HaveAnyUnit(xy2))
                                        {
                                            if (HaveOwner(xy2))
                                            {
                                                if (!IsHim(playerFrom, xy2))
                                                {
                                                    availableCellsSimpleAttack.Add(xy2);
                                                }
                                            }

                                            else if (EGGM.CellUnitEnt_CellOwnerBotCom(xy2).IsBot)
                                            {
                                                availableCellsSimpleAttack.Add(xy2);
                                            }
                                        }
                                    }
                                }
                            }


                            else if (UnitType(xy) == UnitTypes.Bishop || UnitType(xy) == UnitTypes.BishopCrossbow)
                            {
                                if (directType1 == DirectTypes.LeftDown || directType1 == DirectTypes.LeftUp || directType1 == DirectTypes.RightDown || directType1 == DirectTypes.RightUp)
                                {
                                    if (!HaveEnvironment(EnvironmentTypes.Mountain, xy2))
                                    {
                                        if (HaveAnyUnit(xy2))
                                        {
                                            if (HaveOwner(xy2))
                                            {
                                                if (!IsHim(playerFrom, xy2))
                                                {
                                                    availableCellsUniqueAttack.Add(xy2);
                                                }
                                            }

                                            else if (EGGM.CellUnitEnt_CellOwnerBotCom(xy2).IsBot)
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
                                        if (HaveAnyUnit(xy2))
                                        {
                                            if (HaveOwner(xy2))
                                            {
                                                if (!IsHim(playerFrom, xy2))
                                                {
                                                    availableCellsSimpleAttack.Add(xy2);
                                                }
                                            }

                                            else if (IsBot(xy2))
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
        internal static List<int[]> GetStartCellsForSettingUnit(Player player)
        {
            var list = new List<int[]>();

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

            return list;
        }

        #endregion
    }
}