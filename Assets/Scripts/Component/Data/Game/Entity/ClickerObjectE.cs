using ECS;

namespace Game.Game
{
    public sealed class ClickerObjectE : EntityAbstract
    {
        public ref CellClickC CellClickCRef => ref Ent.Get<CellClickC>();
        public ref RayCastTC RayCastTC => ref Ent.Get<RayCastTC>();

        public CellClickTypes ClickT
        {
            get => CellClickCRef.Click;
            set => CellClickCRef.Click = value;
        }

        public ClickerObjectE(in CellClickTypes click, in EcsWorld gameW) : base(gameW)
        {
            Ent.Add(new CellClickC(click));
        }
    }
}