using Leopotam.Ecs;
using UnityEngine;

internal class WarningUISystem : SystemReduction, IEcsRunSystem
{
    private EcsComponentRef<DonerComponent> _donerComponentRef = default;
    private EcsComponentRef<TakerUnitUnitComponent> _selectorUnitComponent = default;
    private ref EconomyUIComponent EconomyUIComponent => ref _entitiesGeneralManager.EconomyUIComponent;

    private float _timer;

    internal WarningUISystem(ECSmanager eCSmanager) :base(eCSmanager)
    {
        _donerComponentRef = eCSmanager.EntitiesGeneralManager.DonerComponentRef;
        _selectorUnitComponent = eCSmanager.EntitiesGeneralManager.SelectorUnitComponent;
    }

    public void Run()
    {
        if (_donerComponentRef.Unref().NeedSetKing)
        {
            _selectorUnitComponent.Unref().GameDownTakeUnit0.image.color = Color.red;
        }

        if (EconomyUIComponent.NeedFood
            || EconomyUIComponent.NeedWood
            || EconomyUIComponent.NeedOre
            || EconomyUIComponent.NeedIron
            || EconomyUIComponent.NeedGold)
        {

            _timer += Time.deltaTime;

            if (EconomyUIComponent.NeedFood) EconomyUIComponent.FoodText.color = Color.red;
            if (EconomyUIComponent.NeedWood) EconomyUIComponent.WoodText.color = Color.red;
            if (EconomyUIComponent.NeedOre) EconomyUIComponent.OreText.color = Color.red;
            if (EconomyUIComponent.NeedIron) EconomyUIComponent.IronText.color = Color.red;
            if (EconomyUIComponent.NeedGold) EconomyUIComponent.GoldText.color = Color.red;

            if (_timer >= 2)
            {
                EconomyUIComponent.NeedFood = false;
                EconomyUIComponent.NeedWood = false;
                EconomyUIComponent.NeedOre = false;
                EconomyUIComponent.NeedIron = false;
                EconomyUIComponent.NeedGold = false;

                EconomyUIComponent.FoodText.color = Color.white;
                EconomyUIComponent.WoodText.color = Color.white;
                EconomyUIComponent.OreText.color = Color.white;
                EconomyUIComponent.IronText.color = Color.white;
                EconomyUIComponent.GoldText.color = Color.white;

                _timer = 0;
            }
        }

    }
}
