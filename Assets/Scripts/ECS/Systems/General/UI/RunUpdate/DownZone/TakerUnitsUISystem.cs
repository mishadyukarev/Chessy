using Assets.Scripts;
using Assets.Scripts.Workers.Info;
using static Assets.Scripts.Main;

internal sealed class TakerUnitsUISystem : RPCGeneralSystemReduction
{
    public override void Run()
    {
        base.Run();

        if (InfoUnitsWorker.IsSettedKing(Instance.IsMasterClient))
            _eGGUIM.TakerKingEnt_ButtonCom.Button.gameObject.SetActive(false);
        else _eGGUIM.TakerKingEnt_ButtonCom.Button.gameObject.SetActive(true);
    }
}
