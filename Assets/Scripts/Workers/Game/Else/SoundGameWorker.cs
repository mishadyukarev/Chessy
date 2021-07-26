using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Workers.Cell;
using System;
using UnityEngine;

namespace Assets.Scripts.Workers
{
    internal class SoundGameWorker : MainGeneralWorker
    {
        private static AudioSource GetAudioSource(SoundEffectTypes soundEffectType)
        {
            switch (soundEffectType)
            {
                case SoundEffectTypes.None:
                    throw new Exception();

                case SoundEffectTypes.AttackArcher:
                    return EGGM.AttackArcherEnt_AudioSourceCom.AudioSource;

                case SoundEffectTypes.AttackMelee:
                    return EGGM.AttackAudioSource;

                case SoundEffectTypes.Building:
                    return EGGM.BuildingSoundEnt_AudioSourceCom.AudioSource;
                    

                case SoundEffectTypes.Fire:
                    return EGGM.FireSoundEnt_AudioSourceCom.AudioSource;
                    

                case SoundEffectTypes.Setting:
                    return EGGM.SettingSoundEnt_AudioSourceCom.AudioSource;
                    

                case SoundEffectTypes.Mistake:
                    return EGGM.MistakeAudioSource;
                    

                case SoundEffectTypes.SoundGoldPack:
                    return EGGM.BuySoundEnt_AudioSourceCom.AudioSource;
                    

                case SoundEffectTypes.Melting:
                    return EGGM.MeltingSoundEnt_AudioSourceCom.AudioSource;
                    

                case SoundEffectTypes.Destroy:
                    return EGGM.DestroySoundEnt_AudioSourceCom.AudioSource;
                    

                case SoundEffectTypes.UpgradeUnitMelee:
                    return EGGM.UpgradeUnitMeleeSoundEnt_AudioSourceCom.AudioSource;
                    

                case SoundEffectTypes.UpgradeUnitArcher:
                    return EGGM.PickArcherEnt_AudioSourceCom.AudioSource;
                    

                case SoundEffectTypes.Seeding:
                    return EGGM.SeedingSoundEnt_AudioSourceCom.AudioSource;
                    

                case SoundEffectTypes.ClickToTable:
                    return EGGM.ShiftUnitSoundEnt_AudioSourceCom.AudioSource;
                    

                case SoundEffectTypes.Truce:
                    return EGGM.TruceSoundEnt_AudioSourceCom.AudioSource;
                    

                case SoundEffectTypes.PickMelee:
                    return EGGM.PickMeleeEnt_AudioSourceCom.AudioSource;
                    

                case SoundEffectTypes.PickArcher:
                    return EGGM.PickArcherEnt_AudioSourceCom.AudioSource;
                    

                default:
                    throw new Exception();
            }
        }

        internal static void PlaySoundEffect(SoundEffectTypes soundEffectType) => GetAudioSource(soundEffectType).Play();
        internal static bool IsPlaying(SoundEffectTypes soundEffectType) => GetAudioSource(soundEffectType).isPlaying;
    }
}
