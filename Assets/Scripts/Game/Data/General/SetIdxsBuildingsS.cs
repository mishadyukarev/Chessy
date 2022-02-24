//namespace Game.Game
//{
//    sealed class SetIdxsBuildingsS : SystemAbstract, IEcsRunSystem
//    {
//        internal SetIdxsBuildingsS(in EntitiesModel ents) : base(ents)
//        {
//        }

//        public void Run()
//        {
//            for (var buildT = BuildingTypes.None + 1; buildT < BuildingTypes.End; buildT++)
//            {
//                for (var playerT = PlayerTypes.None + 1; playerT < PlayerTypes.End; playerT++)
//                {
//                    E.BuildingsInfo(playerT, LevelTypes.First, buildT).IdxC.Clear();
//                }
//            }

//            for (byte idx_0 = 0; idx_0 < Start_Values.ALL_CELLS_AMOUNT; idx_0++)
//            {
//                if (E.BuildTC(idx_0).HaveBuilding)
//                {
//                    E.BuildingsInfo(E.BuildingMainE(idx_0)).IdxC.Add(idx_0);
//                }
//            }
//        }
//    }
//}