using Chessy.Model;
using Chessy.Model.Values;
using Chessy.Model;

namespace Chessy.Model
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
                _eV.SoundASC(ClipTypes.Background2).AS.volume = StartValues.Volume(ClipTypes.Background2, _e.TestModeT);
            }


            if (_eV.SoundASC(ClipTypes.Truce).IsPlaying)
            {
                _eV.SoundASC(ClipTypes.AfterUpdate).AS.volume = 0f;
            }
            else
            {
                _eV.SoundASC(ClipTypes.AfterUpdate).AS.volume = StartValues.Volume(ClipTypes.AfterUpdate, _e.TestModeT);
            }
        }
    }
}
