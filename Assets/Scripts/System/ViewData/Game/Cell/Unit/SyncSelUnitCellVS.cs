using System;

namespace Game.Game
{
    sealed class SyncSelUnitCellVS : SystemViewAbstract, IEcsRunSystem
    {
        internal SyncSelUnitCellVS(in Entities ents, in EntitiesView entsView) : base(ents, entsView) { }

        public void Run()
        {
            if (Es.ClickerObject.CellClickC.Is(CellClickTypes.SetUnit))
            {
                var idx_cur = Es.CurrentIdxE.IdxC.Idx;

                ref var mainUnit_cur = ref CellVEs(idx_cur).UnitVEs.UnitMainSR;
                ref var mainUnit_pre = ref CellVEs(Es.PreviousVisionIdxE.IdxC.Idx).UnitVEs.UnitMainSR;


                if (UnitEs(idx_cur).MainE.HaveUnit)
                {
                    if (UnitEs(idx_cur).VisibleE(Es.WhoseMove.CurPlayerI).IsVisibleC.IsVisible)
                    {
                        mainUnit_pre.Enable();
                    }

                    else
                    {
                        mainUnit_cur.Enable();
                    }
                }

                else
                {
                    mainUnit_cur.Enable();

                }


                var selUnitT = Es.SelectedUnitE.UnitTC.Unit;
                var selLevelUnitT = Es.SelectedUnitE.LevelTC.Level;

                if(selUnitT == UnitTypes.Archer)
                {
                    mainUnit_cur.Sprite = VEs.ResourceSpriteEs.Sprite(false, selLevelUnitT).SpriteC.Sprite;
                }
                else
                {
                    mainUnit_cur.Sprite = VEs.ResourceSpriteEs.Sprite(selUnitT, selLevelUnitT).SpriteC.Sprite;
                }
            }
        }
    }
}
