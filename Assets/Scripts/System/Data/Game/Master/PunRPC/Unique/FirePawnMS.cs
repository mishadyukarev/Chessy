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
            var uniq_cur = Entities.MasterEs.UniqueAbilityC.Ability;


            ref var fire_0 = ref Entities.CellEs.FireEs.Fire(idx_0).Fire;


            if (Entities.CellEs.UnitEs.Step(idx_0).Steps.Amount >= CellUnitStepValues.NeedSteps(uniq_cur))
            {
                if (Entities.CellEs.EnvironmentEs.Environment(EnvironmentTypes.AdultForest, idx_0).Resources.Have)
                {
                    Entities.Rpc.SoundToGeneral(RpcTarget.All, AbilityTypes.FirePawn);

                    fire_0.Enable();
                    Entities.CellEs.UnitEs.Step(idx_0).Steps.Take(CellUnitStepValues.NeedSteps(uniq_cur));
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