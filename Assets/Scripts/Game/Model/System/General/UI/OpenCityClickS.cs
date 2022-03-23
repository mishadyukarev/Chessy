namespace Chessy.Game.System.Model
{
    public struct OpenCityClickS
    {
        public void Click(in Chessy.Game.Entity.Model.EntitiesModelGame e)
        {
            e.Sound(ClipTypes.Click).Invoke();
            e.IsSelectedCity = !e.IsSelectedCity;
        }
    }
}