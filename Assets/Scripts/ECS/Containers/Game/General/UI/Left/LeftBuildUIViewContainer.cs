using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Game.General.Systems.StartFill;
using System;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.Workers.Game.UI.Left
{
    internal sealed class LeftBuildUIViewContainer
    {
        private static Button GetButton(LeftBuildButtonTypes leftBuildButtonType)
        {
            switch (leftBuildButtonType)
            {
                case LeftBuildButtonTypes.None:
                    throw new Exception();

                case LeftBuildButtonTypes.BuyPawn:
                    return MainGameSystem.BuyPawnUIEnt_ButtonCom.Button;

                case LeftBuildButtonTypes.BuyRook:
                    return MainGameSystem.BuyRookUIEnt_ButtonCom.Button;

                case LeftBuildButtonTypes.BuyBishop:
                    return MainGameSystem.BuyBishopUIEnt_ButtonCom.Button;

                case LeftBuildButtonTypes.Melt:
                    return MainGameSystem.MeltOreEnt_ButtonCom.Button;

                case LeftBuildButtonTypes.UpgradeUnit:
                    return MainGameSystem.UpgradeUnitUIEnt_ButtonCom.Button;

                case LeftBuildButtonTypes.UpgradeFarm:
                    return MainGameSystem.UpgradeFarmUIEnt_ButtonCom.Button;

                case LeftBuildButtonTypes.UpgradeWoodcutter:
                    return MainGameSystem.UpgradeWoodcutterUIEnt_ButtonCom.Button;

                case LeftBuildButtonTypes.UpgradeMine:
                    return MainGameSystem.UpgradeMineUIEnt_ButtonCom.Button;

                default:
                    throw new Exception();
            }
        }

        internal static void SetActiveZone(bool isActive) => MainGameSystem.BuildingZoneEnt_ParentCom.ParentGO.gameObject.SetActive(isActive);

        internal static void AddListener(LeftBuildButtonTypes leftBuildButtonType, UnityAction unityAction) => GetButton(leftBuildButtonType).onClick.AddListener(unityAction);
    }
}
