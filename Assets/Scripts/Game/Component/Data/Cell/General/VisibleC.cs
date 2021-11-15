using System;
using System.Collections.Generic;

namespace Chessy.Game
{
    public struct VisibleC
    {
        private Dictionary<PlayerTypes, bool> _isVisibleDict;
        public bool IsVisibled(PlayerTypes key) => _isVisibleDict[key];
        public void SetVisibled(PlayerTypes key, bool value) => _isVisibleDict[key] = value;


        public VisibleC(bool needNew)
        {
            if (needNew)
            {
                _isVisibleDict = new Dictionary<PlayerTypes, bool>();

                for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
                {
                    _isVisibleDict.Add(player, default);
                }
            }
            else throw new Exception();
        }
    }
}