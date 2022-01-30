namespace Game.Game
{
    sealed class IceWallMS : SystemAbstract, IEcsRunSystem
    {
        public IceWallMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            var idx_0 = Es.MasterEs.IceWall.IdxC.Idx;

            var curAbility = Es.MasterEs.UniqueAbilityC.Ability;


            if (Es.CellEs.UnitEs.StatEs.Step(idx_0).Steps.Amount >= CellUnitStepValues.NeedSteps(curAbility))
            {
                Es.CellEs.UnitEs.StatEs.Step(idx_0).Steps.Take(CellUnitStepValues.NeedSteps(curAbility));

                Es.CellEs.UnitEs.Unique(curAbility, idx_0).Cooldown += 5;

                Es.CellEs.BuildEs.Build(idx_0).SetNew(BuildingTypes.IceWall, Es.CellEs.UnitEs.Main(idx_0).OwnerC.Player);
                Es.WhereBuildingEs.HaveBuild(BuildingTypes.IceWall, Es.CellEs.UnitEs.Main(idx_0).OwnerC.Player, idx_0).HaveBuilding.Have = true;
            }
        }
    }
}