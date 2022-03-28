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
            e.Sound(ClipTypes.Click).Invoke();
            e.ZoneInfoC.IsActiveEnvironment = !e.ZoneInfoC.IsActiveEnvironment;

            e.NeedUpdateView = true;
        }
    }
}