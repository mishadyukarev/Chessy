using UnityEngine;
using UnityEngine.UI;
using static MainGame;

internal sealed class TruceUISystem : RPCGeneralReduction
{
    private bool IsCurrentTruced => _eGM.TruceEnt_ActivatedDictCom.IsActivatedDictionary[Instance.IsMasterClient];
    private Button CurrentButton => _eGM.TruceEnt_ButtonCom.Button;

    internal TruceUISystem(ECSmanager eCSmanager) : base(eCSmanager)
    {
        CurrentButton.onClick.AddListener(Truce);
    }

    public override void Run()
    {
        base.Run();

        if (IsCurrentTruced) CurrentButton.image.color = Color.red;
        else CurrentButton.image.color = Color.white;
    }

    private void Truce() => _photonPunRPC.TruceToMaster(!IsCurrentTruced);
}
