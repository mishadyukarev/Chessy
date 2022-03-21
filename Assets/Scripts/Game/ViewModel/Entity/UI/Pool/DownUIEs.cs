using Chessy.Common;
using Chessy.Game.Entity.View.UI.Down;
using UnityEngine;
using UnityEngine.UI;

namespace Chessy.Game
{
    public readonly struct DownUIEs
    {
        public readonly DownPawnUIE PawnE;
        public readonly DonerUIE DonerE;
        public readonly DownHeroUIE HeroE;
        public readonly DownToolWeaponUIE ToolWeaponE;
        public readonly CityButtonUIE CityButtonUIE;
        public readonly CostUIE CostE;
        public readonly ButtonUIC BookButtonC;

        public DownUIEs(in Transform downZone)
        {
            PawnE = new DownPawnUIE(downZone);

            ToolWeaponE = new DownToolWeaponUIE(downZone);
            DonerE = new DonerUIE(downZone);
            HeroE = new DownHeroUIE(downZone);
            CityButtonUIE = new CityButtonUIE(downZone);
            CostE = new CostUIE(downZone);

            BookButtonC = new ButtonUIC(downZone.Find("Book+").Find("Button+").GetComponent<Button>());
        }
    }
}