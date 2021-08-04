using Assets.Scripts;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Game.UI;

internal sealed class ReadyMasterSystem : SystemMasterReduction
{
    private bool IsReady => _eMM.ReadyEnt_IsActivatedCom.IsActivated;

    public override void Run()
    {
        base.Run();


        MiddleUIDataContainer.SetIsReady(RpcMasterDataContainer.InfoFrom.Sender.IsMasterClient, IsReady);

        if (MiddleUIDataContainer.IsReady(true) && MiddleUIDataContainer.IsReady(false))
        {
            MiddleUIDataContainer.IsStartedGame = true;
        }

        else
        {
            MiddleUIDataContainer.IsStartedGame = false;
        }
    }
}
