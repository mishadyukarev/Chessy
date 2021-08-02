namespace Assets.Scripts.Workers.Game.UI
{
    internal sealed class MiddleUIDataContainer
    {
        private static EntViewGameGeneralUIManager EGGUIM => Main.Instance.ECSmanager.EntViewGameGeneralUIManager;

        internal static bool IsReady(bool key) => EGGUIM.ReadyEnt_ActivatedDictCom.IsActivatedButtonDict[key];
        internal static void SetIsReady(bool key, bool value) => EGGUIM.ReadyEnt_ActivatedDictCom.IsActivatedButtonDict[key] = value;
        internal static bool IsStartedGame
        {
            get => EGGUIM.ReadyEnt_StartedGameCom.IsStartedGame;
            set => EGGUIM.ReadyEnt_StartedGameCom.IsStartedGame = value;
        }
        internal static int AmountMotions
        {
            get => EGGUIM.MotionEnt_AmountCom.AmountMotions;
            set => EGGUIM.MotionEnt_AmountCom.AmountMotions = value;
        }
    }
}
