using System;
using static Assets.Scripts.Main;

namespace Assets.Scripts
{
    public static class CellEnvironmentWorker
    {
        internal static EntitiesGameGeneralManager EGGM => Instance.EntGGM;
        internal static SpritesData SpritesData => Instance.ECSmanager.EntitiesCommonManager.ResourcesEnt_ResourcesCommonCom.SpritesConfig;

        internal static void SetNewEnvironment(EnvironmentTypes environmentType, params int[] xy)
        {
            EGGM.CellEnvEnt_CellEnvCom(xy).AddEnvironment(environmentType);

            switch (environmentType)
            {
                case EnvironmentTypes.None:
                    throw new Exception();

                case EnvironmentTypes.Fertilizer:

                    _amountFoodResources = UnityEngine.Random.Range(MinAmountResources(environmentType), MaxAmountResources(environmentType));
                    break;

                case EnvironmentTypes.YoungForest:
                    _haveYoungTree = true;
                    break;

                case EnvironmentTypes.AdultForest:
                    _haveAdultTree = true;
                    _amountWoodResources = UnityEngine.Random.Range(MinAmountResources(environmentType), MaxAmountResources(environmentType));
                    break;

                case EnvironmentTypes.Hill:
                    _haveHill = true;
                    _amountOreResources = MaxAmountResources(environmentType);
                    break;

                case EnvironmentTypes.Mountain:
                    _haveMountain = true;
                    break;

                default:
                    throw new Exception();
            }
        }
        internal static void SetEnvironment(EnvironmentTypes environmentType, int amountEnvironmet)
        {
            switch (environmentType)
            {
                case EnvironmentTypes.None:
                    break;

                case EnvironmentTypes.Mountain:
                    _haveMountain = true;
                    break;

                case EnvironmentTypes.AdultForest:
                    _haveAdultTree = true;
                    _amountWoodResources = amountEnvironmet;
                    break;

                case EnvironmentTypes.YoungForest:
                    _haveYoungTree = true;
                    break;

                case EnvironmentTypes.Hill:
                    _haveHill = true;
                    _amountOreResources = amountEnvironmet;
                    break;

                case EnvironmentTypes.Fertilizer:
                    _haveFertilizer = true;
                    _amountFoodResources = amountEnvironmet;
                    break;

                default:
                    break;
            }
        }
        internal static void ResetEnvironment(EnvironmentTypes environmentType)
        {
            switch (environmentType)
            {
                case EnvironmentTypes.None:
                    throw new Exception();

                case EnvironmentTypes.Mountain:
                    _haveMountain = false;
                    break;

                case EnvironmentTypes.AdultForest:
                    _haveAdultTree = false;
                    _amountWoodResources = 0;
                    break;

                case EnvironmentTypes.YoungForest:
                    _haveYoungTree = false;
                    break;

                case EnvironmentTypes.Hill:
                    _haveHill = false;
                    break;

                case EnvironmentTypes.Fertilizer:
                    _haveFertilizer = false;
                    _amountFoodResources = 0;
                    break;

                default:
                    throw new Exception();
            }
        }
    }
}