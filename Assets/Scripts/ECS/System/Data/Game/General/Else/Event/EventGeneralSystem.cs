using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.System.Data.Game.General.Cell;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Common;
using Assets.Scripts.Workers.Game.UI;
using Photon.Pun;
using System;
using static Assets.Scripts.Main;

namespace Assets.Scripts
{
    internal sealed class EventGeneralSystem : SystemGeneralReduction
    {
        private int[] XySelectedCell => SelectorSystem.XySelectedCell;


        public override void Init()
        {
            base.Init();

            Instance.ECSmanager.EntViewGameGeneralUIManager.ReadyEnt_ButtonCom.Button.onClick.AddListener(Ready);

            Instance.ECSmanager.EntViewGameGeneralUIManager.TakerKingEnt_ButtonCom.Button.onClick.AddListener(delegate { GetUnit(Instance.ECSmanager.EntViewGameGeneralUIManager.TakerKingEnt_UnitTypeCom.UnitType); });
            Instance.ECSmanager.EntViewGameGeneralUIManager.TakerPawnEntityButtonComponent.Button.onClick.AddListener(delegate { GetUnit(Instance.ECSmanager.EntViewGameGeneralUIManager.TakerPawnEntityUnitTypeComponent.UnitType); });
            Instance.ECSmanager.EntViewGameGeneralUIManager.TakerRookEntityButtonComponent.Button.onClick.AddListener(delegate { GetUnit(Instance.ECSmanager.EntViewGameGeneralUIManager.TakerRookEntityUnitTypeComponent.UnitType); });
            Instance.ECSmanager.EntViewGameGeneralUIManager.TakerBishopEntityButtonComponent.Button.onClick.AddListener(delegate { GetUnit(Instance.ECSmanager.EntViewGameGeneralUIManager.TakerBishopEntityUnitTypeComponent.UnitType); });

            Instance.ECSmanager.EntViewGameGeneralUIManager.DonerUIEnt_ButtonCom.Button.onClick.AddListener(delegate { Done(); });

            Instance.ECSmanager.EntViewGameGeneralUIManager.EnvironmentInfoEnt_ButtonCom.Button.onClick.AddListener(EnvironmentInfo);

            Instance.ECSmanager.EntViewGameGeneralUIManager.LeaveEnt_ButtonCom.Button.onClick.AddListener(PhotonScene.LeaveRoom);

            Instance.ECSmanager.EntViewGameGeneralUIManager.ProtectConditionEnt_ButtonCom.Button.onClick.AddListener(StandartAbilityButton1);
            Instance.ECSmanager.EntViewGameGeneralUIManager.RelaxConditionEnt_ButtonCom.Button.onClick.AddListener(StandartAbilityButton2);
        }


        private void Ready() => PhotonPunRPC.ReadyToMaster(!MiddleUIDataContainer.IsReady(PhotonNetwork.IsMasterClient));
        private void GetUnit(UnitTypes unitType)
        {
            if (!DownDonerUIDataContainer.IsDoned(PhotonNetwork.IsMasterClient))
            {
                PhotonPunRPC.GetUnitToMaster(unitType);
            }
        }
        private void Done()
        {
            SelectorSystem.SelectorType = SelectorTypes.Other;

            switch (DataCommContainerElseSaver.StepModeType)
            {
                case StepModeTypes.None:
                    throw new Exception();

                case StepModeTypes.ByQueue:
                    if (!DownDonerUIDataContainer.IsDoned(PhotonNetwork.IsMasterClient))
                        PhotonPunRPC.DoneToMaster(!DownDonerUIDataContainer.IsDoned(PhotonNetwork.IsMasterClient));
                    break;

                case StepModeTypes.Together:
                    PhotonPunRPC.DoneToMaster(!DownDonerUIDataContainer.IsDoned(PhotonNetwork.IsMasterClient));
                    break;

                default:
                    throw new Exception();
            }
        }
        private void EnvironmentInfo()
        {
            Instance.ECSmanager.EntViewGameGeneralUIManager.EnvironmentInfoEnt_IsActivatedCom.IsActivated = !Instance.ECSmanager.EntViewGameGeneralUIManager.EnvironmentInfoEnt_IsActivatedCom.IsActivated;
        }
        private void StandartAbilityButton1()
        {
            if (!DownDonerUIDataContainer.IsDoned(PhotonNetwork.IsMasterClient))
            {
                if (CellUnitsDataSystem.IsConditionType(ConditionUnitTypes.Protected, XySelectedCell))
                {
                    PhotonPunRPC.ProtectRelaxUnitToMaster(ConditionUnitTypes.None, XySelectedCell);
                }
                else
                {
                    PhotonPunRPC.ProtectRelaxUnitToMaster(ConditionUnitTypes.Protected, XySelectedCell);
                }
            }

        }
        private void StandartAbilityButton2()
        {
            if (!DownDonerUIDataContainer.IsDoned(PhotonNetwork.IsMasterClient))
            {
                if (CellUnitsDataSystem.IsConditionType(ConditionUnitTypes.Relaxed, XySelectedCell))
                {
                    PhotonPunRPC.ProtectRelaxUnitToMaster(ConditionUnitTypes.None, XySelectedCell);
                }
                else
                {
                    PhotonPunRPC.ProtectRelaxUnitToMaster(ConditionUnitTypes.Relaxed, XySelectedCell);
                }
            }
        }
    }
}