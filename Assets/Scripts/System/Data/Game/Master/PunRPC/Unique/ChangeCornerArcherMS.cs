using static Game.Game.CellUnitEs;

namespace Game.Game
{
    struct ChangeCornerArcherMS : IEcsRunSystem
    {
        public void Run()
        {
            IdxDoingMC.Get(out var idx_0);
            var uniq = EntityMPool.UniqueAbilityC.Ability;

            var sender = InfoC.Sender(MGOTypes.Master);

            ref var corner_0 = ref CellUnitElseEs.Corned(idx_0);


            if (CellUnitHpEs.HaveMax(idx_0))
            {
                if (CellUnitStepEs.Have(idx_0, uniq))
                {
                    corner_0.ChangeCorner();

                    CellUnitStepEs.Take(idx_0, uniq);

                    EntityPool.Rpc.SoundToGeneral(sender, ClipTypes.PickArcher);
                }
                else
                {
                    EntityPool.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                }
            }
            else
            {
                EntityPool.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreHp, sender);
            }
        }
    }
}