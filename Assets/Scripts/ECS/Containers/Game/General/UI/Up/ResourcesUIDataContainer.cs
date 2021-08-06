//using Assets.Scripts.ECS.Entities.Game.General.UI.Data.Containers;
//using Assets.Scripts.ECS.Game.General.Systems.StartFill;
//using Photon.Realtime;
//using System;


//namespace Assets.Scripts.Workers.Info
//{
//    internal sealed class ResourcesUIDataContainer
//    {
//        private static ResourcesDataUIContainer _myContainer;

//        internal ResourcesUIDataContainer(ResourcesDataUIContainer ourContainer)
//        {
//            _myContainer = ourContainer;
//        }

//        internal static int GetAmountResources(ResourceTypes resourceType, bool key)
//        {
//            switch (resourceType)
//            {
//                case ResourceTypes.None:
//                    throw new Exception();

//                case ResourceTypes.Food:
//                    return _myContainer.FoodInfoEnt_AmountResourcesDictCom.AmountResourcesDict[key];

//                case ResourceTypes.Wood:
//                    return _myContainer.WoodInfoEnt_AmountResourcesDictCom.AmountResourcesDict[key];

//                case ResourceTypes.Ore:
//                    return _myContainer.OreInfoEnt_AmountResourcesDictCom.AmountResourcesDict[key];

//                case ResourceTypes.Iron:
//                    return _myContainer.IronInfoEnt_AmountResourcesDictCom.AmountResourcesDict[key];

//                case ResourceTypes.Gold:
//                    return _myContainer.GoldInfoEnt_AmountResourcesDictCom.AmountResourcesDict[key];

//                default:
//                    throw new Exception();
//            }
//        }
//        internal static void SetAmountResources(ResourceTypes resourceType, bool key, int value)
//        {
//            switch (resourceType)
//            {
//                case ResourceTypes.None:
//                    throw new Exception();

//                case ResourceTypes.Food:
//                    _myContainer.FoodInfoEnt_AmountResourcesDictCom.AmountResourcesDict[key] = value;
//                    break;

//                case ResourceTypes.Wood:
//                    _myContainer.WoodInfoEnt_AmountResourcesDictCom.AmountResourcesDict[key] = value;
//                    break;

//                case ResourceTypes.Ore:
//                    _myContainer.OreInfoEnt_AmountResourcesDictCom.AmountResourcesDict[key] = value;
//                    break;

//                case ResourceTypes.Iron:
//                    _myContainer.IronInfoEnt_AmountResourcesDictCom.AmountResourcesDict[key] = value;
//                    break;

//                case ResourceTypes.Gold:
//                    _myContainer.GoldInfoEnt_AmountResourcesDictCom.AmountResourcesDict[key] = value;
//                    break;

//                default:
//                    throw new Exception();
//            }
//        }
//        internal static void AddAmountResources(ResourceTypes resourceType, bool key, int adding = 1) => SetAmountResources(resourceType, key, GetAmountResources(resourceType, key) + adding);
//        internal static void TakeAmountResources(ResourceTypes resourceType, bool key, int taking = 1) => SetAmountResources(resourceType, key, GetAmountResources(resourceType, key) - taking);


//    }

//}
