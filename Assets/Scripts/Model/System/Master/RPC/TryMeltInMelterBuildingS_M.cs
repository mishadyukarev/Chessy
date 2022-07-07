﻿using Chessy.Model.Enum;
using Chessy.Model.Values;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
namespace Chessy.Model.System
{
    public partial class SystemsModel
    {
        internal void TryMeltInMelterBuildingM(in Player sender)
        {
            var whoDoing = PhotonNetwork.OfflineMode ? PlayerTypes.First : sender.GetPlayer();


            var needRes = new Dictionary<ResourceTypes, float>();

            needRes.Add(ResourceTypes.Food, 0);
            needRes.Add(ResourceTypes.Wood, EconomyValues.WOOD_NEED_FOR_MELTING);
            needRes.Add(ResourceTypes.Ore, EconomyValues.ORE_NEED_FOR_MELTING);
            needRes.Add(ResourceTypes.Iron, 0);
            needRes.Add(ResourceTypes.Gold, 0);

            var canBuy = true;

            for (var resT = ResourceTypes.None + 1; resT < ResourceTypes.End; resT++)
            {
                if (needRes[resT] > _e.ResourcesInInventory(whoDoing, resT))
                {
                    canBuy = false;
                    break;
                }
            }

            if (canBuy)
            {
                for (var resT = ResourceTypes.None + 1; resT < ResourceTypes.End; resT++)
                {
                    _e.ResourcesInInventoryC(whoDoing).Subtract(resT, needRes[resT]);
                }

                if (_e.LessonT == LessonTypes.NeedBuildSmelterAndMeltOre)
                {
                    SetNextLesson();
                    _e.IsSelectedCity = true;
                }

                _e.ResourcesInInventoryC(whoDoing).Add(ResourceTypes.Iron, AmountResourcesAfterMelting.IRON_AFTER_MELTING);
                _e.ResourcesInInventoryC(whoDoing).Add(ResourceTypes.Gold, AmountResourcesAfterMelting.GOLD_AFTER_MELTING);

                RpcSs.ExecuteSoundActionToGeneral(sender, ClipTypes.Melting);
            }
            else
            {
               RpcSs.SimpleMistakeToGeneral(sender, needRes);
            }
        }
    }
}