using UnityEngine;

namespace Assets.Scripts.Workers.Game.UI.Middle
{
    internal sealed class DonerUIWorker
    {
        private static EntGameGeneralUIViewManager EGGUIM => Main.Instance.ECSmanager.EntGameGeneralUIViewManager;

        internal static void SetColor(Color color) => EGGUIM.DonerUIEnt_ButtonCom.Button.image.color = color;
    }
}
