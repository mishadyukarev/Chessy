using Chessy.Common;

namespace Chessy.Game
{
    static class SoundVS
    {

        public static void Sync(in EntitiesView eV)
        {
            //if (eV.EntityVPool.SoundV(ClipTypes.Truce).IsPlaying
            //    || eV.EntityVPool.SoundV(ClipTypes.AfterBuildTown).IsPlaying
            //    || eV.EntityVPool.SoundV(ClipTypes.PickUpgrade).IsPlaying)
            //{
            //    SoundC.Volume = 0;
            //}
            //else
            //{
            //    SoundC.Volume = SoundC.SavedVolume;
            //}

            if (eV.EntityVPool.SoundV(ClipTypes.AfterUpdate).IsPlaying /*|| eV.EntityVPool.SoundV(ClipTypes.HeroAbility).IsPlaying*/)
            {
                eV.EntityVPool.SoundV(ClipTypes.BackgroundInGame).AudioSource.volume = 0.01f;
            }
            else
            {
                eV.EntityVPool.SoundV(ClipTypes.BackgroundInGame).AudioSource.volume = 0.25f;
            }
        }
    }
}
