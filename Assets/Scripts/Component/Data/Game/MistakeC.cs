namespace Game.Game
{
    public struct MistakeC
    {
        public MistakeTypes Mistake;

        public void Reset() => Mistake = MistakeTypes.None;
    }
}