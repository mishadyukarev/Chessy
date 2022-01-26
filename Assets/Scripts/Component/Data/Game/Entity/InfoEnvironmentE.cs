using ECS;

namespace Game.Game
{
    public class InfoEnvironmentE : EntityAbstract
    {
        public ref IsActiveC IsActiveC => ref Ent.Get<IsActiveC>();

        public InfoEnvironmentE(in EcsWorld gameW) : base(gameW)
        {
        }
    }
}