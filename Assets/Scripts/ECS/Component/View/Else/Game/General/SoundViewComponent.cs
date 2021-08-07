using Assets.Scripts.ECS.Game.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.ECS.Component.View.Else.Game.General
{
    internal struct SoundViewComponent
    {
        private AudioSource _mistake_AS;
        private AudioSource _attack_AS;
        private AudioSource _attackArcher_AS;
        private AudioSource _pickArcher_AS;
        private AudioSource _pickMelee_AS;
        private AudioSource _build_AS;
        private AudioSource _fire_AS;
        private AudioSource _settingUnit_AS;
        private AudioSource _createUnit_AS;
        private AudioSource _melt_AS;
        private AudioSource _destroy_AS;
        private AudioSource _upgradeUnitMelee_AS;
        private AudioSource _seeding_AS;
        private AudioSource _shiftUnit_AS;
        private AudioSource _truce_AS;

        internal SoundViewComponent(GameObject audioSourceParent_GO)
        {

            _mistake_AS = audioSourceParent_GO.AddComponent<AudioSource>();
            _mistake_AS.clip = ResourcesComponent.SoundConfig.MistakeAudioClip;


            _attack_AS = audioSourceParent_GO.AddComponent<AudioSource>();
            _attack_AS.clip = ResourcesComponent.SoundConfig.AttackSwordAudioClip;


            _attackArcher_AS = audioSourceParent_GO.AddComponent<AudioSource>();
            _attackArcher_AS.clip = ResourcesComponent.SoundConfig.AttackArcherAC;
            _attackArcher_AS.volume = 0.6f;


            _pickArcher_AS = audioSourceParent_GO.AddComponent<AudioSource>();
            _pickArcher_AS.clip = ResourcesComponent.SoundConfig.PickArcherAudioClip;
            _pickArcher_AS.volume = 0.7f;


            _pickMelee_AS = audioSourceParent_GO.AddComponent<AudioSource>();
            _pickMelee_AS.clip = ResourcesComponent.SoundConfig.PickMeleeAC;
            _pickMelee_AS.volume = 0.1f;


            _build_AS = audioSourceParent_GO.AddComponent<AudioSource>();
            _build_AS.clip = ResourcesComponent.SoundConfig.BuildingAC;
            _build_AS.volume = 0.1f;


            _settingUnit_AS = audioSourceParent_GO.AddComponent<AudioSource>();
            _settingUnit_AS.clip = ResourcesComponent.SoundConfig.SettingUnitAC;


            _fire_AS = audioSourceParent_GO.AddComponent<AudioSource>();
            _fire_AS.clip = ResourcesComponent.SoundConfig.FireAC;
            _fire_AS.volume = 0.2f;


            _createUnit_AS = audioSourceParent_GO.AddComponent<AudioSource>();
            _createUnit_AS.clip = ResourcesComponent.SoundConfig.BuyAC;
            _createUnit_AS.volume = 0.3f;


            _melt_AS = audioSourceParent_GO.AddComponent<AudioSource>();
            _melt_AS.clip = ResourcesComponent.SoundConfig.Melting_Clip;
            _melt_AS.volume = 0.3f;


            _destroy_AS = audioSourceParent_GO.AddComponent<AudioSource>();
            _destroy_AS.clip = ResourcesComponent.SoundConfig.Destroy_Clip;
            _destroy_AS.volume = 0.3f;


            _upgradeUnitMelee_AS = audioSourceParent_GO.AddComponent<AudioSource>();
            _upgradeUnitMelee_AS.clip = ResourcesComponent.SoundConfig.UpgradeUnitMelee_Clip;
            _upgradeUnitMelee_AS.volume = 0.2f;


            _seeding_AS = audioSourceParent_GO.AddComponent<AudioSource>();
            _seeding_AS.clip = ResourcesComponent.SoundConfig.Seeding_Clip;
            _seeding_AS.volume = 0.2f;


            _shiftUnit_AS = audioSourceParent_GO.AddComponent<AudioSource>();
            _shiftUnit_AS.clip = ResourcesComponent.SoundConfig.ShiftUnit_Clip;
            _shiftUnit_AS.volume = 0.6f;


            _truce_AS = audioSourceParent_GO.AddComponent<AudioSource>();
            _truce_AS.clip = ResourcesComponent.SoundConfig.Truce_Clip;
            _truce_AS.volume = 0.6f;
        }
    }
}
