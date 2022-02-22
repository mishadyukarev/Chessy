﻿using System;

namespace Game.Game
{
    sealed class UpdateIceWallMS : SystemAbstract, IEcsRunSystem
    {
        internal UpdateIceWallMS(in EntitiesModel ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < E.LengthCells; idx_0++)
            {
                if (E.BuildTC(idx_0).HaveBuilding && E.BuildTC(idx_0).Is(BuildingTypes.IceWall))
                {
                    E.BuildHpC(idx_0).Health--;
                    //if (!Es.BuildHpC(idx_0).IsAlive) //Es.BuildTC(idx_0).Destroy(Es);
                }
            }
        }
    }
}