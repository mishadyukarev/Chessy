using Chessy.Common.Interface;
using Chessy.Game.Model.Entity;
using Chessy.Game.Model.System;

namespace Chessy.Game
{
    public sealed class ClickSkipLessonCenterS : SystemModel, IClickUI
    {
        internal ClickSkipLessonCenterS(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG)
        {
        }

        public void Click()
        {
            sMG.TurnOffTraining();
        }
    }
}