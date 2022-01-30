using ECS;
using System;

namespace Game.Game
{
    public abstract class CellEnvironmentE : EntityAbstract
    {
        IdxC IdxC => Ent.Get<IdxC>();
        protected EnvironmetTC EnvironmentTC => Ent.Get<EnvironmetTC>();
        protected ResourceTC ResourceTC => Ent.Get<ResourceTC>();
        protected ref AmountC Resources => ref Ent.Get<AmountC>();

        public int AmountResources => Resources.Amount;
        public bool HaveEnvironment => Resources.Have;
        public bool HaveMaxResources => Resources.Amount >= CellEnvironmentValues.MaxResources(EnvironmentTC.Environment);
        public float ProtectionPercent => UnitDamageValues.ProtectionPercent(EnvironmentTC.Environment);
        public int NeedStepsShiftAttackUnit
        {
            get
            {
                switch (EnvironmentTC.Environment)
                {
                    case EnvironmentTypes.Fertilizer: return 0;
                    case EnvironmentTypes.YoungForest: return 0;
                    case EnvironmentTypes.AdultForest: return 1;
                    case EnvironmentTypes.Hill: return 1;
                    default: throw new Exception();
                }
            }
        }


        public CellEnvironmentE(in EnvironmentTypes envT, in ResourceTypes resT, in byte idx, in EcsWorld world) : base(world)
        {
            Ent
                .Add(new EnvironmetTC(envT))
                .Add(new ResourceTC(resT))
                .Add(new IdxC(idx));
        }

        public virtual void SetNew(in WhereEnviromentEs whereEnviromentEs)
        {
            whereEnviromentEs.Info(EnvironmentTC.Environment, IdxC.Idx).HaveEnv.Have = true;
            Resources.Amount = CellEnvironmentValues.RandomResources(EnvironmentTC.Environment);
        }
        public virtual void Destroy(in WhereEnviromentEs whereEnviromentEs)
        {
            whereEnviromentEs.Info(EnvironmentTC.Environment, IdxC.Idx).HaveEnv.Have = false;
            Resources.Reset();
        }
    }
}