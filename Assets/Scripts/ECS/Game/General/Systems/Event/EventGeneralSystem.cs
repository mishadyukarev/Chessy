using Assets.Scripts.Abstractions.Enums;
using static Assets.Scripts.Main;

namespace Assets.Scripts
{
    internal sealed class EventGeneralSystem : RPCGeneralSystemReduction
    {
        private PhotonSceneManager _sceneManager;

        private int[] XySelectedCell => _eGM.SelectorEnt_SelectorCom.XySelectedCell;

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

            _eGM.DonerEnt_ButtonCom.AddListener(delegate { Done(); });

            _eGM.TruceEnt_ButtonCom.AddListener(Truce);

            _eGM.EnvironmentInfoEnt_ButtonCom.AddListener(EnvironmentInfo);

            _eGM.LeaveEnt_ButtonCom.AddListener(_sceneManager.LeaveRoom);

            _eGM.StandartFirstAbilityEnt_ButtonCom.AddListener(StandartAbilityButton1);
            _eGM.StandartSecondAbilityEnt_ButtonCom.AddListener(StandartAbilityButton2);
        }


        private void Ready() => _photonPunRPC.ReadyToMaster(!_eGM.ReadyEnt_ActivatedDictCom.IsActivated(Instance.IsMasterClient));
        private void GetUnit(UnitTypes unitType)
        {
            if (!_eGM.DonerEnt_IsActivatedDictCom.IsActivated(Instance.IsMasterClient)) _photonPunRPC.GetUnitToMaster(unitType);
        }
        private void Done() => _photonPunRPC.DoneToMaster(!_eGM.DonerEnt_IsActivatedDictCom.IsActivated(Instance.IsMasterClient));
        private void Truce() => _photonPunRPC.TruceToMaster(!_eGM.TruceEnt_ActivatedDictCom.IsActivated(Instance.IsMasterClient));
        private void EnvironmentInfo() => _eGM.EnvironmentInfoEnt_IsActivatedCom.IsActivated = !_eGM.EnvironmentInfoEnt_IsActivatedCom.IsActivated;
        private void StandartAbilityButton1() => _photonPunRPC.ProtectRelaxUnitToMaster(ProtectRelaxTypes.Protected, XySelectedCell);
        private void StandartAbilityButton2() => _photonPunRPC.ProtectRelaxUnitToMaster(ProtectRelaxTypes.Relaxed, XySelectedCell);
    }
}