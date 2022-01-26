namespace Game.Game
{
    public struct IceWallMS : IEcsRunSystem
    {
        public void Run()
        {
            var idx_0 = IceWallME.IdxC.Idx;

            var curAbility = EntityMPool.UniqueAbilityC.Ability;


            if (CellUnitEs.Step(idx_0).AmountC.Amount >= CellUnitStepValues.NeedSteps(curAbility))
            {
                CellUnitEs.Step(idx_0).AmountC.Take(CellUnitStepValues.NeedSteps(curAbility));

                CellUnitEs.CooldownUnique(curAbility, idx_0).Cooldown += 5;

                //EntitiesPool.IceWalls[idx_0].Hp += 4;
            }
        }
    }
}