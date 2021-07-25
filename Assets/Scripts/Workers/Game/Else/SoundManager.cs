using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Workers.Cell;
using System;
using static Assets.Scripts.Main;

namespace Assets.Scripts.Workers
{
    internal class SoundManager : MainGeneralWorker
    {
        internal static EntitiesGameGeneralManager EGGM => Instance.EntGGM;
        internal static void PlaySoundEffect(SoundEffectTypes soundEffectType)
        {
            switch (soundEffectType)
            {
                case SoundEffectTypes.None:
                    throw new Exception();

                case SoundEffectTypes.AttackArcher:
                    EGGM.AttackArcherEnt_AudioSourceCom.AudioSource.Play();
                    break;

                case SoundEffectTypes.AttackMelee:
                    EGGM.AttackAudioSource.Play();
                    break;

                case SoundEffectTypes.Building:
                    EGGM.BuildingSoundEnt_AudioSourceCom.AudioSource.Play();
                    break;

                case SoundEffectTypes.Fire:
                    EGGM.FireSoundEnt_AudioSourceCom.AudioSource.Play();
                    break;

                case SoundEffectTypes.Setting:
                    EGGM.SettingSoundEnt_AudioSourceCom.AudioSource.Play();
                    break;

                case SoundEffectTypes.Mistake:
                    EGGM.MistakeAudioSource.Play();
                    break;

                case SoundEffectTypes.SoundGoldPack:
                    EGGM.BuySoundEnt_AudioSourceCom.AudioSource.Play();
                    break;

                case SoundEffectTypes.Melting:
                    EGGM.MeltingSoundEnt_AudioSourceCom.AudioSource.Play();
                    break;

                case SoundEffectTypes.Destroy:
                    EGGM.DestroySoundEnt_AudioSourceCom.AudioSource.Play();
                    break;

                case SoundEffectTypes.UpgradeUnitMelee:
                    EGGM.UpgradeUnitMeleeSoundEnt_AudioSourceCom.AudioSource.Play();
                    break;

                case SoundEffectTypes.UpgradeUnitArcher:
                    EGGM.PickArcherEnt_AudioSourceCom.AudioSource.Play();
                    break;

                case SoundEffectTypes.Seeding:
                    EGGM.SeedingSoundEnt_AudioSourceCom.AudioSource.Play();
                    break;

                case SoundEffectTypes.ClickToTable:
                    EGGM.ShiftUnitSoundEnt_AudioSourceCom.AudioSource.Play();
                    break;

                case SoundEffectTypes.Truce:
                    EGGM.TruceSoundEnt_AudioSourceCom.AudioSource.Play();
                    break;

                default:
                    throw new Exception();
            }
        }
    }
}
