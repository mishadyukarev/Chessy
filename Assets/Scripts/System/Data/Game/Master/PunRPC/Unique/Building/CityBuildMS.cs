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

            var forBuildType = Es.MasterEs.Build<BuildingTC>().Build;
            var idx_0 = Es.MasterEs.Build<IdxC>().Idx;



            if (forBuildType == BuildingTypes.City)
            {
                ref var build_0 = ref Es.CellEs.BuildEs.Build(idx_0).BuildTC;
                ref var ownBuild_0 = ref Es.CellEs.BuildEs.Build(idx_0).PlayerTC;

                ref var fire_0 = ref Es.CellEs.FireEs.Fire(idx_0).Fire;


                var whoseMove = Es.WhoseMove.WhoseMove.Player;


                if (Es.CellEs.UnitEs.StatEs.Step(idx_0).Steps.Amount >= CellUnitStepValues.NeedSteps(BuildingTypes.City))
                {
                    bool haveNearBorder = false;

                    foreach (var idx_1 in Es.CellEs.GetIdxsAround(idx_0))
                    {
                        if (!Es.CellEs.ParentE(idx_1).IsActiveSelf.IsActive)
                        {
                            haveNearBorder = true;
                            break;
                        }
                    }

                    if (!haveNearBorder)
                    {
                        Es.Rpc.SoundToGeneral(sender, ClipTypes.Building);
                        Es.Rpc.SoundToGeneral(sender, ClipTypes.AfterBuildTown);


                        Es.CellEs.BuildEs.Build(idx_0).SetNew(forBuildType, whoseMove);
                        Es.WhereBuildingEs.HaveBuild(forBuildType, whoseMove, idx_0).HaveBuilding.Have = true;

                        Es.CellEs.UnitEs.StatEs.Step(idx_0).Steps.Take(CellUnitStepValues.NeedSteps(BuildingTypes.City));


                        fire_0.Disable();


                        EnvironmentEs.AdultForest(idx_0).Destroy(TrailEs.Trails(idx_0), Es.WhereEnviromentEs);
                        EnvironmentEs.Fertilizer(idx_0).Destroy(Es.WhereEnviromentEs);
                        EnvironmentEs.YoungForest(idx_0).Destroy(Es.WhereEnviromentEs);
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
}
