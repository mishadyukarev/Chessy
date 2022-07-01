//using System;

//using Chessy.Model.Entity; namespace Chessy.Model
//{
//    sealed class UpdateIceWallMS : SystemAbstract, IEcsRunSystem
//    {
//        internal UpdateIceWallMS(in Chessy.Model.EntitiesModel ents) : base(ents)
//        {
//        }

//        public void Run()
//        {
//            for (byte cell_0 = 0; cell_0 < E.LengthCells; cell_0++)
//            {
//                if (E.BuildingTC(cell_0).HaveBuilding && E.BuildingTC(cell_0).Is(BuildingTypes.IceWall))
//                {
//                    E.BuildHpC(cell_0).Health--;
//                    //if (!Es.BuildHpC(cell_0).IsAlive) //Es.BuildTC(cell_0).Destroy(Es);
//                }
//            }
//        }
//    }
//}