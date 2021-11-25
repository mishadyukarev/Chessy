﻿using System;
using System.Collections.Generic;

namespace Game.Game
{
    public struct VisibleC : IUnitCell, IBuildCell, ITrailCell
    {
        Dictionary<PlayerTypes, bool> _isVisibled;
        public bool IsVisibled(PlayerTypes key) => _isVisibled[key];
        

        public VisibleC(bool needNew)
        {
            if (needNew)
            {
                _isVisibled = new Dictionary<PlayerTypes, bool>();

                for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
                {
                    _isVisibled.Add(player, default);
                }
            }
            else throw new Exception();
        }

        public void SetVisibled(PlayerTypes key, bool value) => _isVisibled[key] = value;
    }
}