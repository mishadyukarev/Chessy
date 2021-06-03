using UnityEngine;
using static Main;

internal class EnvironmentUISystem : SystemGeneralReduction
{
    private bool _isActive;
    private int[] XySelectedCell => _eGM.SelectorEntSelectorCom.XYselectedCell;

    internal EnvironmentUISystem()
    {
        Instance.CanvasGameManager.LeftZoneEnvironmentZoneEnvironmentInfoButton.onClick.AddListener(EnvironmentInfo);
    }

    public override void Run()
    {
        base.Run();

        if (_eGM.SelectorEntSelectorCom.IsSelected && _eGM.CellBuildingEnt_BuildingTypeCom(XySelectedCell).BuildingType != BuildingTypes.City)
        {
            Instance.CanvasGameManager.LeftZoneEnvironmentZoneGO.SetActive(true);
        }
        else
        {
            Instance.CanvasGameManager.LeftZoneEnvironmentZoneGO.SetActive(false);
        }

        Instance.CanvasGameManager.LeftZoneEnvironmentZoneForestResourcesText.text = "Forest: " + _eGM.CellEnvEnt_CellEnvCom(XySelectedCell).AmountForestResources;
        Instance.CanvasGameManager.LeftZoneEnvironmentZoneFertilizerResourcesText.text = "Fertilizer: " + _eGM.CellEnvEnt_CellEnvCom(XySelectedCell).AmountFertilizerResources;
        Instance.CanvasGameManager.LeftZoneEnvironmentZoneOreResourcesText.text = "Ore: " + _eGM.CellEnvEnt_CellEnvCom(XySelectedCell).AmountOreResources;



        if (_isActive)
        {
            for (int x = 0; x < _eGM.Xamount; x++)
            {
                for (int y = 0; y < _eGM.Yamount; y++)
                {
                    if (_eGM.CellEnvEnt_CellEnvCom(x, y).HaveFertilizer)
                    {
                        Instance.ObjectPoolGame.CellSupportVisionFertilizerGOs[x, y].GetComponent<SpriteRenderer>().enabled = true;
                        Instance.ObjectPoolGame.CellSupportVisionFertilizerGOs[x, y].transform.localScale = new Vector3(_eGM.CellEnvEnt_CellEnvCom(x, y).AmountFertilizerResources / 20f, 0.15f, 1);
                    }
                    else
                    {
                        Instance.ObjectPoolGame.CellSupportVisionFertilizerGOs[x, y].GetComponent<SpriteRenderer>().enabled = false;
                    }

                    if (_eGM.CellEnvEnt_CellEnvCom(x, y).HaveAdultTree)
                    {
                        Instance.ObjectPoolGame.CellSupportVisionForestGOs[x, y].GetComponent<SpriteRenderer>().enabled = true;
                        Instance.ObjectPoolGame.CellSupportVisionForestGOs[x, y].transform.localScale = new Vector3(_eGM.CellEnvEnt_CellEnvCom(x, y).AmountForestResources / 20f, 0.15f, 1);
                    }
                    else
                    {
                        Instance.ObjectPoolGame.CellSupportVisionForestGOs[x, y].GetComponent<SpriteRenderer>().enabled = false;
                    }

                    if (_eGM.CellEnvEnt_CellEnvCom(x, y).HaveHill)
                    {
                        Instance.ObjectPoolGame.CellSupportVisionOreGOs[x, y].GetComponent<SpriteRenderer>().enabled = true;
                        Instance.ObjectPoolGame.CellSupportVisionOreGOs[x, y].transform.localScale = new Vector3(_eGM.CellEnvEnt_CellEnvCom(x, y).AmountOreResources / 60f, 0.15f, 1);
                    }
                    else
                    {
                        Instance.ObjectPoolGame.CellSupportVisionOreGOs[x, y].GetComponent<SpriteRenderer>().enabled = false;
                    }
                }
            }
        }
        else
        {
            for (int x = 0; x < _eGM.Xamount; x++)
            {
                for (int y = 0; y < _eGM.Yamount; y++)
                {
                    Instance.ObjectPoolGame.CellSupportVisionFertilizerGOs[x, y].GetComponent<SpriteRenderer>().enabled = false;
                    Instance.ObjectPoolGame.CellSupportVisionForestGOs[x, y].GetComponent<SpriteRenderer>().enabled = false;
                    Instance.ObjectPoolGame.CellSupportVisionOreGOs[x, y].GetComponent<SpriteRenderer>().enabled = false;
                }
            }
        }
    }

    private void EnvironmentInfo()
    {
        _isActive = !_isActive;
    }
}
