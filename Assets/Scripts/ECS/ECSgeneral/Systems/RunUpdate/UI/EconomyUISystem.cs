using Leopotam.Ecs;
using TMPro;

internal class EconomyUISystem : SystemReduction, IEcsRunSystem
{
    private ref EconomyComponent EconomyComponent => ref _entitiesGeneralManager.EconomyEntity.Get<EconomyComponent>();

    private TextMeshProUGUI _goldAmountText;
    private TextMeshProUGUI _foodAmountText;
    private TextMeshProUGUI _woodAmountText;
    private TextMeshProUGUI _oreAmountText;
    private TextMeshProUGUI _ironAmountText;

    internal EconomyUISystem(ECSmanager eCSmanager) : base(eCSmanager)
    {
        _goldAmountText = MainGame.InstanceGame.GameObjectPool.GoldAmmountText;
        _foodAmountText = MainGame.InstanceGame.GameObjectPool.FoodAmmountText;
        _woodAmountText = MainGame.InstanceGame.GameObjectPool.WoodAmmountText;
        _oreAmountText = MainGame.InstanceGame.GameObjectPool.OreAmmountText;
        _ironAmountText = MainGame.InstanceGame.GameObjectPool.IronAmmountText;
    }

    public void Run()
    {
        _foodAmountText.text = EconomyComponent.CurrentFood.ToString();
        _woodAmountText.text = EconomyComponent.CurrentWood.ToString();
        _oreAmountText.text = EconomyComponent.CurrentOre.ToString();
        _ironAmountText.text = EconomyComponent.CurrentIron.ToString();
        _goldAmountText.text = EconomyComponent.CurrentGold.ToString();
    }
}
