﻿using Assets.Scripts.Abstractions.Enums;
using System;

namespace Assets.Scripts.Workers.Game.UI.Left
{
    internal class EnvirZoneLeftUIViewContainer
    {
        private static EntViewGameGeneralUIManager EGGUIM => Main.Instance.ECSmanager.EntViewGameGeneralUIManager;

        internal static bool IsActivatedEnvrInfo => EGGUIM.EnvironmentInfoEnt_IsActivatedCom.IsActivated;

        internal static void SetActiveZone(bool isActive) => EGGUIM.EnvironmentZoneEnt_ParentCom.ParentGO.SetActive(isActive);
        internal static void SetTextInfoCell(EnvirTextInfoTypes envirTextInfoTypes, string text)
        {
            switch (envirTextInfoTypes)
            {
                case EnvirTextInfoTypes.None:
                    throw new Exception();

                case EnvirTextInfoTypes.Fertilizer:
                    EGGUIM.EnvFerilizerEnt_TextMeshProUGUICom.TextMeshProUGUI.text = text;
                    break;

                case EnvirTextInfoTypes.Wood:
                    EGGUIM.EnvForestEnt_TextMeshProUGUICom.TextMeshProUGUI.text = text;
                    break;

                case EnvirTextInfoTypes.Ore:
                    EGGUIM.EnvOreEnt_TextMeshProUGUICom.TextMeshProUGUI.text = text;
                    break;

                default:
                    throw new Exception();
            }
        }
    }
}