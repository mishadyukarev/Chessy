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

            ref var fire_0 = ref Es.CellEs.FireEs.Fire(idx_0).Fire;


            if (Es.CellEs.UnitEs.StatEs.Step(idx_0).Steps.Have)
            {
                fire_0.Disable();

                Es.CellEs.UnitEs.StatEs.Step(idx_0).Steps.Take();
            }

            else
            {
                Es.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}