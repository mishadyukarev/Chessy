using Chessy.Game.Values;
using Chessy.Game.Values.Cell.Unit.Stats;
using UnityEngine;

namespace Chessy.Game
{
    sealed class UnitStatCellSyncS : SystemViewAbstract, IEcsRunSystem
    {
        public UnitStatCellSyncS(in EntitiesModel ents, in EntitiesView entsView) : base(ents, entsView)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < E.LengthCells; idx_0++)
            {
                VEs.CellEs(idx_0).Bar(CellBarTypes.Hp).Disable();


                VEs.CellEs(idx_0).Block(CellBlockTypes.Condition).Disable();
                VEs.CellEs(idx_0).Block(CellBlockTypes.MaxSteps).Disable();
                VEs.CellEs(idx_0).Block(CellBlockTypes.NeedWater).Disable();


                if (E.UnitEs(idx_0).ForPlayer(E.CurPlayerITC.Player).IsVisible)
                {
                    if (E.UnitTC(idx_0).HaveUnit && !E.IsAnimal(E.UnitTC(idx_0).Unit))
                    {
                        VEs.CellEs(idx_0).Bar(CellBarTypes.Hp).Enable();
                        VEs.CellEs(idx_0).Bar(CellBarTypes.Hp).Color = Color.red;

                        float xCordinate = (float)E.UnitHpC(idx_0).Health / Hp_VALUES.HP;
                        VEs.CellEs(idx_0).Bar(CellBarTypes.Hp).LocalScale = new Vector3(xCordinate * 0.67f, 0.13f, 1);

                        VEs.CellEs(idx_0).Block(CellBlockTypes.NeedWater).SetActive(E.UnitWaterC(idx_0).Water <= WaterValues.MAX * 0.4f);
                        VEs.CellEs(idx_0).Block(CellBlockTypes.MaxSteps).SetActive(E.UnitStepC(idx_0).Steps >= E.UnitStatsE(idx_0).MaxStepsC.Steps);



                        if (E.UnitConditionTC(idx_0).Is(ConditionUnitTypes.Protected))
                        {
                            VEs.CellEs(idx_0).Block(CellBlockTypes.Condition).Enable();
                            VEs.CellEs(idx_0).Block(CellBlockTypes.Condition).Color = Color.yellow;
                        }

                        else if (E.UnitConditionTC(idx_0).Is(ConditionUnitTypes.Relaxed))
                        {
                            VEs.CellEs(idx_0).Block(CellBlockTypes.Condition).Enable();
                            VEs.CellEs(idx_0).Block(CellBlockTypes.Condition).Color = Color.green;
                        }

                        else
                        {
                            VEs.CellEs(idx_0).Block(CellBlockTypes.Condition).Disable();
                        }

                        if (E.UnitPlayerTC(idx_0).Is(PlayerTypes.First))
                        {
                            VEs.CellEs(idx_0).Bar(CellBarTypes.Hp).Color = Color.blue;
                            VEs.CellEs(idx_0).Block(CellBlockTypes.MaxSteps).Color = Color.blue;
                        }
                        else
                        {
                            VEs.CellEs(idx_0).Bar(CellBarTypes.Hp).Color = Color.red;
                            VEs.CellEs(idx_0).Block(CellBlockTypes.MaxSteps).Color = Color.red;
                        }
                    }
                }
            }
        }
    }
}
