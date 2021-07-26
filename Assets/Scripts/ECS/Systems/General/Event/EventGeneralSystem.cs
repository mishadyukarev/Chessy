using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Game.UI;
using Photon.Pun;
using static Assets.Scripts.Main;

namespace Assets.Scripts
{
    internal sealed class EventGeneralSystem : RPCGeneralSystemReduction
    {
        private PhotonSceneManager _sceneManager;

        private int[] XySelectedCell => SelectorWorker.GetXy(SelectorCellTypes.Selected);

        internal EventGeneralSystem()
        {
            _sceneManager = Instance.PhotonManager.SceneManager;
        }

        public override void Init()
        {
            base.Init();

            _eGGUIM.ReadyEnt_ButtonCom.Button.onClick.AddListener(Ready);

            _eGGUIM.TakerKingEnt_ButtonCom.Button.onClick.AddListener(delegate { GetUnit(_eGGUIM.TakerKingEnt_UnitTypeCom.UnitType); });
            _eGGUIM.TakerPawnEntityButtonComponent.Button.onClick.AddListener(delegate { GetUnit(_eGGUIM.TakerPawnEntityUnitTypeComponent.UnitType); });
            _eGGUIM.TakerRookEntityButtonComponent.Button.onClick.AddListener(delegate { GetUnit(_eGGUIM.TakerRookEntityUnitTypeComponent.UnitType); });
            _eGGUIM.TakerBishopEntityButtonComponent.Button.onClick.AddListener(delegate { GetUnit(_eGGUIM.TakerBishopEntityUnitTypeComponent.UnitType); });

            _eGGUIM.DonerUIEnt_ButtonCom.Button.onClick.AddListener(delegate { Done(); });

            _eGGUIM.EnvironmentInfoEnt_ButtonCom.Button.onClick.AddListener(EnvironmentInfo);

            _eGGUIM.LeaveEnt_ButtonCom.Button.onClick.AddListener(_sceneManager.LeaveRoom);

            _eGGUIM.StandartFirstAbilityEnt_ButtonCom.Button.onClick.AddListener(StandartAbilityButton1);
            _eGGUIM.StandartSecondAbilityEnt_ButtonCom.Button.onClick.AddListener(StandartAbilityButton2);
        }


        private void Ready() => PhotonPunRPC.ReadyToMaster(!UIMiddleWorker.IsReady(Instance.IsMasterClient));
        private void GetUnit(UnitTypes unitType)
        {
            if (!UIDownWorker.IsDoned(PhotonNetwork.IsMasterClient))
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
                    if (!UIDownWorker.IsDoned(PhotonNetwork.IsMasterClient)) 
                        PhotonPunRPC.DoneToMaster(!UIDownWorker.IsDoned(PhotonNetwork.IsMasterClient));
                    break;

                case StepModeTypes.Together:
                    PhotonPunRPC.DoneToMaster(!UIDownWorker.IsDoned(PhotonNetwork.IsMasterClient));
                    break;

                default:
                    break;
            }
        }
        private void EnvironmentInfo()
        {
            _eGGUIM.EnvironmentInfoEnt_IsActivatedCom.IsActivated = !_eGGUIM.EnvironmentInfoEnt_IsActivatedCom.IsActivated;
        }
        private void StandartAbilityButton1()
        {
            if (!UIDownWorker.IsDoned(PhotonNetwork.IsMasterClient))
            {
                if (CellUnitsDataWorker.IsProtectRelaxType(ConditionTypes.Protected, XySelectedCell))
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
            if (!UIDownWorker.IsDoned(PhotonNetwork.IsMasterClient))
            {
                if (CellUnitsDataWorker.IsProtectRelaxType(ConditionTypes.Relaxed, XySelectedCell))
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