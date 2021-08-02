using UnityEngine;

namespace Assets.Scripts.Workers.Game.UI.Middle
{
    internal sealed class DonerUIWorker
    {
        private static EntViewGameGeneralUIManager EGGUIM => Main.Instance.ECSmanager.EntViewGameGeneralUIManager;

        internal static void SetColor(Color color) => EGGUIM.DonerUIEnt_ButtonCom.Button.image.color = color;
    }
}
