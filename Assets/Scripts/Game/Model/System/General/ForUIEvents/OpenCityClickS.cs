using Chessy.Common.Interface;
using Chessy.Game.Entity.Model;

namespace Chessy.Game.System.Model
{
    public sealed class OpenCityClickS : SystemModelGameAbs, IClickUI
    {
        public OpenCityClickS(in EntitiesModelGame eMGame) : base(eMGame)
        {
        }

        public void Click()
        {
            e.Sound(ClipTypes.Click).Invoke();

            e.IsSelectedCity = !e.IsSelectedCity;

            e.NeedUpdateView = true;
        }
    }
}