﻿using Assets.Scripts.ECS.System.View.Game.General.UI;

namespace Assets.Scripts.Workers.Game.UI.Middle
{
    internal sealed class UpdatedUIWorker
    {
        private static SysViewGameGeneralUIManager EGGUIM => Main.Instance.ECSmanager.SysViewGameGeneralUIManager;

        internal static bool IsActivated
        {
            get => EGGUIM.MotionEnt_ActivatedCom.IsActivated;
            set => EGGUIM.MotionEnt_ActivatedCom.IsActivated = value;
        }
        internal static string Text
        {
            get => EGGUIM.MotionEnt_TextMeshProUGUICom.TextMeshProUGUI.text;
            set => EGGUIM.MotionEnt_TextMeshProUGUICom.TextMeshProUGUI.text = value;
        }
        internal static int AmountMotions
        {
            get => EGGUIM.MotionEnt_AmountCom.AmountMotions;
            set => EGGUIM.MotionEnt_AmountCom.AmountMotions = value;
        }
        internal static void SetActiveParent(bool isActive) => EGGUIM.MotionEnt_ParentCom.ParentGO.SetActive(isActive);
    }
}
