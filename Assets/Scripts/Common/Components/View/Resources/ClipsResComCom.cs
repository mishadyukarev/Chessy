using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Common
{
    public struct ClipsResComCom
    {
        private static Dictionary<ClipGameTypes, AudioClip> _audioGameClips;
        private static Dictionary<ClipComTypes, AudioClip> _audioComClips;
            
        internal ClipsResComCom(bool needUpload) : this()
        {
            if (needUpload)
            {
                _audioGameClips = new Dictionary<ClipGameTypes, AudioClip>();

                var name = "Clips/";

                _audioGameClips.Add(ClipGameTypes.Mistake, Resources.Load<AudioClip>(name + "Mistake_Clip"));
                _audioGameClips.Add(ClipGameTypes.AttackMelee, Resources.Load<AudioClip>(name + "Attack_Clip"));
                _audioGameClips.Add(ClipGameTypes.AttackArcher, Resources.Load<AudioClip>(name + "AttackArcher"));
                _audioGameClips.Add(ClipGameTypes.PickArcher, Resources.Load<AudioClip>(name + "PickArcher"));
                _audioGameClips.Add(ClipGameTypes.PickMelee, Resources.Load<AudioClip>(name + "PickMelee"));
                _audioGameClips.Add(ClipGameTypes.Building, Resources.Load<AudioClip>(name + "Building_Clip"));
                _audioGameClips.Add(ClipGameTypes.Fire, Resources.Load<AudioClip>(name + "Fire_Clip"));
                _audioGameClips.Add(ClipGameTypes.SoundGoldPack, Resources.Load<AudioClip>(name + "Buy_Clip"));
                _audioGameClips.Add(ClipGameTypes.Melting, Resources.Load<AudioClip>(name + "Melting_Clip"));
                _audioGameClips.Add(ClipGameTypes.Destroy, Resources.Load<AudioClip>(name + "Destroy_Clip"));
                _audioGameClips.Add(ClipGameTypes.UpgradeUnitMelee, Resources.Load<AudioClip>(name + "UpgradeMelee_Clip"));
                _audioGameClips.Add(ClipGameTypes.Seeding, Resources.Load<AudioClip>(name + "Seeding_Clip"));
                _audioGameClips.Add(ClipGameTypes.ClickToTable, Resources.Load<AudioClip>(name + "ClickToTable_Clip"));
                _audioGameClips.Add(ClipGameTypes.Truce, Resources.Load<AudioClip>(name + "Truce_Clip"));



                _audioComClips = new Dictionary<ClipComTypes, AudioClip>();
                _audioComClips.Add(ClipComTypes.Music, Resources.Load<AudioClip>(name + "Music"));

            }
        }

        public static AudioClip AudioClip(ClipGameTypes clipGameType)
        {
            if (_audioGameClips.ContainsKey(clipGameType)) return _audioGameClips[clipGameType];
            else
            {
                throw new Exception();
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