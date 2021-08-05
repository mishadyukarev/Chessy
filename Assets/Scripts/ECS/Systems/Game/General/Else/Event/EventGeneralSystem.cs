using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Game.General.Systems.StartFill;
using Assets.Scripts.ECS.System.Data.Common;
using Assets.Scripts.ECS.System.Data.Game.General.Cell;
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

             MainGameSystem.ReadyEnt_ButtonCom.Button.onClick.AddListener(Ready);

             MainGameSystem.TakerKingEnt_ButtonCom.Button.onClick.AddListener(delegate { GetUnit(MainGameSystem.TakerKingEnt_UnitTypeCom.UnitType); });
             MainGameSystem.TakerPawnEntityButtonComponent.Button.onClick.AddListener(delegate { GetUnit(MainGameSystem.TakerPawnEntityUnitTypeComponent.UnitType); });
             MainGameSystem.TakerRookEntityButtonComponent.Button.onClick.AddListener(delegate { GetUnit(MainGameSystem.TakerRookEntityUnitTypeComponent.UnitType); });
             MainGameSystem.TakerBishopEntityButtonComponent.Button.onClick.AddListener(delegate { GetUnit(MainGameSystem.TakerBishopEntityUnitTypeComponent.UnitType); });

             MainGameSystem.DonerUIEnt_ButtonCom.Button.onClick.AddListener(delegate { Done(); });

             MainGameSystem.EnvironmentInfoEnt_ButtonCom.Button.onClick.AddListener(EnvironmentInfo);

             MainGameSystem.LeaveEnt_ButtonCom.Button.onClick.AddListener(PhotonScene.LeaveRoom);

             MainGameSystem.ProtectConditionEnt_ButtonCom.Button.onClick.AddListener(StandartAbilityButton1);
             MainGameSystem.RelaxConditionEnt_ButtonCom.Button.onClick.AddListener(StandartAbilityButton2);
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

            switch (MainCommonSystem.CommonEnt_SaverCom.StepModeType)
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
            MainGameSystem.EnvironmentInfoEnt_IsActivatedCom.IsActivated = !MainGameSystem.EnvironmentInfoEnt_IsActivatedCom.IsActivated;
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