using TMPro;
using UnityEngine;

namespace Assets.Scripts.Workers.Game.UI.Middle
{
    internal sealed class EndGameUIWorker
    {
        private static EntViewGameGeneralUIManager EGGUIM => Main.Instance.ECSmanager.EntViewGameGeneralUIManager;

        private static GameObject ParentGO => EGGUIM.EndGameEnt_ParentCom.ParentGO;
        private static TextMeshProUGUI TMP => EGGUIM.EndGameEnt_TextMeshProGUICom.TextMeshProUGUI;

        internal static bool IsLocalWinnet => EGGUIM.EndGameEnt_EndGameCom.PlayerWinner.IsLocal;
        internal static bool IsEndGame => EGGUIM.EndGameEnt_EndGameCom.IsEndGame;
        internal static string Text
        {
            get => TMP.text;
            set => TMP.text = value;
        }

        internal static void SetActiveParent(bool isActive) => ParentGO.SetActive(isActive);
    }
}
