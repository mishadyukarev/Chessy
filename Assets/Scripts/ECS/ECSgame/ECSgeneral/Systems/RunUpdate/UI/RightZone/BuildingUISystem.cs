using UnityEngine.UI;
using static Main;

internal sealed class BuildingUISystem : RPCGeneralSystemReduction
{
    private Button _buildingAbilityButton0;
    private Button _buildingAbilityButton1;
    private Button _buildingAbilityButton2;
    private Button _buildingAbilityButton3;
    private Button _buildingAbilityButton4;

    private int[] _xySelectedCell => _eGM.SelectorEnt_SelectorCom.XySelectedCell;

    internal BuildingUISystem()
    {

        //_buildingAbilityButton0 = Instance.CanvasGameManager.BuildingAbilityButton0;
        //_buildingAbilityButton0.onClick.AddListener(delegate { Build(BuildingTypes.City); });

        //_buildingAbilityButton1 = Instance.CanvasGameManager.BuildingAbilityButton1;
        //_buildingAbilityButton1.onClick.AddListener(delegate { Build(BuildingTypes.Farm); });

        //_buildingAbilityButton2 = Instance.CanvasGameManager.BuildingAbilityButton2;
        //_buildingAbilityButton2.onClick.AddListener(delegate { Build(BuildingTypes.Woodcutter); });

        //_buildingAbilityButton3 = Instance.CanvasGameManager.BuildingAbilityButton3;
        //_buildingAbilityButton3.onClick.AddListener(delegate { Build(BuildingTypes.Mine); });

        //_buildingAbilityButton4 = Instance.CanvasGameManager.BuildingAbilityButton4;
        //_buildingAbilityButton4.onClick.AddListener(delegate { Destroy(); });
    }


    public override void Run()
    {
        base.Run();

        //_buildingAbilityButton0.gameObject.SetActive(false);
        //_buildingAbilityButton1.gameObject.SetActive(false);
        //_buildingAbilityButton2.gameObject.SetActive(false);
        //_buildingAbilityButton3.gameObject.SetActive(false);
        //_buildingAbilityButton4.gameObject.SetActive(false);



        //if (_eGM.CellUnitEnt_UnitTypeCom(_xySelectedCell).HaveUnit)
        //{
        //    if (_eGM.CellUnitEnt_CellOwnerCom(_xySelectedCell).IsMine)
        //    {
        //        switch (_eGM.CellUnitEnt_UnitTypeCom(_xySelectedCell).UnitType)
        //        {
        //            case UnitTypes.King:

        //                if (_eGM.CellBuilEnt_BuilTypeCom(_xySelectedCell).HaveBuilding)
        //                {
        //                    if (!_eGM.CellBuilEnt_OwnerCom(_xySelectedCell).IsMine)
        //                    {
        //                        _buildingAbilityButton4.gameObject.SetActive(true);
        //                    }
        //                }

        //                break;


        //            case UnitTypes.Pawn:
        //                if (!_eGM.BuildingsEnt_BuildingsCom.IsSettedCityDict[Instance.IsMasterClient])
        //                    _buildingAbilityButton0.gameObject.SetActive(true);


        //                _buildingAbilityButton1.gameObject.SetActive(true);
        //                _buildingAbilityButton2.gameObject.SetActive(true);
        //                _buildingAbilityButton3.gameObject.SetActive(true);

        //                if (_eGM.CellBuilEnt_BuilTypeCom(_xySelectedCell).HaveBuilding)
        //                {
        //                    if (_eGM.CellBuilEnt_OwnerCom(_xySelectedCell).IsMine)
        //                    {
        //                        if (_eGM.CellBuilEnt_BuilTypeCom(_xySelectedCell).BuildingType != BuildingTypes.City)
        //                            _buildingAbilityButton4.gameObject.SetActive(true);
        //                    }
        //                    else
        //                    {
        //                        _buildingAbilityButton4.gameObject.SetActive(true);
        //                    }
        //                }

        //                break;
        //        }
        //    }
        //}
    }

    private void Build(BuildingTypes buildingType) => _photonPunRPC.BuildToMaster(_xySelectedCell, buildingType);

    private void Destroy() => _photonPunRPC.DestroyBuildingToMaster(_xySelectedCell);
}
