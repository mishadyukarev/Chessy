namespace Game.Game
{
    struct PutOutFireMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);

            IdxDoingMC.Get(out var idx_0);

            ref var fire_0 = ref Entities.CellEs.FireEs.Fire(idx_0).Fire;


            if (Entities.CellEs.UnitEs.Step(idx_0).Steps.Have)
            {
                fire_0.Disable();

                Entities.CellEs.UnitEs.Step(idx_0).Steps.Take();
            }

            else
            {
                Entities.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}