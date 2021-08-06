using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Game.General.Systems.StartFill;
using Assets.Scripts.ECS.System.Data.Common;
using Assets.Scripts.ECS.System.Data.Game.General.Cell;
using Assets.Scripts.Workers.Game.UI;
using Leopotam.Ecs;
using Photon.Pun;
using System;

namespace Assets.Scripts
{
    internal sealed class EventGameGeneralSystem : IEcsInitSystem
    {
        private EcsFilter<SelectorComponent> _selectorFilter;
        private int[] XySelectedCell => _selectorFilter.Get1(0).XySelectedCell;


        public void Init()
        {
            MainGameSystem.ReadyEnt_ButtonCom.Button.onClick.AddListener(Ready);

            MainGameSystem.TakerKingEnt_ButtonCom.Button.onClick.AddListener(delegate { GetUnit(MainGameSystem.TakerKingEnt_UnitTypeCom.UnitType); });
            MainGameSystem.TakerPawnEntityButtonComponent.Button.onClick.AddListener(delegate { GetUnit(MainGameSystem.TakerPawnEntityUnitTypeComponent.UnitType); });
            MainGameSystem.TakerRookEntityButtonComponent.Button.onClick.AddListener(delegate { GetUnit(MainGameSystem.TakerRookEntityUnitTypeComponent.UnitType); });
            MainGameSystem.TakerBishopEntityButtonComponent.Button.onClick.AddListener(delegate { GetUnit(MainGameSystem.TakerBishopEntityUnitTypeComponent.UnitType); });

            MainGameSystem.DonerUIEnt_ButtonCom.Button.onClick.AddListener(delegate { Done(); });

            MainGameSystem.EnvironmentInfoEnt_ButtonCom.Button.onClick.AddListener(EnvironmentInfo);

            MainGameSystem.LeaveEnt_ButtonCom.Button.onClick.AddListener(PhotonSceneGameGeneralSystem.LeaveRoom);

            MainGameSystem.ProtectConditionEnt_ButtonCom.Button.onClick.AddListener(StandartAbilityButton1);
            MainGameSystem.RelaxConditionEnt_ButtonCom.Button.onClick.AddListener(StandartAbilityButton2);
        }


        private void Ready() => RPCGameSystem.ReadyToMaster(!MiddleUIDataContainer.IsReady(PhotonNetwork.IsMasterClient));
        private void GetUnit(UnitTypes unitType)
        {
            if (!DownDonerUIDataContainer.IsDoned(PhotonNetwork.IsMasterClient))
            {
                RPCGameSystem.GetUnitToMaster(unitType);
            }
        }
        private void Done()
        {
            _selectorFilter.Get1(0).SelectorType = SelectorTypes.StartClick;

            switch (SaverComponent.StepModeType)
            {
                case StepModeTypes.None:
                    throw new Exception();

                case StepModeTypes.ByQueue:
                    if (!DownDonerUIDataContainer.IsDoned(PhotonNetwork.IsMasterClient))
                        RPCGameSystem.DoneToMaster(!DownDonerUIDataContainer.IsDoned(PhotonNetwork.IsMasterClient));
                    break;

                case StepModeTypes.Together:
                    RPCGameSystem.DoneToMaster(!DownDonerUIDataContainer.IsDoned(PhotonNetwork.IsMasterClient));
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
                    RPCGameSystem.ProtectRelaxUnitToMaster(ConditionUnitTypes.None, XySelectedCell);
                }
                else
                {
                    RPCGameSystem.ProtectRelaxUnitToMaster(ConditionUnitTypes.Protected, XySelectedCell);
                }
            }

        }
        private void StandartAbilityButton2()
        {
            if (!DownDonerUIDataContainer.IsDoned(PhotonNetwork.IsMasterClient))
            {
                if (CellUnitsDataSystem.IsConditionType(ConditionUnitTypes.Relaxed, XySelectedCell))
                {
                    RPCGameSystem.ProtectRelaxUnitToMaster(ConditionUnitTypes.None, XySelectedCell);
                }
                else
                {
                    RPCGameSystem.ProtectRelaxUnitToMaster(ConditionUnitTypes.Relaxed, XySelectedCell);
                }
            }
        }
    }
}