using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Entities.Game.General.Else.Vis;
using Assets.Scripts.Workers.Cell;
using System;
using UnityEngine;

namespace Assets.Scripts.Workers
{
    internal sealed class SoundGameGeneralViewWorker
    {
        private static EntGameGeneralElseViewManager EntGGElseViewM => Main.Instance.ECSmanager.EntGameGeneralElseViewManager;


        private static AudioSource GetAudioSource(SoundEffectTypes soundEffectType)
        {
            switch (soundEffectType)
            {
                case SoundEffectTypes.None:
                    throw new Exception();

                case SoundEffectTypes.AttackArcher:
                    return EntGGElseViewM.AttackArcherEnt_AudioSourceCom.AudioSource;

                case SoundEffectTypes.AttackMelee:
                    return EntGGElseViewM.AttackAudioSource;

                case SoundEffectTypes.Building:
                    return EntGGElseViewM.BuildingSoundEnt_AudioSourceCom.AudioSource;


                case SoundEffectTypes.Fire:
                    return EntGGElseViewM.FireSoundEnt_AudioSourceCom.AudioSource;


                case SoundEffectTypes.Setting:
                    return EntGGElseViewM.SettingSoundEnt_AudioSourceCom.AudioSource;


                case SoundEffectTypes.Mistake:
                    return EntGGElseViewM.MistakeAudioSource;


                case SoundEffectTypes.SoundGoldPack:
                    return EntGGElseViewM.BuySoundEnt_AudioSourceCom.AudioSource;


                case SoundEffectTypes.Melting:
                    return EntGGElseViewM.MeltingSoundEnt_AudioSourceCom.AudioSource;


                case SoundEffectTypes.Destroy:
                    return EntGGElseViewM.DestroySoundEnt_AudioSourceCom.AudioSource;


                case SoundEffectTypes.UpgradeUnitMelee:
                    return EntGGElseViewM.UpgradeUnitMeleeSoundEnt_AudioSourceCom.AudioSource;


                case SoundEffectTypes.UpgradeUnitArcher:
                    return EntGGElseViewM.PickArcherEnt_AudioSourceCom.AudioSource;


                case SoundEffectTypes.Seeding:
                    return EntGGElseViewM.SeedingSoundEnt_AudioSourceCom.AudioSource;


                case SoundEffectTypes.ClickToTable:
                    return EntGGElseViewM.ShiftUnitSoundEnt_AudioSourceCom.AudioSource;


                case SoundEffectTypes.Truce:
                    return EntGGElseViewM.TruceSoundEnt_AudioSourceCom.AudioSource;


                case SoundEffectTypes.PickMelee:
                    return EntGGElseViewM.PickMeleeEnt_AudioSourceCom.AudioSource;


                case SoundEffectTypes.PickArcher:
                    return EntGGElseViewM.PickArcherEnt_AudioSourceCom.AudioSource;


                default:
                    throw new Exception();
            }
        }

        internal static void PlaySoundEffect(SoundEffectTypes soundEffectType) => GetAudioSource(soundEffectType).Play();
        internal static bool IsPlaying(SoundEffectTypes soundEffectType) => GetAudioSource(soundEffectType).isPlaying;
    }
}
