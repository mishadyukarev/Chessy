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
            var uniq_cur = Es.MasterEs.UniqueAbilityC.Ability;


            ref var fire_0 = ref Es.CellEs.FireEs.Fire(idx_0).Fire;


            if (Es.CellEs.UnitEs.StatEs.Step(idx_0).Steps.Amount >= CellUnitStepValues.NeedSteps(uniq_cur))
            {
                if (Es.CellEs.EnvironmentEs.AdultForest( idx_0).HaveEnvironment)
                {
                    Es.Rpc.SoundToGeneral(RpcTarget.All, AbilityTypes.FirePawn);

                    fire_0.Enable();
                    Es.CellEs.UnitEs.StatEs.Step(idx_0).Steps.Take(CellUnitStepValues.NeedSteps(uniq_cur));
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