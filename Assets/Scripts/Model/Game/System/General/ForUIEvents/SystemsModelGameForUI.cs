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


        internal SystemsModelGameForUI(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG)
        {
            DoneClickS = new DoneClickS(sMC, eMC, sMG, eMG);
            OpenCityClickS = new OpenCityClickS(sMC, eMC, sMG, eMG);
            GetHeroClickDownS = new GetHeroDownS(sMC, eMC, sMG, eMG);
            GetPawnClickS = new GetPawnS(sMC, eMC, sMG, eMG);
            ToggleToolWeaponClickS = new ToggleToolWeaponS(sMC, eMC, sMG, eMG);

            EnvironmentInfoClickS = new EnvironmentInfoS(sMC, eMC, sMG, eMG);
            ReadyClickS = new ClickReadyS(sMC, eMC, sMG, eMG);
            GetKingClickS = new GetKingClickS(sMC, eMC, sMG, eMG);
            BuildBuildingClickS = new BuildBuildingClickS(sMC, eMC, sMG, eMG);

            GetHeroClickCenterS = new GetHeroClickCenterS(sMC, eMC, sMG, eMG);

            AbilityClickS = new AbilityClickS(sMC, eMC, sMG, eMG);
            ConditionClickS = new ConditionClickS(sMC, eMC, sMG, eMG);
        }
    }
}