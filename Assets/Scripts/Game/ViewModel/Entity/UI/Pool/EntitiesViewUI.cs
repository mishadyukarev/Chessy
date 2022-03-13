using Chessy.Common;
using Chessy.Game.View.UI.System;
using System;

namespace Chessy.Game
{
    public class EntitiesViewUI
    {
        public readonly ActionC UpdateC;

        public readonly LeftUIEs LeftEs;
        public readonly RightUIEs RightEs;
        public readonly CenterUIEs CenterEs;
        public readonly DownUIEs DownEs;
        public readonly UpUIEs UpEs;

        public LeftEnvironmentUIEs LeftEnvEs => LeftEs.EnvironmentEs;

        public EntitiesViewUI(in EntitiesModel ents)
        {
            LeftEs = new LeftUIEs(default);
            RightEs = new RightUIEs(default);
            CenterEs = new CenterUIEs(default);
            DownEs = new DownUIEs(default);
            UpEs = new UpUIEs(default);



            UpdateC.Action += (Action)

            ///Right
            new RightZoneUIS(this, ents).Run
            + new StatsUIS(this, ents).Run
            + new RightUnitProtectUIS(this, ents).Run
            + new RelaxUIS(this.RightEs.RelaxE, ents).Run
            + new ShieldUIS(this, ents).Run
            + new RightEffectsUIS(ents.Resources, this, ents).Run;
            for (var buttonT = ButtonTypes.None + 1; buttonT < ButtonTypes.End; buttonT++)
            {
                UpdateC.Action += new UniqueButtonUIS(buttonT, this.RightEs.Unique(buttonT), ents.Resources, ents).Run;
            }


            ///Down
            UpdateC.Action += (Action)
            new DonerUIS(this.DownEs.DonerE, ents).Run
            + new DownPawnUIS(this.DownEs.PawnE, ents).Run
            + new DownToolWeaponUIS(this.DownEs.ToolWeaponE, ents).Run
            + new DownHeroUIS(this.DownEs.HeroE, ents).Run


            ///Up
            + new EconomyUpUIS(this, ents).Run
            + new UpWindUIS(this, ents).Run
            + new UpSunsUIS(this, ents).Run


            ///Center
            + new CenterSelectorUIS(this, ents).Run
            + new CenterEndGameUIS(this, ents).Run
            + new CenterReadyUIS(this, ents).Run
            + new CenterKingUIS(this, ents).Run
            + new CenterFriendUIS(this, ents).Run
            + new CenterHeroesUIS(this, ents).Run
            + new CenterMistakeUIS(this, ents).Run
            + new CenterMotionUIS(this, ents).Run
            + new CenterBuildingZonesUIS(this, ents).Run


            ///Left
            + new LeftZonesUIS(this, ents).Run
            + new EnvUIS(this, ents).Run
            + new LeftCityUIS(this, ents).Run;
        }
    }
}