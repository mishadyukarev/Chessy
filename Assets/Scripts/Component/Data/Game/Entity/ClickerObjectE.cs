using ECS;

namespace Game.Game
{
    public sealed class ClickerObjectE : EntityAbstract
    {
        public ref CellClickC CellClickC => ref Ent.Get<CellClickC>();
        public ref RayCastTC RayCastTC => ref Ent.Get<RayCastTC>();


        public ClickerObjectE(in CellClickTypes click, in EcsWorld gameW) : base(gameW)
        {
            Ent.Add(new CellClickC(click));
        }
    }
}