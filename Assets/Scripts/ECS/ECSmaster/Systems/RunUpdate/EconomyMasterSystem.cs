using Leopotam.Ecs;
using static MainGame;

internal class EconomyMasterSystem : SystemMasterReduction, IEcsRunSystem
{
    internal EconomyMasterSystem(ECSmanager eCSmanager) : base(eCSmanager)
    {
        _eGM.FoodEntityAmountDictionaryComponent.AmountDictionary[true] = InstanceGame.StartValuesGameConfig.AMOUNT_FOOD_MASTER;
        _eGM.WoodEntityAmountDictionaryComponent.AmountDictionary[true] = InstanceGame.StartValuesGameConfig.AMOUNT_WOOD_MASTER;
        _eGM.OreEntityAmountDictionaryComponent.AmountDictionary[true] = InstanceGame.StartValuesGameConfig.AMOUNT_ORE_MASTER;
        _eGM.IronEntityAmountDictionaryComponent.AmountDictionary[true] = InstanceGame.StartValuesGameConfig.AMOUNT_IRON_MASTER;
        _eGM.GoldEntityAmountDictionaryComponent.AmountDictionary[true] = InstanceGame.StartValuesGameConfig.AMOUNT_GOLD_MASTER;

        _eGM.FoodEntityAmountDictionaryComponent.AmountDictionary[false] = InstanceGame.StartValuesGameConfig.AMOUNT_FOOD_OTHER;
        _eGM.WoodEntityAmountDictionaryComponent.AmountDictionary[false] = InstanceGame.StartValuesGameConfig.AMOUNT_WOOD_OTHER;
        _eGM.OreEntityAmountDictionaryComponent.AmountDictionary[false] = InstanceGame.StartValuesGameConfig.AMOUNT_ORE_OTHER;
        _eGM.IronEntityAmountDictionaryComponent.AmountDictionary[false] = InstanceGame.StartValuesGameConfig.AMOUNT_IRON_OTHER;
        _eGM.GoldEntityAmountDictionaryComponent.AmountDictionary[false] = InstanceGame.StartValuesGameConfig.AMOUNT_GOLD_OTHER;
    }

    public void Run()
    {

    }
}
