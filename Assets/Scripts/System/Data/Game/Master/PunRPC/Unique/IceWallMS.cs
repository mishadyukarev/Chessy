namespace Game.Game
{
    public struct IceWallMS : IEcsRunSystem
    {
        public void Run()
        {
            var idx_0 = Entities.MasterEs.IceWall.IdxC.Idx;

            var curAbility = Entities.MasterEs.UniqueAbilityC.Ability;


            if (Entities.CellEs.UnitEs.Step(idx_0).Steps.Amount >= CellUnitStepValues.NeedSteps(curAbility))
            {
                Entities.CellEs.UnitEs.Step(idx_0).Steps.Take(CellUnitStepValues.NeedSteps(curAbility));

                Entities.CellEs.UnitEs.CooldownUnique(curAbility, idx_0).Cooldown += 5;

                Entities.CellEs.BuildEs.Build(idx_0).SetNew(BuildingTypes.IceWall, Entities.CellEs.UnitEs.Else(idx_0).OwnerC.Player);
                Entities.WhereBuildingEs.HaveBuild(BuildingTypes.IceWall, Entities.CellEs.UnitEs.Else(idx_0).OwnerC.Player, idx_0).HaveBuilding.Have = true;
            }
        }
    }
}