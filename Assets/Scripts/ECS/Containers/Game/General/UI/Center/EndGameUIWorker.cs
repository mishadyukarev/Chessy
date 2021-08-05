using Assets.Scripts.ECS.Game.General.Systems.StartFill;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Workers.Game.UI.Middle
{
    internal sealed class EndGameUIWorker
    {
        private static GameObject ParentGO =>  MainGameSystem.EndGameEnt_ParentCom.ParentGO;
        private static TextMeshProUGUI TMP =>  MainGameSystem.EndGameEnt_TextMeshProGUICom.TextMeshProUGUI;

        internal static bool IsLocalWinnet =>  MainGameSystem.EndGameEnt_EndGameCom.PlayerWinner.IsLocal;
        internal static bool IsEndGame =>  MainGameSystem.EndGameEnt_EndGameCom.IsEndGame;
        internal static string Text
        {
            get => TMP.text;
            set => TMP.text = value;
        }

        internal static void SetActiveParent(bool isActive) => ParentGO.SetActive(isActive);
    }
}
