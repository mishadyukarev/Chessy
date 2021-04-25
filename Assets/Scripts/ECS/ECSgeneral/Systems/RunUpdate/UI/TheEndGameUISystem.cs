using Leopotam.Ecs;
using TMPro;
using UnityEngine;

internal class TheEndGameUISystem : IEcsRunSystem
{
    private EcsComponentRef<TheEndGameComponent> _theEndGameComponentRef = default;

    private RectTransform _parentTheEndGameZone;
    private TextMeshProUGUI _theEndGameText;


    internal TheEndGameUISystem(ECSmanager eCSmanager, SupportGameManager supportGameManager, PhotonGameManager photonManager, StartSpawnGameManager startSpawnGameManager)
    {
        _theEndGameComponentRef = eCSmanager.EntitiesGeneralManager.TheEndGameComponentRef;

        _parentTheEndGameZone = startSpawnGameManager.ParentTheEndGameZone;
        _theEndGameText = startSpawnGameManager.TheEndGameText;
    }

    public void Run()
    {
        if (_theEndGameComponentRef.Unref().IsEndGame)
        {
            _parentTheEndGameZone.gameObject.SetActive(true);
            if (_theEndGameComponentRef.Unref().PlayerWinner.IsLocal) _theEndGameText.text = "You're WINNER!";
            else _theEndGameText.text = "You're loser :(";
        }
    }
}
