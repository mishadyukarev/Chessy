using ECS;

namespace Game.Game
{
    public sealed class InputE : EntityAbstract
    {
        public ref IsClickedC IsClickedC => ref Ent.Get<IsClickedC>();

        public InputE(in EcsWorld gameW) : base(gameW)
        {

        }
    }
}