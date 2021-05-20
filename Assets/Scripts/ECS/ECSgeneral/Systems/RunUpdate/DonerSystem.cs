using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.UI;
using static MainGame;

internal class DonerSystem : RPCGeneralReduction, IEcsRunSystem
{
    private bool IsCurrentDone => _eGM.DonerEntityIsActivatedDictionaryComponent.IsActivatedDictionary[Instance.IsMasterClient];
    private Button CurrentButton => _eGM.DonerEntityButtonComponent.Button;

    internal DonerSystem(ECSmanager eCSmanager) : base(eCSmanager)
    {
        CurrentButton.onClick.AddListener(delegate { Done(); });
    }

    public void Run()
    {
        if (IsCurrentDone) CurrentButton.image.color = Color.red;
        else CurrentButton.image.color = Color.white;
    }

    private void Done() => _photonPunRPC.DoneToMaster(!IsCurrentDone);
}
