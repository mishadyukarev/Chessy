using Assets.Scripts.Workers.UI;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Workers.Game.UI
{
    internal sealed class DownDonerUIWorker : MainGeneralUIWorker
    {
        internal static bool IsDoned(bool key) => EGGUIM.DonerUIEnt_IsActivatedDictCom.IsActivatedButtonDict[key];
        internal static void SetDoned(bool key, bool value) => EGGUIM.DonerUIEnt_IsActivatedDictCom.IsActivatedButtonDict[key] = value;

        internal static void AddListener(UnityAction unityAction) => EGGUIM.DonerUIEnt_MistakeCom.MistakeUnityEvent.AddListener(unityAction);
        internal static void SetColorButton(Color color) => EGGUIM.DonerUIEnt_ButtonCom.Button.image.color = color;
    }
}
