//using System;

//namespace Chessy.Game
//{
//    sealed class UpdateIceWallMS : SystemAbstract, IEcsRunSystem
//    {
//        internal UpdateIceWallMS(in Chessy.Game.Entity.Model.EntitiesModel ents) : base(ents)
//        {
//        }

//        public void Run()
//        {
//            for (byte idx_0 = 0; idx_0 < E.LengthCells; idx_0++)
//            {
//                if (E.BuildingTC(idx_0).HaveBuilding && E.BuildingTC(idx_0).Is(BuildingTypes.IceWall))
//                {
//                    E.BuildHpC(idx_0).Health--;
//                    //if (!Es.BuildHpC(idx_0).IsAlive) //Es.BuildTC(idx_0).Destroy(Es);
//                }
//            }
//        }
//    }
//}