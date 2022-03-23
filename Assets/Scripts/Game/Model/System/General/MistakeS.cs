namespace Chessy.Game.System.Model
{
    public struct MistakeS
    {
        public void Mistake(in MistakeTypes mistakeT, in Chessy.Game.Entity.Model.EntitiesModelGame e)
        {
            e.MistakeC.MistakeT = mistakeT;
            e.MistakeC.Timer = 0;
            e.Sound(ClipTypes.WritePensil).Action.Invoke();
        }
    }
}