using Assets.Scripts.ECS.Game.General.Systems.StartFill;

namespace Assets.Scripts.Workers.Game.UI
{
    internal sealed class MiddleUIDataContainer
    {
        internal static bool IsReady(bool key) => MainGameSystem.ReadyEnt_ActivatedDictCom.IsActivatedButtonDict[key];
        internal static void SetIsReady(bool key, bool value) => MainGameSystem.ReadyEnt_ActivatedDictCom.IsActivatedButtonDict[key] = value;
        internal static bool IsStartedGame
        {
            get => MainGameSystem.ReadyEnt_StartedGameCom.IsStartedGame;
            set => MainGameSystem.ReadyEnt_StartedGameCom.IsStartedGame = value;
        }
        internal static int AmountMotions
        {
            get => MainGameSystem.MotionEnt_AmountCom.AmountMotions;
            set => MainGameSystem.MotionEnt_AmountCom.AmountMotions = value;
        }
    }
}
