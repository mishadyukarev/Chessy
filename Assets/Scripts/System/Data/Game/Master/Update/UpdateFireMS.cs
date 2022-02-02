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
            CellWorker.TryGetIdxAround(Es.WindE.CenterCloud.Idx, out var directs);

            foreach (var item in directs)
            {
                EffectEs(item.Value).FireE.Disable();
            }


            var needForFireNext = new List<byte>();

            foreach (byte idx_0 in CellWorker.Idxs)
            {
                var xy_0 = CellEs(idx_0).CellE.XyC.Xy;

                var unit_0 = UnitEs(idx_0).MainE.UnitTC;
                var ownUnit_0 = UnitEs(idx_0).MainE.OwnerC;

                var hpUnit_0 = UnitStatEs(idx_0).Hp.Health;

                var buil_0 = BuildEs(idx_0).BuildingE.BuildTC;
                var ownBuil_0 = BuildEs(idx_0).BuildingE.OwnerC;

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


                        if (UnityEngine.Random.Range(0, 100) < 50)
                        {
                            EnvironmentEs(idx_0).YoungForest.SetNewRandom(Es.WhereEnviromentEs);
                        }

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