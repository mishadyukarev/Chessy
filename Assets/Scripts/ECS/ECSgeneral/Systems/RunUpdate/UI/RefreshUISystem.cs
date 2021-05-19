﻿
using Leopotam.Ecs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static MainGame;

internal class RefreshUISystem : SystemGeneralReduction, IEcsRunSystem
{
    private GameObject _inGameRefreshZoneGO;
    private Image _inGameRefreshZoneRefreshImage;
    private TextMeshProUGUI _inGameRefreshZoneRefreshText;

    private float _timer;

    internal RefreshUISystem(ECSmanager eCSmanager) :base(eCSmanager)
    {
        _inGameRefreshZoneGO = InstanceGame.GameObjectPool.InGameRefreshZoneGO;
        _inGameRefreshZoneRefreshImage = InstanceGame.GameObjectPool.InGameRefreshZoneRefreshImage;
        _inGameRefreshZoneRefreshText = InstanceGame.GameObjectPool.InGameRefreshZoneRefreshText;
    }

    public void Run()
    {
        if (_eGM.UpdatorEntityActiveComponent.IsActived)
        {
            _inGameRefreshZoneRefreshText.text = "Motion: " + _eGM.UpdatorEntityAmountComponent.Amount;
            _inGameRefreshZoneGO.SetActive(true);

            _timer += Time.deltaTime;

            if(_timer >= 1)
            {
                _inGameRefreshZoneGO.SetActive(false);
                _eGM.UpdatorEntityActiveComponent.IsActived = false;
                _timer = 0;
            }          
        }
    }
}
