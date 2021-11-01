﻿using Scripts.Common;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Game
{
    public struct SoundEffectC
    {
        private static Dictionary<SoundEffectTypes, AudioSource> _soundEffect_AudSources;

        public SoundEffectC(GameObject audioSourceParent_GO)
        {
            _soundEffect_AudSources = new Dictionary<SoundEffectTypes, AudioSource>();

            var mistake_AS = audioSourceParent_GO.AddComponent<AudioSource>();
            mistake_AS.clip = ClipsResComCom.AudioClip(ClipGameTypes.Mistake);
            mistake_AS.volume = 0.4f;
            _soundEffect_AudSources.Add(SoundEffectTypes.Mistake, mistake_AS);


            var attack_AS = audioSourceParent_GO.AddComponent<AudioSource>();
            attack_AS.clip = ClipsResComCom.AudioClip(ClipGameTypes.AttackMelee);
            _soundEffect_AudSources.Add(SoundEffectTypes.AttackMelee, attack_AS);


            var attackArcher_AS = audioSourceParent_GO.AddComponent<AudioSource>();
            attackArcher_AS.clip = ClipsResComCom.AudioClip(ClipGameTypes.AttackArcher);
            attackArcher_AS.volume = 0.6f;
            _soundEffect_AudSources.Add(SoundEffectTypes.AttackArcher, attackArcher_AS);


            var pickArcher_AS = audioSourceParent_GO.AddComponent<AudioSource>();
            pickArcher_AS.clip = ClipsResComCom.AudioClip(ClipGameTypes.PickArcher);
            pickArcher_AS.volume = 0.7f;
            _soundEffect_AudSources.Add(SoundEffectTypes.PickArcher, pickArcher_AS);


            var pickMelee_AS = audioSourceParent_GO.AddComponent<AudioSource>();
            pickMelee_AS.clip = ClipsResComCom.AudioClip(ClipGameTypes.PickMelee);
            pickMelee_AS.volume = 0.1f;
            _soundEffect_AudSources.Add(SoundEffectTypes.PickMelee, pickMelee_AS);


            var build_AS = audioSourceParent_GO.AddComponent<AudioSource>();
            build_AS.clip = ClipsResComCom.AudioClip(ClipGameTypes.Building);
            build_AS.volume = 0.1f;
            _soundEffect_AudSources.Add(SoundEffectTypes.Building, build_AS);


            var clickToTable_AS = audioSourceParent_GO.AddComponent<AudioSource>();
            clickToTable_AS.clip = ClipsResComCom.AudioClip(ClipGameTypes.ClickToTable);
            _soundEffect_AudSources.Add(SoundEffectTypes.ClickToTable, clickToTable_AS);


            var fire_AS = audioSourceParent_GO.AddComponent<AudioSource>();
            fire_AS.clip = ClipsResComCom.AudioClip(ClipGameTypes.Fire);
            fire_AS.volume = 0.2f;
            _soundEffect_AudSources.Add(SoundEffectTypes.Fire, fire_AS);


            var createUnit_AS = audioSourceParent_GO.AddComponent<AudioSource>();
            createUnit_AS.clip = ClipsResComCom.AudioClip(ClipGameTypes.SoundGoldPack);
            createUnit_AS.volume = 0.3f;
            _soundEffect_AudSources.Add(SoundEffectTypes.SoundGoldPack, createUnit_AS);


            var melt_AS = audioSourceParent_GO.AddComponent<AudioSource>();
            melt_AS.clip = ClipsResComCom.AudioClip(ClipGameTypes.Melting);
            melt_AS.volume = 0.3f;
            _soundEffect_AudSources.Add(SoundEffectTypes.Melting, melt_AS);


            var destroy_AS = audioSourceParent_GO.AddComponent<AudioSource>();
            destroy_AS.clip = ClipsResComCom.AudioClip(ClipGameTypes.Destroy);
            destroy_AS.volume = 0.3f;
            _soundEffect_AudSources.Add(SoundEffectTypes.Destroy, destroy_AS);


            var upgradeUnitMelee_AS = audioSourceParent_GO.AddComponent<AudioSource>();
            upgradeUnitMelee_AS.clip = ClipsResComCom.AudioClip(ClipGameTypes.UpgradeUnitMelee);
            upgradeUnitMelee_AS.volume = 0.2f;
            _soundEffect_AudSources.Add(SoundEffectTypes.UpgradeUnitMelee, upgradeUnitMelee_AS);


            var seeding_AS = audioSourceParent_GO.AddComponent<AudioSource>();
            seeding_AS.clip = ClipsResComCom.AudioClip(ClipGameTypes.Seeding);
            seeding_AS.volume = 0.2f;
            _soundEffect_AudSources.Add(SoundEffectTypes.Seeding, seeding_AS);


            var shiftUnit_AS = audioSourceParent_GO.AddComponent<AudioSource>();
            shiftUnit_AS.clip = ClipsResComCom.AudioClip(ClipGameTypes.ClickToTable);
            shiftUnit_AS.volume = 0.6f;


            var truce_AS = audioSourceParent_GO.AddComponent<AudioSource>();
            truce_AS.clip = ClipsResComCom.AudioClip(ClipGameTypes.Truce);
            truce_AS.volume = 0.6f;
            _soundEffect_AudSources.Add(SoundEffectTypes.Truce, truce_AS);
        }

        public static void Play(SoundEffectTypes soundEffectType) => _soundEffect_AudSources[soundEffectType].Play();
        public static bool IsPlaying(SoundEffectTypes soundEffectType) => _soundEffect_AudSources[soundEffectType].isPlaying;
        public static void Mute(SoundEffectTypes soundEffectType, bool isMuted) => _soundEffect_AudSources[soundEffectType].mute = isMuted;
    }
}