using Chessy.Common.Interface;
using Chessy.Game.Entity.Model;

namespace Chessy.Game.System.Model
{
    public sealed class EnvironmentInfoS : SystemModelGameAbs, IClickUI
    {
        public EnvironmentInfoS(in EntitiesModelGame eMGame) : base(eMGame)
        {
        }

        public void Click()
        {
            eMGame.Sound(ClipTypes.Click).Invoke();
            eMGame.ZoneInfoC.IsActiveEnvironment = !eMGame.ZoneInfoC.IsActiveEnvironment;
        }
    }
}