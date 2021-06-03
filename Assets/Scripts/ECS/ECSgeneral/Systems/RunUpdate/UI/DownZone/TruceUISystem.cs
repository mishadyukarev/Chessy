using UnityEngine;
using UnityEngine.UI;
using static Main;

internal sealed class TruceUISystem : RPCGeneralSystemReduction
{
    private bool _isInited = false;
    private bool IsCurrentTruced => _eGM.TruceEnt_ActivatedDictCom.IsActivatedDictionary[Instance.IsMasterClient];
    private Button CurrentButton => _eGM.TruceEnt_ButtonCom.Button;


    public override void Run()
    {
        base.Run();

        if (!_isInited)
        {
            CurrentButton.onClick.AddListener(Truce);
            _isInited = true;
        }

        if (IsCurrentTruced) CurrentButton.image.color = Color.red;
        else CurrentButton.image.color = Color.white;
    }

    private void Truce() => _photonPunRPC.TruceToMaster(!IsCurrentTruced);
}
