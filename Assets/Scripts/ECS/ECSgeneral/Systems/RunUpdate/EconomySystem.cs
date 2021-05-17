using Leopotam.Ecs;
using Photon.Realtime;
using System.Collections.Generic;
using static MainGame;

internal class EconomySystem : SystemGeneralReduction, IEcsRunSystem
{
    internal EconomySystem(ECSmanager eCSmanager) : base(eCSmanager)
    {
        
    }

    public void Run()
    {
        _eGM.FoodEntityAmountDictionaryComponent.AmountDictionary.TryGetValue(InstanceGame.IsMasterClient, out int foodAmount);
        _eGM.WoodEntityAmountDictionaryComponent.AmountDictionary.TryGetValue(InstanceGame.IsMasterClient, out int woodAmount);
        _eGM.OreEntityAmountDictionaryComponent.AmountDictionary.TryGetValue(InstanceGame.IsMasterClient, out int oreAmount);
        _eGM.IronEntityAmountDictionaryComponent.AmountDictionary.TryGetValue(InstanceGame.IsMasterClient, out int ironAmount);
        _eGM.GoldEntityAmountDictionaryComponent.AmountDictionary.TryGetValue(InstanceGame.IsMasterClient, out int goldAmount);


        _eGM.FoodEntityTextMeshProGUIComponent.TextMeshProUGUI.text = foodAmount.ToString();
        _eGM.WoodEntityTextMeshProGUIComponent.TextMeshProUGUI.text = woodAmount.ToString();
        _eGM.OreEntityTextMeshProGUIComponent.TextMeshProUGUI.text = oreAmount.ToString();
        _eGM.IronEntityTextMeshProGUIComponent.TextMeshProUGUI.text = ironAmount.ToString();
        _eGM.GoldEntityTextMeshProGUIComponent.TextMeshProUGUI.text = goldAmount.ToString();
    }
}
