using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Game.General.Entities.Containers;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Cell;
using Assets.Scripts.Workers.Game.Else.Cell;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using static Assets.Scripts.Abstractions.ValuesConsts.UnitValues;
using static Assets.Scripts.Main;

namespace Assets.Scripts
{
    public class CellUnitsDataWorker
    {
        private static CellUnitEntsContainer _cellUnitEntsContainer;



        internal CellUnitsDataWorker(CellUnitEntsContainer cellUnitEntsContainer)
        {
            _cellUnitEntsContainer = cellUnitEntsContainer;
        }



        internal static void SetIsVisibleUnit(bool key, bool value, int[] xy) => _cellUnitEntsContainer.CellUnitEnt_IsVisibleDictCom(xy).IsVisibleDict[key] = value;
        internal static bool IsVisibleUnit(bool key, int[] xy) => _cellUnitEntsContainer.CellUnitEnt_IsVisibleDictCom(xy).IsVisibleDict[key];


        private static void SetStandartValuesUnit(UnitTypes unitType, int amountHealth, int amountSteps, ConditionUnitTypes protectRelaxType, params int[] xy)
        {
            SetUnitType(unitType, xy);
            SetAmountHealth(amountHealth, xy);
            SetAmountSteps(amountSteps, xy);
            SetProtectRelaxType(protectRelaxType, xy);
        }
        private static void ResetStandartValuesUnit(int[] xy)
        {
            UnitTypes unitType = default;
            int amountHealth = default;
            int amountSteps = default;
            ConditionUnitTypes protectRelaxType = default;

            SetStandartValuesUnit(unitType, amountHealth, amountSteps, protectRelaxType, xy);
        }



        #region UnitType

        internal static UnitTypes UnitType(int[] xy) => _cellUnitEntsContainer.CellUnitEnt_UnitTypeCom(xy).UnitType;
        internal static void SetUnitType(UnitTypes unitType, int[] xy) => _cellUnitEntsContainer.CellUnitEnt_UnitTypeCom(xy).UnitType = unitType;
        internal static bool HaveAnyUnit(int[] xy) => UnitType(xy) != UnitTypes.None;
        internal static bool IsUnitType(UnitTypes unitType, int[] xy) => UnitType(xy) == unitType;
        internal static bool IsMelee(int[] xy) => IsMelee(UnitType(xy));
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

        #endregion


        #region Health

        internal static int AmountHealth(int[] xy) => _cellUnitEntsContainer.CellUnitEnt_CellUnitCom(xy).AmountHealth;
        internal static void SetAmountHealth(int value, int[] xy) => _cellUnitEntsContainer.CellUnitEnt_CellUnitCom(xy).AmountHealth = value;
        internal static void AddAmountHealth(int[] xy, int adding = 1) => _cellUnitEntsContainer.CellUnitEnt_CellUnitCom(xy).AmountHealth += adding;
        internal static void TakeAmountHealth(int[] xy, int taking = 1) => _cellUnitEntsContainer.CellUnitEnt_CellUnitCom(xy).AmountHealth -= taking;

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
        internal static int MaxAmountHealth(int[] xy) => MaxAmountHealth(UnitType(xy));
        internal static bool HaveMaxAmountHealth(int[] xy) => AmountHealth(xy) >= MaxAmountHealth(xy);
        internal static bool HaveAmountHealth(int[] xy) => AmountHealth(xy) > 0;
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

        internal static int AmountSteps(int[] xy) => _cellUnitEntsContainer.CellUnitEnt_CellUnitCom(xy).AmountSteps;
        internal static void SetAmountSteps(int value, int[] xy) => _cellUnitEntsContainer.CellUnitEnt_CellUnitCom(xy).AmountSteps = value;
        internal static void AddAmountSteps(int[] xy, int adding = 1) => _cellUnitEntsContainer.CellUnitEnt_CellUnitCom(xy).AmountSteps += adding;
        internal static void TakeAmountSteps(int[] xy, int taking = 1) => _cellUnitEntsContainer.CellUnitEnt_CellUnitCom(xy).AmountSteps -= taking;

        internal static bool HaveMaxAmountSteps(int[] xy)
        {
            switch (UnitType(xy))
            {
                case UnitTypes.None:
                    throw new Exception();

                case UnitTypes.King:
                    return AmountSteps(xy) == STANDART_AMOUNT_STEPS_KING;

                case UnitTypes.Pawn:
                    return AmountSteps(xy) == STANDART_AMOUNT_STEPS_PAWN;

                case UnitTypes.PawnSword:
                    return AmountSteps(xy) == STANDART_AMOUNT_STEPS_PAWN_SWORD;

                case UnitTypes.Rook:
                    return AmountSteps(xy) == STANDART_AMOUNT_STEPS_ROOK;

                case UnitTypes.RookCrossbow:
                    return AmountSteps(xy) == STANDART_AMOUNT_STEPS_ROOK_CROSSBOW;

                case UnitTypes.Bishop:
                    return AmountSteps(xy) == STANDART_AMOUNT_STEPS_BISHOP;

                case UnitTypes.BishopCrossbow:
                    return AmountSteps(xy) == STANDART_AMOUNT_STEPS_BISHOP_CROSSBOW;

                default:
                    throw new Exception();
            }
        }
        internal static bool HaveMinAmountSteps(int[] xy) => AmountSteps(xy) > 0;
        internal static void ResetAmountSteps(int[] xy) => SetAmountSteps(default, xy);
        internal static void RefreshAmountSteps(int[] xy)
        {
            switch (UnitType(xy))
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


        #region Condition
        internal static ConditionUnitTypes ProtectRelaxType(int[] xy) => _cellUnitEntsContainer.CellUnitEnt_ProtectRelaxCom(xy).ProtectRelaxType;
        internal static void SetProtectRelaxType(ConditionUnitTypes protectRelaxType, int[] xy) => _cellUnitEntsContainer.CellUnitEnt_ProtectRelaxCom(xy).ProtectRelaxType = protectRelaxType;

        internal static void ResetProtectedRelaxType(int[] xy) => SetProtectRelaxType(ConditionUnitTypes.None, xy);
        internal static bool IsProtectRelaxType(ConditionUnitTypes protectRelaxType, int[] xy) => _cellUnitEntsContainer.CellUnitEnt_ProtectRelaxCom(xy).ProtectRelaxType == protectRelaxType;


        #region AmountStepsInCondition

        internal static int AmountStepsInProtectRelax(ConditionUnitTypes protectRelaxType, int[] xy)
        {
            switch (protectRelaxType)
            {
                case ConditionUnitTypes.None:
                    return _cellUnitEntsContainer.CellUnitEnt_CellUnitCom(xy).AmountStepsInNone;

                case ConditionUnitTypes.Protected:
                    return _cellUnitEntsContainer.CellUnitEnt_CellUnitCom(xy).AmountStepsInProtected;

                case ConditionUnitTypes.Relaxed:
                    return _cellUnitEntsContainer.CellUnitEnt_CellUnitCom(xy).AmountStepsInRelaxed;

                default:
                    throw new Exception();
            }
        }
        internal static void SetAmountStepsInProtectRelax(ConditionUnitTypes protectRelaxType, int value, int[] xy)
        {
            switch (protectRelaxType)
            {
                case ConditionUnitTypes.None:
                    _cellUnitEntsContainer.CellUnitEnt_CellUnitCom(xy).AmountStepsInNone = value;
                    break;

                case ConditionUnitTypes.Protected:
                    _cellUnitEntsContainer.CellUnitEnt_CellUnitCom(xy).AmountStepsInProtected = value;
                    break;

                case ConditionUnitTypes.Relaxed:
                    _cellUnitEntsContainer.CellUnitEnt_CellUnitCom(xy).AmountStepsInRelaxed = value;
                    break;

                default:
                    throw new Exception();
            }
        }
        internal static void AddAmountStepsInProtectRelax(ConditionUnitTypes protectRelaxType, int[] xy, int adding = 1)
        {
            switch (protectRelaxType)
            {
                case ConditionUnitTypes.None:
                    _cellUnitEntsContainer.CellUnitEnt_CellUnitCom(xy).AmountStepsInNone += adding;
                    break;

                case ConditionUnitTypes.Protected:
                    _cellUnitEntsContainer.CellUnitEnt_CellUnitCom(xy).AmountStepsInProtected += adding;
                    break;

                case ConditionUnitTypes.Relaxed:
                    _cellUnitEntsContainer.CellUnitEnt_CellUnitCom(xy).AmountStepsInRelaxed += adding;
                    break;

                default:
                    throw new Exception();
            }
        }
        internal static void TakeAmountStepsInProtectRelax(ConditionUnitTypes protectRelaxType, int[] xy, int taking = 1)
        {
            switch (protectRelaxType)
            {
                case ConditionUnitTypes.None:
                    _cellUnitEntsContainer.CellUnitEnt_CellUnitCom(xy).AmountStepsInNone -= taking;
                    break;

                case ConditionUnitTypes.Protected:
                    _cellUnitEntsContainer.CellUnitEnt_CellUnitCom(xy).AmountStepsInProtected -= taking;
                    break;

                case ConditionUnitTypes.Relaxed:
                    _cellUnitEntsContainer.CellUnitEnt_CellUnitCom(xy).AmountStepsInRelaxed -= taking;
                    break;

                default:
                    throw new Exception();
            }
        }

        internal static void ResetAmountStepsInProtectRelax(ConditionUnitTypes protectRelaxType, int[] xy) => SetAmountStepsInProtectRelax(protectRelaxType, default, xy);

        #endregion

        #endregion


        #region Owner

        internal static Player Owner(int[] xy) => _cellUnitEntsContainer.CellUnitEnt_CellOwnerCom(xy).Owner;
        internal static void SetOwner(Player newOwner, int[] xy) => _cellUnitEntsContainer.CellUnitEnt_CellOwnerCom(xy).Owner = newOwner;

        internal static void ResetOwner(int[] xy) => SetOwner(default, xy);
        internal static bool HaveOwner(int[] xy) => Owner(xy) != default;
        internal static int ActorNumber(int[] xy) => Owner(xy).ActorNumber;
        internal static bool IsMine(int[] xy) => Owner(xy).IsLocal;
        internal static bool IsMasterClient(int[] xy) => Owner(xy).IsMasterClient;
        internal static bool IsHim(Player player, int[] xy) => ActorNumber(xy) == player.ActorNumber;

        #endregion


        #region Bot

        internal static bool IsBot(int[] xy) => _cellUnitEntsContainer.CellUnitEnt_CellOwnerBotCom(xy).IsBot;
        internal static void SetIsBot(bool isBot, int[] xy) => _cellUnitEntsContainer.CellUnitEnt_CellOwnerBotCom(xy).IsBot = isBot;
        private static void ResetIsBot(int[] xy) => SetIsBot(false, xy);

        #endregion


        #region Damage

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
        internal static int SimplePowerDamage(int[] xy) => SimplePowerDamage(UnitType(xy));
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
        internal static int UniquePowerDamage(int[] xy) => UniquePowerDamage(UnitType(xy));
        internal static int PowerProtection(int[] xy)
        {
            var unitType = UnitType(xy);

            int powerProtection = 0;

            if (IsProtectRelaxType(ConditionUnitTypes.Protected, xy))
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

            else if (IsProtectRelaxType(ConditionUnitTypes.Relaxed, xy))
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
                    if (CellEnvirDataWorker.HaveEnvironment(EnvironmentTypes.Fertilizer, xy))
                        powerProtection -= PROTECTION_FOOD_FOR_KING;

                    if (CellEnvirDataWorker.HaveEnvironment(EnvironmentTypes.AdultForest, xy))
                        powerProtection += PROTECTION_TREE_FOR_KING;

                    if (CellEnvirDataWorker.HaveEnvironment(EnvironmentTypes.Hill, xy))
                        powerProtection += PROTECTION_HILL_FOR_KING;
                    break;

                case UnitTypes.Pawn:
                    if (CellEnvirDataWorker.HaveEnvironment(EnvironmentTypes.Fertilizer, xy))
                        powerProtection -= PROTECTION_FOOD_FOR_PAWN;

                    if (CellEnvirDataWorker.HaveEnvironment(EnvironmentTypes.AdultForest, xy))
                        powerProtection += PROTECTION_TREE_FOR_PAWN;

                    if (CellEnvirDataWorker.HaveEnvironment(EnvironmentTypes.Hill, xy))
                        powerProtection += PROTECTION_HILL_FOR_PAWN;
                    break;


                case UnitTypes.PawnSword:
                    if (CellEnvirDataWorker.HaveEnvironment(EnvironmentTypes.Fertilizer, xy))
                        powerProtection -= PROTECTION_FOOD_FOR_PAWN_SWORD;

                    if (CellEnvirDataWorker.HaveEnvironment(EnvironmentTypes.AdultForest, xy))
                        powerProtection += PROTECTION_TREE_FOR_PAWN_SWORD;

                    if (CellEnvirDataWorker.HaveEnvironment(EnvironmentTypes.Hill, xy))
                        powerProtection += PROTECTION_HILL_FOR_PAWN_SWORD;
                    break;


                case UnitTypes.Rook:
                    if (CellEnvirDataWorker.HaveEnvironment(EnvironmentTypes.Fertilizer, xy))
                        powerProtection -= PROTECTION_FOOD_FOR_ROOK;

                    if (CellEnvirDataWorker.HaveEnvironment(EnvironmentTypes.AdultForest, xy))
                        powerProtection += PROTECTION_TREE_FOR_ROOK;

                    if (CellEnvirDataWorker.HaveEnvironment(EnvironmentTypes.Hill, xy))
                        powerProtection += PROTECTION_HILL_FOR_ROOK;
                    break;


                case UnitTypes.RookCrossbow:
                    if (CellEnvirDataWorker.HaveEnvironment(EnvironmentTypes.Fertilizer, xy))
                        powerProtection -= PROTECTION_FOOD_FOR_ROOK_CROSSBOW;

                    if (CellEnvirDataWorker.HaveEnvironment(EnvironmentTypes.AdultForest, xy))
                        powerProtection += PROTECTION_TREE_FOR_ROOK_CROSSBOW;

                    if (CellEnvirDataWorker.HaveEnvironment(EnvironmentTypes.Hill, xy))
                        powerProtection += PROTECTION_HILL_FOR_ROOK_CROSSBOW;
                    break;


                case UnitTypes.Bishop:
                    if (CellEnvirDataWorker.HaveEnvironment(EnvironmentTypes.Fertilizer, xy))
                        powerProtection -= PROTECTION_FOOD_FOR_BISHOP;

                    if (CellEnvirDataWorker.HaveEnvironment(EnvironmentTypes.AdultForest, xy))
                        powerProtection += PROTECTION_TREE_FOR_BISHOP;

                    if (CellEnvirDataWorker.HaveEnvironment(EnvironmentTypes.Hill, xy))
                        powerProtection += PROTECTION_HILL_FOR_BISHOP;
                    break;


                case UnitTypes.BishopCrossbow:
                    if (CellEnvirDataWorker.HaveEnvironment(EnvironmentTypes.Fertilizer, xy))
                        powerProtection -= PROTECTION_FOOD_FOR_BISHOP_CROSSBOW;

                    if (CellEnvirDataWorker.HaveEnvironment(EnvironmentTypes.AdultForest, xy))
                        powerProtection += PROTECTION_TREE_FOR_BISHOP_CROSSBOW;

                    if (CellEnvirDataWorker.HaveEnvironment(EnvironmentTypes.Hill, xy))
                        powerProtection += PROTECTION_HILL_FOR_BISHOP_CROSSBOW;
                    break;
            }

            switch (CellBuildingsDataWorker.GetBuildingType(xy))
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

        #endregion


        #region Actions

        internal static void ShiftPlayerUnitToBaseCell(int[] fromXy, int[] toXy)
        {
            var unitType = UnitType(fromXy);
            var amountHealth = AmountHealth(fromXy);
            var amountSteps = AmountSteps(fromXy);
            var protectRelaxType = ProtectRelaxType(fromXy);
            var player = Owner(fromXy);

            SetStandartValuesUnit(unitType, amountHealth, amountSteps, protectRelaxType, toXy);
            SetOwner(player, toXy);

            SetStandartValuesUnit(default, default, default, default, fromXy);
            ResetOwner(fromXy);
        }

        internal static void SetPlayerUnit(UnitTypes unitType, int amountHealth, int amountSteps, ConditionUnitTypes protectRelaxType, Player player, int[] xy)
        {
            SetStandartValuesUnit(unitType, amountHealth, amountSteps, protectRelaxType, xy);
            SetOwner(player, xy);
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

            SetPlayerUnit(unitType, amountHealth, amountSteps, ConditionUnitTypes.None, player, xy);
        }

        internal static void ResetUnit(int[] xy)
        {
            ResetStandartValuesUnit(xy);

            ResetOwner(xy);
            ResetIsBot(xy);
        }

        internal static void SyncPlayerUnit(UnitTypes unitType, int amountHealth, int amountSteps, ConditionUnitTypes protectRelaxType, Player player, int[] xy)
        {
            SetStandartValuesUnit(unitType, amountHealth, amountSteps, protectRelaxType, xy);
            SetOwner(player, xy);
        }

        internal static void ChangePlayerUnit(int[] xy, UnitTypes newUnitType)
        {
            SetUnitType(newUnitType, xy);

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


        #endregion


        #region Bot

        internal static void SetBotUnit(UnitTypes unitType, bool haveBot, int amountHealth, int amountSteps, ConditionUnitTypes protectRelaxType, int[] xy)
        {
            SetStandartValuesUnit(unitType, amountHealth, amountSteps, protectRelaxType, xy);
            SetIsBot(haveBot, xy);
        }

        #endregion


        #region ForMoving

        internal static List<int[]> GetCellsForShift(int[] xy)
        {
            var list = new List<int[]>();

            var listAvailable = CellSpaceWorker.TryGetXYAround(xy);

            foreach (var xy1 in listAvailable)
            {
                if (!CellEnvirDataWorker.HaveEnvironment(EnvironmentTypes.Mountain, xy1) && !HaveAnyUnit(xy1))
                {
                    if (AmountSteps(xy) >= CellEnvirDataWorker.NeedAmountSteps(xy1) || HaveMaxAmountSteps(xy))
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
                    var xy1 = CellSpaceWorker.GetXYCell(xy, directType1);


                    if (!CellEnvirDataWorker.HaveEnvironment(EnvironmentTypes.Mountain, xy1))
                    {
                        if (CellEnvirDataWorker.NeedAmountSteps(xy1) <= AmountSteps(xy) || HaveMaxAmountSteps(xy))
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
                    var xy1 = CellSpaceWorker.GetXYCell(xy, directType1);

                    if (CellGameWorker.IsActiveSelfParentCell(xy1))
                    {
                        if (HaveMinAmountSteps(xy))
                        {
                            if (!CellEnvirDataWorker.HaveEnvironment(EnvironmentTypes.Mountain, xy1))
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


                        var xy2 = CellSpaceWorker.GetXYCell(xy1, directType1);

                        if (CellUnitsDataWorker.IsVisibleUnit(Instance.IsMasterClient, xy2))
                        {
                            if (UnitType(xy) == UnitTypes.Rook || UnitType(xy) == UnitTypes.RookCrossbow)
                            {
                                if (directType1 == DirectTypes.Left || directType1 == DirectTypes.Right || directType1 == DirectTypes.Down || directType1 == DirectTypes.Up)
                                {
                                    if (!CellEnvirDataWorker.HaveEnvironment(EnvironmentTypes.Mountain, xy2))
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

                                            else if (IsBot(xy2))
                                            {
                                                availableCellsUniqueAttack.Add(xy2);
                                            }
                                        }
                                    }
                                }

                                if (directType1 == DirectTypes.LeftDown || directType1 == DirectTypes.LeftUp || directType1 == DirectTypes.RightDown || directType1 == DirectTypes.RightUp)
                                {
                                    if (!CellEnvirDataWorker.HaveEnvironment(EnvironmentTypes.Mountain, xy2))
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


                            else if (UnitType(xy) == UnitTypes.Bishop || UnitType(xy) == UnitTypes.BishopCrossbow)
                            {
                                if (directType1 == DirectTypes.LeftDown || directType1 == DirectTypes.LeftUp || directType1 == DirectTypes.RightDown || directType1 == DirectTypes.RightUp)
                                {
                                    if (!CellEnvirDataWorker.HaveEnvironment(EnvironmentTypes.Mountain, xy2))
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

                                            else if (IsBot(xy2))
                                            {
                                                availableCellsUniqueAttack.Add(xy2);
                                            }
                                        }
                                    }
                                }

                                if (directType1 == DirectTypes.Left || directType1 == DirectTypes.Right || directType1 == DirectTypes.Down || directType1 == DirectTypes.Up)
                                {
                                    if (!CellEnvirDataWorker.HaveEnvironment(EnvironmentTypes.Mountain, xy2))
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

            for (int x = 0; x < _cellUnitEntsContainer.Xamount; x++)
                for (int y = 0; y < _cellUnitEntsContainer.Yamount; y++)
                {
                    var xy = new int[] { x, y };

                    if (!CellEnvirDataWorker.HaveEnvironment(EnvironmentTypes.Mountain, xy))
                    {
                        if (!HaveAnyUnit(xy))
                        {
                            if (InfoCellWorker.IsStartedCell(player.IsMasterClient, xy))
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