using Assets.Scripts.Abstractions.ValuesConsts;
using Assets.Scripts.ECS.Components;
using Leopotam.Ecs;
using Photon.Pun;
using System;
using static Assets.Scripts.Abstractions.ValuesConsts.CellValues;

namespace Assets.Scripts.ECS.System.Data.Game.General.Cell
{
    internal sealed class CellEnvrDataSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _gameWorld;
        private static EcsEntity[,] _cellFertilizerEnts;
        private static EcsEntity[,] _cellYoungForestEnts;
        private static EcsEntity[,] _cellAdultForestEnts;
        private static EcsEntity[,] _cellHillEnts;
        private static EcsEntity[,] _cellMountainEnts;

        private const int MAX_AMOUNT_FOOD = 10;
        private const int MAX_AMOUNT_WOOD = 10;
        private const int MAX_AMOUNT_ORE = 999;

        private const int MIN_AMOUNT_FOOD = 5;
        private const int MIN_AMOUNT_WOOD = 5;
        private const int MIN_AMOUNT_ORE = 999;

        internal const int START_FERTILIZER_PERCENT = 30;
        internal const int START_FOREST_PERCENT = 40;
        internal const int START_HILL_PERCENT = 40;
        internal const int START_MOUNTAIN_PERCENT = 25;



        internal static ref HaveEnvironmentComponent HaveFertilizerEnvCom(int[] xy) => ref _cellFertilizerEnts[xy[X], xy[Y]].Get<HaveEnvironmentComponent>();
        internal static ref HaveEnvironmentComponent HaveYoungForestEnvCom(int[] xy) => ref _cellYoungForestEnts[xy[X], xy[Y]].Get<HaveEnvironmentComponent>();
        internal static ref HaveEnvironmentComponent HaveAdultForestEnvCom(int[] xy) => ref _cellAdultForestEnts[xy[X], xy[Y]].Get<HaveEnvironmentComponent>();
        internal static ref HaveEnvironmentComponent HaveHillEnvCom(int[] xy) => ref _cellHillEnts[xy[X], xy[Y]].Get<HaveEnvironmentComponent>();
        internal static ref HaveEnvironmentComponent HaveMountainEnvCom(int[] xy) => ref _cellMountainEnts[xy[X], xy[Y]].Get<HaveEnvironmentComponent>();


        public void Init()
        {
            _cellFertilizerEnts = new EcsEntity[CELL_COUNT_X, CELL_COUNT_Y];
            _cellYoungForestEnts = new EcsEntity[CELL_COUNT_X, CELL_COUNT_Y];
            _cellAdultForestEnts = new EcsEntity[CELL_COUNT_X, CELL_COUNT_Y];
            _cellHillEnts = new EcsEntity[CELL_COUNT_X, CELL_COUNT_Y];
            _cellMountainEnts = new EcsEntity[CELL_COUNT_X, CELL_COUNT_Y];

            for (int x = 0; x < CELL_COUNT_X; x++)
                for (int y = 0; y < CELL_COUNT_Y; y++)
                {
                    _cellFertilizerEnts[x, y] = _gameWorld.NewEntity()
                        .Replace(new AmountResourcesComponent())
                        .Replace(new HaveEnvironmentComponent());


                    _cellYoungForestEnts[x, y] = _gameWorld.NewEntity()
                        .Replace(new HaveEnvironmentComponent());


                    _cellAdultForestEnts[x, y] = _gameWorld.NewEntity()
                        .Replace(new AmountResourcesComponent())
                        .Replace(new HaveEnvironmentComponent());


                    _cellHillEnts[x, y] = _gameWorld.NewEntity()
                        .Replace(new AmountResourcesComponent())
                        .Replace(new HaveEnvironmentComponent());


                    _cellMountainEnts[x, y] = _gameWorld.NewEntity()
                        .Replace(new HaveEnvironmentComponent());
                }

            if (PhotonNetwork.IsMasterClient)
            {
                for (int x = 0; x < CELL_COUNT_X; x++)
                    for (int y = 0; y < CELL_COUNT_Y; y++)
                    {
                        var xy = new int[] { x, y };

                        int random;

                        if (y == 4 || y == 6)
                        {
                            random = UnityEngine.Random.Range(1, 100);
                            if (random <= START_MOUNTAIN_PERCENT)
                                SetNewEnvironment(EnvironmentTypes.Mountain, xy);
                            else
                            {
                                random = UnityEngine.Random.Range(1, 100);
                                if (random <= START_FOREST_PERCENT)
                                {
                                    SetNewEnvironment(EnvironmentTypes.AdultForest, xy);
                                }
                            }
                        }
                        else
                        {

                            random = UnityEngine.Random.Range(1, 100);
                            if (random <= START_FOREST_PERCENT)
                            {
                                SetNewEnvironment(EnvironmentTypes.AdultForest, xy);
                            }
                            else
                            {
                                random = UnityEngine.Random.Range(1, 100);
                                if (random <= START_FERTILIZER_PERCENT)
                                {
                                    SetNewEnvironment(EnvironmentTypes.Fertilizer, xy);
                                }
                            }


                            if (y == 5)
                            {

                                random = UnityEngine.Random.Range(1, 100);
                                if (random <= START_HILL_PERCENT)
                                    SetNewEnvironment(EnvironmentTypes.Hill, xy);

                            }
                        }
                    }
            }


            if (PhotonNetwork.OfflineMode)
            {
                // Bot
            }
        }

        public void Run()
        {

        }


        internal static bool HaveEnvironment(EnvironmentTypes environmentType, int[] xy)
        {
            switch (environmentType)
            {
                case EnvironmentTypes.None:
                    throw new Exception();

                case EnvironmentTypes.Fertilizer:
                    return HaveFertilizerEnvCom(xy).HaveEnvironment;

                case EnvironmentTypes.YoungForest:
                    return HaveYoungForestEnvCom(xy).HaveEnvironment;

                case EnvironmentTypes.AdultForest:
                    return HaveAdultForestEnvCom(xy).HaveEnvironment;

                case EnvironmentTypes.Hill:
                    return HaveHillEnvCom(xy).HaveEnvironment;

                case EnvironmentTypes.Mountain:
                    return HaveMountainEnvCom(xy).HaveEnvironment;

                default:
                    throw new Exception();
            }
        }
        private static void SetHaveEnvironment(bool haveEnvironment, EnvironmentTypes environmentType, int[] xy)
        {
            switch (environmentType)
            {
                case EnvironmentTypes.None:
                    throw new Exception();

                case EnvironmentTypes.Fertilizer:
                    _cellFertilizerEnts[xy[X], xy[Y]].Get<HaveEnvironmentComponent>().HaveEnvironment = haveEnvironment;
                    break;

                case EnvironmentTypes.YoungForest:
                    _cellYoungForestEnts[xy[X], xy[Y]].Get<HaveEnvironmentComponent>().HaveEnvironment = haveEnvironment;
                    break;

                case EnvironmentTypes.AdultForest:
                    _cellAdultForestEnts[xy[X], xy[Y]].Get<HaveEnvironmentComponent>().HaveEnvironment = haveEnvironment;
                    break;

                case EnvironmentTypes.Hill:
                    _cellHillEnts[xy[X], xy[Y]].Get<HaveEnvironmentComponent>().HaveEnvironment = haveEnvironment;
                    break;

                case EnvironmentTypes.Mountain:
                    _cellMountainEnts[xy[X], xy[Y]].Get<HaveEnvironmentComponent>().HaveEnvironment = haveEnvironment;
                    break;

                default:
                    throw new Exception();
            }
        }

        internal static void ResetEnvironment(EnvironmentTypes environmentType, int[] xy) => SetEnvironment(environmentType, false, 0, xy);

        #region Resources

        internal static int GetAmountResources(EnvironmentTypes environmentType, int[] xy)
        {
            switch (environmentType)
            {
                case EnvironmentTypes.None:
                    throw new Exception();

                case EnvironmentTypes.Fertilizer:
                    return _cellFertilizerEnts[xy[X], xy[Y]].Get<AmountResourcesComponent>().AmountResources;

                case EnvironmentTypes.YoungForest:
                    throw new Exception();

                case EnvironmentTypes.AdultForest:
                    return _cellAdultForestEnts[xy[X], xy[Y]].Get<AmountResourcesComponent>().AmountResources;

                case EnvironmentTypes.Hill:
                    return _cellHillEnts[xy[X], xy[Y]].Get<AmountResourcesComponent>().AmountResources;

                case EnvironmentTypes.Mountain:
                    throw new Exception();

                default:
                    throw new Exception();
            }
        }
        internal static void SetAmountResources(EnvironmentTypes environmentType, int value, int[] xy)
        {
            switch (environmentType)
            {
                case EnvironmentTypes.None:
                    throw new Exception();

                case EnvironmentTypes.Fertilizer:
                    _cellFertilizerEnts[xy[X], xy[Y]].Get<AmountResourcesComponent>().AmountResources = value;
                    break;

                case EnvironmentTypes.YoungForest:
                    throw new Exception();

                case EnvironmentTypes.AdultForest:
                    _cellAdultForestEnts[xy[X], xy[Y]].Get<AmountResourcesComponent>().AmountResources = value;
                    break;

                case EnvironmentTypes.Hill:
                    _cellHillEnts[xy[X], xy[Y]].Get<AmountResourcesComponent>().AmountResources = value;
                    break;

                case EnvironmentTypes.Mountain:
                    throw new Exception();

                default:
                    throw new Exception();
            }
        }

        internal static void AddAmountResources(EnvironmentTypes environmentType, int[] xy, int adding = 1)
            => SetAmountResources(environmentType, GetAmountResources(environmentType, xy) + adding, xy);
        internal static void TakeAmountResources(EnvironmentTypes environmentType, int[] xy, int taking = 1)
            => SetAmountResources(environmentType, GetAmountResources(environmentType, xy) - taking, xy);

        internal static bool HaveResources(EnvironmentTypes environmentType, int[] xy) => GetAmountResources(environmentType, xy) > 0;
        internal static int MaxAmountResources(EnvironmentTypes environmentTypes)
        {
            switch (environmentTypes)
            {
                case EnvironmentTypes.None:
                    throw new Exception();

                case EnvironmentTypes.Fertilizer:
                    return MAX_AMOUNT_FOOD;

                case EnvironmentTypes.YoungForest:
                    throw new Exception();

                case EnvironmentTypes.AdultForest:
                    return MAX_AMOUNT_WOOD;

                case EnvironmentTypes.Hill:
                    return MAX_AMOUNT_ORE;

                case EnvironmentTypes.Mountain:
                    throw new Exception();

                default:
                    throw new Exception();
            }
        }
        internal static int MinAmountResources(EnvironmentTypes environmentTypes)
        {
            switch (environmentTypes)
            {
                case EnvironmentTypes.None:
                    throw new Exception();

                case EnvironmentTypes.Fertilizer:
                    return MIN_AMOUNT_FOOD;

                case EnvironmentTypes.YoungForest:
                    throw new Exception();

                case EnvironmentTypes.AdultForest:
                    return MIN_AMOUNT_WOOD;

                case EnvironmentTypes.Hill:
                    return MIN_AMOUNT_ORE;

                case EnvironmentTypes.Mountain:
                    throw new Exception();

                default:
                    throw new Exception();
            }
        }

        #endregion

        internal static void SetEnvironment(EnvironmentTypes environmentType, bool haveEnv, int amountEnvironmet, int[] xy)
        {
            SetHaveEnvironment(haveEnv, environmentType, xy);

            switch (environmentType)
            {
                case EnvironmentTypes.None:
                    throw new Exception();

                case EnvironmentTypes.Fertilizer:
                    SetAmountResources(environmentType, amountEnvironmet, xy);
                    break;

                case EnvironmentTypes.YoungForest:
                    SetHaveEnvironment(haveEnv, environmentType, xy);
                    break;

                case EnvironmentTypes.AdultForest:
                    SetAmountResources(environmentType, amountEnvironmet, xy);
                    break;

                case EnvironmentTypes.Hill:
                    SetAmountResources(environmentType, amountEnvironmet, xy);
                    break;

                case EnvironmentTypes.Mountain:
                    break;

                default:
                    throw new Exception();
            }
        }
        internal static void SetNewEnvironment(EnvironmentTypes environmentType, int[] xy)
        {
            int randAmountResour = 0;
            switch (environmentType)
            {
                case EnvironmentTypes.None:
                    throw new Exception();

                case EnvironmentTypes.Fertilizer:
                    randAmountResour = UnityEngine.Random.Range(MinAmountResources(environmentType), MaxAmountResources(environmentType));
                    break;

                case EnvironmentTypes.YoungForest:
                    break;

                case EnvironmentTypes.AdultForest:
                    randAmountResour = UnityEngine.Random.Range(MinAmountResources(environmentType), MaxAmountResources(environmentType));
                    break;

                case EnvironmentTypes.Hill:
                    randAmountResour = UnityEngine.Random.Range(MinAmountResources(environmentType), MaxAmountResources(environmentType));
                    break;

                case EnvironmentTypes.Mountain:
                    break;

                default:
                    throw new Exception();
            }

            SetEnvironment(environmentType, true, randAmountResour, xy);
        }


        internal static int NeedAmountSteps(int[] xy)
        {
            int amountSteps = 1;

            if (HaveEnvironment(EnvironmentTypes.Fertilizer, xy))
                amountSteps += UnitValues.NEED_AMOUNT_STEPS_FOOD;

            if (HaveEnvironment(EnvironmentTypes.AdultForest, xy))
                amountSteps += UnitValues.NEED_AMOUNT_STEPS_TREE;

            if (HaveEnvironment(EnvironmentTypes.Hill, xy))
                amountSteps += UnitValues.NEED_AMOUNT_STEPS_HILL;

            return amountSteps;
        }
    }
}
