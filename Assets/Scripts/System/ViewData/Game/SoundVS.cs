using Game.Common;
using static Game.Game.EntityVPool;

namespace Game.Game
{
    sealed class SoundVS : SystemViewAbstract, IEcsRunSystem
    {
        public SoundVS(in Entities ents, in EntitiesView entsView) : base(ents, entsView)
        {
        }

        public void Run()
        {
            if (SoundV(ClipTypes.Truce).IsPlaying
                || SoundV(ClipTypes.AfterBuildTown).IsPlaying
                || SoundV(ClipTypes.PickUpgrade).IsPlaying)
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
