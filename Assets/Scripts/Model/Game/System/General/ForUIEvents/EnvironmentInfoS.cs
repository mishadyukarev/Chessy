using Chessy.Common.Entity;
using Chessy.Common.Enum;
using Chessy.Common.Interface;
using Chessy.Common.Model.System;
using Chessy.Game.Model.Entity;

namespace Chessy.Game.Model.System
{
    public sealed class EnvironmentInfoS : SystemModelGameAbs, IClickUI
    {
        public EnvironmentInfoS(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMC, eMC, sMG, eMG)
        {
        }

        public void Click()
        {
            eMC.SoundActionC(ClipCommonTypes.Click).Invoke();
            eMG.ZoneInfoC.IsActiveEnvironment = !eMG.ZoneInfoC.IsActiveEnvironment;

            eMG.NeedUpdateView = true;
        }
    }
}