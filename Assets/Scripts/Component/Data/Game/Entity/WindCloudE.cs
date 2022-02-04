using ECS;

namespace Game.Game
{
    public sealed class WindCloudE : EntityAbstract
    {
        public ref DirectTC DirectWind => ref Ent.Get<DirectTC>();
        public ref IdxC CenterCloud => ref Ent.Get<IdxC>();

        public WindCloudE(in EcsWorld gameW) : base(gameW)
        {
            Ent.Add(new DirectTC(DirectTypes.Right))
                .Add(new IdxC(60));
        }
    }
}