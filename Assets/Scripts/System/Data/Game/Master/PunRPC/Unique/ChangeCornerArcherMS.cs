namespace Game.Game
{
    sealed class ChangeCornerArcherMS : SystemCellAbstract, IEcsRunSystem
    {
        public ChangeCornerArcherMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            IdxDoingMC.Get(out var idx_0);
            var uniq = Es.MasterEs.UniqueAbilityC.Ability;

            var sender = InfoC.Sender(MGOTypes.Master);

            if (UnitEs.StatEs.Hp(idx_0).HaveMax)
            {
                if (UnitEs.StatEs.Step(idx_0).Steps.Amount >= CellUnitStepValues.NeedSteps(uniq))
                {
                    UnitEs.Main(idx_0).ChangeCorner();

                    UnitEs.StatEs.Step(idx_0).Steps.Amount -= CellUnitStepValues.NeedSteps(uniq);

                    Es.Rpc.SoundToGeneral(sender, ClipTypes.PickArcher);
                }
                else
                {
                    Es.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                }
            }
            else
            {
                Es.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreHp, sender);
            }
        }
    }
}