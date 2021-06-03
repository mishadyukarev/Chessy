using static Main;

internal sealed class EconomyUISystem : SystemGeneralReduction
{
    public override void Run()
    {
        base.Run();

        _eGM.FoodEntityTextMeshProGUIComponent.TextMeshProUGUI.text = _eGM.EconomyEnt_EconomyCom.Food(Instance.IsMasterClient).ToString();
        _eGM.WoodEntityTextMeshProGUIComponent.TextMeshProUGUI.text = _eGM.EconomyEnt_EconomyCom.Wood(Instance.IsMasterClient).ToString();
        _eGM.OreEntityTextMeshProGUIComponent.TextMeshProUGUI.text = _eGM.EconomyEnt_EconomyCom.Ore(Instance.IsMasterClient).ToString();
        _eGM.IronEntityTextMeshProGUIComponent.TextMeshProUGUI.text = _eGM.EconomyEnt_EconomyCom.Iron(Instance.IsMasterClient).ToString();
        _eGM.GoldEntityTextMeshProGUIComponent.TextMeshProUGUI.text = _eGM.EconomyEnt_EconomyCom.Gold(Instance.IsMasterClient).ToString();
    }
}
