using ECS;

namespace Game.Game
{
    public sealed class CellParenE : CellEntityAbstract
    {
        public ref IsActiveC IsActiveSelf => ref Ent.Get<IsActiveC>();

        public CellParenE(in bool isActive, in byte idx, in EcsWorld gameW) : base(idx, gameW)
        {
            Ent.Add(new IsActiveC(isActive));
        }
    }
}