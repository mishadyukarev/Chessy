namespace Chessy.Model.Component
{
    public struct VisibleToOtherPlayerOrNotC
    {
        internal readonly bool[] IsVisibleArray;

        public bool IsVisible(in PlayerTypes playerT) => IsVisibleArray[(byte)playerT];

        internal VisibleToOtherPlayerOrNotC(in bool def) => IsVisibleArray = new bool[(byte)PlayerTypes.End];

        internal void Set(in PlayerTypes playerT, in bool isVisible) => IsVisibleArray[(byte)playerT] = isVisible;

        internal void Sync(in bool[] isVisible)
        {
            for (var i = 0; i < isVisible.Length; i++)
            {
                IsVisibleArray[i] = isVisible[i];
            }
        }
        internal void Dispose()
        {
            for (var i = 0; i < IsVisibleArray.Length; i++) IsVisibleArray[i] = default;
        }
    }
}