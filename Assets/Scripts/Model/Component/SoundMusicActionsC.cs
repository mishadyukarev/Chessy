using Chessy.Common.Enum;
using System;
using System.Collections.Generic;

namespace Chessy
{
    public struct SoundMusicActionsC
    {
        readonly Dictionary<ClipCommonTypes, Action> _sound;

        public Action SoundActionC(in ClipCommonTypes clipT) => _sound[clipT];

        internal SoundMusicActionsC(Dictionary<ClipCommonTypes, Action> sound)
        {
            _sound = sound;
        }
    }
}