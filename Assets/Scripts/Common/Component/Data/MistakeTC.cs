namespace Game.Game
{
    public struct MistakeTC
    {
        public MistakeTypes Mistake;

        public bool HaveMistake => !Is(MistakeTypes.None, MistakeTypes.End);
        public bool Is(params MistakeTypes[] mistakes)
        {
            foreach (var item in mistakes) if (item == Mistake) return true;
            return false;
        }


        public MistakeTC(in MistakeTypes mistakeT) => Mistake = mistakeT;
    }
}