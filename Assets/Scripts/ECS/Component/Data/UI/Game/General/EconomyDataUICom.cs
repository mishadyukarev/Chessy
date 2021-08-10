//using System;
//using System.Collections.Generic;

//namespace Assets.Scripts.ECS.Component.View.UI.Game.General
//{
//    internal struct EconomyDataUICom
//    {
//        private Dictionary<ResourceTypes, Dictionary<bool, int>> _amountResources;

//        internal EconomyDataUICom(Dictionary<ResourceTypes, Dictionary<bool, int>> dict)
//        {
//            _amountResources = dict;

//            for (ResourceTypes resourcesType = 0; resourcesType < (ResourceTypes)Enum.GetNames(typeof(ResourceTypes)).Length; resourcesType++)
//            {
//                var dict1 = new Dictionary<bool, int>();
//                dict1.Add(true, default);

//                _amountResources.Add(resourcesType, dict1);
//            }
//        }
//    }
//}
