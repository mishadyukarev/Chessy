using System.Collections.Generic;

namespace Game.Game
{
    sealed class FireUpdateMS : SystemAbstract, IEcsRunSystem
    {
        internal FireUpdateMS(in EntitiesModel ents) : base(ents)
        {
        }

        public void Run()
        {
            foreach (var cellE in E.CellEs(E.CenterCloudIdxC.Idx).AroundCellEs)
            {
                E.HaveFire(cellE.IdxC.Idx) = false;
            }


            var needForFireNext = new List<byte>();

            for (byte idx_0 = 0; idx_0 < Start_Values.ALL_CELLS_AMOUNT; idx_0++)
            {
                if (E.HaveFire(idx_0))
                {
                    E.AdultForestC(idx_0).Resources -= CellEnvironment_Values.FireAdultForest;

                    if (E.UnitTC(idx_0).HaveUnit)
                    {
                        if (E.UnitTC(idx_0).Is(UnitTypes.Hell))
                        {
                            E.UnitHpC(idx_0).Health = CellUnitStatHp_Values.MAX_HP;
                        }
                        else
                        {
                            E.UnitAttackE.Attack(CellUnitStatHp_Values.FIRE_DAMAGE, E.NextPlayer(E.UnitPlayerTC(idx_0).Player).Player, idx_0);
                        }
                    }

                    if (!E.AdultForestC(idx_0).HaveAnyResources)
                    {
                        E.BuildTC(idx_0).Building = BuildingTypes.None;

                        if (UnityEngine.Random.Range(0f, 1f) < CellEnvironment_Values.PERCENT_SPAWN_FOR_YOUNG_FOREST_AFTER_FIRE)
                        {
                            E.YoungForestC(idx_0).Resources -= CellEnvironment_Values.FireAdultForest;
                        }


                        E.HaveFire(idx_0) = false;


                        foreach (var cellE in E.CellEs(idx_0).AroundCellEs)
                        {
                            needForFireNext.Add(cellE.IdxC.Idx);
                        }
                    }
                }
            }

            foreach (var idx_0 in needForFireNext)
            {
                if (E.CellEs(idx_0).IsActiveParentSelf)
                {
                    if (E.AdultForestC(idx_0).HaveAnyResources)
                    {
                        E.HaveFire(idx_0) = true;
                    }
                }
            }
        }
    }
}