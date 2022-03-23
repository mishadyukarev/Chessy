namespace Chessy.Game.System.Model
{
    public struct EnvironmentInfoS
    {
        public void Info(in Chessy.Game.Entity.Model.EntitiesModelGame e)
        {
            e.Sound(ClipTypes.Click).Invoke();
            e.ZoneInfoC.IsActiveEnvironment = !e.ZoneInfoC.IsActiveEnvironment;
        }
    }
}