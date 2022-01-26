using ECS;

namespace Game.Game
{
    public sealed class FriendZoneE : EntityAbstract
    {
        public ref IsActiveC IsActiveC => ref Ent.Get<IsActiveC>();

        public FriendZoneE(in bool isActive, in EcsWorld gameW) : base(gameW)
        {
            Ent.Add(new IsActiveC(isActive));
        }
    }
}