using Assets.Scripts.Abstractions.Enums;
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
                if (_eGM.CellEffectEnt_CellEffectCom(ToXyCopy).HaveEffect(EffectTypes.Fire))
                {
                    _eGM.CellEffectEnt_CellEffectCom(ToXyCopy).ResetEffect(EffectTypes.Fire);
                    _eGM.CellUnitEnt_CellUnitCom(ToXyCopy).TakeAmountSteps();
                }
                else if (_eGM.CellEnvEnt_CellEnvCom(ToXyCopy).HaveEnvironment(EnvironmentTypes.AdultForest))
                {
                    if (_eGM.CellUnitEnt_CellOwnerCom(FromXyCopy).HaveOwner)

                        if (EconomyManager.CanFireSomething(_eGM.CellUnitEnt_CellOwnerCom(FromXyCopy).Owner, _eGM.CellUnitEnt_UnitTypeCom(FromXyCopy).UnitType, out bool[] haves))
                        {
                            _photonPunRPC.SoundToGeneral(RpcTarget.All, SoundEffectTypes.Fire);

                            EconomyManager.Fire(_eGM.CellUnitEnt_CellOwnerCom(FromXyCopy).Owner, _eGM.CellUnitEnt_UnitTypeCom(FromXyCopy).UnitType);

                            _eGM.CellEffectEnt_CellEffectCom(ToXyCopy).SetEffect(EffectTypes.Fire);
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
