using ECS;

namespace Game.Game
{
    public sealed class ToolWeaponInInventor : EntityAbstract
    {
        public ref AmountC ToolWeapons => ref Ent.Get<AmountC>();

        public ToolWeaponInInventor(in int amountTWs, in EcsWorld world) : base(world)
        {
            Ent.Add(new AmountC(amountTWs));
        }
    }
}