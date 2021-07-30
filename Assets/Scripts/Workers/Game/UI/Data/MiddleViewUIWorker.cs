using Assets.Scripts.Workers.UI;
using UnityEngine.Events;

namespace Assets.Scripts.Workers.Game.UI
{
    internal sealed class MiddleViewUIWorker : MainGeneralUIWorker
    {
        internal static bool IsReady(bool key) => EGGUIM.ReadyEnt_ActivatedDictCom.IsActivatedButtonDict[key];
        internal static void SetIsReady(bool key, bool value) => EGGUIM.ReadyEnt_ActivatedDictCom.IsActivatedButtonDict[key] = value;
        internal static bool IsStartedGame
        {
            get => EGGUIM.ReadyEnt_StartedGameCom.IsStartedGame;
            set => EGGUIM.ReadyEnt_StartedGameCom.IsStartedGame = value;
        }
        //internal static MistakeTypes MistakeTypeBar
        //{
        //    get => EGGUIM.MistakeBarEnt_MistakeTypesCom.MistakeType;
        //    set => EGGUIM.MistakeBarEnt_MistakeTypesCom.MistakeType = value;
        //}
        //internal static void SetActiveMistakeBar(bool isActive) => EGGUIM.MistakeBarVisUIEnt_ParentCom.ParentGO.SetActive(isActive);
        //internal static string TextMistakeBar
        //{
        //    get => EGGUIM.MistakeBarVisUIEnt_TextMeshProUGUICom.TextMeshProUGUI.text;
        //    set => EGGUIM.MistakeBarVisUIEnt_TextMeshProUGUICom.TextMeshProUGUI.text = value;
        //}
        //internal static void AddListenerMistakeBar(UnityAction unityAction) => EGGUIM.MistakeBarEnt_UnityEventCom.UnityEvent.AddListener(unityAction);
    }
}
