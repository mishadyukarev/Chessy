using Assets.Scripts.Abstractions.Enums;
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
        private int[] XySelectedCell => SelectorWorker.GetXy(SelectorCellTypes.Selected);


        public override void Init()
        {
            base.Init();

            Instance.ECSmanager.EntGameGeneralUIViewManager.ReadyEnt_ButtonCom.Button.onClick.AddListener(Ready);

            Instance.ECSmanager.EntGameGeneralUIViewManager.TakerKingEnt_ButtonCom.Button.onClick.AddListener(delegate { GetUnit(Instance.ECSmanager.EntGameGeneralUIViewManager.TakerKingEnt_UnitTypeCom.UnitType); });
            Instance.ECSmanager.EntGameGeneralUIViewManager.TakerPawnEntityButtonComponent.Button.onClick.AddListener(delegate { GetUnit(Instance.ECSmanager.EntGameGeneralUIViewManager.TakerPawnEntityUnitTypeComponent.UnitType); });
            Instance.ECSmanager.EntGameGeneralUIViewManager.TakerRookEntityButtonComponent.Button.onClick.AddListener(delegate { GetUnit(Instance.ECSmanager.EntGameGeneralUIViewManager.TakerRookEntityUnitTypeComponent.UnitType); });
            Instance.ECSmanager.EntGameGeneralUIViewManager.TakerBishopEntityButtonComponent.Button.onClick.AddListener(delegate { GetUnit(Instance.ECSmanager.EntGameGeneralUIViewManager.TakerBishopEntityUnitTypeComponent.UnitType); });

            Instance.ECSmanager.EntGameGeneralUIViewManager.DonerUIEnt_ButtonCom.Button.onClick.AddListener(delegate { Done(); });

            Instance.ECSmanager.EntGameGeneralUIViewManager.EnvironmentInfoEnt_ButtonCom.Button.onClick.AddListener(EnvironmentInfo);

            Instance.ECSmanager.EntGameGeneralUIViewManager.LeaveEnt_ButtonCom.Button.onClick.AddListener(PhotonScene.LeaveRoom);

            Instance.ECSmanager.EntGameGeneralUIViewManager.ProtectConditionEnt_ButtonCom.Button.onClick.AddListener(StandartAbilityButton1);
            Instance.ECSmanager.EntGameGeneralUIViewManager.RelaxConditionEnt_ButtonCom.Button.onClick.AddListener(StandartAbilityButton2);
        }


        private void Ready() => PhotonPunRPC.ReadyToMaster(!MiddleViewUIWorker.IsReady(PhotonNetwork.IsMasterClient));
        private void GetUnit(UnitTypes unitType)
        {
            if (!DownDonerUIWorker.IsDoned(PhotonNetwork.IsMasterClient))
            {
                PhotonPunRPC.GetUnitToMaster(unitType);
            }
        }
        private void Done()
        {
            SelectorWorker.SelectorType = SelectorTypes.Other;

            switch (SaverComWorker.StepModeType)
            {
                case StepModeTypes.None:
                    throw new Exception();

                case StepModeTypes.ByQueue:
                    if (!DownDonerUIWorker.IsDoned(PhotonNetwork.IsMasterClient))
                        PhotonPunRPC.DoneToMaster(!DownDonerUIWorker.IsDoned(PhotonNetwork.IsMasterClient));
                    break;

                case StepModeTypes.Together:
                    PhotonPunRPC.DoneToMaster(!DownDonerUIWorker.IsDoned(PhotonNetwork.IsMasterClient));
                    break;

                default:
                    throw new Exception();
            }
        }
        private void EnvironmentInfo()
        {
            Instance.ECSmanager.EntGameGeneralUIViewManager.EnvironmentInfoEnt_IsActivatedCom.IsActivated = !Instance.ECSmanager.EntGameGeneralUIViewManager.EnvironmentInfoEnt_IsActivatedCom.IsActivated;
        }
        private void StandartAbilityButton1()
        {
            if (!DownDonerUIWorker.IsDoned(PhotonNetwork.IsMasterClient))
            {
                if (CellUnitsDataWorker.IsConditionType(ConditionUnitTypes.Protected, XySelectedCell))
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
            if (!DownDonerUIWorker.IsDoned(PhotonNetwork.IsMasterClient))
            {
                if (CellUnitsDataWorker.IsConditionType(ConditionUnitTypes.Relaxed, XySelectedCell))
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