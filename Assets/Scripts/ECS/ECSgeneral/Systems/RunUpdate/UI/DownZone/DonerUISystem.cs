using UnityEngine;
using UnityEngine.UI;
using static Main;

internal sealed class DonerUISystem : RPCGeneralSystemReduction
{
    private bool _isInited = false;
    private bool IsCurrentDone => _eGM.DonerEntityIsActivatedDictionaryComponent.IsActivatedDictionary[Instance.IsMasterClient];
    private Button CurrentButton => _eGM.DonerEntityButtonComponent.Button;

    public override void Run()
    {
        base.Run();

        if (!_isInited)
        {
            CurrentButton.onClick.AddListener(delegate { Done(); });
            _isInited = true;
        }

        if (IsCurrentDone) CurrentButton.image.color = Color.red;
        else CurrentButton.image.color = Color.white;
    }

    private void Done() => _photonPunRPC.DoneToMaster(!IsCurrentDone);
}
