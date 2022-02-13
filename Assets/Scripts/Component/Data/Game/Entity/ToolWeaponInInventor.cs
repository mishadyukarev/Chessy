using ECS;

namespace Game.Game
{
    public sealed class ToolWeaponInInventor : EntityAbstract
    {
        ref AmountC ToolWeaponsC => ref Ent.Get<AmountC>();

        public int ToolWeapons
        {
            get => ToolWeaponsC.Amount;
            set => ToolWeaponsC.Amount = value;
        }
        public bool HaveToolWeapon => ToolWeaponsC.Amount > 0;

        public ToolWeaponInInventor(in int amountTWs, in EcsWorld world) : base(world)
        {
            Ent.Add(new AmountC(amountTWs));
        }

        public void Take(in int taking = 1)
        {
            ToolWeapons -= taking;
        }
        public void Add(in int adding = 1)
        {
            ToolWeapons += adding;
        }
    }
}