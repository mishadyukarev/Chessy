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
        public readonly BookLittleUIE BookLittleE;

        public DownUIEs(in Transform downZone)
        {
            PawnE = new DownPawnUIE(downZone);

            ToolWeaponE = new DownToolWeaponUIE(downZone);
            DonerE = new DonerUIE(downZone);
            HeroE = new DownHeroUIE(downZone);
            CityButtonUIE = new CityButtonUIE(downZone);


            var book = downZone.Find("Book+");

            BookLittleE = new BookLittleUIE(book.Find("Button+").GetComponent<Button>(), book.Find("Animation+").GetComponent<Animation>());
        }
    }
}