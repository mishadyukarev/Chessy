using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Static.Cell;
using Photon.Pun;

namespace Assets.Scripts.ECS.Game.Master.Systems.PunRPC
{
    internal sealed class FireMasterSystem : RPCMasterSystemReduction
    {
        private PhotonMessageInfo InfoFrom => _eMM.FromInfoEnt_FromInfoCom.InfoFrom;
        private int[] FromXyCopy => _eMM.FireEnt_FromToXyCom.FromXy;
        private int[] ToXyCopy => _eMM.FireEnt_FromToXyCom.ToXy;

        public override void Run()
        {
            base.Run();

            if (_eGM.CellUnitEnt_CellUnitCom(FromXyCopy).HaveMinAmountSteps)
            {
                

                if (CellEffectsWorker.HaveEffect(EffectTypes.Fire, ToXyCopy))
                {
                    CellEffectsWorker.ResetEffect(EffectTypes.Fire, ToXyCopy);
                    _eGM.CellUnitEnt_CellUnitCom(ToXyCopy).TakeAmountSteps();
                }
                else if (CellEnvironmentWorker.HaveEnvironment(EnvironmentTypes.AdultForest, ToXyCopy))
                {
                    if (_eGM.CellUnitEnt_CellOwnerCom(FromXyCopy).HaveOwner)

                        if (EconomyManager.CanFireSomething(_eGM.CellUnitEnt_CellOwnerCom(FromXyCopy).Owner, _eGM.CellUnitEnt_UnitTypeCom(FromXyCopy).UnitType, out bool[] haves))
                        {
                            _photonPunRPC.SoundToGeneral(RpcTarget.All, SoundEffectTypes.Fire);

                            EconomyManager.Fire(_eGM.CellUnitEnt_CellOwnerCom(FromXyCopy).Owner, _eGM.CellUnitEnt_UnitTypeCom(FromXyCopy).UnitType);

                            CellEffectsWorker.SetEffect(EffectTypes.Fire, ToXyCopy);
                            _eGM.CellUnitEnt_CellUnitCom(ToXyCopy).TakeAmountSteps();
                        }
                        else
                        {
                            _photonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.Mistake);
                            _photonPunRPC.MistakeEconomyToGeneral(InfoFrom.Sender, haves);
                        }
                }
                else
                {
                    _photonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.Mistake);
                }
            }
            else
            {
                _photonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.Mistake);
            }
        }
    }
}
