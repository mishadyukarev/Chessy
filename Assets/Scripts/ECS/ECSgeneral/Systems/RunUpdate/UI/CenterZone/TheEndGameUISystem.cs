using Leopotam.Ecs;
using TMPro;
using UnityEngine;
using static Main;

internal class TheEndGameUISystem : SystemGeneralReduction, IEcsRunSystem
{
    private RectTransform _parentTheEndGameZone;
    private TextMeshProUGUI _theEndGameText;


    internal TheEndGameUISystem()
    {
        _parentTheEndGameZone = Instance.CanvasGameManager.ParentTheEndGameZone;
        _theEndGameText = Instance.CanvasGameManager.TheEndGameText;
    }

    public override void Run()
    {
        base.Run();

        if (_eGM.EndGameEntEndGameCom.IsEndGame)
        {
            _parentTheEndGameZone.gameObject.SetActive(true);
            if (_eGM.EndGameEntEndGameCom.PlayerWinner.IsLocal) _theEndGameText.text = "You're WINNER!";
            else _theEndGameText.text = "You're loser :(";
        }
    }
}
