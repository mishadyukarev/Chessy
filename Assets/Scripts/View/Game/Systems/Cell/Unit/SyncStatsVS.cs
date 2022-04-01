using Chessy.Game.Entity.Model;
using Chessy.Game.Values.Cell.Unit.Stats;
using UnityEngine;

namespace Chessy.Game
{
    public struct SyncStatsVS
    {
        public void Sync(in byte idx_0, in EntitiesViewGame vEs, in EntitiesModelGame e)
        {
            vEs.CellEs(idx_0).Bar(CellBarTypes.Hp).Disable();

            vEs.CellEs(idx_0).Block(CellBlockTypes.Condition).Disable();
            vEs.CellEs(idx_0).Block(CellBlockTypes.MaxSteps).Disable();
            vEs.CellEs(idx_0).Block(CellBlockTypes.NeedWater).Disable();


            if (e.UnitEs(idx_0).ForPlayer(e.CurPlayerITC.PlayerT).IsVisible)
            {
                if (e.UnitTC(idx_0).HaveUnit && !e.UnitTC(idx_0).IsAnimal)
                {
                    vEs.CellEs(idx_0).Bar(CellBarTypes.Hp).Enable();
                    vEs.CellEs(idx_0).Bar(CellBarTypes.Hp).SR.color = Color.red;

                    float xCordinate = (float)e.UnitHpC(idx_0).Health / HpValues.MAX;
                    vEs.CellEs(idx_0).Bar(CellBarTypes.Hp).Transform.localScale = new Vector3(xCordinate * 0.67f, 0.13f, 1);

                    vEs.CellEs(idx_0).Block(CellBlockTypes.NeedWater).SetActive(e.UnitWaterC(idx_0).Water <= WaterValues.MAX * 0.4f);
                    vEs.CellEs(idx_0).Block(CellBlockTypes.MaxSteps).SetActive(e.UnitStepC(idx_0).Steps >= StepValues.MAX);



                    if (e.UnitConditionTC(idx_0).Is(ConditionUnitTypes.Protected))
                    {
                        vEs.CellEs(idx_0).Block(CellBlockTypes.Condition).Enable();
                        vEs.CellEs(idx_0).Block(CellBlockTypes.Condition).SR.color = Color.yellow;
                    }

                    else if (e.UnitConditionTC(idx_0).Is(ConditionUnitTypes.Relaxed))
                    {
                        vEs.CellEs(idx_0).Block(CellBlockTypes.Condition).Enable();
                        vEs.CellEs(idx_0).Block(CellBlockTypes.Condition).SR.color = Color.green;
                    }

                    else
                    {
                        vEs.CellEs(idx_0).Block(CellBlockTypes.Condition).Disable();
                    }

                    if (e.UnitPlayerTC(idx_0).Is(PlayerTypes.First))
                    {
                        vEs.CellEs(idx_0).Bar(CellBarTypes.Hp).SR.color = Color.blue;
                        vEs.CellEs(idx_0).Block(CellBlockTypes.MaxSteps).SR.color = Color.blue;
                    }
                    else
                    {
                        vEs.CellEs(idx_0).Bar(CellBarTypes.Hp).SR.color = Color.red;
                        vEs.CellEs(idx_0).Block(CellBlockTypes.MaxSteps).SR.color = Color.red;
                    }
                }
            }
        }
    }
}
