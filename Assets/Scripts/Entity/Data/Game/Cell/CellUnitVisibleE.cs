using ECS;

namespace Game.Game
{
    public sealed class CellUnitVisibleE : EntityAbstract
    {
        ref IsVisibleC IsVisibleC => ref Ent.Get<IsVisibleC>();

        public bool IsVisible => IsVisibleC.IsVisible;

        public CellUnitVisibleE(in EcsWorld gameW) : base(gameW) { }

        public void SetVisible(in bool isVisible) => IsVisibleC.IsVisible = isVisible;
    }
}