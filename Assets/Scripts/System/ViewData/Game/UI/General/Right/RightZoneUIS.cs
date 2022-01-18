﻿using static Game.Game.CellUnitEs;
using static Game.Game.EntityPool;

namespace Game.Game
{
    struct RightZoneUIS : IEcsRunSystem
    {
        public void Run()
        {
            ref var unit_sel = ref Unit<UnitTC>(SelIdx<IdxC>().Idx);

            var activeParent = false;


            if (SelIdx<SelIdxEC>().IsSelCell)
            {
                if (unit_sel.Have)
                {
                    if (Unit<IsVisibleC>(WhoseMoveE.CurPlayerI, SelIdx<IdxC>().Idx).IsVisible)
                    {
                        activeParent = true;
                    }
                }
            }

            UIEntRight.Zone<GameObjectVC>().SetActive(activeParent);
        }
    }
}