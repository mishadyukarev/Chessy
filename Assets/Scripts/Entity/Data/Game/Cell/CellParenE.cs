using ECS;

namespace Game.Game
{
    public sealed class CellParenE : EntityAbstract
    {
        public ref IsActiveC IsActiveSelf => ref Ent.Get<IsActiveC>();

        public CellParenE(in bool isActive, in EcsWorld gameW) : base(gameW)
        {
            Ent.Add(new IsActiveC(isActive));
        }
    }
}