using Chessy.Common;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Chessy.Game
{
    public struct ClipResourcesVC
    {
        private static Dictionary<ClipTypes, AudioClip> _clips;
        private static Dictionary<UniqAbilTypes, AudioClip> _uniqAbilClips;

        public ClipResourcesVC(bool needUpload) : this()
        {
            if (needUpload)
            {
                _clips = new Dictionary<ClipTypes, AudioClip>();

                for (var clip = ClipTypes.None + 1; clip < ClipTypes.End; clip++)
                {
                    _clips.Add(clip, Resources.Load<AudioClip>(clip + "_Clip"));
                }


                _uniqAbilClips = new Dictionary<UniqAbilTypes, AudioClip>();

                for (var uniq = UniqAbilTypes.None + 1; uniq < UniqAbilTypes.End; uniq++)
                {
                    string name = "Uniq/";
                    
                    if(uniq == UniqAbilTypes.FireArcher || uniq == UniqAbilTypes.FirePawn) name += "Fire";
                    else name += uniq;

                    _uniqAbilClips.Add(uniq, Resources.Load<AudioClip>(name + "_Clip"));
                }
            }

            else throw new Exception();
        }

        public static AudioClip AudioClip(ClipTypes clip)
        {
            if (_clips.ContainsKey(clip)) return _clips[clip];
            else
            {
                throw new Exception();
            }
        }

        public static AudioClip AudioClip(UniqAbilTypes uniqAbil)
        {
            if (!_uniqAbilClips.ContainsKey(uniqAbil)) throw new Exception();

            return _uniqAbilClips[uniqAbil];
        }
    }
}