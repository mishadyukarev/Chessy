﻿using System;
using UnityEditor;
using UnityEngine;

namespace Game.Game
{
    public struct UpSunsUIS : IEcsRunSystem
    {
        public void Run()
        {
            switch (SunSidesE.SunSideTC.SunSide)
            {
                case SunSideTypes.Dawn:
                    UpSunsUIEs.ImageC(true).SetActive(false);
                    UpSunsUIEs.ImageC(false).SetActive(true);
                    break;

                case SunSideTypes.Center:
                    UpSunsUIEs.ImageC(true).SetActive(false);
                    UpSunsUIEs.ImageC(false).SetActive(false);
                    break;

                case SunSideTypes.Sunset:
                    UpSunsUIEs.ImageC(true).SetActive(true);
                    UpSunsUIEs.ImageC(false).SetActive(false);
                    break;

                case SunSideTypes.Night:
                    UpSunsUIEs.ImageC(true).SetActive(false);
                    UpSunsUIEs.ImageC(false).SetActive(false);
                    break;

                default: throw new Exception();
            }
        }
    }
}