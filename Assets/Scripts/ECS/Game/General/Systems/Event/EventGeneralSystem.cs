using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Workers;
using static Assets.Scripts.Main;

namespace Assets.Scripts
{
    internal sealed class EventGeneralSystem : RPCGeneralSystemReduction
    {
        private PhotonSceneManager _sceneManager;

        private int[] XySelectedCell => SelectorWorker.GetXy(SelectorCellTypes.Selected);
        private bool IsActivatedDoner => _eGGUIM.DonerUIEnt_IsActivatedDictCom.IsActivated(Instance.IsMasterClient);

        internal EventGeneralSystem()
        {
            _sceneManager = Instance.PhotonManager.SceneManager;
        }

        public override void Init()
        {
            base.Init();

            _eGGUIM.ReadyEnt_ButtonCom.AddListener(Ready);

            _eGGUIM.TakerKingEnt_ButtonCom.AddListener(delegate { GetUnit(_eGGUIM.TakerKingEnt_UnitTypeCom.UnitType); });
            _eGGUIM.TakerPawnEntityButtonComponent.AddListener(delegate { GetUnit(_eGGUIM.TakerPawnEntityUnitTypeComponent.UnitType); });
            _eGGUIM.TakerRookEntityButtonComponent.AddListener(delegate { GetUnit(_eGGUIM.TakerRookEntityUnitTypeComponent.UnitType); });
            _eGGUIM.TakerBishopEntityButtonComponent.AddListener(delegate { GetUnit(_eGGUIM.TakerBishopEntityUnitTypeComponent.UnitType); });

            _eGGUIM.DonerUIEnt_ButtonCom.AddListener(delegate { Done(); });

            _eGGUIM.EnvironmentInfoEnt_ButtonCom.AddListener(EnvironmentInfo);

            _eGGUIM.LeaveEnt_ButtonCom.AddListener(_sceneManager.LeaveRoom);

            _eGGUIM.StandartFirstAbilityEnt_ButtonCom.AddListener(StandartAbilityButton1);
            _eGGUIM.StandartSecondAbilityEnt_ButtonCom.AddListener(StandartAbilityButton2);
        }


        private void Ready() => PhotonPunRPC.ReadyToMaster(!_eGGUIM.ReadyEnt_ActivatedDictCom.IsActivated(Instance.IsMasterClient));
        private void GetUnit(UnitTypes unitType)
        {
            if (!IsActivatedDoner)
            {
                PhotonPunRPC.GetUnitToMaster(unitType);
            }
        }
        private void Done()
        {
            SelectorWorker.ResetUpgradeModType();

            switch (Instance.EntComM.SaverEnt_StepModeTypeCom.StepModeType)
            {
                case StepModeTypes.None:
                    break;

                case StepModeTypes.ByQueue:
                    if (!IsActivatedDoner) PhotonPunRPC.DoneToMaster(!IsActivatedDoner);
                    break;

                case StepModeTypes.Together:
                    PhotonPunRPC.DoneToMaster(!IsActivatedDoner);
                    break;

                default:
                    break;
            }
        }
        private void EnvironmentInfo()
        {
            _eGGUIM.EnvironmentInfoEnt_IsActivatedCom.ToggleActivated();
        }
        private void StandartAbilityButton1()
        {
            if (!IsActivatedDoner)
            {
                if (CellUnitWorker.IsProtectRelaxType(ConditionTypes.Protected, XySelectedCell))
                {
                    PhotonPunRPC.ProtectRelaxUnitToMaster(ConditionTypes.None, XySelectedCell);
                }
                else
                {
                    PhotonPunRPC.ProtectRelaxUnitToMaster(ConditionTypes.Protected, XySelectedCell);
                }
            }

        }
        private void StandartAbilityButton2()
        {
            if (!IsActivatedDoner)
            {
                if (CellUnitWorker.IsProtectRelaxType(ConditionTypes.Relaxed, XySelectedCell))
                {
                    PhotonPunRPC.ProtectRelaxUnitToMaster(ConditionTypes.None, XySelectedCell);
                }
                else
                {
                    PhotonPunRPC.ProtectRelaxUnitToMaster(ConditionTypes.Relaxed, XySelectedCell);
                }
            }
        }
    }
}