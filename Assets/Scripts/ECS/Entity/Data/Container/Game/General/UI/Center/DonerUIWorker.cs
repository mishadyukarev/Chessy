using Assets.Scripts.ECS.System.View.Game.General.UI;
using UnityEngine;

namespace Assets.Scripts.Workers.Game.UI.Middle
{
    internal sealed class DonerUIWorker
    {
        private static SysViewGameGeneralUIManager EGGUIM => Main.Instance.ECSmanager.SysViewGameGeneralUIManager;

        internal static void SetColor(Color color) => EGGUIM.DonerUIEnt_ButtonCom.Button.image.color = color;
    }
}
