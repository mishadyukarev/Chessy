using ECS;

namespace Game.Game
{
    public abstract class CellEnvironmentE : CellEntityAbstract
    {
        public readonly EnvironmentTypes EnvT;
        public readonly ResourceTypes Resource;

        protected ref AmountC ResourcesC => ref Ent.Get<AmountC>();

        public int Resources
        {
            get => ResourcesC.Amount;
            set => ResourcesC.Amount = value;
        }

        public bool HaveEnvironment => Resources > 0;
        public bool HaveMaxResources => Resources >= CellEnvironmentValues.MaxResources(EnvT);
        public bool HaveMinResources => Resources >= CellEnvironmentValues.MinResources(EnvT);
        public bool HaveMoveMaxResources => Resources > CellEnvironmentValues.MaxResources(EnvT);
        public int MaxResources => CellEnvironmentValues.MaxResources(EnvT);
        public int MinResources => CellEnvironmentValues.MinResources(EnvT);
        public float ProtectionPercent => CellUnitMainDamageValues.ProtectionPercent(EnvT);
        protected int StandartExtract(in BuildingTypes build) => CellEnvironmentValues.StandartExtract(build, EnvT);

        public int AmountExtractBuilding(in BuildingUpgradeEs buildUpgEs, in CellBuildingE buildE)
        {
            var extract = StandartExtract(buildE.Building);

            if (buildUpgEs.HaveUpgrade(buildE, UpgradeTypes.PickCenter).HaveUpgrade.Have)
            {
                extract += (int)(extract * CellEnvironmentValues.Upgrade(buildE.Building, UpgradeTypes.PickCenter));
            }

            if (extract > ResourcesC.Amount)
                extract = ResourcesC.Amount;

            return extract;
        }


        protected CellEnvironmentE(in EnvironmentTypes envT, in ResourceTypes resT, in byte idx, in EcsWorld gameW) : base(idx, gameW)
        {
            EnvT = envT;
            Resource = resT;
        }

        public void Take(in int taking = 1)
        {
            Resources -= taking;
        }
        public void Add(in int adding = 1)
        {
            if (!HaveMaxResources)
            {
                ResourcesC.Amount += adding;
                if (HaveMoveMaxResources) ResourcesC.Amount = CellEnvironmentValues.MaxResources(EnvT);
            }
        }

        public void SetMaxResources() => Resources = CellEnvironmentValues.MaxResources(EnvT);



        public void SetRandomResources() => Resources = UnityEngine.Random.Range(MinResources, MaxResources + 1);
        public void SetRandomFromCenterResources() => Resources = UnityEngine.Random.Range(MaxResources / 2, MaxResources + 1);

        public void Destroy()
        {
            Resources = 0;
        }
    }
}