﻿using Leopotam.Ecs;
using Chessy.Common;

namespace Chessy.Game
{
    public sealed class SoundS : IEcsRunSystem
    {
        public void Run()
        {
            if (SoundEffectC.IsPlaying(new[] { ClipTypes.Truce, ClipTypes.AfterBuildTown, ClipTypes.PickUpgrade }))
            {
                SoundComC.Volume = 0;
            }
            else
            {
                SoundComC.Volume = SoundComC.SavedVolume;
            }
        }
    }
}