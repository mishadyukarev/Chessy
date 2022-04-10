using Chessy.Common.Entity;
using Chessy.Common.Model.System;
using Chessy.Game.Model.Entity;

namespace Chessy.Game.Model.System
{
    public sealed class SystemsModelGameForUI
    {
        //Down
        public readonly DoneClickS DoneClickS;
        public readonly OpenCityClickS OpenCityClickS;
        public readonly GetHeroDownS GetHeroClickDownS;
        public readonly GetPawnS GetPawnClickS;
        public readonly ToggleToolWeaponS ToggleToolWeaponClickS;

        //Left
        public readonly EnvironmentInfoS EnvironmentInfoClickS;
        public readonly ClickReadyS ReadyClickS;
        public readonly GetKingClickS GetKingClickS;
        public readonly BuildBuildingClickS BuildBuildingClickS;

        //Center
        public readonly GetHeroClickCenterS GetHeroClickCenterS;

        //Right
        public readonly AbilityClickS AbilityClickS;
        public readonly ConditionClickS ConditionClickS;


        internal SystemsModelGameForUI(in SystemsModelGame sMG, in EntitiesModelGame eMG)
        {
            DoneClickS = new DoneClickS(sMG, eMG);
            OpenCityClickS = new OpenCityClickS(sMG, eMG);
            GetHeroClickDownS = new GetHeroDownS(sMG, eMG);
            GetPawnClickS = new GetPawnS(sMG, eMG);
            ToggleToolWeaponClickS = new ToggleToolWeaponS(sMG, eMG);

            EnvironmentInfoClickS = new EnvironmentInfoS(sMG, eMG);
            ReadyClickS = new ClickReadyS(sMG, eMG);
            GetKingClickS = new GetKingClickS(sMG, eMG);
            BuildBuildingClickS = new BuildBuildingClickS(sMG, eMG);

            GetHeroClickCenterS = new GetHeroClickCenterS(sMG, eMG);

            AbilityClickS = new AbilityClickS(sMG, eMG);
            ConditionClickS = new ConditionClickS(sMG, eMG);
        }
    }
}