using System.Collections.Generic;

namespace Scripts.Game
{
    public struct VisibleC
    {
        private Dictionary<PlayerTypes, bool> _isVisibleDict;
        public bool IsVisibled(PlayerTypes key) => _isVisibleDict[key];
        public void SetVisibled(PlayerTypes key, bool value) => _isVisibleDict[key] = value;


        public VisibleC(bool needNew) : this()
        {
            if (needNew)
            {
                _isVisibleDict = new Dictionary<PlayerTypes, bool>();

                for (var playerType = Support.MinPlayerType; playerType < Support.MaxPlayerType; playerType++)
                {
                    _isVisibleDict.Add(playerType, default);
                }
            }
        }
    }
}