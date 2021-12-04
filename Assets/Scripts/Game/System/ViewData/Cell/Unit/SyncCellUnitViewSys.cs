using Leopotam.Ecs;

namespace Game.Game
{
    public sealed class SyncCellUnitViewSys : IEcsRunSystem
    {
        public void Run()
        {
            foreach (byte idx_0 in EntityPool.Idxs)
            {
                ref var unit_0 = ref EntityPool.Unit<UnitC>(idx_0);
                ref var levelUnit_0 = ref EntityPool.Unit<LevelC>(idx_0);
                ref var visUnit_0 = ref EntityPool.Unit<VisibleC>(idx_0);

                ref var corner_0 = ref EntityPool.Unit<CornerArcherC>(idx_0);

                ref var tw_0 = ref EntityPool.UnitToolWeapon<ToolWeaponC>(idx_0);
                ref var twLevel_0 = ref EntityPool.UnitToolWeapon<LevelC>(idx_0);

                ref var mainUnitC_0 = ref EntityVPool.UnitCellVC<UnitMainVC>(idx_0);
                ref var extraUnitC_0 = ref EntityVPool.UnitCellVC<UnitExtraVC>(idx_0);


                mainUnitC_0.SetEnabled(false);
                extraUnitC_0.Disable_SR();

                if (unit_0.Have)
                {
                    if (visUnit_0.IsVisibled(WhoseMoveC.CurPlayerI))
                    {
                        mainUnitC_0.SetEnabled(true);

                        if (unit_0.Is(UnitTypes.Pawn))
                        {
                            mainUnitC_0.SetSprite(unit_0.Unit, levelUnit_0.Level, false);

                            if (tw_0.HaveTW)
                            {
                                extraUnitC_0.Enable_SR();
                                extraUnitC_0.SetToolWeapon_Sprite(tw_0.ToolWeapon, twLevel_0.Level);
                            }
                        }

                        else if (unit_0.Is(UnitTypes.Archer))
                        {
                            mainUnitC_0.SetSprite(unit_0.Unit, levelUnit_0.Level, corner_0.IsCornered);
                        }

                        else
                        {
                            mainUnitC_0.SetSprite(unit_0.Unit, levelUnit_0.Level, false);
                        }


                        mainUnitC_0.SetAlpha(visUnit_0.IsVisibled(WhoseMoveC.NextPlayerFrom(WhoseMoveC.CurPlayerI)));
                        extraUnitC_0.SetAlpha(visUnit_0.IsVisibled(WhoseMoveC.NextPlayerFrom(WhoseMoveC.CurPlayerI)));
                    }
                }
            }
        }
    }
}
