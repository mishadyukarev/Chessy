using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Game
{
    public struct SoundEffectVC
    {
        static Dictionary<ClipTypes, AudioSource> _soundEffect_ASs;
        static Dictionary<UniqueAbilTypes, AudioSource> _uniqEff;

        public SoundEffectVC(GameObject aSParent)
        {
            _soundEffect_ASs = new Dictionary<ClipTypes, AudioSource>();
            _uniqEff = new Dictionary<UniqueAbilTypes, AudioSource>();

            var mistake_AS = aSParent.AddComponent<AudioSource>();
            mistake_AS.clip = ClipResourcesVC.AudioClip(ClipTypes.Mistake);
            mistake_AS.volume = 0.4f;
            _soundEffect_ASs.Add(ClipTypes.Mistake, mistake_AS);


            var attack_AS = aSParent.AddComponent<AudioSource>();
            attack_AS.clip = ClipResourcesVC.AudioClip(ClipTypes.AttackMelee);
            _soundEffect_ASs.Add(ClipTypes.AttackMelee, attack_AS);


            var attackArcher_AS = aSParent.AddComponent<AudioSource>();
            attackArcher_AS.clip = ClipResourcesVC.AudioClip(ClipTypes.AttackArcher);
            attackArcher_AS.volume = 0.6f;
            _soundEffect_ASs.Add(ClipTypes.AttackArcher, attackArcher_AS);


            var pickArcher_AS = aSParent.AddComponent<AudioSource>();
            pickArcher_AS.clip = ClipResourcesVC.AudioClip(ClipTypes.PickArcher);
            pickArcher_AS.volume = 0.7f;
            _soundEffect_ASs.Add(ClipTypes.PickArcher, pickArcher_AS);


            var pickMelee_AS = aSParent.AddComponent<AudioSource>();
            pickMelee_AS.clip = ClipResourcesVC.AudioClip(ClipTypes.PickMelee);
            pickMelee_AS.volume = 0.1f;
            _soundEffect_ASs.Add(ClipTypes.PickMelee, pickMelee_AS);


            var build_AS = aSParent.AddComponent<AudioSource>();
            build_AS.clip = ClipResourcesVC.AudioClip(ClipTypes.Building);
            build_AS.volume = 0.1f;
            _soundEffect_ASs.Add(ClipTypes.Building, build_AS);


            var clickToTable_AS = aSParent.AddComponent<AudioSource>();
            clickToTable_AS.clip = ClipResourcesVC.AudioClip(ClipTypes.ClickToTable);
            _soundEffect_ASs.Add(ClipTypes.ClickToTable, clickToTable_AS);


            var createUnit_AS = aSParent.AddComponent<AudioSource>();
            createUnit_AS.clip = ClipResourcesVC.AudioClip(ClipTypes.SoundGoldPack);
            createUnit_AS.volume = 0.3f;
            _soundEffect_ASs.Add(ClipTypes.SoundGoldPack, createUnit_AS);


            var melt_AS = aSParent.AddComponent<AudioSource>();
            melt_AS.clip = ClipResourcesVC.AudioClip(ClipTypes.Melting);
            melt_AS.volume = 0.3f;
            _soundEffect_ASs.Add(ClipTypes.Melting, melt_AS);


            var destroy_AS = aSParent.AddComponent<AudioSource>();
            destroy_AS.clip = ClipResourcesVC.AudioClip(ClipTypes.Destroy);
            destroy_AS.volume = 0.3f;
            _soundEffect_ASs.Add(ClipTypes.Destroy, destroy_AS);


            var upgradeUnitMelee_AS = aSParent.AddComponent<AudioSource>();
            upgradeUnitMelee_AS.clip = ClipResourcesVC.AudioClip(ClipTypes.UpgradeMelee);
            upgradeUnitMelee_AS.volume = 0.2f;
            _soundEffect_ASs.Add(ClipTypes.UpgradeMelee, upgradeUnitMelee_AS);


            var shiftUnit_AS = aSParent.AddComponent<AudioSource>();
            shiftUnit_AS.clip = ClipResourcesVC.AudioClip(ClipTypes.ClickToTable);
            shiftUnit_AS.volume = 0.6f;


            var afterBuildTown_AS = aSParent.AddComponent<AudioSource>();
            afterBuildTown_AS.clip = ClipResourcesVC.AudioClip(ClipTypes.AfterBuildTown);
            afterBuildTown_AS.volume = 0.2f;
            _soundEffect_ASs.Add(ClipTypes.AfterBuildTown, afterBuildTown_AS);


            var truce_AS = aSParent.AddComponent<AudioSource>();
            truce_AS.clip = ClipResourcesVC.AudioClip(ClipTypes.Truce);
            truce_AS.volume = 0.6f;
            _soundEffect_ASs.Add(ClipTypes.Truce, truce_AS);


            var cur_AS = aSParent.AddComponent<AudioSource>();
            cur_AS.clip = ClipResourcesVC.AudioClip(ClipTypes.PickUpgrade);
            cur_AS.volume = 0.3f;
            _soundEffect_ASs.Add(ClipTypes.PickUpgrade, cur_AS);


            cur_AS = aSParent.AddComponent<AudioSource>();
            cur_AS.clip = Resources.Load<AudioClip>("GetHero_Clip");
            cur_AS.volume = 0.25f;
            _soundEffect_ASs.Add(ClipTypes.GetHero, cur_AS);



            for (var uniq = UniqueAbilTypes.First; uniq < UniqueAbilTypes.End; uniq++)
            {
                if (uniq != UniqueAbilTypes.CircularAttack
                    && uniq != UniqueAbilTypes.PutOutFirePawn
                    && uniq != UniqueAbilTypes.PutOutFirePawn
                    && uniq != UniqueAbilTypes.ChangeCornerArcher)
                {
                    cur_AS = aSParent.AddComponent<AudioSource>();
                    cur_AS.clip = ClipResourcesVC.AudioClip(uniq);
                    _uniqEff.Add(uniq, cur_AS);

                    var volume = 0f;
                    switch (uniq)
                    {
                        case UniqueAbilTypes.None: throw new Exception();
                        case UniqueAbilTypes.CircularAttack: throw new Exception();
                        case UniqueAbilTypes.BonusNear: volume = 0.3f; break;
                        case UniqueAbilTypes.FirePawn: volume = 0.2f; break;
                        case UniqueAbilTypes.PutOutFirePawn: throw new Exception();
                        case UniqueAbilTypes.Seed: volume = 0.2f; break;
                        case UniqueAbilTypes.FireArcher: volume = 0.2f; break;
                        case UniqueAbilTypes.ChangeCornerArcher: throw new Exception();
                        case UniqueAbilTypes.GrowAdultForest: volume = 0.3f; break;
                        case UniqueAbilTypes.StunElfemale: volume = 0.3f; break;
                        case UniqueAbilTypes.ChangeDirWind: volume = 0.1f; break;
                        case UniqueAbilTypes.End: throw new Exception();
                        default: throw new Exception();
                    }

                    cur_AS.volume = volume;
                }
            }
        }

        public static void Play(ClipTypes eff)
        {
            if (!_soundEffect_ASs.ContainsKey(eff)) throw new Exception();

            _soundEffect_ASs[eff].Play();
        }
        public static void Play(UniqueAbilTypes uniq)
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
