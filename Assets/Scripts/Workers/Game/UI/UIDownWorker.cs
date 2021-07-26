using Assets.Scripts.Workers.UI;

namespace Assets.Scripts.Workers.Game.UI
{
    internal sealed class UIDownWorker : MainGeneralUIWorker
    {
        internal static bool IsDoned(bool key) => EGGUIM.DonerUIEnt_IsActivatedDictCom.IsActivatedButtonDict[key];
        internal static void SetDoned(bool key, bool value) => EGGUIM.DonerUIEnt_IsActivatedDictCom.IsActivatedButtonDict[key] = value;
    }
}
