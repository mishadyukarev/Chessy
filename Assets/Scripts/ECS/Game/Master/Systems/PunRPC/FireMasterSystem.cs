﻿using Photon.Pun;
using static Assets.Scripts.Abstractions.ValuesConst;

namespace Assets.Scripts.ECS.Game.Master.Systems.PunRPC
{
    internal sealed class FireMasterSystem : RPCMasterSystemReduction
    {
        private PhotonMessageInfo Info => _eMM.FromInfoEnt_FromInfoCom.Info;
        private int[] FromXyCopy => _eMM.FireEnt_FromToXyCom.FromXyCopy;
        private int[] ToXyCopy => _eMM.FireEnt_FromToXyCom.ToXyCopy;

        public override void Run()
        {
            base.Run();


            if (_eGM.CellEffectEnt_CellEffectCom(ToXyCopy).HaveEffect(EffectTypes.Fire))
            {
                _eGM.CellEffectEnt_CellEffectCom(ToXyCopy).ResetEffect(EffectTypes.Fire);
                _eGM.CellUnitEnt_CellUnitCom(ToXyCopy).TakeAmountSteps();
            }
            else if (_eGM.CellEnvEnt_CellEnvCom(ToXyCopy).HaveEnvironment(EnvironmentTypes.AdultForest))

                if (_eGM.CellUnitEnt_CellOwnerCom(FromXyCopy).HaveOwner)

                    if (EconomyManager.CanFireSomething(_eGM.CellUnitEnt_CellOwnerCom(FromXyCopy).Owner, _eGM.CellUnitEnt_UnitTypeCom(FromXyCopy).UnitType, out bool[] haves))
                    {
                        EconomyManager.Fire(_eGM.CellUnitEnt_CellOwnerCom(FromXyCopy).Owner, _eGM.CellUnitEnt_UnitTypeCom(FromXyCopy).UnitType);

                        _eGM.CellEffectEnt_CellEffectCom(ToXyCopy).SetEffect(EffectTypes.Fire);
                        _eGM.CellUnitEnt_CellUnitCom(ToXyCopy).TakeAmountSteps();
                    }
                    else
                    {
                        _photonPunRPC.MistakeEconomyToGeneral(Info.Sender, haves[FOOD_NUMBER], haves[WOOD_NUMBER], haves[ORE_NUMBER], haves[IRON_NUMBER], haves[GOLD_NUMBER]);
                    }
        }
    }
}
