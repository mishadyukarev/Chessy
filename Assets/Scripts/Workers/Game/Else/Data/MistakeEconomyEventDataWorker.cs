using Assets.Scripts.ECS.Entities.Game.General.Else.Data.Containers;
using System;
using UnityEngine.Events;

namespace Assets.Scripts.Workers.Game.Else.Data
{
    internal sealed class MistakeEconomyEventDataWorker
    {
        private static MistakeEconomyEventDataContainer _container;

        internal MistakeEconomyEventDataWorker(MistakeEconomyEventDataContainer eventDataContainerEnts)
        {
            _container = eventDataContainerEnts;
        }

        private static UnityEvent GetUnityEventEconomyMistake(ResourceTypes resourceType)
        {
            switch (resourceType)
            {
                case ResourceTypes.None:
                    throw new Exception();

                case ResourceTypes.Food:
                    return _container.MistakeFoodEventEnt_UnityEventCom.UnityEvent;

                case ResourceTypes.Wood:
                    return _container.MistakeWoodEventEnt_UnityEventCom.UnityEvent;

                case ResourceTypes.Ore:
                    return _container.MistakeOreEventEnt_UnityEventCom.UnityEvent;

                case ResourceTypes.Iron:
                    return _container.MistakeIronEventEnt_UnityEventCom.UnityEvent;

                case ResourceTypes.Gold:
                    return _container.MistakeGoldEventEnt_UnityEventCom.UnityEvent;

                default:
                    throw new Exception();
            }
        }

        internal static void AddListenerEconomyMistake(ResourceTypes resourceType, UnityAction unityAction)
            => GetUnityEventEconomyMistake(resourceType).AddListener(unityAction);

        internal static void InvokeEconomyMistake(ResourceTypes resourceType) => GetUnityEventEconomyMistake(resourceType).Invoke();
    }
}