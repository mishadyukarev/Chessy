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
                ref var fire_0 = ref CellEs.FireEs.Fire(idx_0).Fire;


                var whoseMove = Es.WhoseMove.WhoseMove.Player;


                if (UnitEs.StatEs.Step(idx_0).Steps.Amount >= CellUnitStepValues.NeedSteps(BuildingTypes.City))
                {
                    bool haveNearBorder = false;

                    foreach (var idx_1 in CellEs.GetIdxsAround(idx_0))
                    {
                        if (!CellEs.ParentE(idx_1).IsActiveSelf.IsActive)
                        {
                            haveNearBorder = true;
                            break;
                        }
                    }

                    if (!haveNearBorder)
                    {
                        Es.Rpc.SoundToGeneral(sender, ClipTypes.Building);
                        Es.Rpc.SoundToGeneral(sender, ClipTypes.AfterBuildTown);


                        BuildEs.BuildingE(idx_0).SetNew(forBuildType, whoseMove, BuildEs, Es.WhereBuildingEs);
                        Es.WhereBuildingEs.HaveBuild(forBuildType, whoseMove, idx_0).HaveBuilding.Have = true;

                        UnitEs.StatEs.Step(idx_0).Steps.Amount -= CellUnitStepValues.NeedSteps(BuildingTypes.City);


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
