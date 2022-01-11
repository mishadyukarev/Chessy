using ECS;
using Game.Common;

namespace Game.Game
{
    public readonly struct EntityDownUIPool
    {
        static EntityDownUIPool()
        {

        }
        public EntityDownUIPool(in WorldEcs gameW)
        {
            var downZone_GO = CanvasC.FindUnderCurZone("DownZone");

            new TwGiveTakeUIC(downZone_GO);
            new DonerUICom(downZone_GO);
            new UpgUnitUIC(downZone_GO.transform);

            var takeUnitZone = downZone_GO.transform.Find("TakeUnitZone");
            new GetPawnArcherUIC(takeUnitZone);
            new GetScoutUIC(takeUnitZone);
            new GetHeroDownUIC(takeUnitZone);
        }
    }
}