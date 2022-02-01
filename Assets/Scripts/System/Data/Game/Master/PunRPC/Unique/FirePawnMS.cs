using Photon.Pun;
using System;

namespace Game.Game
{
    sealed class FirePawnMS : SystemAbstract, IEcsRunSystem
    {
        public FirePawnMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);

            IdxDoingMC.Get(out var idx_0);
            var uniq_cur = Es.MasterEs.AbilityC.Ability;

            if (UnitStatEs(idx_0).StepE.Have(uniq_cur))
            {
                if (EnvironmentEs(idx_0).AdultForest.HaveEnvironment)
                {
                    Es.Rpc.SoundToGeneral(RpcTarget.All, AbilityTypes.FirePawn);

                    EffectEs(idx_0).FireE.Enable();
                    UnitStatEs(idx_0).StepE.Take(uniq_cur);
                }
                else
                {
                    throw new Exception();
                }
            }

            else
            {
                Es.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}