namespace Chessy.Game.System.Model
{
    public struct OpenCityClickS
    {
        public void Click(in EntitiesModel e)
        {
            e.Sound(ClipTypes.Click).Invoke();
            e.IsSelectedCity = !e.IsSelectedCity;
        }
    }
}