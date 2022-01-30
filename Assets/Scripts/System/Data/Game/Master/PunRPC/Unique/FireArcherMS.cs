using Photon.Pun;

namespace Game.Game
{
    sealed class FireArcherMS : SystemAbstract, IEcsRunSystem
    {
        public FireArcherMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);


            Es.MasterEs.FireArcher<IdxFromToC>().Get(out var idx_from, out var idx_to);
            var uniq_cur = Es.MasterEs.UniqueAbilityC.Ability;

            ref var fire_to = ref Es.CellEs.FireEs.Fire(idx_to).Fire;

            var whoseMove = Es.WhoseMove.WhoseMove.Player;

            if (Es.CellEs.UnitEs.StatEs.Step(idx_from).Steps.Amount >= 2)
            {
                if (CellsForArsonArcherEs.Idxs<IdxsC>(idx_from).Contains(idx_to))
                {
                    Es.Rpc.SoundToGeneral(RpcTarget.All, AbilityTypes.FireArcher);

                    Es.CellEs.UnitEs.StatEs.Step(idx_from).Steps.Take(CellUnitStepValues.NeedSteps(uniq_cur));
                    fire_to.Enable();
                }
            }

            else
            {
                Es.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}
