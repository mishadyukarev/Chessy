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

            ref var corner_0 = ref CellUnitEs.Else(idx_0).CornedC;


            if (CellUnitEs.Hp(idx_0).HaveMax)
            {
                if (CellUnitEs.Step(idx_0).AmountC.Amount >= CellUnitStepValues.NeedSteps(uniq))
                {
                    corner_0.ChangeCorner();

                    CellUnitEs.Step(idx_0).AmountC.Take(CellUnitStepValues.NeedSteps(uniq));

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