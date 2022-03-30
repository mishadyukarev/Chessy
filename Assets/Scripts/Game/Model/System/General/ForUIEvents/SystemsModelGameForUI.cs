using Chessy.Game.Entity.Model;
using Chessy.Game.System.Model;

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


        public SystemsModelGameForUI(in SystemsModelGame sMGame, in EntitiesModelGame eMGame)
        {
            DoneClickS = new DoneClickS(sMGame, eMGame);
            OpenCityClickS = new OpenCityClickS(sMGame, eMGame);
            GetHeroClickDownS = new GetHeroDownS(sMGame, eMGame);
            GetPawnClickS = new GetPawnS(sMGame, eMGame);
            ToggleToolWeaponClickS = new ToggleToolWeaponS(sMGame, eMGame);

            EnvironmentInfoClickS = new EnvironmentInfoS(sMGame, eMGame);
            ReadyClickS = new ClickReadyS(sMGame, eMGame);
            GetKingClickS = new GetKingClickS(sMGame, eMGame);
            BuildBuildingClickS = new BuildBuildingClickS(sMGame, eMGame);

            GetHeroClickCenterS = new GetHeroClickCenterS(sMGame, eMGame);

            AbilityClickS = new AbilityClickS(sMGame, eMGame);
            ConditionClickS = new ConditionClickS(sMGame, eMGame);
        }
    }
}