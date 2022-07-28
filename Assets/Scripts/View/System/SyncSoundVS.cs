using Chessy.Model;
using Chessy.Model.Entity;
using Chessy.Model.Values;
using Chessy.View.UI.Entity;

namespace Chessy.View.System
{
    sealed class SyncSoundVS : SystemAbstract
    {
        readonly EntitiesView _eV;

        internal SyncSoundVS(in EntitiesView eV, EntitiesModel eM) : base(eM)
        {
            _eV = eV;
        }

        internal void Sync()
        {
            if (_eV.SoundASC(ClipTypes.AfterUpdate).IsPlaying
                || _eV.SoundASC(AbilityTypes.GrowAdultForest).IsPlaying
                || _eV.SoundASC(ClipTypes.Truce).IsPlaying)
            {

                _eV.SoundASC(ClipTypes.Background2).AS.volume = 0;
            }
            else
            {
                _eV.SoundASC(ClipTypes.Background2).AS.volume = VolumesSounds.Volume(ClipTypes.Background2, AboutGameC.TestModeT);
            }


            if (_eV.SoundASC(ClipTypes.Truce).IsPlaying)
            {
                _eV.SoundASC(ClipTypes.AfterUpdate).AS.volume = 0f;
            }
            else
            {
                _eV.SoundASC(ClipTypes.AfterUpdate).AS.volume = VolumesSounds.Volume(ClipTypes.AfterUpdate, AboutGameC.TestModeT);
            }
        }
    }
}
