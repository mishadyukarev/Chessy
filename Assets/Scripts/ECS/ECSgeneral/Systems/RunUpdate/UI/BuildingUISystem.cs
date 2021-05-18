using Leopotam.Ecs;
using UnityEngine.UI;
using static MainGame;

internal class BuildingUISystem : CellGeneralReduction, IEcsRunSystem
{
    private PhotonPunRPC _photonPunRPC;
    private Button _buildingAbilityButton0;
    private Button _buildingAbilityButton1;
    private Button _buildingAbilityButton2;
    private Button _buildingAbilityButton3;
    private Button _buildingAbilityButton4;

    private int[] _xySelectedCell => _eGM.SelectorComponentSelectorEnt.XYselectedCell;

    internal BuildingUISystem(ECSmanager eCSmanager) : base(eCSmanager)
    {
        _photonPunRPC = InstanceGame.PhotonGameManager.PhotonPunRPC;


        _buildingAbilityButton0 = InstanceGame.GameObjectPool.BuildingAbilityButton0;
        _buildingAbilityButton0.onClick.AddListener(delegate { Build(BuildingTypes.City); });

        _buildingAbilityButton1 = InstanceGame.GameObjectPool.BuildingAbilityButton1;
        _buildingAbilityButton1.onClick.AddListener(delegate { Build(BuildingTypes.Farm); });

        _buildingAbilityButton2 = InstanceGame.GameObjectPool.BuildingAbilityButton2;
        _buildingAbilityButton2.onClick.AddListener(delegate { Build(BuildingTypes.Woodcutter); });

        _buildingAbilityButton3 = InstanceGame.GameObjectPool.BuildingAbilityButton3;
        _buildingAbilityButton3.onClick.AddListener(delegate { Build(BuildingTypes.Mine); });

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



        if (_eGM.CellUnitComponent(_xySelectedCell).HaveUnit)
        {
            if (_eGM.CellUnitComponent(_xySelectedCell).IsMine)
            {
                switch (_eGM.CellUnitComponent(_xySelectedCell).UnitType)
                {
                    case UnitTypes.King:

                        if (_eGM.CellBuildingComponent(_xySelectedCell).HaveBuilding)
                        {
                            if (!_eGM.CellBuildingComponent(_xySelectedCell).IsMine)
                            {
                                _buildingAbilityButton4.gameObject.SetActive(true);
                            }
                        }

                        break;


                    case UnitTypes.Pawn:

                        if (!_eGM.InfoEntityBuildingsInfoComponent.IsBuildedCityDictionary[true]) _buildingAbilityButton0.gameObject.SetActive(true);
                        _buildingAbilityButton1.gameObject.SetActive(true);
                        _buildingAbilityButton2.gameObject.SetActive(true);
                        _buildingAbilityButton3.gameObject.SetActive(true);

                        if (_eGM.CellBuildingComponent(_xySelectedCell).HaveBuilding)
                        {
                            if (_eGM.CellBuildingComponent(_xySelectedCell).IsMine)
                            {
                                if (_eGM.CellBuildingComponent(_xySelectedCell).BuildingType != BuildingTypes.City)
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
