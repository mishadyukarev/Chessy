using Assets.Scripts;
using Assets.Scripts.Workers.Info;
using Assets.Scripts.Workers.UI.Info;
using static Assets.Scripts.Main;

internal sealed class EconomyUISystem : SystemGeneralReduction
{
    public override void Run()
    {
        base.Run();

        //var amountAddingFood = InfoResourcesDataWorker.GetCommonAmountExtraction(ResourceTypes.Food, PhotonNetwork.IsMasterClient);

        //if (amountAddingFood < 0)
        //    InfoResourcesUIWorker.SetAddingText(ResourceTypes.Food, amountAddingFood.ToString());
        //else InfoResourcesUIWorker.SetAddingText(ResourceTypes.Food, "+ " + amountAddingFood.ToString());

        InfoResourcesUIWorker.SetMainText(ResourceTypes.Food, InfoResourcesDataWorker.AmountResources(ResourceTypes.Food, Instance.IsMasterClient).ToString());
        InfoResourcesUIWorker.SetMainText(ResourceTypes.Wood, InfoResourcesDataWorker.AmountResources(ResourceTypes.Wood, Instance.IsMasterClient).ToString());
        InfoResourcesUIWorker.SetMainText(ResourceTypes.Ore, InfoResourcesDataWorker.AmountResources(ResourceTypes.Ore, Instance.IsMasterClient).ToString());
        InfoResourcesUIWorker.SetMainText(ResourceTypes.Iron, InfoResourcesDataWorker.AmountResources(ResourceTypes.Iron, Instance.IsMasterClient).ToString());
        InfoResourcesUIWorker.SetMainText(ResourceTypes.Gold, InfoResourcesDataWorker.AmountResources(ResourceTypes.Gold, Instance.IsMasterClient).ToString());
    }
}
