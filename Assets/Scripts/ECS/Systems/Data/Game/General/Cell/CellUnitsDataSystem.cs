using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Abstractions.ValuesConsts;
using Assets.Scripts.ECS.Components;
using Assets.Scripts.ECS.Game.Components;
using Assets.Scripts.ECS.Game.General.Components;
using Assets.Scripts.ECS.Game.General.Systems.StartFill;
using Assets.Scripts.ECS.System.View.Game.General.Cell;
using Assets.Scripts.Workers.Cell;
using Leopotam.Ecs;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using static Assets.Scripts.Abstractions.ValuesConsts.CellValues;
using static Assets.Scripts.Abstractions.ValuesConsts.UnitValues;

namespace Assets.Scripts.ECS.System.Data.Game.General.Cell
{
    internal sealed class CellUnitsDataSystem : IEcsInitSystem
    {
        private EcsWorld _gameWorld;

        private static EcsEntity[,] _cellUnitEnts;

        public void Init()
        {
            _cellUnitEnts = new EcsEntity[CELL_COUNT_X, CELL_COUNT_Y];

            for (int x = 0; x < CELL_COUNT_X; x++)
                for (int y = 0; y < CELL_COUNT_Y; y++)
                {
                    _cellUnitEnts[x, y] = _gameWorld.NewEntity()
                        .Replace(new CellUnitComponent())
                        .Replace(new UnitTypeComponent())
                        .Replace(new OwnerComponent())
                        .Replace(new OwnerBotComponent())
                        .Replace(new IsVisibleDictComponent(new Dictionary<bool, bool>()))
                        .Replace(new ProtectRelaxComponent());
                }
        }


        internal static void SetIsVisibleUnit(bool key, bool value, int[] xy) => _cellUnitEnts[xy[X], xy[Y]].Get<IsVisibleDictComponent>().IsVisibleDict[key] = value;
        internal static bool IsVisibleUnit(bool key, int[] xy) => _cellUnitEnts[xy[X], xy[Y]].Get<IsVisibleDictComponent>().IsVisibleDict[key];


        private static void SetStandartValuesUnit(UnitTypes unitType, int amountHealth, int amountSteps, ConditionUnitTypes protectRelaxType, params int[] xy)
        {
            SetUnitType(unitType, xy);
            SetAmountHealth(amountHealth, xy);
            SetAmountSteps(amountSteps, xy);
            SetConditionType(protectRelaxType, xy);
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

        internal static UnitTypes UnitType(int[] xy) => _cellUnitEnts[xy[X], xy[Y]].Get<UnitTypeComponent>().UnitType;
        internal static void SetUnitType(UnitTypes unitType, int[] xy) => _cellUnitEnts[xy[X], xy[Y]].Get<UnitTypeComponent>().UnitType = unitType;
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

        internal static int AmountHealth(int[] xy) => _cellUnitEnts[xy[X], xy[Y]].Get<CellUnitComponent>().AmountHealth;
        internal static void SetAmountHealth(int value, int[] xy) => _cellUnitEnts[xy[X], xy[Y]].Get<CellUnitComponent>().AmountHealth = value;
        internal static void AddAmountHealth(int[] xy, int adding = 1) => _cellUnitEnts[xy[X], xy[Y]].Get<CellUnitComponent>().AmountHealth += adding;
        internal static void TakeAmountHealth(int[] xy, int taking = 1) => _cellUnitEnts[xy[X], xy[Y]].Get<CellUnitComponent>().AmountHealth -= taking;

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

        internal static int AmountSteps(int[] xy) => _cellUnitEnts[xy[X], xy[Y]].Get<CellUnitComponent>().AmountSteps;
        internal static void SetAmountSteps(int value, int[] xy) => _cellUnitEnts[xy[X], xy[Y]].Get<CellUnitComponent>().AmountSteps = value;
        internal static void AddAmountSteps(int[] xy, int adding = 1) => _cellUnitEnts[xy[X], xy[Y]].Get<CellUnitComponent>().AmountSteps += adding;
        internal static void TakeAmountSteps(int[] xy, int taking = 1) => _cellUnitEnts[xy[X], xy[Y]].Get<CellUnitComponent>().AmountSteps -= taking;

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
        internal static ConditionUnitTypes ConditionType(int[] xy) => _cellUnitEnts[xy[X], xy[Y]].Get<ProtectRelaxComponent>().ProtectRelaxType;
        internal static void SetConditionType(ConditionUnitTypes protectRelaxType, int[] xy) => _cellUnitEnts[xy[X], xy[Y]].Get<ProtectRelaxComponent>().ProtectRelaxType = protectRelaxType;

        internal static void ResetConditionType(int[] xy) => SetConditionType(ConditionUnitTypes.None, xy);
        internal static bool IsConditionType(ConditionUnitTypes protectRelaxType, int[] xy) => ConditionType(xy) == protectRelaxType;


        #region AmountStepsInCondition

        internal static int AmountStepsInProtectRelax(ConditionUnitTypes protectRelaxType, int[] xy)
        {
            switch (protectRelaxType)
            {
                case ConditionUnitTypes.None:
                    return _cellUnitEnts[xy[X], xy[Y]].Get<CellUnitComponent>().AmountStepsInNone;

                case ConditionUnitTypes.Protected:
                    return _cellUnitEnts[xy[X], xy[Y]].Get<CellUnitComponent>().AmountStepsInProtected;

                case ConditionUnitTypes.Relaxed:
                    return _cellUnitEnts[xy[X], xy[Y]].Get<CellUnitComponent>().AmountStepsInRelaxed;

                default:
                    throw new Exception();
            }
        }
        internal static void SetAmountStepsInProtectRelax(ConditionUnitTypes protectRelaxType, int value, int[] xy)
        {
            switch (protectRelaxType)
            {
                case ConditionUnitTypes.None:
                    _cellUnitEnts[xy[X], xy[Y]].Get<CellUnitComponent>().AmountStepsInNone = value;
                    break;

                case ConditionUnitTypes.Protected:
                    _cellUnitEnts[xy[X], xy[Y]].Get<CellUnitComponent>().AmountStepsInProtected = value;
                    break;

                case ConditionUnitTypes.Relaxed:
                    _cellUnitEnts[xy[X], xy[Y]].Get<CellUnitComponent>().AmountStepsInRelaxed = value;
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
                    _cellUnitEnts[xy[X], xy[Y]].Get<CellUnitComponent>().AmountStepsInNone += adding;
                    break;

                case ConditionUnitTypes.Protected:
                    _cellUnitEnts[xy[X], xy[Y]].Get<CellUnitComponent>().AmountStepsInProtected += adding;
                    break;

                case ConditionUnitTypes.Relaxed:
                    _cellUnitEnts[xy[X], xy[Y]].Get<CellUnitComponent>().AmountStepsInRelaxed += adding;
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
                    _cellUnitEnts[xy[X], xy[Y]].Get<CellUnitComponent>().AmountStepsInNone -= taking;
                    break;

                case ConditionUnitTypes.Protected:
                    _cellUnitEnts[xy[X], xy[Y]].Get<CellUnitComponent>().AmountStepsInProtected -= taking;
                    break;

                case ConditionUnitTypes.Relaxed:
                    _cellUnitEnts[xy[X], xy[Y]].Get<CellUnitComponent>().AmountStepsInRelaxed -= taking;
                    break;

                default:
                    throw new Exception();
            }
        }

        internal static void ResetAmountStepsInProtectRelax(ConditionUnitTypes protectRelaxType, int[] xy) => SetAmountStepsInProtectRelax(protectRelaxType, default, xy);

        #endregion

        #endregion


        #region Owner

        internal static Player Owner(int[] xy) => _cellUnitEnts[xy[X], xy[Y]].Get<OwnerComponent>().Owner;
        internal static void SetOwner(Player newOwner, int[] xy) => _cellUnitEnts[xy[X], xy[Y]].Get<OwnerComponent>().Owner = newOwner;

        internal static void ResetOwner(int[] xy) => SetOwner(default, xy);
        internal static bool HaveOwner(int[] xy) => Owner(xy) != default;
        internal static int ActorNumber(int[] xy) => Owner(xy).ActorNumber;
        internal static bool IsMine(int[] xy) => Owner(xy).IsLocal;
        internal static bool IsMasterClient(int[] xy) => Owner(xy).IsMasterClient;
        internal static bool IsHim(Player player, int[] xy) => ActorNumber(xy) == player.ActorNumber;

        #endregion


        #region Bot

        internal static bool IsBot(int[] xy) => _cellUnitEnts[xy[X], xy[Y]].Get<OwnerBotComponent>().IsBot;
        internal static void SetIsBot(bool isBot, int[] xy) => _cellUnitEnts[xy[X], xy[Y]].Get<OwnerBotComponent>().IsBot = isBot;
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

            if (IsConditionType(ConditionUnitTypes.Protected, xy))
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

            else if (IsConditionType(ConditionUnitTypes.Relaxed, xy))
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
                    if (CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.Fertilizer, xy))
                        powerProtection -= PROTECTION_FOOD_FOR_KING;

                    if (CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.AdultForest, xy))
                        powerProtection += PROTECTION_TREE_FOR_KING;

                    if (CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.Hill, xy))
                        powerProtection += PROTECTION_HILL_FOR_KING;
                    break;

                case UnitTypes.Pawn:
                    if (CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.Fertilizer, xy))
                        powerProtection -= PROTECTION_FOOD_FOR_PAWN;

                    if (CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.AdultForest, xy))
                        powerProtection += PROTECTION_TREE_FOR_PAWN;

                    if (CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.Hill, xy))
                        powerProtection += PROTECTION_HILL_FOR_PAWN;
                    break;


                case UnitTypes.PawnSword:
                    if (CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.Fertilizer, xy))
                        powerProtection -= PROTECTION_FOOD_FOR_PAWN_SWORD;

                    if (CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.AdultForest, xy))
                        powerProtection += PROTECTION_TREE_FOR_PAWN_SWORD;

                    if (CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.Hill, xy))
                        powerProtection += PROTECTION_HILL_FOR_PAWN_SWORD;
                    break;


                case UnitTypes.Rook:
                    if (CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.Fertilizer, xy))
                        powerProtection -= PROTECTION_FOOD_FOR_ROOK;

                    if (CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.AdultForest, xy))
                        powerProtection += PROTECTION_TREE_FOR_ROOK;

                    if (CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.Hill, xy))
                        powerProtection += PROTECTION_HILL_FOR_ROOK;
                    break;


                case UnitTypes.RookCrossbow:
                    if (CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.Fertilizer, xy))
                        powerProtection -= PROTECTION_FOOD_FOR_ROOK_CROSSBOW;

                    if (CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.AdultForest, xy))
                        powerProtection += PROTECTION_TREE_FOR_ROOK_CROSSBOW;

                    if (CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.Hill, xy))
                        powerProtection += PROTECTION_HILL_FOR_ROOK_CROSSBOW;
                    break;


                case UnitTypes.Bishop:
                    if (CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.Fertilizer, xy))
                        powerProtection -= PROTECTION_FOOD_FOR_BISHOP;

                    if (CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.AdultForest, xy))
                        powerProtection += PROTECTION_TREE_FOR_BISHOP;

                    if (CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.Hill, xy))
                        powerProtection += PROTECTION_HILL_FOR_BISHOP;
                    break;


                case UnitTypes.BishopCrossbow:
                    if (CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.Fertilizer, xy))
                        powerProtection -= PROTECTION_FOOD_FOR_BISHOP_CROSSBOW;

                    if (CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.AdultForest, xy))
                        powerProtection += PROTECTION_TREE_FOR_BISHOP_CROSSBOW;

                    if (CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.Hill, xy))
                        powerProtection += PROTECTION_HILL_FOR_BISHOP_CROSSBOW;
                    break;
            }

            switch (CellBuildDataSystem.BuildTypeCom(xy).BuildingType)
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
            var protectRelaxType = ConditionType(fromXy);
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


        internal static void SyncAll(UnitTypes unitType, Player owner, bool haveBot, int amountHealth, int amountSteps, ConditionUnitTypes conditionType, int[] xy)
        {
            SetStandartValuesUnit(unitType, amountHealth, amountSteps, conditionType, xy);
            SetOwner(owner, xy);
            SetIsBot(haveBot, xy);
        }


        #region ForMoving

        internal static List<int[]> GetCellsForShift(int[] xy)
        {
            var list = new List<int[]>();

            var listAvailable = CellSpaceWorker.TryGetXyAround(xy);

            foreach (var xy1 in listAvailable)
            {
                if (!CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.Mountain, xy1) && !HaveAnyUnit(xy1))
                {
                    if (AmountSteps(xy) >= CellEnvrDataSystem.NeedAmountSteps(xy1) || HaveMaxAmountSteps(xy))
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


                    if (!CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.Mountain, xy1))
                    {
                        if (CellEnvrDataSystem.NeedAmountSteps(xy1) <= AmountSteps(xy) || HaveMaxAmountSteps(xy))
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

                    if (CellViewSystem.IsActiveSelfParentCell(xy1))
                    {
                        if (HaveMinAmountSteps(xy))
                        {
                            if (!CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.Mountain, xy1))
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

                        if (IsVisibleUnit(PhotonNetwork.IsMasterClient, xy2))
                        {
                            if (UnitType(xy) == UnitTypes.Rook || UnitType(xy) == UnitTypes.RookCrossbow)
                            {
                                if (directType1 == DirectTypes.Left || directType1 == DirectTypes.Right || directType1 == DirectTypes.Down || directType1 == DirectTypes.Up)
                                {
                                    if (!CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.Mountain, xy2))
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
                                    if (!CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.Mountain, xy2))
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
                                    if (!CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.Mountain, xy2))
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
                                    if (!CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.Mountain, xy2))
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

            for (int x = 0; x < CellValues.CELL_COUNT_X; x++)
                for (int y = 0; y < CellValues.CELL_COUNT_Y; y++)
                {
                    var xy = new int[] { x, y };

                    if (!CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.Mountain, xy))
                    {
                        if (!HaveAnyUnit(xy))
                        {
                            if (InitSystem.XyStartCellsCom.IsStartedCell(player.IsMasterClient, xy))
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
