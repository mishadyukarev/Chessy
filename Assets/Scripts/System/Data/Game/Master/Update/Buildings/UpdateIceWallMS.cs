﻿using System;

namespace Game.Game
{
    sealed class UpdateIceWallMS : SystemAbstract, IEcsRunSystem
    {
        internal UpdateIceWallMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < Es.LengthCells; idx_0++)
            {
                if (Es.BuildTC(idx_0).HaveBuilding && Es.BuildTC(idx_0).Is(BuildingTypes.IceWall))
                {
                    Es.BuildHpC(idx_0).Health--;
                    //if (!Es.BuildHpC(idx_0).IsAlive) //Es.BuildTC(idx_0).Destroy(Es);
                }
            }
        }
    }
}