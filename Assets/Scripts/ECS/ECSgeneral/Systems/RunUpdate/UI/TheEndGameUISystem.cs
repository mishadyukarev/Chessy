using Leopotam.Ecs;
using TMPro;
using UnityEngine;
using static MainGame;

internal class TheEndGameUISystem : SystemGeneralReduction, IEcsRunSystem
{
    private RectTransform _parentTheEndGameZone;
    private TextMeshProUGUI _theEndGameText;


    internal TheEndGameUISystem(ECSmanager eCSmanager) : base(eCSmanager)
    {
        _parentTheEndGameZone = Instance.ObjectPool.ParentTheEndGameZone;
        _theEndGameText = Instance.ObjectPool.TheEndGameText;
    }

    public void Run()
    {
        if (_eGM.EndGameEntEndGameCom.IsEndGame)
        {
            _parentTheEndGameZone.gameObject.SetActive(true);
            if (_eGM.EndGameEntEndGameCom.PlayerWinner.IsLocal) _theEndGameText.text = "You're WINNER!";
            else _theEndGameText.text = "You're loser :(";
        }
    }
}
