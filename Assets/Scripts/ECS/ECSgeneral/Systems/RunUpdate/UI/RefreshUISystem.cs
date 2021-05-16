
using Leopotam.Ecs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static MainGame;

internal class RefreshUISystem : IEcsRunSystem
{
    private EcsComponentRef<InfoRefreshComponent> _refreshComponentRef;
    private EcsComponentRef<InfoMotionComponent> _infoMotionComponentRef;

    private GameObject _inGameRefreshZoneGO;
    private Image _inGameRefreshZoneRefreshImage;
    private TextMeshProUGUI _inGameRefreshZoneRefreshText;

    private float _timer;

    internal RefreshUISystem(ECSmanager eCSmanager)
    {
        _refreshComponentRef = eCSmanager.EntitiesGeneralManager.RefreshComponentRef;
        _infoMotionComponentRef = eCSmanager.EntitiesGeneralManager.InfoMotionComponentRef;

        _inGameRefreshZoneGO = InstanceGame.GameObjectPool.InGameRefreshZoneGO;
        _inGameRefreshZoneRefreshImage = InstanceGame.GameObjectPool.InGameRefreshZoneRefreshImage;
        _inGameRefreshZoneRefreshText = InstanceGame.GameObjectPool.InGameRefreshZoneRefreshText;
    }

    public void Run()
    {
        if (_refreshComponentRef.Unref().IsRefreshed)
        {
            _inGameRefreshZoneRefreshText.text = "Motion: " + _infoMotionComponentRef.Unref().NumberMotion;
            _inGameRefreshZoneGO.SetActive(true);

            _timer += Time.deltaTime;

            if(_timer >= 1)
            {
                _inGameRefreshZoneGO.SetActive(false);
                _refreshComponentRef.Unref().IsRefreshed = false;
                _timer = 0;
            }          
        }
    }
}
