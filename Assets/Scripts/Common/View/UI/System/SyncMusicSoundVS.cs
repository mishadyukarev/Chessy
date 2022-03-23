using Chessy.Common.Entity;
using Chessy.Common.Entity.View;

namespace Chessy.Common.View.System
{
    public struct SyncMusicSoundVS
    {
        public void Sync(in EntitiesModelCommon eMC, in EntitiesViewCommon eVC)
        {
            eVC.Sound(Enum.ClipTypes.Music).AudioSource.volume = eMC.VolumeMusic;
        }
    }
}