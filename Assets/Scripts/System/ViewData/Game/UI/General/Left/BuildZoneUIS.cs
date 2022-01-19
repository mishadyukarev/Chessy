﻿using static Game.Game.CellBuildE;
using static Game.Game.EntityLeftCityUIPool;
using static Game.Game.EntityPool;

namespace Game.Game
{
    struct BuildZoneUIS : IEcsRunSystem
    {
        public void Run()
        {
            ref var unit_sel = ref Build<BuildingTC>(SelectedIdxE.IdxC.Idx);
            ref var own_sel = ref Build<PlayerTC>(SelectedIdxE.IdxC.Idx);


            if (SelectedIdxE.IsSelCell && unit_sel.Is(BuildingTypes.City))
            {
                if (own_sel.Is(WhoseMoveE.CurPlayerI))
                {
                    Melt<ButtonUIC>().SetActiveParent(true);
                }
                else Melt<ButtonUIC>().SetActiveParent(false);
            }
            else
            {
                Melt<ButtonUIC>().SetActiveParent(false);
            }
        }
    }
}