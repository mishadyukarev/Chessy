using Photon.Pun;

namespace Game.Game
{
    struct FireArcherMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);


            EntitiesMaster.FireArcher<IdxFromToC>().Get(out var idx_from, out var idx_to);
            var uniq_cur = EntitiesMaster.UniqueAbilityC.Ability;

            ref var fire_to = ref CellFireEs.Fire(idx_to).Fire;

            var whoseMove = Entities.WhoseMove.WhoseMove.Player;

            if (CellUnitEs.Step(idx_from).AmountC.Amount >= 2)
            {
                if (CellsForArsonArcherEs.Idxs<IdxsC>(idx_from).Contains(idx_to))
                {
                    Entities.Rpc.SoundToGeneral(RpcTarget.All, AbilityTypes.FireArcher);

                    CellUnitEs.Step(idx_from).AmountC.Take(CellUnitStepValues.NeedSteps(uniq_cur));
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
