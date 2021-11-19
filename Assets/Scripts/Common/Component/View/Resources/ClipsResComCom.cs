using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Common
{
    public struct ClipsResComCom
    {

        private static Dictionary<ClipComTypes, AudioClip> _audioComClips;

        public ClipsResComCom(bool needUpload) : this()
        {
            if (needUpload)
            {
                _audioComClips = new Dictionary<ClipComTypes, AudioClip>();
                _audioComClips.Add(ClipComTypes.Music, Resources.Load<AudioClip>("Music"));

            }
        }



        public static AudioClip AudioClip(ClipComTypes clipComType)
        {
            if (_audioComClips.ContainsKey(clipComType)) return _audioComClips[clipComType];
            else
            {
                throw new Exception();
            }
        }
    }
}