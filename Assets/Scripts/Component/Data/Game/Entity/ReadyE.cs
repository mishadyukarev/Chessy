using ECS;

namespace Game.Game
{
    public sealed class ReadyE : EntityAbstract
    {
        public ref IsReadyC IsReadyC => ref Ent.Get<IsReadyC>();

        public ReadyE(in EcsWorld gameW) : base(gameW)
        {

        }
    }
}