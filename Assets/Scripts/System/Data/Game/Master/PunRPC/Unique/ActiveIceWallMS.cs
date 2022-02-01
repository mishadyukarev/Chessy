namespace Game.Game
{
    sealed class ActiveIceWallMS : SystemAbstract, IEcsRunSystem
    {
        internal ActiveIceWallMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            var whoseMove = Es.WhoseMove.WhoseMove.Player;
            var ability = Es.MasterEs.AbilityC.Ability;


            Es.WhereBuildingEs.TryGetBuilding(BuildingTypes.IceWall, whoseMove, out var idx_0);
            BuildEs(idx_0).BuildingE.Destroy(BuildEs(idx_0), Es.WhereBuildingEs);

            foreach (var idx_1 in CellEsWorker.GetIdxsAround(idx_0))
            {
                if (UnitEs(idx_1).MainE.HaveUnit(UnitStatEs(idx_1)))
                {
                    if (UnitEs(idx_1).MainE.OwnerC.Is(whoseMove))
                    {
                        UnitStatEs(idx_1).Water.SetMax(UnitEs(idx_1).MainE, Es.UnitStatUpgradesEs);
                        UnitEffectEs(idx_1).ShieldE.Set(ability);
                    }
                    else
                    {
                        UnitEffectEs(idx_1).StunE.Set(ability);
                    }    
                }
            }
        }
    }
}