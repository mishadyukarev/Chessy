using Chessy.Common;

namespace Chessy.Game
{
    static class SoundVS
    {
        public static void Sync(in EntitiesView eV)
        {
            if (eV.EntityVPool.SoundV(ClipTypes.Truce).IsPlaying
                || eV.EntityVPool.SoundV(ClipTypes.AfterBuildTown).IsPlaying
                || eV.EntityVPool.SoundV(ClipTypes.PickUpgrade).IsPlaying)
            {
                SoundC.Volume = 0;
            }
            else
            {
                SoundC.Volume = SoundC.SavedVolume;
            }
        }
    }
}
