using Photon.Pun;
using static Assets.Scripts.Abstractions.ValuesConst;

namespace Assets.Scripts.ECS.Game.Master.Systems.PunRPC
{
    internal sealed class FireMasterSystem : RPCMasterSystemReduction
    {
        private PhotonMessageInfo Info => _eMM.FromInfoEnt_FromInfoCom.Info;
        private int[] FromXy => _eMM.FireEnt_FromToXyCom.FromXy;
        private int[] ToXy => _eMM.FireEnt_FromToXyCom.ToXy;

        public override void Run()
        {
            base.Run();


            if (_eGM.CellEffectEnt_CellEffectCom(ToXy).HaveFire)
            {
                _eGM.CellEffectEnt_CellEffectCom(ToXy).SetResetEffect(false, EffectTypes.Fire);
                _eGM.CellUnitEnt_CellUnitCom(ToXy).AmountSteps -= 1;
            }
            else if (_eGM.CellEnvEnt_CellEnvCom(ToXy).HaveAdultForest)

                if (_eGM.CellUnitEnt_CellOwnerCom(FromXy).HaveOwner)

                    if (EconomyManager.CanFireSomething(_eGM.CellUnitEnt_CellOwnerCom(FromXy).Owner, _eGM.CellUnitEnt_UnitTypeCom(FromXy).UnitType, out bool[] haves))
                    {
                        EconomyManager.Fire(_eGM.CellUnitEnt_CellOwnerCom(FromXy).Owner, _eGM.CellUnitEnt_UnitTypeCom(FromXy).UnitType);

                        _eGM.CellEffectEnt_CellEffectCom(ToXy).SetResetEffect(true, EffectTypes.Fire);
                        _eGM.CellUnitEnt_CellUnitCom(ToXy).AmountSteps -= 1;
                    }
                    else
                    {
                        _photonPunRPC.MistakeEconomyToGeneral(Info.Sender, haves[FOOD_NUMBER], haves[WOOD_NUMBER], haves[ORE_NUMBER], haves[IRON_NUMBER], haves[GOLD_NUMBER]);
                    }
        }
    }
}
