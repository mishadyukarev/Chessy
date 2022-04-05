using Chessy.Common;
using Chessy.Game.Values;

namespace Chessy.Game
{
    static class SoundVS
    {

        public static void Sync(in EntitiesViewGame eV)
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

            if (eV.EntityVPool.SoundV(ClipTypes.AfterUpdate).IsPlaying 
                || eV.EntityVPool.SoundV(AbilityTypes.GrowAdultForest).IsPlaying
                || eV.EntityVPool.SoundV(ClipTypes.Truce).IsPlaying)
            {

                eV.EntityVPool.SoundV(ClipTypes.Background2).AS.volume =  0;
            }
            else
            {
                eV.EntityVPool.SoundV(ClipTypes.Background2).AS.volume = StartValues.Volume(ClipTypes.Background2);
            }


            if (eV.EntityVPool.SoundV(ClipTypes.Truce).IsPlaying)
            {
                eV.EntityVPool.SoundV(ClipTypes.AfterUpdate).AS.volume = 0f;
            }
            else
            {
                eV.EntityVPool.SoundV(ClipTypes.AfterUpdate).AS.volume = StartValues.Volume(ClipTypes.AfterUpdate);
            }
        }
    }
}
