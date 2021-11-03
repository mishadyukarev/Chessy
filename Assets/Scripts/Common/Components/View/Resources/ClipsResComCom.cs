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


                _audioGameClips.Add(ClipGameTypes.Mistake, Resources.Load<AudioClip>("Mistake_Clip"));
                _audioGameClips.Add(ClipGameTypes.AttackMelee, Resources.Load<AudioClip>("Attack_Clip"));
                _audioGameClips.Add(ClipGameTypes.AttackArcher, Resources.Load<AudioClip>("AttackArcher"));
                _audioGameClips.Add(ClipGameTypes.PickArcher, Resources.Load<AudioClip>("PickArcher"));
                _audioGameClips.Add(ClipGameTypes.PickMelee, Resources.Load<AudioClip>("PickMelee"));
                _audioGameClips.Add(ClipGameTypes.Building, Resources.Load<AudioClip>("Building_Clip"));
                _audioGameClips.Add(ClipGameTypes.Fire, Resources.Load<AudioClip>("Fire_Clip"));
                _audioGameClips.Add(ClipGameTypes.SoundGoldPack, Resources.Load<AudioClip>("Buy_Clip"));
                _audioGameClips.Add(ClipGameTypes.Melting, Resources.Load<AudioClip>("Melting_Clip"));
                _audioGameClips.Add(ClipGameTypes.Destroy, Resources.Load<AudioClip>("Destroy_Clip"));
                _audioGameClips.Add(ClipGameTypes.UpgradeUnitMelee, Resources.Load<AudioClip>("UpgradeMelee_Clip"));
                _audioGameClips.Add(ClipGameTypes.Seeding, Resources.Load<AudioClip>("Seeding_Clip"));
                _audioGameClips.Add(ClipGameTypes.ClickToTable, Resources.Load<AudioClip>("ClickToTable_Clip"));
                _audioGameClips.Add(ClipGameTypes.Truce, Resources.Load<AudioClip>("Truce_Clip"));
                _audioGameClips.Add(ClipGameTypes.AfterBuildTown, Resources.Load<AudioClip>("AfterBuildTown_Clip"));
                _audioGameClips.Add(ClipGameTypes.PickUpgrade, Resources.Load<AudioClip>("PickUpgrade_Clip"));
                _audioGameClips.Add(ClipGameTypes.BonusKing, Resources.Load<AudioClip>("BonusKing_Clip"));



                _audioComClips = new Dictionary<ClipComTypes, AudioClip>();
                _audioComClips.Add(ClipComTypes.Music, Resources.Load<AudioClip>("Music"));

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