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
            var uniq = Es.MasterEs.AbilityC.Ability;

            var sender = InfoC.Sender(MGOTypes.Master);

            if (UnitStatEs(idx_0).Hp.HaveMax)
            {
                if (UnitStatEs(idx_0).StepE.Have(uniq))
                {
                    UnitEs(idx_0).MainE.ChangeCorner();

                    UnitStatEs(idx_0).StepE.Take(uniq);

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