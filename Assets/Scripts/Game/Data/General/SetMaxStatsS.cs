//namespace Game.Game
//{
//    sealed class SetMaxStatsS : SystemAbstract, IEcsRunSystem
//    {
//        internal SetMaxStatsS(in EntitiesModel ents) : base(ents)
//        {
//        }

//        public void Run()
//        {
//            for (var playerT = PlayerTypes.None + 1; playerT < PlayerTypes.End; playerT++)
//            {
//                for (var levT = LevelTypes.None + 1; levT < LevelTypes.End; levT++)
//                {
//                    for (var unitT = UnitTypes.None + 1; unitT < UnitTypes.End; unitT++)
//                    {
//                        for (var statT = UnitStatTypes.Steps; statT <= UnitStatTypes.Water; statT++)
//                        {
//                            E.UnitInfo(playerT, levT, unitT).MaxStepsC.Steps    E.UnitInfo(playerT, levT, unitT).Max(statT)
//                        }
//                    }
//                }
//            }
//        }
//    } }