namespace Game.Game
{
    public struct UpdateAnimalsMS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (var idx_0 in CellEs.Idxs)
            {
                ref var unitC_0 = ref CellUnitEs.Unit<UnitTC>(idx_0);

                //if (idx_0 == 73)
                //{
                //    unitC_0.Unit = UnitTypes.Camel;
                //}


                //if (unitC_0.Is(UnitTypes.Camel))
                //{

                //}
            }
        }
    }
}