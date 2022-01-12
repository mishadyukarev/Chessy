using static Game.Game.EntityCellPool;
using static Game.Game.EntCellUnit;
using static Game.Game.EntityCellVPool;

namespace Game.Game
{
    struct SyncCellUnitVS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (byte idx_0 in Idxs)
            {
                ref var unit_0 = ref Unit<UnitC>(idx_0);
                ref var levelUnit_0 = ref Unit<LevelC>(idx_0);

                ref var corner_0 = ref Unit<CornerArcherC>(idx_0);

                ref var tw_0 = ref UnitTW<ToolWeaponC>(idx_0);
                ref var twLevel_0 = ref UnitTW<LevelC>(idx_0);

                ref var mainUnit_0 = ref UnitV<UnitMainVC>(idx_0);
                ref var extraUnit_0 = ref UnitV<UnitExtraVC>(idx_0);


                mainUnit_0.SetEnabled(false);
                extraUnit_0.Disable_SR();

                if (unit_0.Have)
                {
                    if (Unit<VisibledC>(WhoseMoveC.CurPlayerI, idx_0).IsVisibled)
                    {
                        mainUnit_0.SetEnabled(true);

                        if (unit_0.Is(UnitTypes.Pawn))
                        {
                            mainUnit_0.SetSprite(unit_0.Unit, levelUnit_0.Level, false);

                            if (tw_0.HaveTW)
                            {
                                extraUnit_0.Enable_SR();
                                extraUnit_0.SetToolWeapon_Sprite(tw_0.ToolWeapon, twLevel_0.Level);
                            }
                        }

                        else if (unit_0.Is(UnitTypes.Archer))
                        {
                            mainUnit_0.SetSprite(unit_0.Unit, levelUnit_0.Level, corner_0.IsCornered);
                        }

                        else
                        {
                            mainUnit_0.SetSprite(unit_0.Unit, levelUnit_0.Level, false);
                        }


                        mainUnit_0.SetAlpha(Unit<VisibledC>(WhoseMoveC.NextPlayerFrom(WhoseMoveC.CurPlayerI), idx_0).IsVisibled);
                        extraUnit_0.SetAlpha(Unit<VisibledC>(WhoseMoveC.NextPlayerFrom(WhoseMoveC.CurPlayerI), idx_0).IsVisibled);
                    }
                }
            }
        }
    }
}
