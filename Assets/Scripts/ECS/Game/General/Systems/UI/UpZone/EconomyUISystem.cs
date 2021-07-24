using Assets.Scripts;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Info;
using Assets.Scripts.Workers.UI.Info;
using Photon.Pun;
using static Assets.Scripts.Main;

internal sealed class EconomyUISystem : SystemGeneralReduction
{
    public override void Run()
    {
        base.Run();

        var amountAddingFood = InfoResourcesWorker.GetCommonAmountExtraction(ResourceTypes.Food, PhotonNetwork.IsMasterClient);

        if (amountAddingFood < 0)
            InfoResourcesUIWorker.SetAddingText(ResourceTypes.Food, amountAddingFood.ToString());
        else InfoResourcesUIWorker.SetAddingText(ResourceTypes.Food, "+ " + amountAddingFood.ToString());

        InfoResourcesUIWorker.SetMainText(ResourceTypes.Food, InfoResourcesWorker.AmountResources(ResourceTypes.Food, Instance.IsMasterClient).ToString());
        InfoResourcesUIWorker.SetMainText(ResourceTypes.Wood, InfoResourcesWorker.AmountResources(ResourceTypes.Wood, Instance.IsMasterClient).ToString());
        InfoResourcesUIWorker.SetMainText(ResourceTypes.Ore, InfoResourcesWorker.AmountResources(ResourceTypes.Ore, Instance.IsMasterClient).ToString());
        InfoResourcesUIWorker.SetMainText(ResourceTypes.Iron, InfoResourcesWorker.AmountResources(ResourceTypes.Iron, Instance.IsMasterClient).ToString());
        InfoResourcesUIWorker.SetMainText(ResourceTypes.Gold, InfoResourcesWorker.AmountResources(ResourceTypes.Gold, Instance.IsMasterClient).ToString());
    }
}
