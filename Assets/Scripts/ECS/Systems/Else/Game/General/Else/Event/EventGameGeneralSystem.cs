using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component.Data.UI.Game.General;
using Assets.Scripts.ECS.Component.View.UI.Game.General;
using Leopotam.Ecs;
using Photon.Pun;
using System;

namespace Assets.Scripts
{
    internal sealed class EventGameGeneralSystem : IEcsInitSystem
    {
        private EcsFilter<XyCellComponent> _xyCellFilter = default;
        private EcsFilter<CellUnitDataComponent> _cellUnitFilter = default;

        //private EcsFilter<ReadyDataUICom, ReadyViewUICom> _readyFilter = default;
        private EcsFilter<SelectorComponent> _selectorFilter = default;
        //private EcsFilter<LeaveViewUIComponent> _leaveUIFilter = default;
        private EcsFilter<TakerUnitsViewUICom> _takerUIFilter = default;
        private EcsFilter<DonerDataUIComponent, DonerViewUIComponent> _donerUIFilter = default;
        //private EcsFilter<EnvirZoneDataUICom, EnvirZoneViewUICom> _envirZoneUIFilter = default;
        private EcsFilter<UnitZoneViewUICom> _unitZoneUIFilter = default;

        private byte IdxSelectedCell => _selectorFilter.Get1(0).IdxSelectedCell;
        private CellUnitDataComponent CellUnitDataCom(byte idx) => _cellUnitFilter.Get1(idx);
        private bool IsDoned(bool key) => _donerUIFilter.Get1(0).IsDoned(key);

        public void Init()
        {
            _unitZoneUIFilter.Get1(0).AddListenerToBuildButton(BuildingButtonTypes.First, delegate { Build(BuildingTypes.Farm); });
            _unitZoneUIFilter.Get1(0).AddListenerToBuildButton(BuildingButtonTypes.Second, delegate { Build(BuildingTypes.Mine); });
            _unitZoneUIFilter.Get1(0).AddListenerToBuildButton(BuildingButtonTypes.Third, delegate { Build(BuildingTypes.City); });

            //_readyFilter.Get2(0).AddListenerToReadyButton(Ready);

            _takerUIFilter.Get1(0).AddListener(UnitTypes.King, delegate { GetUnit(UnitTypes.King); });
            _takerUIFilter.Get1(0).AddListener(UnitTypes.Pawn, delegate { GetUnit(UnitTypes.Pawn); });
            _takerUIFilter.Get1(0).AddListener(UnitTypes.Rook, delegate { GetUnit(UnitTypes.Rook); });
            _takerUIFilter.Get1(0).AddListener(UnitTypes.Bishop, delegate { GetUnit(UnitTypes.Bishop); });

            _donerUIFilter.Get2(0).AddListener(delegate { Done(); });

            //_envirZoneUIFilter.Get2(0).AddListenerToEnvInfo(EnvironmentInfo);

            //_leaveUIFilter.Get1(0).AddListener(PhotonSceneGameGeneralSystem.LeaveRoom);

            _unitZoneUIFilter.Get1(0).AddListenerToCondtionButton(ConditionUnitTypes.Protected, StandartAbilityButton1);
            _unitZoneUIFilter.Get1(0).AddListenerToCondtionButton(ConditionUnitTypes.Relaxed, StandartAbilityButton2);
        }


        //private void Ready() => RPCGameSystem.ReadyToMaster(!_readyFilter.Get1(0).IsReady(PhotonNetwork.IsMasterClient));
        private void GetUnit(UnitTypes unitType)
        {
            if (!IsDoned(PhotonNetwork.IsMasterClient))
            {
                RPCGameSystem.GetUnitToMaster(unitType);
            }
        }
        private void Done()
        {
            _selectorFilter.Get1(0).CellClickType = CellClickTypes.Start;

            switch (SaverComponent.StepModeType)
            {
                case StepModeTypes.None:
                    throw new Exception();

                case StepModeTypes.ByQueue:
                    if (!IsDoned(PhotonNetwork.IsMasterClient))
                        RPCGameSystem.DoneToMaster(!IsDoned(PhotonNetwork.IsMasterClient));
                    break;

                case StepModeTypes.Together:
                    RPCGameSystem.DoneToMaster(!IsDoned(PhotonNetwork.IsMasterClient));
                    break;

                default:
                    throw new Exception();
            }
        }
        //private void EnvironmentInfo()
        //{
        //    _envirZoneUIFilter.Get1(0).IsActivatedInfo = !_envirZoneUIFilter.Get1(0).IsActivatedInfo;
        //}
        private void StandartAbilityButton1()
        {
            if (!IsDoned(PhotonNetwork.IsMasterClient))
            {
                if (CellUnitDataCom(IdxSelectedCell).IsConditionType(ConditionUnitTypes.Protected))
                {
                    RPCGameSystem.ProtectRelaxUnitToMaster(ConditionUnitTypes.None, IdxSelectedCell);
                }
                else
                {
                    RPCGameSystem.ProtectRelaxUnitToMaster(ConditionUnitTypes.Protected, IdxSelectedCell);
                }
            }
        }
        private void StandartAbilityButton2()
        {
            if (!IsDoned(PhotonNetwork.IsMasterClient))
            {
                if (CellUnitDataCom(IdxSelectedCell).IsConditionType(ConditionUnitTypes.Relaxed))
                {
                    RPCGameSystem.ProtectRelaxUnitToMaster(ConditionUnitTypes.None, IdxSelectedCell);
                }
                else
                {
                    RPCGameSystem.ProtectRelaxUnitToMaster(ConditionUnitTypes.Relaxed, IdxSelectedCell);
                }
            }
        }
        private void Build(BuildingTypes buildingType)
        {
            if (!IsDoned(PhotonNetwork.IsMasterClient)) RPCGameSystem.BuildToMaster(IdxSelectedCell, buildingType);
        }
        private void Destroy()
        {
            if (!IsDoned(PhotonNetwork.IsMasterClient)) RPCGameSystem.DestroyBuildingToMaster(IdxSelectedCell);
        }
    }
}