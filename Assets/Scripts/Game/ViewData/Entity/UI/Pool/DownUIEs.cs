using Chessy.Common;

namespace Chessy.Game
{
    public readonly struct DownUIEs
    {
        public readonly DownPawnUIE PawnE;
        public readonly DownDonerUIE DonerE;
        public readonly DownScoutUIE ScoutE;
        public readonly DownHeroUIE HeroE;
        public readonly DownToolWeaponUIE ToolWeaponE;

        public DownUIEs(in bool def)
        {
            var downZone = CanvasC.FindUnderCurZone("DownZone").transform;


            PawnE = new DownPawnUIE(downZone);

            ToolWeaponE = new DownToolWeaponUIE(downZone);
            DonerE = new DownDonerUIE(downZone);

            ScoutE = new DownScoutUIE(downZone);
            HeroE = new DownHeroUIE(downZone);
        }
    }
}