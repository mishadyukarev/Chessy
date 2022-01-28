using ECS;
using System;

namespace Game.Game
{
    public sealed class CellEnvironmentE : EntityAbstract
    {
        public ref EnvironmetC EnvironmentC => ref Ent.Get<EnvironmetC>();
        public ref AmountC Resources => ref Ent.Get<AmountC>();

        public CellEnvironmentE(in EnvironmentTypes env, in EcsWorld world) : base(world)
        {
            Ent.Add(new EnvironmetC(env));
        }

        public void SetNew()
        {
            Resources.Amount = CellEnvironmentValues.RandomResources(EnvironmentC.Environment);
        }
        public void Remove()
        {
            Resources.Reset();
        }
    }
}