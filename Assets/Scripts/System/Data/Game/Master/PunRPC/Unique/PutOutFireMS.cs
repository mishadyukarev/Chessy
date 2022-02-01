namespace Game.Game
{
    sealed class PutOutFireMS : SystemAbstract, IEcsRunSystem
    {
        public PutOutFireMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);

            IdxDoingMC.Get(out var idx_0);

            var ability = Es.MasterEs.AbilityC.Ability;



            if (UnitStatEs(idx_0).StepE.Have(ability))
            {
                EffectEs(idx_0).FireE.Disable();

                UnitStatEs(idx_0).StepE.Take(ability);
            }

            else
            {
                Es.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}