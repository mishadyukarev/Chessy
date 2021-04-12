namespace Asteroids.Chain_of_Responsibility.Second
{
    internal sealed class Enemy
    {
        public string Name;
        public int Attack;
        public int Defense;
        public Enemy(string name, int attack, int defense)
        {
            Name = name;
            Attack = attack;
            Defense = defense;
        }
    }
}

