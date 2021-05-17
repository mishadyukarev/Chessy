﻿using Leopotam.Ecs;
using UnityEngine.UI;
using static MainGame;

internal class TakerUnitUISystem : SystemGeneralReduction, IEcsRunSystem
{
    private EcsComponentRef<TakerUnitUnitComponent> _selectorUnitComponentRef = default;

    private PhotonPunRPC _photonPunRPC = default;

    internal Button _gameDownTakeUnit1;
    internal Button _gameDownTakeUnit2;
    internal Button _gameDownTakeUnit3;

    internal TakerUnitUISystem(ECSmanager eCSmanager): base(eCSmanager)
    {
        _photonPunRPC = InstanceGame.PhotonGameManager.PhotonPunRPC;

        _selectorUnitComponentRef = eCSmanager.EntitiesGeneralManager.SelectorUnitComponent;


        _selectorUnitComponentRef.Unref().GameDownTakeUnit0 = InstanceGame.GameObjectPool.GameDownTakeUnit0Button;
        _selectorUnitComponentRef.Unref().GameDownTakeUnit0.onClick.AddListener(delegate { GetUnit(UnitTypes.King); });

        _gameDownTakeUnit1 = InstanceGame.GameObjectPool.GameDownTakeUnit1Button;
        _gameDownTakeUnit1.onClick.AddListener(delegate { GetUnit(UnitTypes.Pawn); });

        _gameDownTakeUnit2 = InstanceGame.GameObjectPool.GameDownTakeUnit2Button;
        _gameDownTakeUnit2.onClick.AddListener(delegate { GetUnit(UnitTypes.Rook); });

        _gameDownTakeUnit3 = InstanceGame.GameObjectPool.GameDownTakeUnit3Button;
        _gameDownTakeUnit3.onClick.AddListener(delegate { GetUnit(UnitTypes.Bishop); });
    }

    public void Run()
    {
        if (_eGM.UnitsInfoComponent.IsSettedCurrentKing) _selectorUnitComponentRef.Unref().GameDownTakeUnit0.gameObject.SetActive(false);
    }

    private void GetUnit(UnitTypes unitType)
    {
        if (!_eGM.DonerComponent.IsCurrentDone) _photonPunRPC.GetUnit(unitType);
    }
}
