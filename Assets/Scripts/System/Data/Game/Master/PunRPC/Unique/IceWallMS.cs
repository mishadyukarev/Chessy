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

            var curAbility = Es.MasterEs.AbilityC.Ability;


            if (UnitStatEs(idx_0).StepE.Have(curAbility))
            {
                UnitStatEs(idx_0).StepE.Take(curAbility);

                UnitEs(idx_0).CooldownAbility(curAbility).SetAfterAbility();

                BuildEs(idx_0).BuildingE.SetNew(BuildingTypes.IceWall, UnitEs(idx_0).MainE.OwnerC.Player, BuildEs(idx_0), Es.WhereBuildingEs);
                Es.WhereBuildingEs.HaveBuild(BuildingTypes.IceWall, UnitEs(idx_0).MainE.OwnerC.Player, idx_0).HaveBuilding.Have = true;
            }
        }
    }
}