﻿using Chessy.Common.Interface;
using Chessy.Game.Entity.Model;
using Chessy.Game.Enum;
using Chessy.Common.Entity;
using Chessy.Common.Model.System;
using Chessy.Common.Enum;

namespace Chessy.Game.Model.System
{
    public sealed class OpenCityClickS : SystemModelGameAbs, IClickUI
    {
        internal OpenCityClickS(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMC, eMC, sMG, eMG) { }

        public void Click()
        {
            eMC.SoundActionC(ClipCommonTypes.Click).Invoke();


            eMG.IsSelectedCity = !eMG.IsSelectedCity;

            if (eMG.LessonTC.Is(LessonTypes.OpeningTown, LessonTypes.ClickOpenTown2))
            {
                eMG.LessonTC.SetNextLesson();

            }
            if (eMG.LessonTC.Is(LessonTypes.BuyingHouse, LessonTypes.ClickBuyMelterInTown))
            {
                if (!eMG.IsSelectedCity) eMG.LessonTC.SetPreviousLesson();
            }

            

            eMG.NeedUpdateView = true;
        }
    }
}