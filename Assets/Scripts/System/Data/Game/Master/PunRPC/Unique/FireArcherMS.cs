using Photon.Pun;

namespace Game.Game
{
    struct FireArcherMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);


            Entities.MasterEs.FireArcher<IdxFromToC>().Get(out var idx_from, out var idx_to);
            var uniq_cur = Entities.MasterEs.UniqueAbilityC.Ability;

            ref var fire_to = ref Entities.CellEs.FireEs.Fire(idx_to).Fire;

            var whoseMove = Entities.WhoseMove.WhoseMove.Player;

            if (Entities.CellEs.UnitEs.Step(idx_from).Steps.Amount >= 2)
            {
                if (CellsForArsonArcherEs.Idxs<IdxsC>(idx_from).Contains(idx_to))
                {
                    Entities.Rpc.SoundToGeneral(RpcTarget.All, AbilityTypes.FireArcher);

                    Entities.CellEs.UnitEs.Step(idx_from).Steps.Take(CellUnitStepValues.NeedSteps(uniq_cur));
                    fire_to.Enable();
                }
            }

            else
            {
                Entities.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}
