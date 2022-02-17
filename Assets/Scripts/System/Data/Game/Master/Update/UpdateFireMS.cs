using System.Collections.Generic;

namespace Game.Game
{
    sealed class UpdateFireMS : SystemAbstract, IEcsRunSystem
    {
        internal UpdateFireMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            foreach (var idx_1 in CellWorker.GetIdxsAround(Es.CenterCloudIdxC.Idx))
            {
                Es.HaveFire(idx_1) = false;
            }


            var needForFireNext = new List<byte>();

            for (byte idx_0 = 0; idx_0 < Es.LengthCells; idx_0++)
            {
                if (Es.HaveFire(idx_0))
                {
                    Es.AdultForestC(idx_0).Resources -= CellEnvironment_Values.FireAdultForest;

                    if (Es.UnitTC(idx_0).HaveUnit)
                    {
                        if (Es.UnitTC(idx_0).Is(UnitTypes.Hell))
                        {
                            Es.UnitHpC(idx_0).Health = CellUnitStatHp_Values.MAX_HP;
                        }
                        else
                        {
                            //Es.UnitE(idx_0).Take(Es, CellUnitStatHpValues.FIRE_DAMAGE);
                        }
                    }

                    if (!Es.AdultForestC(idx_0).HaveAny)
                    {
                        //Es.BuildE(idx_0).BuildingE.Destroy(Es);

                        //Es.AdultForestC(idx_0).Destroy(Es.TrailEs(idx_0).Trails);

                        if (UnityEngine.Random.Range(0f, 1f) < CellEnvironment_Values.PERCENT_SPAWN_FOR_YOUNG_FOREST_AFTER_FIRE)
                        {
                            Es.YoungForestC(idx_0).Resources -= CellEnvironment_Values.FireAdultForest;
                        }


                        Es.HaveFire(idx_0) = false;


                        foreach (var idx_1 in CellWorker.GetIdxsAround(idx_0))
                        {
                            needForFireNext.Add(idx_1);
                        }
                    }
                }
            }

            foreach (var idx_0 in needForFireNext)
            {
                if (Es.CellEs(idx_0).IsActiveParentSelf)
                {
                    if (Es.AdultForestC(idx_0).HaveAny)
                    {
                        Es.HaveFire(idx_0) = true;
                    }
                }
            }
        }
    }
}