using Leopotam.Ecs;
using Photon.Realtime;
using System.Collections.Generic;
using static MainGame;

internal class EconomySystem : SystemGeneralReduction, IEcsRunSystem
{
    internal EconomySystem(ECSmanager eCSmanager) : base(eCSmanager) { }

    public void Run()
    {
        _eGM.FoodEntityTextMeshProGUIComponent.TextMeshProUGUI.text = _eGM.FoodEAmountDictC.AmountDict[InstanceGame.IsMasterClient].ToString();
        _eGM.WoodEntityTextMeshProGUIComponent.TextMeshProUGUI.text = _eGM.WoodEAmountDictC.AmountDict[InstanceGame.IsMasterClient].ToString();
        _eGM.OreEntityTextMeshProGUIComponent.TextMeshProUGUI.text = _eGM.OreEAmountDictC.AmountDict[InstanceGame.IsMasterClient].ToString();
        _eGM.IronEntityTextMeshProGUIComponent.TextMeshProUGUI.text = _eGM.IronEAmountDictC.AmountDict[InstanceGame.IsMasterClient].ToString();
        _eGM.GoldEntityTextMeshProGUIComponent.TextMeshProUGUI.text = _eGM.GoldEAmountDictC.AmountDict[InstanceGame.IsMasterClient].ToString();
    }
}
