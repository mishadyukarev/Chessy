﻿//using Assets.Scripts.Abstractions.Enums;
//using Assets.Scripts.ECS.Game.Components;
//using Assets.Scripts.ECS.System.Data.Common;
//using System;
//using UnityEngine;

//namespace Assets.Scripts.Workers.Common
//{
//    internal sealed class SoundComWorker
//    {
//        private static AudioSource GetAS(SoundComTypes soundComType)
//        {
//            switch (soundComType)
//            {
//                case SoundComTypes.None:
//                    throw new Exception();

//                case SoundComTypes.StandartMusic:
//                    return AudioSourceComponent.AudioSource;

//                default:
//                    throw new Exception();
//            }
//        }

//        internal static float GetVolume(SoundComTypes soundComType) => GetAS(soundComType).volume;
//        internal static void SetVolume(SoundComTypes soundComType, float value) => GetAS(soundComType).volume = value;

//        internal static void ResetVolume(SoundComTypes soundComType) => SetVolume(soundComType, default);
//    }
//}