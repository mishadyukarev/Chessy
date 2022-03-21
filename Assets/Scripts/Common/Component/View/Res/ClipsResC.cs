//using Chessy.Common.Enum;
//using System;
//using System.Collections.Generic;
//using UnityEngine;

//namespace Chessy.Common
//{
//    public struct ClipsResC
//    {
//        private static Dictionary<ClipTypes, AudioClip> _audioComClips;


//        static ClipsResC()
//        {
//            _audioComClips = new Dictionary<ClipTypes, AudioClip>();
//            _audioComClips.Add(ClipTypes.Music, Resources.Load<AudioClip>("Music"));
//        }


//        public static AudioClip AudioClip(ClipTypes clipComType)
//        {
//            if (_audioComClips.ContainsKey(clipComType)) return _audioComClips[clipComType];
//            else
//            {
//                throw new Exception();
//            }
//        }
//    }
//}