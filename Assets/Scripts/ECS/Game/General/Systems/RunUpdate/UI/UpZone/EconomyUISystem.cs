using Assets.Scripts;
using static Assets.Scripts.Main;

internal sealed class EconomyUISystem : SystemGeneralReduction
{
    public override void Run()
    {
        base.Run();

        _eGM.EconomyUIEnt_EconomyUICom.SetText(ResourceTypes.Food, _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Food, Instance.IsMasterClient).ToString());
        _eGM.EconomyUIEnt_EconomyUICom.SetText(ResourceTypes.Wood, _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Wood, Instance.IsMasterClient).ToString());
        _eGM.EconomyUIEnt_EconomyUICom.SetText(ResourceTypes.Ore, _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Ore, Instance.IsMasterClient).ToString());
        _eGM.EconomyUIEnt_EconomyUICom.SetText(ResourceTypes.Iron, _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Iron, Instance.IsMasterClient).ToString());
        _eGM.EconomyUIEnt_EconomyUICom.SetText(ResourceTypes.Gold, _eGM.EconomyEnt_EconomyCom.AmountResources(ResourceTypes.Gold, Instance.IsMasterClient).ToString());
    }
}
