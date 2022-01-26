using ECS;

namespace Game.Game
{
    public sealed class GameInfoE : EntityAbstract
    {
        public ref IsStartedGameC IsStartedGameC => ref Ent.Get<IsStartedGameC>();

        public GameInfoE(in EcsWorld world) : base(world)
        {
        }
    }
}