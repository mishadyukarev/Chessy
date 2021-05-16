using Leopotam.Ecs;
using UnityEngine;

internal class WarningUISystem : IEcsRunSystem
{
    private EcsComponentRef<DonerComponent> _donerComponentRef = default;
    private EcsComponentRef<TakerUnitUnitComponent> _selectorUnitComponent = default;
    private EcsComponentRef<EconomyUIComponent> _economyUIComponentRef = default;

    private float _timer;

    internal WarningUISystem(ECSmanager eCSmanager)
    {
        _donerComponentRef = eCSmanager.EntitiesGeneralManager.DonerComponentRef;
        _selectorUnitComponent = eCSmanager.EntitiesGeneralManager.SelectorUnitComponent;
        _economyUIComponentRef = eCSmanager.EntitiesGeneralManager.EconomyUIComponentRef;
    }

    public void Run()
    {
        if (_donerComponentRef.Unref().IsMistaked)
        {
            _selectorUnitComponent.Unref().GameDownTakeUnit0.image.color = Color.red;
        }

        if (_economyUIComponentRef.Unref().NeedFood
            || _economyUIComponentRef.Unref().NeedWood
            || _economyUIComponentRef.Unref().NeedOre
            || _economyUIComponentRef.Unref().NeedIron
            || _economyUIComponentRef.Unref().NeedGold)
        {

            _timer += Time.deltaTime;

            if (_economyUIComponentRef.Unref().NeedFood) _economyUIComponentRef.Unref().FoodText.color = Color.red;
            if (_economyUIComponentRef.Unref().NeedWood) _economyUIComponentRef.Unref().WoodText.color = Color.red;
            if (_economyUIComponentRef.Unref().NeedOre) _economyUIComponentRef.Unref().OreText.color = Color.red;
            if (_economyUIComponentRef.Unref().NeedIron) _economyUIComponentRef.Unref().IronText.color = Color.red;
            if (_economyUIComponentRef.Unref().NeedGold) _economyUIComponentRef.Unref().GoldText.color = Color.red;

            if (_timer >= 2)
            {
                _economyUIComponentRef.Unref().NeedFood = false;
                _economyUIComponentRef.Unref().NeedWood = false;
                _economyUIComponentRef.Unref().NeedOre = false;
                _economyUIComponentRef.Unref().NeedIron = false;
                _economyUIComponentRef.Unref().NeedGold = false;

                _economyUIComponentRef.Unref().FoodText.color = Color.white;
                _economyUIComponentRef.Unref().WoodText.color = Color.white;
                _economyUIComponentRef.Unref().OreText.color = Color.white;
                _economyUIComponentRef.Unref().IronText.color = Color.white;
                _economyUIComponentRef.Unref().GoldText.color = Color.white;

                _timer = 0;
            }
        }

    }
}
