using Chessy.Game.Model.Entity;

namespace Chessy.Game.Model.System
{
    public sealed partial class SystemsModelGameForUI
    {
        readonly EntitiesModelGame _eMG;
        readonly SystemsModelGame _sMG;

        //Left
        public readonly BuildBuildingClickS BuildBuildingClickS;

        //Center
        public readonly GetHeroClickCenterS GetHeroClickCenterS;
        public readonly ClickSkipLessonCenterS ClickSkipLessonCenterS;

        //Right
        public readonly AbilityClickS AbilityClickS;
        public readonly ConditionClickS ConditionClickS;


        internal SystemsModelGameForUI(in SystemsModelGame sMG, in EntitiesModelGame eMG)
        {
            _eMG = eMG;
            _sMG = sMG;

            GetHeroClickCenterS = new GetHeroClickCenterS(sMG, eMG);
            ClickSkipLessonCenterS = new ClickSkipLessonCenterS(sMG, eMG);

            AbilityClickS = new AbilityClickS(sMG, eMG);
            ConditionClickS = new ConditionClickS(sMG, eMG);
        }
    }
}