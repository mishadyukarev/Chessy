using Chessy.Common;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Chessy.Game
{
    public struct SoundEffectC
    {
        private static Dictionary<ClipTypes, AudioSource> _soundEffect_ASs;
        private static Dictionary<UniqAbilTypes, AudioSource> _uniqEff;

        public SoundEffectC(GameObject aSParent_GO)
        {
            _soundEffect_ASs = new Dictionary<ClipTypes, AudioSource>();
            _uniqEff = new Dictionary<UniqAbilTypes, AudioSource>();

            var mistake_AS = aSParent_GO.AddComponent<AudioSource>();
            mistake_AS.clip = ClipResourcesVC.AudioClip(ClipTypes.Mistake);
            mistake_AS.volume = 0.4f;
            _soundEffect_ASs.Add(ClipTypes.Mistake, mistake_AS);


            var attack_AS = aSParent_GO.AddComponent<AudioSource>();
            attack_AS.clip = ClipResourcesVC.AudioClip(ClipTypes.AttackMelee);
            _soundEffect_ASs.Add(ClipTypes.AttackMelee, attack_AS);


            var attackArcher_AS = aSParent_GO.AddComponent<AudioSource>();
            attackArcher_AS.clip = ClipResourcesVC.AudioClip(ClipTypes.AttackArcher);
            attackArcher_AS.volume = 0.6f;
            _soundEffect_ASs.Add(ClipTypes.AttackArcher, attackArcher_AS);


            var pickArcher_AS = aSParent_GO.AddComponent<AudioSource>();
            pickArcher_AS.clip = ClipResourcesVC.AudioClip(ClipTypes.PickArcher);
            pickArcher_AS.volume = 0.7f;
            _soundEffect_ASs.Add(ClipTypes.PickArcher, pickArcher_AS);


            var pickMelee_AS = aSParent_GO.AddComponent<AudioSource>();
            pickMelee_AS.clip = ClipResourcesVC.AudioClip(ClipTypes.PickMelee);
            pickMelee_AS.volume = 0.1f;
            _soundEffect_ASs.Add(ClipTypes.PickMelee, pickMelee_AS);


            var build_AS = aSParent_GO.AddComponent<AudioSource>();
            build_AS.clip = ClipResourcesVC.AudioClip(ClipTypes.Building);
            build_AS.volume = 0.1f;
            _soundEffect_ASs.Add(ClipTypes.Building, build_AS);


            var clickToTable_AS = aSParent_GO.AddComponent<AudioSource>();
            clickToTable_AS.clip = ClipResourcesVC.AudioClip(ClipTypes.ClickToTable);
            _soundEffect_ASs.Add(ClipTypes.ClickToTable, clickToTable_AS);


            var createUnit_AS = aSParent_GO.AddComponent<AudioSource>();
            createUnit_AS.clip = ClipResourcesVC.AudioClip(ClipTypes.SoundGoldPack);
            createUnit_AS.volume = 0.3f;
            _soundEffect_ASs.Add(ClipTypes.SoundGoldPack, createUnit_AS);


            var melt_AS = aSParent_GO.AddComponent<AudioSource>();
            melt_AS.clip = ClipResourcesVC.AudioClip(ClipTypes.Melting);
            melt_AS.volume = 0.3f;
            _soundEffect_ASs.Add(ClipTypes.Melting, melt_AS);


            var destroy_AS = aSParent_GO.AddComponent<AudioSource>();
            destroy_AS.clip = ClipResourcesVC.AudioClip(ClipTypes.Destroy);
            destroy_AS.volume = 0.3f;
            _soundEffect_ASs.Add(ClipTypes.Destroy, destroy_AS);


            var upgradeUnitMelee_AS = aSParent_GO.AddComponent<AudioSource>();
            upgradeUnitMelee_AS.clip = ClipResourcesVC.AudioClip(ClipTypes.UpgradeMelee);
            upgradeUnitMelee_AS.volume = 0.2f;
            _soundEffect_ASs.Add(ClipTypes.UpgradeMelee, upgradeUnitMelee_AS);


            var seeding_AS = aSParent_GO.AddComponent<AudioSource>();
            seeding_AS.clip = ClipResourcesVC.AudioClip(ClipTypes.Seeding);
            seeding_AS.volume = 0.2f;
            _soundEffect_ASs.Add(ClipTypes.Seeding, seeding_AS);


            var shiftUnit_AS = aSParent_GO.AddComponent<AudioSource>();
            shiftUnit_AS.clip = ClipResourcesVC.AudioClip(ClipTypes.ClickToTable);
            shiftUnit_AS.volume = 0.6f;


            var afterBuildTown_AS = aSParent_GO.AddComponent<AudioSource>();
            afterBuildTown_AS.clip = ClipResourcesVC.AudioClip(ClipTypes.AfterBuildTown);
            afterBuildTown_AS.volume = 0.2f;
            _soundEffect_ASs.Add(ClipTypes.AfterBuildTown, afterBuildTown_AS);


            var truce_AS = aSParent_GO.AddComponent<AudioSource>();
            truce_AS.clip = ClipResourcesVC.AudioClip(ClipTypes.Truce);
            truce_AS.volume = 0.6f;
            _soundEffect_ASs.Add(ClipTypes.Truce, truce_AS);


            var cur_AS = aSParent_GO.AddComponent<AudioSource>();
            cur_AS.clip = ClipResourcesVC.AudioClip(ClipTypes.PickUpgrade);
            cur_AS.volume = 0.3f;
            _soundEffect_ASs.Add(ClipTypes.PickUpgrade, cur_AS);


            cur_AS = aSParent_GO.AddComponent<AudioSource>();
            cur_AS.clip = Resources.Load<AudioClip>("GetHero_Clip");
            cur_AS.volume = 0.25f;
            _soundEffect_ASs.Add(ClipTypes.GetHero, cur_AS);




            cur_AS = aSParent_GO.AddComponent<AudioSource>();
            cur_AS.clip = ClipResourcesVC.AudioClip(UniqAbilTypes.BonusNear);
            cur_AS.volume = 0.3f;
            _uniqEff.Add(UniqAbilTypes.BonusNear, cur_AS);


            cur_AS = aSParent_GO.AddComponent<AudioSource>();
            cur_AS.clip = ClipResourcesVC.AudioClip(UniqAbilTypes.GrowAdultForest);
            cur_AS.volume = 0.3f;
            _uniqEff.Add(UniqAbilTypes.GrowAdultForest, cur_AS);


            cur_AS = aSParent_GO.AddComponent<AudioSource>();
            cur_AS.clip = ClipResourcesVC.AudioClip(UniqAbilTypes.PutOutFireElfemale);
            cur_AS.volume = 0.1f;
            _uniqEff.Add(UniqAbilTypes.PutOutFireElfemale, cur_AS);


            cur_AS = aSParent_GO.AddComponent<AudioSource>();
            cur_AS.clip = ClipResourcesVC.AudioClip(UniqAbilTypes.StunElfemale);
            cur_AS.volume = 0.3f;
            _uniqEff.Add(UniqAbilTypes.StunElfemale, cur_AS);


            var fire_AS = aSParent_GO.AddComponent<AudioSource>();
            fire_AS.clip = ClipResourcesVC.AudioClip(UniqAbilTypes.FireArcher);
            fire_AS.volume = 0.2f;
            _uniqEff.Add(UniqAbilTypes.FireArcher, fire_AS);
            _uniqEff.Add(UniqAbilTypes.FirePawn, fire_AS);
        }

        public static void Play(ClipTypes eff)
        {
            if (!_soundEffect_ASs.ContainsKey(eff)) throw new Exception();

            _soundEffect_ASs[eff].Play();
        }
        public static void Play(UniqAbilTypes uniq)
        {
            if (!_uniqEff.ContainsKey(uniq)) throw new Exception();

            _uniqEff[uniq].Play();
        }

        public static bool IsPlaying(ClipTypes eff)
        {
            if (!_soundEffect_ASs.ContainsKey(eff)) throw new Exception();

            return _soundEffect_ASs[eff].isPlaying;
        }
        public static bool IsPlaying(ClipTypes[] effs)
        {
            foreach (var item in effs)
            {
                if (!_soundEffect_ASs.ContainsKey(item)) throw new Exception();

                if (_soundEffect_ASs[item].isPlaying) return true;
            }
            return false;
        }
        public static void Mute(ClipTypes eff, bool isMuted) => _soundEffect_ASs[eff].mute = isMuted;
    }
}
