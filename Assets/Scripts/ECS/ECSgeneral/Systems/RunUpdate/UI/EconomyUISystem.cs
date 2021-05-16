﻿using Leopotam.Ecs;
using TMPro;

internal class EconomyUISystem : IEcsRunSystem
{
    private EcsComponentRef<EconomyComponent> _economyComponentRef = default;

    private TextMeshProUGUI _goldAmountText;
    private TextMeshProUGUI _foodAmountText;
    private TextMeshProUGUI _woodAmountText;
    private TextMeshProUGUI _oreAmountText;
    private TextMeshProUGUI _ironAmountText;

    internal EconomyUISystem(ECSmanager eCSmanager)
    {
        _economyComponentRef = eCSmanager.EntitiesGeneralManager.EconomyComponentRef;

        _goldAmountText = MainGame.InstanceGame.GameObjectPool.GoldAmmountText;
        _foodAmountText = MainGame.InstanceGame.GameObjectPool.FoodAmmountText;
        _woodAmountText = MainGame.InstanceGame.GameObjectPool.WoodAmmountText;
        _oreAmountText = MainGame.InstanceGame.GameObjectPool.OreAmmountText;
        _ironAmountText = MainGame.InstanceGame.GameObjectPool.IronAmmountText;
    }

    public void Run()
    {
        _goldAmountText.text = _economyComponentRef.Unref().Gold.ToString();
        _foodAmountText.text = _economyComponentRef.Unref().Food.ToString();
        _woodAmountText.text = _economyComponentRef.Unref().Wood.ToString();
        _oreAmountText.text = _economyComponentRef.Unref().Ore.ToString();
        _ironAmountText.text = _economyComponentRef.Unref().Iron.ToString();
    }
}
