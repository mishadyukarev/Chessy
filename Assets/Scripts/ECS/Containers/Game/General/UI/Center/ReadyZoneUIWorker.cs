using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Workers.Game.UI
{
    internal sealed class ReadyZoneUIWorker
    {
        private static GameGeneralSystemManager EGGUIM => Main.Instance.ECSmanager.GameGeneralSystemManager;

        private static GameObject ReadyParentGO => EGGUIM.ReadyEnt_ParentCom.ParentGO;
        private static Button ReadyButton => EGGUIM.ReadyEnt_ButtonCom.Button;
        internal static bool IsStartedGame => EGGUIM.ReadyEnt_StartedGameCom.IsStartedGame;

        internal static void SetActiveParentGO(bool isActive) => ReadyParentGO.SetActive(isActive);
        internal static void SetColorButton(Color color) => ReadyButton.image.color = color;
    }
}
