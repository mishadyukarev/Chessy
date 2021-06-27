﻿using Assets.Scripts;
using UnityEngine;

internal sealed class EnvironmentUISystem : SystemGeneralReduction
{
    private int[] XySelectedCell => _eGM.SelectorEnt_SelectorCom.XySelectedCell;


    public override void Run()
    {
        base.Run();

        if (_eGM.SelectorEnt_SelectorCom.IsSelected && _eGM.CellBuildEnt_BuilTypeCom(XySelectedCell).BuildingType != BuildingTypes.City)
        {
            _eGM.EnvironmentZoneEnt_ParentCom.SetActive(true);
        }
        else
        {
            _eGM.EnvironmentZoneEnt_ParentCom.SetActive(false);
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
                        _eGM.CellSupStatEnt_CellSupStatCom(x, y).ActiveVision(true, SupportStaticTypes.Fertilizer);
                        _eGM.CellSupStatEnt_CellSupStatCom(x, y).SetScale(SupportStaticTypes.Fertilizer, new Vector3(_eGM.CellEnvEnt_CellEnvCom(x, y).AmountFertilizerResources / 20f, 0.15f, 1));
                    }
                    else
                    {
                        _eGM.CellSupStatEnt_CellSupStatCom(x, y).ActiveVision(false, SupportStaticTypes.Fertilizer);
                    }

                    if (_eGM.CellEnvEnt_CellEnvCom(x, y).HaveAdultTree)
                    {
                        _eGM.CellSupStatEnt_CellSupStatCom(x, y).ActiveVision(true, SupportStaticTypes.Wood);
                        _eGM.CellSupStatEnt_CellSupStatCom(x, y).SetScale(SupportStaticTypes.Wood, new Vector3(_eGM.CellEnvEnt_CellEnvCom(x, y).AmountForestResources / 20f, 0.15f, 1));
                    }
                    else
                    {
                        _eGM.CellSupStatEnt_CellSupStatCom(x, y).ActiveVision(false, SupportStaticTypes.Wood);
                    }

                    if (_eGM.CellEnvEnt_CellEnvCom(x, y).HaveHill)
                    {
                        _eGM.CellSupStatEnt_CellSupStatCom(x, y).ActiveVision(true, SupportStaticTypes.Ore);
                        _eGM.CellSupStatEnt_CellSupStatCom(x, y).SetScale(SupportStaticTypes.Ore, new Vector3(_eGM.CellEnvEnt_CellEnvCom(x, y).AmountOreResources / 60f, 0.15f, 1));
                    }
                    else
                    {
                        _eGM.CellSupStatEnt_CellSupStatCom(x, y).ActiveVision(false, SupportStaticTypes.Ore);
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
                    _eGM.CellSupStatEnt_CellSupStatCom(x, y).ActiveVision(false, SupportStaticTypes.Fertilizer);
                    _eGM.CellSupStatEnt_CellSupStatCom(x, y).ActiveVision(false, SupportStaticTypes.Wood);
                    _eGM.CellSupStatEnt_CellSupStatCom(x, y).ActiveVision(false, SupportStaticTypes.Ore);
                }
            }
        }
    }
}