namespace Game.Game
{
    public struct IceWallMS : IEcsRunSystem
    {
        public void Run()
        {
            var idx_0 = IceWallME.IdxC.Idx;

            var curAbility = EntityMPool.UniqueAbilityC.Ability;


            if (CellUnitEntities.Step(idx_0).AmountC.Amount >= CellUnitStepValues.NeedSteps(curAbility))
            {
                CellUnitEntities.Step(idx_0).AmountC.Take(CellUnitStepValues.NeedSteps(curAbility));

                CellUnitEntities.CooldownUnique(curAbility, idx_0).Cooldown += 5;

                //EntitiesPool.IceWalls[idx_0].Hp += 4;
            }
        }
    }
}