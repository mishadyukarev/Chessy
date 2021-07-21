using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Static.Cell;
using UnityEngine;
using static Assets.Scripts.Static.Cell.CellSupportStaticWorker;
using static Assets.Scripts.CellEnvironmentWorker;

internal sealed class EnvironmentUISystem : SystemGeneralReduction
{
    private int[] XySelectedCell => _eGM.SelectorEnt_SelectorCom.GetXy(SelectorCellTypes.Selected);


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

        _eGM.EnvFerilizerEnt_TextMeshProUGUICom.SetText("Fertilizer: " + AmountResources(ResourceTypes.Food, XySelectedCell));
        _eGM.EnvForestEnt_TextMeshProUGUICom.SetText("Forest: " + AmountResources(ResourceTypes.Wood, XySelectedCell));
        _eGM.EnvOreEnt_TextMeshProUGUICom.SetText("Ore: " + AmountResources(ResourceTypes.Ore, XySelectedCell));



        if (_eGM.EnvironmentInfoEnt_IsActivatedCom.IsActivated)
        {
            for (int x = 0; x < _eGM.Xamount; x++)
            {
                for (int y = 0; y < _eGM.Yamount; y++)
                {
                    int[] xy = new int[] { x, y };


                    if (HaveEnvironment(EnvironmentTypes.Fertilizer, xy))
                    {
                        ActiveVision(true, SupportStaticTypes.Fertilizer, xy);
                        SetScale(SupportStaticTypes.Fertilizer, new Vector3((float)AmountResources(ResourceTypes.Food, xy) / (float)(MaxAmountResources(EnvironmentTypes.Fertilizer) + MaxAmountResources(EnvironmentTypes.Fertilizer)), 0.15f, 1), xy);
                    }
                    else
                    {
                        ActiveVision(false, SupportStaticTypes.Fertilizer, xy);
                    }

                    if (HaveEnvironment(EnvironmentTypes.AdultForest, xy))
                    {
                        ActiveVision(true, SupportStaticTypes.Wood, xy);
                        SetScale(SupportStaticTypes.Wood, new Vector3((float)AmountResources(ResourceTypes.Wood, xy) / (float)MaxAmountResources(EnvironmentTypes.AdultForest), 0.15f, 1), xy);
                    }
                    else
                    {
                        ActiveVision(false, SupportStaticTypes.Wood, xy);
                    }

                    if (HaveEnvironment(EnvironmentTypes.Hill, xy))
                    {
                        ActiveVision(true, SupportStaticTypes.Ore, xy);
                        SetScale(SupportStaticTypes.Ore, new Vector3((float)AmountResources(ResourceTypes.Ore, xy) / (float)MaxAmountResources(EnvironmentTypes.Hill), 0.15f, 1), xy);
                    }
                    else
                    {
                        ActiveVision(false, SupportStaticTypes.Ore, xy);
                    }
                }
            }
        }
        else
        {
            for (int x = 0; x < _eGM.Xamount; x++)
                for (int y = 0; y < _eGM.Yamount; y++)
                {
                    int[] xy = new int[] { x, y };

                    ActiveVision(false, SupportStaticTypes.Fertilizer, xy);
                    ActiveVision(false, SupportStaticTypes.Wood, xy);
                    ActiveVision(false, SupportStaticTypes.Ore, xy);
                }
        }
    }
}
