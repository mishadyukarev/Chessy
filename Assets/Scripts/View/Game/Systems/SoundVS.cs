using Chessy.Common;
using Chessy.Game.Values;

namespace Chessy.Game
{
    static class SoundVS
    {

        public static void Sync(in EntitiesViewGame eV)
        {
            //if (eV.SoundV(ClipTypes.Truce).IsPlaying
            //    || eV.SoundV(ClipTypes.AfterBuildTown).IsPlaying
            //    || eV.SoundV(ClipTypes.PickUpgrade).IsPlaying)
            //{
            //    SoundC.Volume = 0;
            //}
            //else
            //{
            //    SoundC.Volume = SoundC.SavedVolume;
            //}

            if (eV.SoundV(ClipTypes.AfterUpdate).IsPlaying 
                || eV.SoundV(AbilityTypes.GrowAdultForest).IsPlaying
                || eV.SoundV(ClipTypes.Truce).IsPlaying)
            {

                eV.SoundV(ClipTypes.Background2).AS.volume =  0;
            }
            else
            {
                eV.SoundV(ClipTypes.Background2).AS.volume = StartValues.Volume(ClipTypes.Background2);
            }


            if (eV.SoundV(ClipTypes.Truce).IsPlaying)
            {
                eV.SoundV(ClipTypes.AfterUpdate).AS.volume = 0f;
            }
            else
            {
                eV.SoundV(ClipTypes.AfterUpdate).AS.volume = StartValues.Volume(ClipTypes.AfterUpdate);
            }
        }
    }
}
