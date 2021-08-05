using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Game.General.Systems.StartFill;
using System;

namespace Assets.Scripts.Workers.Game.UI.Left
{
    internal class EnvirZoneLeftUIViewContainer
    {
        internal static bool IsActivatedEnvrInfo => MainGameSystem.EnvironmentInfoEnt_IsActivatedCom.IsActivated;

        internal static void SetActiveZone(bool isActive) => MainGameSystem.EnvironmentZoneEnt_ParentCom.ParentGO.SetActive(isActive);
        internal static void SetTextInfoCell(EnvirTextInfoTypes envirTextInfoTypes, string text)
        {
            switch (envirTextInfoTypes)
            {
                case EnvirTextInfoTypes.None:
                    throw new Exception();

                case EnvirTextInfoTypes.Fertilizer:
                    MainGameSystem.EnvFerilizerEnt_TextMeshProUGUICom.TextMeshProUGUI.text = text;
                    break;

                case EnvirTextInfoTypes.Wood:
                    MainGameSystem.EnvForestEnt_TextMeshProUGUICom.TextMeshProUGUI.text = text;
                    break;

                case EnvirTextInfoTypes.Ore:
                    MainGameSystem.EnvOreEnt_TextMeshProUGUICom.TextMeshProUGUI.text = text;
                    break;

                default:
                    throw new Exception();
            }
        }
    }
}
