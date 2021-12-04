using Game.Common;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Game
{
    public struct ClipResourcesVC
    {
        private static Dictionary<ClipTypes, AudioClip> _clips;
        private static Dictionary<UniqueAbilTypes, AudioClip> _uniq;

        static ClipResourcesVC()
        {
            _clips = new Dictionary<ClipTypes, AudioClip>();

            for (var clip = ClipTypes.First; clip < ClipTypes.End; clip++)
            {
                _clips.Add(clip, Resources.Load<AudioClip>(clip + "_Clip"));
            }


            _uniq = new Dictionary<UniqueAbilTypes, AudioClip>();

            for (var uniq = UniqueAbilTypes.First; uniq < UniqueAbilTypes.End; uniq++)
            {
                string name = "Uniq/";

                if (uniq == UniqueAbilTypes.FireArcher || uniq == UniqueAbilTypes.FirePawn) name += "Fire";
                else name += uniq;

                _uniq.Add(uniq, Resources.Load<AudioClip>(name + "_Clip"));
            }
        }

        public static AudioClip AudioClip(ClipTypes clip)
        {
            if (_clips.ContainsKey(clip)) return _clips[clip];
            else
            {
                throw new Exception();
            }
        }

        public static AudioClip AudioClip(UniqueAbilTypes uniqAbil)
        {
            if (!_uniq.ContainsKey(uniqAbil)) throw new Exception();

            return _uniq[uniqAbil];
        }
    }
}