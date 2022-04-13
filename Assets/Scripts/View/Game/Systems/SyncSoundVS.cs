using Chessy.Common;
using Chessy.Game.Values;

namespace Chessy.Game
{
    sealed class SyncSoundVS
    {
        readonly EntitiesViewGame _eV;

        internal SyncSoundVS(in EntitiesViewGame eV)
        {
            _eV = eV;
        }

        internal void Sync()
        {
            if (_eV.SoundASC(ClipTypes.AfterUpdate).IsPlaying 
                || _eV.SoundASC(AbilityTypes.GrowAdultForest).IsPlaying
                || _eV.SoundASC(ClipTypes.Truce).IsPlaying)
            {

                _eV.SoundASC(ClipTypes.Background2).AS.volume =  0;
            }
            else
            {
                _eV.SoundASC(ClipTypes.Background2).AS.volume = StartValues.Volume(ClipTypes.Background2);
            }


            if (_eV.SoundASC(ClipTypes.Truce).IsPlaying)
            {
                _eV.SoundASC(ClipTypes.AfterUpdate).AS.volume = 0f;
            }
            else
            {
                _eV.SoundASC(ClipTypes.AfterUpdate).AS.volume = StartValues.Volume(ClipTypes.AfterUpdate);
            }
        }
    }
}
