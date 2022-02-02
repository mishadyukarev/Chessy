namespace Game.Game
{
    sealed class CityBuildMS : SystemCellAbstract, IEcsRunSystem
    {
        public CityBuildMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);

            var idx_0 = Es.MasterEs.BuildingCityME.WhereBuildCity.Idx;


            var ability = Es.MasterEs.AbilityC.Ability;


            var whoseMove = Es.WhoseMove.WhoseMove.Player;


            if (UnitStatEs(idx_0).StepE.Have(ability))
            {
                bool haveNearBorder = false;

                foreach (var idx_1 in CellWorker.GetIdxsAround(idx_0))
                {
                    if (!CellEs(idx_1).ParentE.IsActiveSelf.IsActive)
                    {
                        haveNearBorder = true;
                        break;
                    }
                }

                if (!haveNearBorder)
                {
                    Es.Rpc.SoundToGeneral(sender, ClipTypes.Building);
                    Es.Rpc.SoundToGeneral(sender, ClipTypes.AfterBuildTown);


                    BuildEs(idx_0).BuildingE.SetNew(BuildingTypes.City, whoseMove, BuildEs(idx_0), Es.WhereBuildingEs);
                    Es.WhereBuildingEs.HaveBuild(BuildingTypes.City, whoseMove, idx_0).HaveBuilding.Have = true;

                    UnitStatEs(idx_0).StepE.Take(ability);


                    EffectEs(idx_0).FireE.Disable();


                    EnvironmentEs(idx_0).AdultForest.Destroy(TrailEs(idx_0).Trails, Es.WhereEnviromentEs);
                    EnvironmentEs(idx_0).Fertilizer.Destroy(Es.WhereEnviromentEs);
                    EnvironmentEs(idx_0).YoungForest.Destroy(Es.WhereEnviromentEs);
                }

                else
                {
                    Es.Rpc.SimpleMistakeToGeneral(MistakeTypes.NearBorder, sender);
                }
            }
            else
            {
                Es.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}
