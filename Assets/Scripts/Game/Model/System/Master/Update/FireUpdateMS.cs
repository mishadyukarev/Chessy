using Chessy.Game.Entity.Model;
using Chessy.Game.Values;
using Chessy.Game.Values.Cell.Environment;
using Chessy.Game.Values.Cell.Unit.Stats;
using System.Collections.Generic;

namespace Chessy.Game.System.Model
{
    sealed class FireUpdateMS : SystemModelGameAbs
    {
        readonly SystemsModelGame _sMM;

        internal FireUpdateMS(in SystemsModelGame sMM, in EntitiesModelGame eMGame) : base(eMGame)
        {
            _sMM = sMM;
        }

        public void Run()
        {
            foreach (var cellE in eMGame.CellEs(eMGame.WeatherE.CloudC.Center).AroundCellsEs.AroundCellEs)
            {
                eMGame.HaveFire(cellE.IdxC.Idx) = false;
            }


            var needForFireNext = new List<byte>();

            for (byte cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
            {
                if (eMGame.HaveFire(cell_0))
                {
                    _sMM.TakeAdultForestResourcesS.Take(EnvironmentValues.FIRE_ADULT_FOREST, cell_0);

                    if (eMGame.UnitTC(cell_0).HaveUnit)
                    {
                        if (eMGame.UnitTC(cell_0).Is(UnitTypes.Hell))
                        {
                            eMGame.UnitHpC(cell_0).Health = HpValues.MAX;
                        }
                        else
                        {
                            if (eMGame.UnitPlayerTC(cell_0).Is(PlayerTypes.None))
                            {
                                _sMM.CellSs(cell_0).AttackUnitS.Attack(HpValues.FIRE_DAMAGE, PlayerTypes.None);
                            }
                            else
                            {
                                _sMM.CellSs(cell_0).AttackUnitS.Attack(HpValues.FIRE_DAMAGE, eMGame.NextPlayer(eMGame.UnitPlayerTC(cell_0).Player).Player);
                            }
                        }
                    }

                    if (!eMGame.AdultForestC(cell_0).HaveAnyResources)
                    {
                        eMGame.BuildingTC(cell_0).Building = BuildingTypes.None;

                        if (UnityEngine.Random.Range(0f, 1f) < EnvironmentValues.PERCENT_SPAWN_FOR_YOUNG_FOREST_AFTER_FIRE)
                        {
                            eMGame.YoungForestC(cell_0).Resources -= EnvironmentValues.FIRE_ADULT_FOREST;
                        }


                        eMGame.HaveFire(cell_0) = false;


                        foreach (var cellE in eMGame.CellEs(cell_0).AroundCellsEs.AroundCellEs)
                        {
                            needForFireNext.Add(cellE.IdxC.Idx);
                        }
                    }
                }
            }

            foreach (var cell_0 in needForFireNext)
            {
                if (eMGame.CellEs(cell_0).IsActiveParentSelf)
                {
                    if (eMGame.AdultForestC(cell_0).HaveAnyResources)
                    {
                        eMGame.HaveFire(cell_0) = true;
                    }
                }
            }
        }
    }
}