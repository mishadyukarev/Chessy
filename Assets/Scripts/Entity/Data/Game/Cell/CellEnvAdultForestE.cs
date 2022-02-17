//using ECS;

//namespace Game.Game
//{
//    public sealed class CellEnvAdultForestE : CellEnvironmentE
//    {
//        public float AmountExtractPawn(in CellUnitE unitE)
//        {
//            var extract = MaxResources * CellEnvironmentValues.RatioExtractPawnFromMaxResource(unitE.LevelTC.Level, EnvironmentT);

//            if (extract > Resources) extract = Resources;

//            return extract;
//        }

//        public bool CanExtractPawn
//        {
//            get
//            {
//                if (HaveEnvironment
//                    && CellEs.UnitC.Is(UnitTypes.Pawn)
//                    && CellEs.UnitEs.UnitE.ConditionTC.Is(ConditionUnitTypes.Relaxed)
//                    && CellEs.UnitE.HealthC.Health >= CellUnitStatHpValues.MAX_HP)
//                {
//                    return true;
//                }
//                else return false;
//            }
//        }

//        public bool CanExtractWoodcutter(in CellBuildingEs buildEs)
//        {
//            if (buildEs.BuildingE.HaveBuilding
//                && buildEs.BuildingE.Is(BuildingTypes.Woodcutter)
//                && HaveEnvironment) return true;
//            else return false;
//        }


//        internal CellEnvAdultForestE(in CellEs cellEs, in EcsWorld gameW) : base(EnvironmentTypes.AdultForest, ResourceTypes.Wood, cellEs, gameW)
//        {
//        }

//        public void Destroy(in CellTrailE[] trailEs)
//        {
//            SetZeroResources();
//            foreach (var trailE in trailEs) trailE.HealthC.Health = 0;
//        }
//        public void Fire() => Take(CellEnvironmentValues.FireAdultForest);
//    }
//}