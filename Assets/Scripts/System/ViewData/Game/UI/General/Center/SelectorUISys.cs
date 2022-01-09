using static Game.Game.EntityPool;

namespace Game.Game
{
    public sealed class SelectorUISys : IEcsRunSystem
    {
        public void Run()
        {
            SelectorUIC.SyncView(ClickerObject<CellClickC>().Click, SelUniqAbilC.UniqAbil);
        }
    }
}
