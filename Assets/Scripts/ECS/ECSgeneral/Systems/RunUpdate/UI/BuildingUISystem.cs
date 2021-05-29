using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.UI;
using static MainGame;

internal sealed class BuildingUISystem : RPCGeneralReduction
{
    private Button _buildingAbilityButton0;
    private Button _buildingAbilityButton1;
    private Button _buildingAbilityButton2;
    private Button _buildingAbilityButton3;
    private Button _buildingAbilityButton4;

    private int[] _xySelectedCell => _eGM.SelectorEntSelectorCom.XYselectedCell;

    internal BuildingUISystem(ECSmanager eCSmanager) : base(eCSmanager)
    {

        _buildingAbilityButton0 = Instance.ObjectPool.BuildingAbilityButton0;
        _buildingAbilityButton0.onClick.AddListener(delegate { Build(BuildingTypes.City); });

        _buildingAbilityButton1 = Instance.ObjectPool.BuildingAbilityButton1;
        _buildingAbilityButton1.onClick.AddListener(delegate { Build(BuildingTypes.Farm); });

        _buildingAbilityButton2 = Instance.ObjectPool.BuildingAbilityButton2;
        _buildingAbilityButton2.onClick.AddListener(delegate { Build(BuildingTypes.Woodcutter); });

        _buildingAbilityButton3 = Instance.ObjectPool.BuildingAbilityButton3;
        _buildingAbilityButton3.onClick.AddListener(delegate { Build(BuildingTypes.Mine); });

        _buildingAbilityButton4 = Instance.ObjectPool.BuildingAbilityButton4;
        _buildingAbilityButton4.onClick.AddListener(delegate { Destroy(); });
    }


    public override void Run()
    {
        base.Run();

        _buildingAbilityButton0.gameObject.SetActive(false);
        _buildingAbilityButton1.gameObject.SetActive(false);
        _buildingAbilityButton2.gameObject.SetActive(false);
        _buildingAbilityButton3.gameObject.SetActive(false);
        _buildingAbilityButton4.gameObject.SetActive(false);



        if (_eGM.CellEnt_CellUnitCom(_xySelectedCell).HaveUnit)
        {
            if (_eGM.CellEnt_CellUnitCom(_xySelectedCell).IsMine)
            {
                switch (_eGM.CellEnt_CellUnitCom(_xySelectedCell).UnitType)
                {
                    case UnitTypes.King:

                        if (_eGM.CellBuildingEnt_BuildingTypeCom(_xySelectedCell).HaveBuilding)
                        {
                            if (!_eGM.CellBuildingEnt_OwnerCom(_xySelectedCell).IsMine)
                            {
                                _buildingAbilityButton4.gameObject.SetActive(true);
                            }
                        }

                        break;


                    case UnitTypes.Pawn:
                        if (!_eGM.InfoEnt_BuildingsInfoCom.IsSettedCityDict[Instance.IsMasterClient]) 
                            _buildingAbilityButton0.gameObject.SetActive(true);


                        _buildingAbilityButton1.gameObject.SetActive(true);
                        _buildingAbilityButton2.gameObject.SetActive(true);
                        _buildingAbilityButton3.gameObject.SetActive(true);

                        if (_eGM.CellBuildingEnt_BuildingTypeCom(_xySelectedCell).HaveBuilding)
                        {
                            if (_eGM.CellBuildingEnt_OwnerCom(_xySelectedCell).IsMine)
                            {
                                if (_eGM.CellBuildingEnt_BuildingTypeCom(_xySelectedCell).BuildingType != BuildingTypes.City)
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

    private void Build(BuildingTypes buildingType) => _photonPunRPC.BuildToMaster(_xySelectedCell, buildingType);

    private void Destroy() => _photonPunRPC.DestroyBuildingToMaster(_xySelectedCell);
}
