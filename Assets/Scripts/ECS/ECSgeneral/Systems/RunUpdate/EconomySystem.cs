using Leopotam.Ecs;
using Photon.Realtime;
using System.Collections.Generic;
using static MainGame;

internal class EconomySystem : SystemGeneralReduction, IEcsRunSystem
{
    internal EconomySystem(ECSmanager eCSmanager) : base(eCSmanager) { }

    public void Run()
    {
        _eGM.FoodEntityTextMeshProGUIComponent.TextMeshProUGUI.text = _eGM.FoodEntityAmountDictionaryComponent.AmountDictionary[InstanceGame.IsMasterClient].ToString();
        _eGM.WoodEntityTextMeshProGUIComponent.TextMeshProUGUI.text = _eGM.WoodEntityAmountDictionaryComponent.AmountDictionary[InstanceGame.IsMasterClient].ToString();
        _eGM.OreEntityTextMeshProGUIComponent.TextMeshProUGUI.text = _eGM.OreEntityAmountDictionaryComponent.AmountDictionary[InstanceGame.IsMasterClient].ToString();
        _eGM.IronEntityTextMeshProGUIComponent.TextMeshProUGUI.text = _eGM.IronEntityAmountDictionaryComponent.AmountDictionary[InstanceGame.IsMasterClient].ToString();
        _eGM.GoldEntityTextMeshProGUIComponent.TextMeshProUGUI.text = _eGM.GoldEntityAmountDictionaryComponent.AmountDictionary[InstanceGame.IsMasterClient].ToString();
    }
}
