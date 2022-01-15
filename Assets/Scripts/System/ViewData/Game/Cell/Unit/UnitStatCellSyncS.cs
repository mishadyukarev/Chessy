using UnityEngine;
using static Game.Game.CellE;
using static Game.Game.CellUnitE;
using static Game.Game.CellVEs;

namespace Game.Game
{
    struct UnitStatCellSyncS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (byte idx_0 in Idxs)
            {
                ref var unit_0 = ref Unit<UnitTC>(idx_0);
                ref var level_0 = ref Unit<LevelTC>(idx_0);
                ref var ownUnit_0 = ref Unit<PlayerTC>(idx_0);
                ref var hpUnit_0 = ref Unit<HpC>(idx_0);
                ref var step_0 = ref Unit<StepC>(idx_0);
                ref var water_0 = ref Unit<StepC>(idx_0);
                ref var condUnit_0 = ref Unit<ConditionUnitC>(idx_0);

                ref var barsViewCom = ref ElseCellVE<BarsVC>(idx_0);
                ref var blocksViewCom = ref ElseCellVE<BlocksVC>(idx_0);


                barsViewCom.DisableSR(CellBarTypes.Hp);

                blocksViewCom.DisableBlockSR(CellBlockTypes.Condition);
                blocksViewCom.DisableBlockSR(CellBlockTypes.MaxSteps);
                blocksViewCom.DisableBlockSR(CellBlockTypes.NeedWater);


                if (Unit<IsVisibledC>(WhoseMoveE.CurPlayerI, idx_0).IsVisibled)
                {
                    if (unit_0.Have)
                    {

                        barsViewCom.EnableSR(CellBarTypes.Hp);
                        barsViewCom.SetColorHp(Color.red);

                        float maxHpUnit_0 = UnitCellEC.MAX_HP;

                        float xCordinate = (float)hpUnit_0.Hp / maxHpUnit_0;
                        barsViewCom.SetScale(CellBarTypes.Hp, new Vector3(xCordinate * 0.67f, 0.13f, 1));


                        if (Unit<UnitCellEC>(idx_0).NeedWater)
                        {
                            blocksViewCom.EnableBlockSR(CellBlockTypes.NeedWater);
                        }
                        else
                        {
                            blocksViewCom.DisableBlockSR(CellBlockTypes.NeedWater);
                        }

                        if (Unit<UnitCellEC>(idx_0).HaveMaxSteps)
                        {
                            blocksViewCom.EnableBlockSR(CellBlockTypes.MaxSteps);
                        }
                        else
                        {
                            blocksViewCom.DisableBlockSR(CellBlockTypes.MaxSteps);
                        }

                        if (condUnit_0.Is(ConditionUnitTypes.Protected))
                        {
                            blocksViewCom.EnableBlockSR(CellBlockTypes.Condition);
                            blocksViewCom.SetColor(CellBlockTypes.Condition, Color.yellow);
                        }

                        else if (condUnit_0.Is(ConditionUnitTypes.Relaxed))
                        {
                            blocksViewCom.EnableBlockSR(CellBlockTypes.Condition);
                            blocksViewCom.SetColor(CellBlockTypes.Condition, Color.green);
                        }

                        else
                        {
                            blocksViewCom.DisableBlockSR(CellBlockTypes.Condition);
                        }

                        if (ownUnit_0.Is(PlayerTypes.First))
                        {
                            barsViewCom.SetColorHp(Color.blue);
                            blocksViewCom.SetColor(CellBlockTypes.MaxSteps, Color.blue);
                        }
                        else
                        {
                            barsViewCom.SetColorHp(Color.red);
                            blocksViewCom.SetColor(CellBlockTypes.MaxSteps, Color.red);
                        }
                    }
                }
            }
        }
    }
}
