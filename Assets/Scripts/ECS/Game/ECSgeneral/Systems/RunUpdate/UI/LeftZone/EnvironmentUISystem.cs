using UnityEngine;

internal sealed class EnvironmentUISystem : SystemGeneralReduction
{
    private int[] XySelectedCell => _eGM.SelectorEnt_SelectorCom.XySelectedCell;


    public override void Run()
    {
        base.Run();

        if (_eGM.SelectorEnt_SelectorCom.IsSelected && _eGM.CellBuilEnt_BuilTypeCom(XySelectedCell).BuildingType != BuildingTypes.City)
        {
            _eGM.LeftZoneEnt_ParentCom.SetActive(true);
        }
        else
        {
            _eGM.LeftZoneEnt_ParentCom.SetActive(false);
        }

        _eGM.EnvFerilizerEnt_TextMeshProUGUICom.Text = "Fertilizer: " + _eGM.CellEnvEnt_CellEnvCom(XySelectedCell).AmountFertilizerResources;
        _eGM.EnvForestEnt_TextMeshProUGUICom.Text = "Forest: " + _eGM.CellEnvEnt_CellEnvCom(XySelectedCell).AmountForestResources;
        _eGM.EnvOreEnt_TextMeshProUGUICom.Text = "Ore: " + _eGM.CellEnvEnt_CellEnvCom(XySelectedCell).AmountOreResources;



        if (_eGM.EnvironmentInfoEnt_IsActivatedCom.IsActivated)
        {
            for (int x = 0; x < _eGM.Xamount; x++)
            {
                for (int y = 0; y < _eGM.Yamount; y++)
                {
                    if (_eGM.CellEnvEnt_CellEnvCom(x, y).HaveFertilizer)
                    {
                        _eGM.CellSupVisEnt_CellSupVisCom(x, y).ActiveVision(true, SupportVisionTypes.Fertilize);
                        _eGM.CellSupVisEnt_CellSupVisCom(x, y).SetScale(SupportVisionTypes.Fertilize, new Vector3(_eGM.CellEnvEnt_CellEnvCom(x, y).AmountFertilizerResources / 20f, 0.15f, 1));
                    }
                    else
                    {
                        _eGM.CellSupVisEnt_CellSupVisCom(x, y).ActiveVision(false, SupportVisionTypes.Fertilize);
                    }

                    if (_eGM.CellEnvEnt_CellEnvCom(x, y).HaveAdultTree)
                    {
                        _eGM.CellSupVisEnt_CellSupVisCom(x, y).ActiveVision(true, SupportVisionTypes.Forest);
                        _eGM.CellSupVisEnt_CellSupVisCom(x, y).SetScale(SupportVisionTypes.Forest, new Vector3(_eGM.CellEnvEnt_CellEnvCom(x, y).AmountForestResources / 20f, 0.15f, 1));
                    }
                    else
                    {
                        _eGM.CellSupVisEnt_CellSupVisCom(x, y).ActiveVision(false, SupportVisionTypes.Forest);
                    }

                    if (_eGM.CellEnvEnt_CellEnvCom(x, y).HaveHill)
                    {
                        _eGM.CellSupVisEnt_CellSupVisCom(x, y).ActiveVision(true, SupportVisionTypes.Ore);
                        _eGM.CellSupVisEnt_CellSupVisCom(x, y).SetScale(SupportVisionTypes.Ore, new Vector3(_eGM.CellEnvEnt_CellEnvCom(x, y).AmountOreResources / 60f, 0.15f, 1));
                    }
                    else
                    {
                        _eGM.CellSupVisEnt_CellSupVisCom(x, y).ActiveVision(false, SupportVisionTypes.Ore);
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
                    _eGM.CellSupVisEnt_CellSupVisCom(x, y).ActiveVision(false, SupportVisionTypes.Fertilize);
                    _eGM.CellSupVisEnt_CellSupVisCom(x, y).ActiveVision(false, SupportVisionTypes.Forest);
                    _eGM.CellSupVisEnt_CellSupVisCom(x, y).ActiveVision(false, SupportVisionTypes.Ore);
                }
            }
        }
    }
}
