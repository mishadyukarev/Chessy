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

            ref var fire_0 = ref CellEs.FireEs.Fire(idx_0).Fire;


            if (UnitEs.StatEs.Step(idx_0).HaveSteps)
            {
                fire_0.Disable();

                UnitEs.StatEs.Step(idx_0).Steps.Amount--;
            }

            else
            {
                Es.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}