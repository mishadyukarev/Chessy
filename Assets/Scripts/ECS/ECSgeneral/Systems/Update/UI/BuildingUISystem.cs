using Leopotam.Ecs;
using UnityEngine.UI;
using static MainGame;

internal class BuildingUISystem : CellReduction, IEcsRunSystem
{
    private EcsComponentRef<SelectorComponent> _selectorComponentRef = default;
    private EcsComponentRef<EconomyComponent.BuildingComponent> _economyBuildingsComponentRef = default;

    private PhotonPunRPC _photonPunRPC;
    private Button _buildingAbilityButton0;
    private Button _buildingAbilityButton1;
    private Button _buildingAbilityButton2;
    private Button _buildingAbilityButton3;
    private Button _buildingAbilityButton4;

    private int[] _xySelectedCell => _selectorComponentRef.Unref().XYselectedCell;

    internal BuildingUISystem(ECSmanager eCSmanager) : base(eCSmanager)
    {
        _photonPunRPC = InstanceGame.PhotonGameManager.PhotonPunRPC;

        _selectorComponentRef = eCSmanager.EntitiesGeneralManager.SelectorComponentRef;
        _economyBuildingsComponentRef = eCSmanager.EntitiesGeneralManager.EconomyBuildingsComponentRef;

        _buildingAbilityButton0 = InstanceGame.GameObjectPool.BuildingAbilityButton0;
        _buildingAbilityButton0.onClick.AddListener(delegate { Build(BuildingTypes.City); });

        _buildingAbilityButton1 = InstanceGame.GameObjectPool.BuildingAbilityButton1;
        _buildingAbilityButton1.onClick.AddListener(delegate { Build(BuildingTypes.Farm); });

        _buildingAbilityButton2 = InstanceGame.GameObjectPool.BuildingAbilityButton2;
        _buildingAbilityButton2.onClick.AddListener(delegate { Build(BuildingTypes.Woodcutter); });

        _buildingAbilityButton3 = InstanceGame.GameObjectPool.BuildingAbilityButton3;
        //_buildingAbilityButton2.onClick.AddListener(delegate { Build(BuildingTypes.Woodcutter); });

        _buildingAbilityButton4 = InstanceGame.GameObjectPool.BuildingAbilityButton4;
        _buildingAbilityButton4.onClick.AddListener(delegate { Destroy(); });
    }


    public void Run()
    {
        _buildingAbilityButton0.gameObject.SetActive(false);
        _buildingAbilityButton1.gameObject.SetActive(false);
        _buildingAbilityButton2.gameObject.SetActive(false);
        _buildingAbilityButton3.gameObject.SetActive(false);
        _buildingAbilityButton4.gameObject.SetActive(false);



        if (CellUnitComponent(_xySelectedCell).HaveUnit)
        {
            if (CellUnitComponent(_xySelectedCell).IsMine)
            {
                switch (CellUnitComponent(_xySelectedCell).UnitType)
                {
                    case UnitTypes.King:

                        if (CellBuildingComponent(_xySelectedCell).HaveBuilding)
                        {
                            if (!CellBuildingComponent(_xySelectedCell).IsMine)
                            {
                                _buildingAbilityButton4.gameObject.SetActive(true);
                            }
                        }

                        break;


                    case UnitTypes.Pawn:

                        if (!_economyBuildingsComponentRef.Unref().IsSettedCity) _buildingAbilityButton0.gameObject.SetActive(true);
                        _buildingAbilityButton1.gameObject.SetActive(true);
                        _buildingAbilityButton2.gameObject.SetActive(true);
                        _buildingAbilityButton3.gameObject.SetActive(true);

                        if (CellBuildingComponent(_xySelectedCell).HaveBuilding)
                        {
                            if (CellBuildingComponent(_xySelectedCell).IsMine)
                            {
                                if (CellBuildingComponent(_xySelectedCell).BuildingType != BuildingTypes.City)
                                    _buildingAbilityButton4.gameObject.SetActive(true);
                            }
                            else
                            {
                                _buildingAbilityButton4.gameObject.SetActive(true);
                            }
                        }

                        break;
                }
            }
        }
    }

    private void Build(BuildingTypes buildingType) => _photonPunRPC.Build(_xySelectedCell, buildingType);

    private void Destroy() => _photonPunRPC.DestroyBuilding(_xySelectedCell);
}
