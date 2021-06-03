using Leopotam.Ecs;
using static Main;

internal class EconomyUISystem : SystemGeneralReduction
{
    internal EconomyUISystem(){ }

    public override void Run()
    {
        base.Run();

        _eGM.FoodEntityTextMeshProGUIComponent.TextMeshProUGUI.text = _eGM.FoodEnt_AmountDictCom.AmountDict[Instance.IsMasterClient].ToString();
        _eGM.WoodEntityTextMeshProGUIComponent.TextMeshProUGUI.text = _eGM.WoodEAmountDictC.AmountDict[Instance.IsMasterClient].ToString();
        _eGM.OreEntityTextMeshProGUIComponent.TextMeshProUGUI.text = _eGM.OreEAmountDictC.AmountDict[Instance.IsMasterClient].ToString();
        _eGM.IronEntityTextMeshProGUIComponent.TextMeshProUGUI.text = _eGM.IronEAmountDictC.AmountDict[Instance.IsMasterClient].ToString();
        _eGM.GoldEntityTextMeshProGUIComponent.TextMeshProUGUI.text = _eGM.GoldEAmountDictC.AmountDict[Instance.IsMasterClient].ToString();
    }
}
