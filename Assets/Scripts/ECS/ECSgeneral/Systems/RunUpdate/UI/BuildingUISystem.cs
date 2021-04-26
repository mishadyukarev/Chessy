using Leopotam.Ecs;
using System;
using UnityEngine.UI;

internal class BuildingUISystem : IEcsRunSystem
{
    private EcsComponentRef<SelectorComponent> _selectorComponentRef = default;

    private PhotonPunRPC _photonPunRPC;
    private Button _buildingAbilityButton1;

    private int[] _xySelectedCell => _selectorComponentRef.Unref().XYselectedCell;

    internal BuildingUISystem(ECSmanager eCSmanager, PhotonGameManager photonGameManager, StartSpawnGameManager startSpawnGameManager)
    {
        _photonPunRPC = photonGameManager.PhotonPunRPC;

        _selectorComponentRef = eCSmanager.EntitiesGeneralManager.SelectorComponentRef;


        _buildingAbilityButton1 = startSpawnGameManager.BuildingAbilityButton1;
        _buildingAbilityButton1.onClick.AddListener(delegate { Build(BuildingTypes.Farm); });
    }


    public void Run()
    {
        //_buildingAbilityButton1
    }

    private void Build(BuildingTypes buildingType) => _photonPunRPC.Build(_xySelectedCell, buildingType);
}
