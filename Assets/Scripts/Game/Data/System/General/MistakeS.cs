namespace Chessy.Game.System.Model
{
    public struct MistakeS
    {
        public void Mistake(in MistakeTypes mistakeT, in EntitiesModel e)
        {
            e.MistakeC.MistakeT = mistakeT;
            e.MistakeC.Timer = 0;
            e.Sound(ClipTypes.WritePensil).Action.Invoke();
        }
    }
}