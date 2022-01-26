using ECS;

namespace Game.Game
{
    public sealed class CellParenE : EntityAbstract
    {
        public ref IsActiveC IsActiveSelf => ref Ent.Get<IsActiveC>();

        public CellParenE(in EcsWorld gameW, in bool isActive) : base(gameW)
        {
            Ent.Add(new IsActiveC(isActive));
        }
    }
}