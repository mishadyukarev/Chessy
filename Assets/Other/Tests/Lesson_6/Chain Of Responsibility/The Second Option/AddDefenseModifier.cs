namespace Asteroids.Chain_of_Responsibility.Second
{
    internal sealed class AddDefenseModifier : EnemyModifier
    {
        private readonly int _maxDefense;
        public AddDefenseModifier(Enemy enemy, int maxDefense)
        : base(enemy)
        {
            _maxDefense = maxDefense;
        }
        public override void Handle()
        {
            if (_enemy.Defense <= _maxDefense)
            {
                _enemy.Defense++;
            }
            base.Handle();
        }
    }
}

