using UnityEngine;
using static Main;

internal sealed class ReadyUISystem : RPCGeneralReduction
{
    private bool IsCurrentReady => _eGM.ReadyEnt_ReadyCom.IsActivatedDictionary[Instance.IsMasterClient];

    internal ReadyUISystem()
    {
        Instance.CanvasGameManager.ReadyButton.onClick.AddListener(delegate { Ready(); });
    }

    public override void Run()
    {
        base.Run();

        Debug.Log(_eGM.ReadyEnt_ReadyCom.IsActivatedDictionary[Instance.IsMasterClient]);

        if (_eGM.ReadyEnt_ReadyCom.IsSkipped)
        {
            Instance.CanvasGameManager.ParentReadyZone.gameObject.SetActive(false);
        }
        else Instance.CanvasGameManager.ParentReadyZone.gameObject.SetActive(true);

        if (IsCurrentReady) Instance.CanvasGameManager.ReadyButton.image.color = Color.red;
        else Instance.CanvasGameManager.ReadyButton.image.color = Color.white;
    }

    private void Ready() => _photonPunRPC.ReadyToMaster(!IsCurrentReady);
}
