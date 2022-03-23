namespace Asteroids.Chain_of_Responsibility.Second
{
    internal class EnemyModifier
    {
        protected Enemy _enemy;
        protected EnemyModifier Next;

        public EnemyModifier(Enemy enemy)
        {
            _enemy = enemy;
        }
        public void Add(EnemyModifier cm)
        {
            if (Next != null)
            {
                Next.Add(cm);
            }
            else
            {
                Next = cm;
            }
        }
        public virtual void Handle() => Next?.Handle();
    }
}
