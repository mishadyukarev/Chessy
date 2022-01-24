namespace Game.Game
{
    public struct IceWallMS : IEcsRunSystem
    {
        public void Run()
        {
            var idx_0 = IceWallME.IdxC.Idx;

            var curAbility = EntityMPool.UniqueAbilityC.Ability;


            if (EntitiesPool.UnitStep.Have(idx_0, curAbility))
            {
                EntitiesPool.UnitStep.Take(idx_0, curAbility);

                CellUnitAbilityUniqueEs.Cooldown(curAbility, idx_0) += 5;

                EntitiesPool.IceWalls[idx_0].Hp += 4;
            }
        }
    }
}