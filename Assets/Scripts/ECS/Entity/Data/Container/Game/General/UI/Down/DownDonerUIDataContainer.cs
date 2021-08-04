using Assets.Scripts.ECS.System.View.Game.General.UI;
using UnityEngine.Events;

namespace Assets.Scripts.Workers.Game.UI
{
    internal sealed class DownDonerUIDataContainer
    {
        private static SysViewGameGeneralUIManager EGGUIM => Main.Instance.ECSmanager.SysViewGameGeneralUIManager;

        internal static bool IsDoned(bool key) => EGGUIM.DonerUIEnt_IsActivatedDictCom.IsActivatedButtonDict[key];
        internal static void SetDoned(bool key, bool value) => EGGUIM.DonerUIEnt_IsActivatedDictCom.IsActivatedButtonDict[key] = value;

        internal static void AddListener(UnityAction unityAction) => EGGUIM.DonerUIEnt_MistakeCom.MistakeUnityEvent.AddListener(unityAction);
    }
}
