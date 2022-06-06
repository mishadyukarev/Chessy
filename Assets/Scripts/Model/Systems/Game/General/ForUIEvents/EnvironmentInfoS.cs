using Chessy.Common.Enum;
using Chessy.Common.Interface;
using Chessy.Game.Model.Entity;

namespace Chessy.Game.Model.System
{
    public sealed class EnvironmentInfoS : SystemModel, IClickUI
    {
        public EnvironmentInfoS(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG)
        {
        }

        public void Click()
        {
            eMG.Common.SoundActionC(ClipCommonTypes.Click).Invoke();
            eMG.ZoneInfoC.IsActiveEnvironment = !eMG.ZoneInfoC.IsActiveEnvironment;

            eMG.NeedUpdateView = true;
        }
    }
}