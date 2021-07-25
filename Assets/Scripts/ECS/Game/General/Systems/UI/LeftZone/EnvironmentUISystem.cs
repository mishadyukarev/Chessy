using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Workers;
using UnityEngine;
using static Assets.Scripts.CellEnvirDataWorker;
using static Assets.Scripts.Workers.Cell.CellSupVisBarsWorker;

internal sealed class EnvironmentUISystem : SystemGeneralReduction
{
    private int[] XySelectedCell => SelectorWorker.GetXy(SelectorCellTypes.Selected);


    public override void Run()
    {
        base.Run();

        if (_eGM.SelectorEnt_SelectorCom.IsSelected && CellBuildingsDataWorker.GetBuildingType(XySelectedCell) != BuildingTypes.City)
        {
            _eGGUIM.EnvironmentZoneEnt_ParentCom.SetActive(true);
        }
        else
        {
            _eGGUIM.EnvironmentZoneEnt_ParentCom.SetActive(false);
        }

        _eGGUIM.EnvFerilizerEnt_TextMeshProUGUICom.SetText("Fertilizer: " + GetAmountResources(EnvironmentTypes.Fertilizer, XySelectedCell));
        _eGGUIM.EnvForestEnt_TextMeshProUGUICom.SetText("Forest: " + GetAmountResources(EnvironmentTypes.AdultForest, XySelectedCell));
        _eGGUIM.EnvOreEnt_TextMeshProUGUICom.SetText("Ore: " + GetAmountResources(EnvironmentTypes.Hill, XySelectedCell));



        if (_eGGUIM.EnvironmentInfoEnt_IsActivatedCom.IsActivated)
        {
            for (int x = 0; x < _eGM.Xamount; x++)
            {
                for (int y = 0; y < _eGM.Yamount; y++)
                {
                    int[] xy = new int[] { x, y };


                    if (HaveEnvironment(EnvironmentTypes.Fertilizer, xy))
                    {
                        ActiveVision(true, SupportStaticTypes.Fertilizer, xy);
                        SetScale(SupportStaticTypes.Fertilizer, new Vector3((float)GetAmountResources(EnvironmentTypes.Fertilizer, xy) / (float)(MaxAmountResources(EnvironmentTypes.Fertilizer) + MaxAmountResources(EnvironmentTypes.Fertilizer)), 0.15f, 1), xy);
                    }
                    else
                    {
                        ActiveVision(false, SupportStaticTypes.Fertilizer, xy);
                    }

                    if (HaveEnvironment(EnvironmentTypes.AdultForest, xy))
                    {
                        ActiveVision(true, SupportStaticTypes.Wood, xy);
                        SetScale(SupportStaticTypes.Wood, new Vector3((float)GetAmountResources(EnvironmentTypes.AdultForest, xy) / (float)MaxAmountResources(EnvironmentTypes.AdultForest), 0.15f, 1), xy);
                    }
                    else
                    {
                        ActiveVision(false, SupportStaticTypes.Wood, xy);
                    }

                    if (HaveEnvironment(EnvironmentTypes.Hill, xy))
                    {
                        ActiveVision(true, SupportStaticTypes.Ore, xy);
                        SetScale(SupportStaticTypes.Ore, new Vector3((float)GetAmountResources(EnvironmentTypes.Hill, xy) / (float)MaxAmountResources(EnvironmentTypes.Hill), 0.15f, 1), xy);
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
