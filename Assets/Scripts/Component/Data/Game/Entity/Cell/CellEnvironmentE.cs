using ECS;
using System;

namespace Game.Game
{
    public abstract class CellEnvironmentE : EntityAbstract
    {
        public readonly byte Idx;
        public readonly EnvironmentTypes EnvT;
        public readonly ResourceTypes ResourceT;

        //protected ref HaveC HaveEnvCRef => ref Ent.Get<HaveC>();
        protected ref AmountC ResourcesCRef => ref Ent.Get<AmountC>();
        public AmountC ResourcesC => Ent.Get<AmountC>();

        public virtual bool HaveEnvironment => ResourcesC.Amount > 0;
        public bool HaveMaxResources => ResourcesCRef.Amount >= CellEnvironmentValues.MaxResources(EnvT);
        public int MaxResources => CellEnvironmentValues.MaxResources(EnvT);
        public float ProtectionPercent => CellUnitMainDamageValues.ProtectionPercent(EnvT);


        protected CellEnvironmentE(in EnvironmentTypes envT, in ResourceTypes resT, in byte idx, in EcsWorld world) : base(world)
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

        public void SetNewRandom(in WhereEnviromentEs whereEnviromentEs)
        {
            whereEnviromentEs.Info(EnvT, Idx).HaveEnv.Have = true;
            ResourcesCRef.Amount = CellEnvironmentValues.RandomResources(EnvT);
        }
        public void SetNewMax(in WhereEnviromentEs whereEnviromentEs)
        {
            whereEnviromentEs.Info(EnvT, Idx).HaveEnv.Have = true;
            ResourcesCRef.Amount = CellEnvironmentValues.MaxResources(EnvT);
        }
        public void SetMaxResources()
        {
            ResourcesCRef.Amount = CellEnvironmentValues.MaxResources(EnvT);
        }
        public void Destroy(in WhereEnviromentEs whereEnviromentEs)
        {
            whereEnviromentEs.Info(EnvT, Idx).HaveEnv.Have = false;
            ResourcesCRef.Amount = 0;
        }
    }
}