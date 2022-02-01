using ECS;

namespace Game.Game
{
    public sealed class CellTrailPlayerE : EntityAbstract
    {
        ref IsVisibleC IsVisibleCRef => ref Ent.Get<IsVisibleC>();
        public IsVisibleC IsVisibleC => Ent.Get<IsVisibleC>();

        internal CellTrailPlayerE(in EcsWorld gameW) : base(gameW)
        {
        }

        public void SetVisible(in bool isVisibled)
        {
            IsVisibleCRef.IsVisible = isVisibled;
        }
    }
}