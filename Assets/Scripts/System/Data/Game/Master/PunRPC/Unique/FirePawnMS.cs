using Photon.Pun;
using System;
using static Game.Game.CellEnvironmentEs;

namespace Game.Game
{
    struct FirePawnMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);

            IdxDoingMC.Get(out var idx_0);
            var uniq_cur = EntitiesMaster.UniqueAbilityC.Ability;


            ref var fire_0 = ref CellFireEs.Fire(idx_0).Fire;


            if (CellUnitEs.Step(idx_0).AmountC.Amount >= CellUnitStepValues.NeedSteps(uniq_cur))
            {
                if (Environment(EnvironmentTypes.AdultForest, idx_0).Resources.Have)
                {
                    Entities.Rpc.SoundToGeneral(RpcTarget.All, AbilityTypes.FirePawn);

                    fire_0.Enable();
                    CellUnitEs.Step(idx_0).AmountC.Take(CellUnitStepValues.NeedSteps(uniq_cur));
                }
                else
                {
                    throw new Exception();
                }
            }

            else
            {
                Entities.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}