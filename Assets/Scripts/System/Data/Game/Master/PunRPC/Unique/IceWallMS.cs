namespace Game.Game
{
    sealed class IceWallMS : SystemCellAbstract, IEcsRunSystem
    {
        public IceWallMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            var idx_0 = Es.MasterEs.IceWall.IdxC.Idx;

            var curAbility = Es.MasterEs.UniqueAbilityC.Ability;


            if (UnitEs.StatEs.Step(idx_0).Steps.Amount >= CellUnitStepValues.NeedSteps(curAbility))
            {
                UnitEs.StatEs.Step(idx_0).Steps.Amount -= CellUnitStepValues.NeedSteps(curAbility);

                UnitEs.CooldownAbility(curAbility, idx_0).SetAfterAbility();

                BuildEs.BuildingE(idx_0).SetNew(BuildingTypes.IceWall, UnitEs.Main(idx_0).OwnerC.Player, BuildEs, Es.WhereBuildingEs);
                Es.WhereBuildingEs.HaveBuild(BuildingTypes.IceWall, UnitEs.Main(idx_0).OwnerC.Player, idx_0).HaveBuilding.Have = true;
            }
        }
    }
}