using ECS;

namespace Game.Game
{
    public abstract class CellEnvironmentE : EntityAbstract
    {
        public readonly byte Idx;
        public readonly EnvironmentTypes EnvT;
        public readonly ResourceTypes ResourceT;

        protected ref AmountC ResourcesCRef => ref Ent.Get<AmountC>();
        public AmountC ResourcesC => Ent.Get<AmountC>();

        public int Resources => ResourcesC.Amount;
        public bool HaveEnvironment => ResourcesC.Amount > 0;
        public bool HaveMaxResources => ResourcesCRef.Amount >= CellEnvironmentValues.MaxResources(EnvT);
        public bool HaveMinResources => ResourcesCRef.Amount >= CellEnvironmentValues.MinResources(EnvT);
        public int MaxResources => CellEnvironmentValues.MaxResources(EnvT);
        public int MinResources => CellEnvironmentValues.MinResources(EnvT);
        public float ProtectionPercent => CellUnitMainDamageValues.ProtectionPercent(EnvT);
        protected int StandartExtract(in BuildingTypes build) => CellEnvironmentValues.StandartExtract(build, EnvT);

        public int AmountExtractBuilding(in BuildingUpgradeEs buildUpgEs, in CellBuildEs buildEs)
        {
            var extract = StandartExtract(buildEs.BuildingE.BuildTC.Build);

            if (buildUpgEs.HaveUpgrade(buildEs.BuildingE, UpgradeTypes.PickCenter).HaveUpgrade.Have)
            {
                extract += (int)(extract * CellEnvironmentValues.Upgrade(buildEs.BuildingE.BuildTC.Build, UpgradeTypes.PickCenter));
            }

            if (extract > ResourcesC.Amount)
                extract = ResourcesC.Amount;

            return extract;
        }


        protected CellEnvironmentE(in EnvironmentTypes envT, in ResourceTypes resT, in byte idx, in EcsWorld gameW) : base(gameW)
        {
            Idx = idx;
            EnvT = envT;
            ResourceT = resT;
        }

        protected void Take(in int taking = 1)
        {
            ResourcesCRef.Amount -= taking;
        }
        protected void Add(in int adding = 1)
        {
            ResourcesCRef.Amount += adding;
            if (ResourcesC.Amount > CellEnvironmentValues.MaxResources(EnvT))
                ResourcesCRef.Amount = CellEnvironmentValues.MaxResources(EnvT);
        }

        public void SetRandomResources() => ResourcesCRef.Amount = UnityEngine.Random.Range(MinResources, MaxResources + 1);
        public void SetRandomFromCenterResources() => ResourcesCRef.Amount = UnityEngine.Random.Range(MaxResources / 2, MaxResources + 1);
        public void SetNewMax()
        {
            ResourcesCRef.Amount = CellEnvironmentValues.MaxResources(EnvT);
        }
        public void SetMaxResources()
        {
            ResourcesCRef.Amount = CellEnvironmentValues.MaxResources(EnvT);
        }
        public void Destroy()
        {
            ResourcesCRef.Amount = 0;
        }
    }
}