using Chessy.Common;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Chessy.Game
{
    public struct SoundEffectC
    {
        private static Dictionary<ClipGameTypes, AudioSource> _soundEffect_ASs;
        private static Dictionary<UniqAbilTypes, AudioSource> _uniqEff;

        public SoundEffectC(GameObject audioSourceParent_GO)
        {
            _soundEffect_ASs = new Dictionary<ClipGameTypes, AudioSource>();
            _uniqEff = new Dictionary<UniqAbilTypes, AudioSource>();

            var mistake_AS = audioSourceParent_GO.AddComponent<AudioSource>();
            mistake_AS.clip = ClipsResComCom.AudioClip(ClipGameTypes.Mistake);
            mistake_AS.volume = 0.4f;
            _soundEffect_ASs.Add(ClipGameTypes.Mistake, mistake_AS);


            var attack_AS = audioSourceParent_GO.AddComponent<AudioSource>();
            attack_AS.clip = ClipsResComCom.AudioClip(ClipGameTypes.AttackMelee);
            _soundEffect_ASs.Add(ClipGameTypes.AttackMelee, attack_AS);


            var attackArcher_AS = audioSourceParent_GO.AddComponent<AudioSource>();
            attackArcher_AS.clip = ClipsResComCom.AudioClip(ClipGameTypes.AttackArcher);
            attackArcher_AS.volume = 0.6f;
            _soundEffect_ASs.Add(ClipGameTypes.AttackArcher, attackArcher_AS);


            var pickArcher_AS = audioSourceParent_GO.AddComponent<AudioSource>();
            pickArcher_AS.clip = ClipsResComCom.AudioClip(ClipGameTypes.PickArcher);
            pickArcher_AS.volume = 0.7f;
            _soundEffect_ASs.Add(ClipGameTypes.PickArcher, pickArcher_AS);


            var pickMelee_AS = audioSourceParent_GO.AddComponent<AudioSource>();
            pickMelee_AS.clip = ClipsResComCom.AudioClip(ClipGameTypes.PickMelee);
            pickMelee_AS.volume = 0.1f;
            _soundEffect_ASs.Add(ClipGameTypes.PickMelee, pickMelee_AS);


            var build_AS = audioSourceParent_GO.AddComponent<AudioSource>();
            build_AS.clip = ClipsResComCom.AudioClip(ClipGameTypes.Building);
            build_AS.volume = 0.1f;
            _soundEffect_ASs.Add(ClipGameTypes.Building, build_AS);


            var clickToTable_AS = audioSourceParent_GO.AddComponent<AudioSource>();
            clickToTable_AS.clip = ClipsResComCom.AudioClip(ClipGameTypes.ClickToTable);
            _soundEffect_ASs.Add(ClipGameTypes.ClickToTable, clickToTable_AS);


            var fire_AS = audioSourceParent_GO.AddComponent<AudioSource>();
            fire_AS.clip = Resources.Load<AudioClip>("Fire_Clip");
            fire_AS.volume = 0.2f;
            _uniqEff.Add(UniqAbilTypes.FireArcher, fire_AS);
            _uniqEff.Add(UniqAbilTypes.FirePawn, fire_AS);



            var createUnit_AS = audioSourceParent_GO.AddComponent<AudioSource>();
            createUnit_AS.clip = ClipsResComCom.AudioClip(ClipGameTypes.SoundGoldPack);
            createUnit_AS.volume = 0.3f;
            _soundEffect_ASs.Add(ClipGameTypes.SoundGoldPack, createUnit_AS);


            var melt_AS = audioSourceParent_GO.AddComponent<AudioSource>();
            melt_AS.clip = ClipsResComCom.AudioClip(ClipGameTypes.Melting);
            melt_AS.volume = 0.3f;
            _soundEffect_ASs.Add(ClipGameTypes.Melting, melt_AS);


            var destroy_AS = audioSourceParent_GO.AddComponent<AudioSource>();
            destroy_AS.clip = ClipsResComCom.AudioClip(ClipGameTypes.Destroy);
            destroy_AS.volume = 0.3f;
            _soundEffect_ASs.Add(ClipGameTypes.Destroy, destroy_AS);


            var upgradeUnitMelee_AS = audioSourceParent_GO.AddComponent<AudioSource>();
            upgradeUnitMelee_AS.clip = ClipsResComCom.AudioClip(ClipGameTypes.UpgradeUnitMelee);
            upgradeUnitMelee_AS.volume = 0.2f;
            _soundEffect_ASs.Add(ClipGameTypes.UpgradeUnitMelee, upgradeUnitMelee_AS);


            var seeding_AS = audioSourceParent_GO.AddComponent<AudioSource>();
            seeding_AS.clip = ClipsResComCom.AudioClip(ClipGameTypes.Seeding);
            seeding_AS.volume = 0.2f;
            _soundEffect_ASs.Add(ClipGameTypes.Seeding, seeding_AS);


            var shiftUnit_AS = audioSourceParent_GO.AddComponent<AudioSource>();
            shiftUnit_AS.clip = ClipsResComCom.AudioClip(ClipGameTypes.ClickToTable);
            shiftUnit_AS.volume = 0.6f;


            var afterBuildTown_AS = audioSourceParent_GO.AddComponent<AudioSource>();
            afterBuildTown_AS.clip = ClipsResComCom.AudioClip(ClipGameTypes.AfterBuildTown);
            afterBuildTown_AS.volume = 0.2f;
            _soundEffect_ASs.Add(ClipGameTypes.AfterBuildTown, afterBuildTown_AS);


            var truce_AS = audioSourceParent_GO.AddComponent<AudioSource>();
            truce_AS.clip = ClipsResComCom.AudioClip(ClipGameTypes.Truce);
            truce_AS.volume = 0.6f;
            _soundEffect_ASs.Add(ClipGameTypes.Truce, truce_AS);


            var cur_AS = audioSourceParent_GO.AddComponent<AudioSource>();
            cur_AS.clip = ClipsResComCom.AudioClip(ClipGameTypes.PickUpgrade);
            cur_AS.volume = 0.3f;
            _soundEffect_ASs.Add(ClipGameTypes.PickUpgrade, cur_AS);


            cur_AS = audioSourceParent_GO.AddComponent<AudioSource>();
            cur_AS.clip = ClipsResComCom.AudioClip(ClipGameTypes.BonusKing);
            cur_AS.volume = 0.3f;
            _soundEffect_ASs.Add(ClipGameTypes.BonusKing, cur_AS);


            cur_AS = audioSourceParent_GO.AddComponent<AudioSource>();
            cur_AS.clip = Resources.Load<AudioClip>("GrowAdForestElfemale_Clip");
            cur_AS.volume = 0.3f;
            _uniqEff.Add(UniqAbilTypes.GrowAdultForest, cur_AS);


            cur_AS = audioSourceParent_GO.AddComponent<AudioSource>();
            cur_AS.clip = Resources.Load<AudioClip>("PutOutFireElfemale_Clip");
            cur_AS.volume = 0.1f;
            _uniqEff.Add(UniqAbilTypes.PutOutFireElfemale, cur_AS);


            cur_AS = audioSourceParent_GO.AddComponent<AudioSource>();
            cur_AS.clip = Resources.Load<AudioClip>("StunElfemale_Clip");
            cur_AS.volume = 0.25f;
            _uniqEff.Add(UniqAbilTypes.StunElfemale, cur_AS);


            cur_AS = audioSourceParent_GO.AddComponent<AudioSource>();
            cur_AS.clip = Resources.Load<AudioClip>("GetHero_Clip");
            cur_AS.volume = 0.25f;
            _soundEffect_ASs.Add(ClipGameTypes.GetHero, cur_AS);
        }

        public static void Play(ClipGameTypes eff)
        {
            if (!_soundEffect_ASs.ContainsKey(eff)) throw new Exception();

            _soundEffect_ASs[eff].Play();
        }
        public static void Play(UniqAbilTypes uniq)
        {
            if (!_uniqEff.ContainsKey(uniq)) throw new Exception();

            _uniqEff[uniq].Play();
        }

        public static bool IsPlaying(ClipGameTypes eff)
        {
            if (!_soundEffect_ASs.ContainsKey(eff)) throw new Exception();

            return _soundEffect_ASs[eff].isPlaying;
        }
        public static bool IsPlaying(ClipGameTypes[] effs)
        {
            foreach (var item in effs)
            {
                if (!_soundEffect_ASs.ContainsKey(item)) throw new Exception();

                if (_soundEffect_ASs[item].isPlaying) return true;
            }
            return false;
        }
        public static void Mute(ClipGameTypes eff, bool isMuted) => _soundEffect_ASs[eff].mute = isMuted;
    }
}
