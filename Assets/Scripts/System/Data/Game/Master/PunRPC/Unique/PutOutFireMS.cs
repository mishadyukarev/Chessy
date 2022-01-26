namespace Game.Game
{
    struct PutOutFireMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);

            IdxDoingMC.Get(out var idx_0);

            ref var fire_0 = ref CellFireEs.Fire(idx_0).Fire;


            if (CellUnitEs.Step(idx_0).AmountC.Have)
            {
                fire_0.Disable();

                CellUnitEs.Step(idx_0).AmountC.Take();
            }

            else
            {
                Entities.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}