//using ECS;

//namespace Game.Game
//{
//    public abstract class CellEnvironmentE : CellEntityAbstract
//    {
//        public readonly EnvironmentTypes EnvironmentT;
//        public readonly ResourceTypes Resource;

//        public ResourcesC ResourcesC => Ent.Get<ResourcesC>();

//        public float Resources => ResourcesC.Resources;
//        public float MaxResources => CellEnvironmentValues.STANDART_MAX_AMOUNT_RESOURCES;
//        public float MinResources => CellEnvironmentValues.MinResources(EnvironmentT);

//        public bool HaveEnvironment => ResourcesC.Resources > 0;
//        public bool HaveMaxResources => Resources >= CellEnvironmentValues.STANDART_MAX_AMOUNT_RESOURCES;

//        protected float StandartExtract(in BuildingTypes build) => CellEnvironmentValues.StandartExtract(build, EnvironmentT);
//        public float AmountExtractBuilding(in BuildingUpgradeEs buildUpgEs, in CellBuildingE buildE)
//        {
//            var extract = StandartExtract(buildE.Building);

//            if (buildUpgEs.HaveUpgrade(buildE, UpgradeTypes.PickCenter).HaveUpgrade.Have)
//            {
//                extract += extract * CellEnvironmentValues.Upgrade(buildE.Building, UpgradeTypes.PickCenter);
//            }

//            if (extract > Resources)
//                extract = Resources;

//            return extract;
//        }

//        protected CellEnvironmentE(in EnvironmentTypes envT, in ResourceTypes resT, in CellEs cellEs, in EcsWorld gameW) : base(cellEs, gameW)
//        {
//            EnvironmentT = envT;
//            Resource = resT;
//        }


//        public void SetResources(in float resources) => ResourcesC.Resources = resources;
//        public void SetZeroResources() => SetResources(0);
//        public void SetMaxResources() => SetResources(CellEnvironmentValues.STANDART_MAX_AMOUNT_RESOURCES);
//        public void SetRandomResources(in float low, in float max) => SetResources(UnityEngine.Random.Range(low, max));
//        public void SetRandomResources() => SetRandomResources(MinResources, MaxResources);
//        public void SetRandomFromCenterResources() => SetRandomResources(MaxResources / 2, MaxResources);

//        public void Add(in float adding)
//        {
//            if (!HaveMaxResources)
//            {
//                ResourcesC.Resources += adding;
//                if (Resources > CellEnvironmentValues.STANDART_MAX_AMOUNT_RESOURCES) SetResources(CellEnvironmentValues.STANDART_MAX_AMOUNT_RESOURCES);
//            }
//        }

//        public void Take(in float taking)
//        {
//            ResourcesC.Resources -= taking;
//        }
//    }
//}