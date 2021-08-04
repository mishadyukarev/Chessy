using Assets.Scripts.Abstractions.Enums;
using System;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.Workers.Game.UI.Left
{
    internal sealed class LeftBuildUIViewContainer
    {
        private static GameGeneralSystemManager EGGUIM => Main.Instance.ECSmanager.GameGeneralSystemManager;

        private static Button GetButton(LeftBuildButtonTypes leftBuildButtonType)
        {
            switch (leftBuildButtonType)
            {
                case LeftBuildButtonTypes.None:
                    throw new Exception();

                case LeftBuildButtonTypes.BuyPawn:
                    return EGGUIM.BuyPawnUIEnt_ButtonCom.Button;

                case LeftBuildButtonTypes.BuyRook:
                    return EGGUIM.BuyRookUIEnt_ButtonCom.Button;

                case LeftBuildButtonTypes.BuyBishop:
                    return EGGUIM.BuyBishopUIEnt_ButtonCom.Button;

                case LeftBuildButtonTypes.Melt:
                    return EGGUIM.MeltOreEnt_ButtonCom.Button;

                case LeftBuildButtonTypes.UpgradeUnit:
                    return EGGUIM.UpgradeUnitUIEnt_ButtonCom.Button;

                case LeftBuildButtonTypes.UpgradeFarm:
                    return EGGUIM.UpgradeFarmUIEnt_ButtonCom.Button;

                case LeftBuildButtonTypes.UpgradeWoodcutter:
                    return EGGUIM.UpgradeWoodcutterUIEnt_ButtonCom.Button;

                case LeftBuildButtonTypes.UpgradeMine:
                    return EGGUIM.UpgradeMineUIEnt_ButtonCom.Button;

                default:
                    throw new Exception();
            }
        }

        internal static void SetActiveZone(bool isActive) => EGGUIM.BuildingZoneEnt_ParentCom.ParentGO.gameObject.SetActive(isActive);

        internal static void AddListener(LeftBuildButtonTypes leftBuildButtonType, UnityAction unityAction) => GetButton(leftBuildButtonType).onClick.AddListener(unityAction);
    }
}
