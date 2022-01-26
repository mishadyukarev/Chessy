using ECS;

namespace Game.Game
{
    public sealed class WindE : EntityAbstract
    {
        public ref DirectTC DirectWind => ref Ent.Get<DirectTC>();
        public ref IdxC CenterCloud => ref Ent.Get<IdxC>();

        public WindE(in EcsWorld gameW) : base(gameW)
        {
            Ent.Add(new DirectTC(DirectTypes.Right));
        }
    }
}