using Chessy.Game.Entity.Model;
using Chessy.Game.System.Model;

namespace Chessy.Game.Model.System
{
    public sealed class SystemsModelGameForUI : SystemModelGameAbs
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


        public SystemsModelGameForUI(in SystemsModelGame sMGame, in EntitiesModelGame eMGame) : base(eMGame)
        {
            DoneClickS = new DoneClickS(sMGame.MistakeS, eMGame);
            OpenCityClickS = new OpenCityClickS(eMGame);
            GetHeroClickDownS = new GetHeroDownS(eMGame);
            GetPawnClickS = new GetPawnS(eMGame);
            ToggleToolWeaponClickS = new ToggleToolWeaponS(eMGame);

            EnvironmentInfoClickS = new EnvironmentInfoS(eMGame);
            ReadyClickS = new ClickReadyS(eMGame);
            GetKingClickS = new GetKingClickS(eMGame);
            BuildBuildingClickS = new BuildBuildingClickS(eMGame);

            GetHeroClickCenterS = new GetHeroClickCenterS(eMGame);

            AbilityClickS = new AbilityClickS(eMGame);
            ConditionClickS = new ConditionClickS(eMGame);
        }
    }
}