using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.System.Data.Common;
using Assets.Scripts.ECS.System.Data.Game.General.Cell;
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

            Instance.ECSmanager.SysViewGameGeneralUIManager.ReadyEnt_ButtonCom.Button.onClick.AddListener(Ready);

            Instance.ECSmanager.SysViewGameGeneralUIManager.TakerKingEnt_ButtonCom.Button.onClick.AddListener(delegate { GetUnit(Instance.ECSmanager.SysViewGameGeneralUIManager.TakerKingEnt_UnitTypeCom.UnitType); });
            Instance.ECSmanager.SysViewGameGeneralUIManager.TakerPawnEntityButtonComponent.Button.onClick.AddListener(delegate { GetUnit(Instance.ECSmanager.SysViewGameGeneralUIManager.TakerPawnEntityUnitTypeComponent.UnitType); });
            Instance.ECSmanager.SysViewGameGeneralUIManager.TakerRookEntityButtonComponent.Button.onClick.AddListener(delegate { GetUnit(Instance.ECSmanager.SysViewGameGeneralUIManager.TakerRookEntityUnitTypeComponent.UnitType); });
            Instance.ECSmanager.SysViewGameGeneralUIManager.TakerBishopEntityButtonComponent.Button.onClick.AddListener(delegate { GetUnit(Instance.ECSmanager.SysViewGameGeneralUIManager.TakerBishopEntityUnitTypeComponent.UnitType); });

            Instance.ECSmanager.SysViewGameGeneralUIManager.DonerUIEnt_ButtonCom.Button.onClick.AddListener(delegate { Done(); });

            Instance.ECSmanager.SysViewGameGeneralUIManager.EnvironmentInfoEnt_ButtonCom.Button.onClick.AddListener(EnvironmentInfo);

            Instance.ECSmanager.SysViewGameGeneralUIManager.LeaveEnt_ButtonCom.Button.onClick.AddListener(PhotonScene.LeaveRoom);

            Instance.ECSmanager.SysViewGameGeneralUIManager.ProtectConditionEnt_ButtonCom.Button.onClick.AddListener(StandartAbilityButton1);
            Instance.ECSmanager.SysViewGameGeneralUIManager.RelaxConditionEnt_ButtonCom.Button.onClick.AddListener(StandartAbilityButton2);
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
            SelectorSystem.SelectorType = SelectorTypes.StartClick;

            switch (MainDataCommSys.CommonZoneEnt_SaverCom.StepModeType)
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
            Instance.ECSmanager.SysViewGameGeneralUIManager.EnvironmentInfoEnt_IsActivatedCom.IsActivated = !Instance.ECSmanager.SysViewGameGeneralUIManager.EnvironmentInfoEnt_IsActivatedCom.IsActivated;
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