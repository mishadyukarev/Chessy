using System.Collections.Generic;

namespace Scripts.Game
{
    internal struct VisibleCom
    {
        private Dictionary<PlayerTypes, bool> _isVisibleDict;
        internal bool IsVisibled(PlayerTypes key) => _isVisibleDict[key];
        internal void SetVisibled(PlayerTypes key, bool value) => _isVisibleDict[key] = value;


        internal VisibleCom(bool needNew) : this()
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