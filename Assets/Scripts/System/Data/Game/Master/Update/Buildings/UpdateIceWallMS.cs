namespace Game.Game
{
    sealed class UpdateIceWallMS : SystemAbstract, IEcsRunSystem
    {
        internal UpdateIceWallMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < Es.LengthCells; idx_0++)
            {
                if (Es.BuildE(idx_0).HaveBuilding && Es.BuildE(idx_0).Is(BuildingTypes.IceWall))
                {
                    Es.BuildE(idx_0).Defrost(Es);
                }
            }
        }
    }
}