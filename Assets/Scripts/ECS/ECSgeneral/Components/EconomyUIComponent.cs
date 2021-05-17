using TMPro;

internal struct EconomyUIComponent
{
    internal bool NeedFood;
    internal bool NeedWood;
    internal bool NeedOre;
    internal bool NeedIron;
    internal bool NeedGold;


    internal TextMeshProUGUI FoodText;
    internal TextMeshProUGUI WoodText;
    internal TextMeshProUGUI OreText;
    internal TextMeshProUGUI IronText;
    internal TextMeshProUGUI GoldText;

    internal EconomyUIComponent(GameObjectPool gameObjectPool)
    {
        NeedFood = default;
        NeedWood = default;
        NeedOre = default;
        NeedIron = default;
        NeedGold = default;

        FoodText = gameObjectPool.FoodAmmountText;
        WoodText = gameObjectPool.WoodAmmountText;
        OreText = gameObjectPool.OreAmmountText;
        IronText = gameObjectPool.IronAmmountText;
        GoldText = gameObjectPool.GoldAmmountText;
    }
}
