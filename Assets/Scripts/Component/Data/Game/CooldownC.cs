namespace Game.Game
{
    public struct CooldownC : IUnitUniqueCellE
    {
        public int Cooldown;

        public bool Have => Cooldown > 0;

        public void Add(in int adding = 1) => Cooldown += adding;
        public void Take(in int taking = 1) => Cooldown -= taking;
    }
}