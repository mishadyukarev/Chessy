using Assets.Scripts.ECS.Components;
using Assets.Scripts.ECS.Components.UI;
using Leopotam.Ecs;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.ECS.Entities.Game.General.UI.Vis.Containers
{
    internal sealed class ResourcesVisUIContainer
    {
        private EcsEntity _foodInfoUIEnt;
        internal ref TextMeshProUGUIComponent FoodInfoUIEnt_TextMeshProUGUICom => ref _foodInfoUIEnt.Get<TextMeshProUGUIComponent>();
        internal ref UnityEventComponent FoodInfoUIEnt_MistakeResourcesUICom => ref _foodInfoUIEnt.Get<UnityEventComponent>();
        internal ref AddingTMPUIComponent FoodInfoUIEnt_AddingTMPUICom => ref _foodInfoUIEnt.Get<AddingTMPUIComponent>();


        private EcsEntity _woodInfoUIEnt;
        internal ref TextMeshProUGUIComponent WoodInfoUIEnt_TextMeshProUGUICom => ref _woodInfoUIEnt.Get<TextMeshProUGUIComponent>();
        internal ref UnityEventComponent WoodInfoUIEnt_MistakeResourcesUICom => ref _woodInfoUIEnt.Get<UnityEventComponent>();
        internal ref AddingTMPUIComponent WoodInfoUIEnt_AddingTMPUICom => ref _woodInfoUIEnt.Get<AddingTMPUIComponent>();


        private EcsEntity _oreInfoUIEnt;
        internal ref TextMeshProUGUIComponent OreInfoUIEnt_TextMeshProUGUICom => ref _oreInfoUIEnt.Get<TextMeshProUGUIComponent>();
        internal ref UnityEventComponent OreInfoUIEnt_MistakeResourcesUICom => ref _oreInfoUIEnt.Get<UnityEventComponent>();
        internal ref AddingTMPUIComponent OreInfoUIEnt_AddingTMPUICom => ref _oreInfoUIEnt.Get<AddingTMPUIComponent>();


        private EcsEntity _ironInfoUIEnt;
        internal ref TextMeshProUGUIComponent IronInfoUIEnt_TextMeshProUGUICom => ref _ironInfoUIEnt.Get<TextMeshProUGUIComponent>();
        internal ref UnityEventComponent IronInfoUIEnt_MistakeResourcesUICom => ref _ironInfoUIEnt.Get<UnityEventComponent>();


        private EcsEntity _goldInfoUIEnt;
        internal ref TextMeshProUGUIComponent GoldInfoUIEnt_TextMeshProUGUICom => ref _goldInfoUIEnt.Get<TextMeshProUGUIComponent>();
        internal ref UnityEventComponent GoldInfoUIEnt_MistakeResourcesUICom => ref _goldInfoUIEnt.Get<UnityEventComponent>();


        internal ResourcesVisUIContainer(EcsWorld gameWorld, GameObject upZoneGO)
        {
            _foodInfoUIEnt = gameWorld.NewEntity()
                .Replace(new TextMeshProUGUIComponent(upZoneGO.transform.Find("FoodAmount").GetComponent<TextMeshProUGUI>()))
                .Replace(new UnityEventComponent(new UnityEvent()))
                .Replace(new AddingTMPUIComponent(upZoneGO.transform.Find("FoodAdding_TMP").GetComponent<TextMeshProUGUI>()));

            _woodInfoUIEnt = gameWorld.NewEntity()
                .Replace(new TextMeshProUGUIComponent(upZoneGO.transform.Find("WoodAmount").GetComponent<TextMeshProUGUI>()))
                .Replace(new UnityEventComponent(new UnityEvent()))
                .Replace(new AddingTMPUIComponent(upZoneGO.transform.Find("WoodAdding_TMP").GetComponent<TextMeshProUGUI>()));

            _oreInfoUIEnt = gameWorld.NewEntity()
                .Replace(new TextMeshProUGUIComponent(upZoneGO.transform.Find("OreAmount").GetComponent<TextMeshProUGUI>()))
                .Replace(new UnityEventComponent(new UnityEvent()))
                .Replace(new AddingTMPUIComponent(upZoneGO.transform.Find("OreAdding_TMP").GetComponent<TextMeshProUGUI>()));

            _ironInfoUIEnt = gameWorld.NewEntity()
                .Replace(new TextMeshProUGUIComponent(upZoneGO.transform.Find("IronAmount").GetComponent<TextMeshProUGUI>()))
                .Replace(new UnityEventComponent(new UnityEvent()));

            _goldInfoUIEnt = gameWorld.NewEntity()
                .Replace(new TextMeshProUGUIComponent(upZoneGO.transform.Find("GoldAmount").GetComponent<TextMeshProUGUI>()))
                .Replace(new UnityEventComponent(new UnityEvent()));
        }
    }
}
