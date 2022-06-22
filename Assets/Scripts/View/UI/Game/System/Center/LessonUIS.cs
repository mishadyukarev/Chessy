﻿using Chessy.Common.Entity;
using Chessy.Game.Model.Entity;
using Chessy.Game.Enum;

namespace Chessy.Game.View.UI.System
{
    sealed class LessonUIS : SystemUIAbstract
    {
        readonly CenterUIEs _centerUIEs;
        readonly EntitiesModelCommon _eMCommon;

        internal LessonUIS(in CenterUIEs centerUIEs, in EntitiesModelCommon eMCommon, in EntitiesModelGame eMGame) : base(eMGame)
        {
            _centerUIEs = centerUIEs;
            _eMCommon = eMCommon;
        }

        internal override void Sync()
        {
            for (var lessonT = (LessonTypes)1; lessonT < LessonTypes.End; lessonT++)
            {
                _centerUIEs.LessonGOC(lessonT).SetActive(lessonT == _e.LessonT);
            }
        }
    }
}