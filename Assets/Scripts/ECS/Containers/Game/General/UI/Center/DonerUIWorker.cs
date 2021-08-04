using UnityEngine;

namespace Assets.Scripts.Workers.Game.UI.Middle
{
    internal sealed class DonerUIWorker
    {
        private static GameGeneralSystemManager EGGUIM => Main.Instance.ECSmanager.GameGeneralSystemManager;

        internal static void SetColor(Color color) => EGGUIM.DonerUIEnt_ButtonCom.Button.image.color = color;
    }
}
