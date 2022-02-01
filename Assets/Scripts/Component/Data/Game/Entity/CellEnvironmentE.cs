using ECS;
using System;

namespace Game.Game
{
    public abstract class CellEnvironmentE : EntityAbstract
    {
        protected readonly byte Idx;
        public readonly EnvironmentTypes EnvT;
        public readonly ResourceTypes ResourceT;

        protected ref AmountC ResourcesRef => ref Ent.Get<AmountC>();
        public AmountC Resources => Ent.Get<AmountC>();

        public bool HaveEnvironment => ResourcesRef.Amount > 0;
        public bool HaveMaxResources => ResourcesRef.Amount >= CellEnvironmentValues.MaxResources(EnvT);
        public int MaxResources => CellEnvironmentValues.MaxResources(EnvT);
        public float ProtectionPercent => UnitDamageValues.ProtectionPercent(EnvT);
        public int NeedStepsShiftAttackUnit
        {
            get
            {
                switch (EnvT)
                {
                    case EnvironmentTypes.Fertilizer: return 0;
                    case EnvironmentTypes.YoungForest: return 0;
                    case EnvironmentTypes.AdultForest: return 1;
                    case EnvironmentTypes.Hill: return 1;
                    default: throw new Exception();
                }
            }
        }


        protected CellEnvironmentE(in EnvironmentTypes envT, in ResourceTypes resT, in byte idx, in EcsWorld world) : base(world)
        {
            Idx = idx;
            EnvT = envT;
            ResourceT = resT;
        }

        public void SetNew(in WhereEnviromentEs whereEnviromentEs)
        {
            whereEnviromentEs.Info(EnvT, Idx).HaveEnv.Have = true;
            ResourcesRef.Amount = CellEnvironmentValues.RandomResources(EnvT);
        }
        public virtual void Destroy(in WhereEnviromentEs whereEnviromentEs)
        {
            whereEnviromentEs.Info(EnvT, Idx).HaveEnv.Have = false;
            ResourcesRef.Amount = 0;
        }
    }
}