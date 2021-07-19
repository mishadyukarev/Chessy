using Assets.Scripts.Abstractions.Enums;
using static Assets.Scripts.Main;

namespace Assets.Scripts
{
    internal sealed class EventGeneralSystem : RPCGeneralSystemReduction
    {
        private PhotonSceneManager _sceneManager;

        private int[] XySelectedCell => _eGM.SelectorEnt_SelectorCom.GetXy(SelectorCellTypes.Selected);
        private bool IsActivatedDoner => _eGM.DonerUIEnt_IsActivatedDictCom.IsActivated(Instance.IsMasterClient);

        internal EventGeneralSystem()
        {
            _sceneManager = Instance.PhotonManager.SceneManager;
        }

        public override void Init()
        {
            base.Init();

            _eGM.ReadyEnt_ButtonCom.AddListener(Ready);

            _eGM.TakerKingEnt_ButtonCom.AddListener(delegate { GetUnit(_eGM.TakerKingEnt_UnitTypeCom.UnitType); });
            _eGM.TakerPawnEntityButtonComponent.AddListener(delegate { GetUnit(_eGM.TakerPawnEntityUnitTypeComponent.UnitType); });
            _eGM.TakerRookEntityButtonComponent.AddListener(delegate { GetUnit(_eGM.TakerRookEntityUnitTypeComponent.UnitType); });
            _eGM.TakerBishopEntityButtonComponent.AddListener(delegate { GetUnit(_eGM.TakerBishopEntityUnitTypeComponent.UnitType); });

            _eGM.DonerUIEnt_ButtonCom.AddListener(delegate { Done(); });

            _eGM.EnvironmentInfoEnt_ButtonCom.AddListener(EnvironmentInfo);

            _eGM.LeaveEnt_ButtonCom.AddListener(_sceneManager.LeaveRoom);

            _eGM.StandartFirstAbilityEnt_ButtonCom.AddListener(StandartAbilityButton1);
            _eGM.StandartSecondAbilityEnt_ButtonCom.AddListener(StandartAbilityButton2);
        }


        private void Ready() => _photonPunRPC.ReadyToMaster(!_eGM.ReadyEnt_ActivatedDictCom.IsActivated(Instance.IsMasterClient));
        private void GetUnit(UnitTypes unitType)
        {
            if (!IsActivatedDoner)
            {
                _photonPunRPC.GetUnitToMaster(unitType);
            }
        }
        private void Done()
        {
            _eGM.SelectorEnt_UpgradeModTypeCom.ResetUpgradeModType();

            switch (Instance.EntComM.SaverEnt_StepModeTypeCom.StepModeType)
            {
                case StepModeTypes.None:
                    break;

                case StepModeTypes.ByQueue:
                    if (!IsActivatedDoner) _photonPunRPC.DoneToMaster(!IsActivatedDoner);
                    break;

                case StepModeTypes.Together:
                    _photonPunRPC.DoneToMaster(!IsActivatedDoner);
                    break;

                default:
                    break;
            }
        }
        private void EnvironmentInfo()
        {
            _eGM.EnvironmentInfoEnt_IsActivatedCom.ToggleActivated();
        }
        private void StandartAbilityButton1()
        {
            if (!IsActivatedDoner)
                _photonPunRPC.ProtectRelaxUnitToMaster(ProtectRelaxTypes.Protected, XySelectedCell);
        }
        private void StandartAbilityButton2()
        {
            if (!IsActivatedDoner)
                _photonPunRPC.ProtectRelaxUnitToMaster(ProtectRelaxTypes.Relaxed, XySelectedCell);
        }
    }
}