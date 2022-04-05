namespace Chessy.Game
{
    public struct MistakeTC
    {
        public MistakeTypes MistakeT;/* { get; internal set; }*/

        public bool HaveMistake => !Is(MistakeTypes.None, MistakeTypes.End);
        public bool Is(params MistakeTypes[] mistakes)
        {
            foreach (var item in mistakes) if (item == MistakeT) return true;
            return false;
        }
    }
}