//using Assets.Scripts.Abstractions.Enums;
//using Assets.Scripts.ECS.Entities.Game.General.Else.View.Containers;
//using System;
//using UnityEngine;

//namespace Assets.Scripts.Workers
//{
//    internal sealed class SoundGameGeneralViewWorker
//    {
//        private static SoundElseViewContainer _container;

//        internal SoundGameGeneralViewWorker(SoundElseViewContainer contrainer)
//        {
//            _container = contrainer;
//        }

//        private static AudioSource GetAudioSource(SoundEffectTypes soundEffectType)
//        {
//            switch (soundEffectType)
//            {
//                case SoundEffectTypes.None:
//                    throw new Exception();

//                case SoundEffectTypes.AttackArcher:
//                    return _container.AttackArcherEnt_AudioSourceCom.AudioSource;

//                case SoundEffectTypes.AttackMelee:
//                    return _container.AttackAudioSource;

//                case SoundEffectTypes.Building:
//                    return _container.BuildingSoundEnt_AudioSourceCom.AudioSource;


//                case SoundEffectTypes.Fire:
//                    return _container.FireSoundEnt_AudioSourceCom.AudioSource;


//                case SoundEffectTypes.Setting:
//                    return _container.SettingSoundEnt_AudioSourceCom.AudioSource;


//                case SoundEffectTypes.Mistake:
//                    return _container.MistakeAudioSource;


//                case SoundEffectTypes.SoundGoldPack:
//                    return _container.BuySoundEnt_AudioSourceCom.AudioSource;


//                case SoundEffectTypes.Melting:
//                    return _container.MeltingSoundEnt_AudioSourceCom.AudioSource;


//                case SoundEffectTypes.Destroy:
//                    return _container.DestroySoundEnt_AudioSourceCom.AudioSource;


//                case SoundEffectTypes.UpgradeUnitMelee:
//                    return _container.UpgradeUnitMeleeSoundEnt_AudioSourceCom.AudioSource;


//                case SoundEffectTypes.UpgradeUnitArcher:
//                    return _container.PickArcherEnt_AudioSourceCom.AudioSource;


//                case SoundEffectTypes.Seeding:
//                    return _container.SeedingSoundEnt_AudioSourceCom.AudioSource;


//                case SoundEffectTypes.ClickToTable:
//                    return _container.ShiftUnitSoundEnt_AudioSourceCom.AudioSource;


//                case SoundEffectTypes.Truce:
//                    return _container.TruceSoundEnt_AudioSourceCom.AudioSource;


//                case SoundEffectTypes.PickMelee:
//                    return _container.PickMeleeEnt_AudioSourceCom.AudioSource;


//                case SoundEffectTypes.PickArcher:
//                    return _container.PickArcherEnt_AudioSourceCom.AudioSource;


//                default:
//                    throw new Exception();
//            }
//        }

//        internal static void PlaySoundEffect(SoundEffectTypes soundEffectType) => GetAudioSource(soundEffectType).Play();
//        internal static bool IsPlaying(SoundEffectTypes soundEffectType) => GetAudioSource(soundEffectType).isPlaying;
//    }
//}
