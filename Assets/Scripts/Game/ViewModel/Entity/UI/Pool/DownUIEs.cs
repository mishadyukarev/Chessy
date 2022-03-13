using Chessy.Common;
using Chessy.Game.Entity.View.UI.Down;

namespace Chessy.Game
{
    public readonly struct DownUIEs
    {
        public readonly DownPawnUIE PawnE;
        public readonly DonerUIE DonerE;
        public readonly DownHeroUIE HeroE;
        public readonly DownToolWeaponUIE ToolWeaponE;
        public readonly CityButtonUIE CityButtonUIE;

        public DownUIEs(in bool def)
        {
            var downZone = CanvasC.FindUnderCurZone("DownZone").transform;


            PawnE = new DownPawnUIE(downZone);

            ToolWeaponE = new DownToolWeaponUIE(downZone);
            DonerE = new DonerUIE(downZone);
            HeroE = new DownHeroUIE(downZone);
            CityButtonUIE = new CityButtonUIE(downZone);
        }
    }
}