namespace Game.Game
{
    public struct IceWallMS : IEcsRunSystem
    {
        public void Run()
        {
            var idx_0 = IceWallME.IdxC.Idx;

            var curAbility = EntityMPool.UniqueAbilityC.Ability;


            if (CellUnitStepEs.Have(idx_0, curAbility))
            {
                CellUnitStepEs.Take(idx_0, curAbility);

                CellUnitAbilityUniqueEs.Cooldown(curAbility, idx_0).Add(5);

                CellIceWallEs.Hp(idx_0).Add(4);
            }
        }
    }
}