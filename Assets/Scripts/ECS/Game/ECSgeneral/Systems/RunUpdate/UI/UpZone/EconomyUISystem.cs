using static Main;

internal sealed class EconomyUISystem : SystemGeneralReduction
{
    public override void Run()
    {
        base.Run();

        _eGM.EconomyUIEnt_EconomyUICom.SetText(EconomyTypes.Food, _eGM.EconomyEnt_EconomyCom.AmountResources(EconomyTypes.Food, Instance.IsMasterClient).ToString());
        _eGM.EconomyUIEnt_EconomyUICom.SetText(EconomyTypes.Wood, _eGM.EconomyEnt_EconomyCom.AmountResources(EconomyTypes.Wood, Instance.IsMasterClient).ToString());
        _eGM.EconomyUIEnt_EconomyUICom.SetText(EconomyTypes.Ore, _eGM.EconomyEnt_EconomyCom.Ore(Instance.IsMasterClient).ToString());
        _eGM.EconomyUIEnt_EconomyUICom.SetText(EconomyTypes.Iron, _eGM.EconomyEnt_EconomyCom.Iron(Instance.IsMasterClient).ToString());
        _eGM.EconomyUIEnt_EconomyUICom.SetText(EconomyTypes.Gold, _eGM.EconomyEnt_EconomyCom.Gold(Instance.IsMasterClient).ToString());
    }
}
