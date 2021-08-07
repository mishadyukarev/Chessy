//using Assets.Scripts.ECS.Game.General.Systems.StartFill;
//using UnityEngine;
//using UnityEngine.UI;

//namespace Assets.Scripts.Workers.Game.UI
//{
//    internal sealed class ReadyZoneUIWorker
//    {
//        private static GameObject ReadyParentGO => MainGameSystem.ReadyEnt_ParentCom.ParentGO;
//        private static Button ReadyButton => MainGameSystem.ReadyEnt_ButtonCom.Button;
//        internal static bool IsStartedGame => MainGameSystem.ReadyEnt_StartedGameCom.IsStartedGame;

//        internal static void SetActiveParentGO(bool isActive) => ReadyParentGO.SetActive(isActive);
//        internal static void SetColorButton(Color color) => ReadyButton.image.color = color;
//    }
//}
