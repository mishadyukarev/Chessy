using System.Collections.Generic;

namespace Game.Game
{
    sealed class UpdateFireMS : SystemCellAbstract, IEcsRunSystem
    {
        internal UpdateFireMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            foreach (var idx_1 in CellWorker.GetIdxsAround(Es.WindE.CenterCloud.Idx))
            {
                EffectEs(idx_1).FireE.Disable();
            }


            var needForFireNext = new List<byte>();

            for (byte idx_0 = 0; idx_0 < Es.LengthCells; idx_0++)
            {
                if (EffectEs(idx_0).FireE.HaveFireC.Have)
                {
                    EnvironmentEs(idx_0).AdultForest.Fire();

                    if (UnitEs(idx_0).MainE.HaveUnit(UnitStatEs(idx_0)))
                    {
                        UnitStatEs(idx_0).Hp.Fire(Es);
                    }

                    if (!EnvironmentEs(idx_0).AdultForest.HaveEnvironment)
                    {
                        BuildEs(idx_0).BuildingE.Destroy(BuildEs(idx_0), Es.WhereBuildingEs);

                        EnvironmentEs(idx_0).AdultForest.Destroy(TrailEs(idx_0).Trails, Es.WhereEnviromentEs);

                        EnvironmentEs(idx_0).YoungForest.TrySetAfterFireForest(Es.WhereEnviromentEs);

                        EffectEs(idx_0).FireE.Disable();


                        foreach (var idx_1 in CellWorker.GetIdxsAround(idx_0))
                        {
                            needForFireNext.Add(idx_1);
                        }
                    }
                }
            }

            foreach (var idx_0 in needForFireNext)
            {
                if (CellEs(idx_0).ParentE.IsActiveSelf.IsActive)
                {
                    if (EnvironmentEs(idx_0).AdultForest.HaveEnvironment)
                    {
                        EffectEs(idx_0).FireE.Enable();
                    }
                }
            }
        }
    }
}