using Chessy.Common;

namespace Chessy.Game
{
    sealed class SoundVS : SystemViewAbstract, IEcsRunSystem
    {
        internal SoundVS(in EntitiesModel ents, in EntitiesView entsView) : base(ents, entsView)
        {
        }

        public void Run()
        {
            if (VEs.EntityVPool.SoundV(ClipTypes.Truce).IsPlaying
                || VEs.EntityVPool.SoundV(ClipTypes.AfterBuildTown).IsPlaying
                || VEs.EntityVPool.SoundV(ClipTypes.PickUpgrade).IsPlaying)
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
