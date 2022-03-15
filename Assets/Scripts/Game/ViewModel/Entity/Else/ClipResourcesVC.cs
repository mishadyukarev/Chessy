//using System;
//using System.Collections.Generic;
//using UnityEngine;

//namespace Chessy.Game
//{
//    public struct ClipResourcesVC
//    {
//        static Dictionary<ClipTypes, AudioClip> _clips;
//        static Dictionary<AbilityTypes, AudioClip> _uniq;

//        static ClipResourcesVC()
//        {
//            _clips = new Dictionary<ClipTypes, AudioClip>();

//            for (var clip = ClipTypes.None + 1; clip < ClipTypes.End; clip++)
//            {
//                _clips.Add(clip, UnityEngine.Resources.Load<AudioClip>(clip + "_Clip"));
//            }


//            _uniq = new Dictionary<AbilityTypes, AudioClip>();

//            for (var uniq = AbilityTypes.None + 1; uniq < AbilityTypes.End; uniq++)
//            {
//                string name = "Uniq/";

//                if (uniq == AbilityTypes.FireArcher || uniq == AbilityTypes.FirePawn) name += "Fire";
//                else name += uniq;

//                _uniq.Add(uniq, UnityEngine.Resources.Load<AudioClip>(name + "_Clip"));
//            }
//        }

//        public static AudioClip AudioClip(ClipTypes clip)
//        {
//            if (_clips.ContainsKey(clip)) return _clips[clip];
//            else
//            {
//                throw new Exception();
//            }
//        }

//        public static AudioClip AudioClip(AbilityTypes uniqAbil)
//        {
//            if (!_uniq.ContainsKey(uniqAbil)) throw new Exception();

//            return _uniq[uniqAbil];
//        }
//    }
//}