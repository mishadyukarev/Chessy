using Assets.Scripts.ECS.Game.Components;
using Assets.Scripts.ECS.System.Data.Common;
using Leopotam.Ecs;
using UnityEngine;

namespace Assets.Scripts.ECS.Entities.Game.General.Else.View.Containers
{
    internal sealed class SoundElseViewContainer
    {
        internal AudioSource MistakeAudioSource { get; private set; }
        internal AudioSource AttackAudioSource { get; private set; }


        private EcsEntity _attackArcherSoundEnt;
        internal ref AudioSourceComponent AttackArcherEnt_AudioSourceCom => ref _attackArcherSoundEnt.Get<AudioSourceComponent>();


        private EcsEntity _pickArcherSoundEnt;
        internal ref AudioSourceComponent PickArcherEnt_AudioSourceCom => ref _pickArcherSoundEnt.Get<AudioSourceComponent>();


        private EcsEntity _pickMeleeSoundEnt;
        internal ref AudioSourceComponent PickMeleeEnt_AudioSourceCom => ref _pickMeleeSoundEnt.Get<AudioSourceComponent>();


        private EcsEntity _buildingSoundEnt;
        internal ref AudioSourceComponent BuildingSoundEnt_AudioSourceCom => ref _buildingSoundEnt.Get<AudioSourceComponent>();


        private EcsEntity _fireSoundEnt;
        internal ref AudioSourceComponent FireSoundEnt_AudioSourceCom => ref _fireSoundEnt.Get<AudioSourceComponent>();


        private EcsEntity _settingSoundEnt;
        internal ref AudioSourceComponent SettingSoundEnt_AudioSourceCom => ref _settingSoundEnt.Get<AudioSourceComponent>();


        private EcsEntity _buySoundEnt;
        internal ref AudioSourceComponent BuySoundEnt_AudioSourceCom => ref _buySoundEnt.Get<AudioSourceComponent>();


        private EcsEntity _meltingSoundEnt;
        internal ref AudioSourceComponent MeltingSoundEnt_AudioSourceCom => ref _meltingSoundEnt.Get<AudioSourceComponent>();


        private EcsEntity _destroySoundEnt;
        internal ref AudioSourceComponent DestroySoundEnt_AudioSourceCom => ref _destroySoundEnt.Get<AudioSourceComponent>();


        private EcsEntity _upgradeUnitMeleeSoundEnt;
        internal ref AudioSourceComponent UpgradeUnitMeleeSoundEnt_AudioSourceCom => ref _upgradeUnitMeleeSoundEnt.Get<AudioSourceComponent>();


        private EcsEntity _seedingSoundEnt;
        internal ref AudioSourceComponent SeedingSoundEnt_AudioSourceCom => ref _seedingSoundEnt.Get<AudioSourceComponent>();


        private EcsEntity _shiftUnitSoundEnt;
        internal ref AudioSourceComponent ShiftUnitSoundEnt_AudioSourceCom => ref _shiftUnitSoundEnt.Get<AudioSourceComponent>();


        private EcsEntity _truceSoundEnt;
        internal ref AudioSourceComponent TruceSoundEnt_AudioSourceCom => ref _truceSoundEnt.Get<AudioSourceComponent>();


        internal SoundElseViewContainer(EcsWorld gameWorld)
        {
            var audioSourceParentGO = new GameObject("AudioSource");
            MainCommonSystem.CommonEnt_ParentCom.Attach(audioSourceParentGO.transform);

            MistakeAudioSource = audioSourceParentGO.AddComponent<AudioSource>();
            MistakeAudioSource.clip = MainCommonSystem.CommonEnt_ResourcesCommonCom.SoundConfig.MistakeAudioClip;

            AttackAudioSource = audioSourceParentGO.AddComponent<AudioSource>();
            AttackAudioSource.clip = MainCommonSystem.CommonEnt_ResourcesCommonCom.SoundConfig.AttackSwordAudioClip;



            var attackAS = audioSourceParentGO.AddComponent<AudioSource>();
            attackAS.clip = MainCommonSystem.CommonEnt_ResourcesCommonCom.SoundConfig.AttackArcherAC;
            attackAS.volume = 0.6f;
            _attackArcherSoundEnt = gameWorld.NewEntity()
                .Replace(new AudioSourceComponent(attackAS));


            var pickArcherAS = audioSourceParentGO.AddComponent<AudioSource>();
            pickArcherAS.clip = MainCommonSystem.CommonEnt_ResourcesCommonCom.SoundConfig.PickArcherAudioClip;
            pickArcherAS.volume = 0.7f;
            _pickArcherSoundEnt = gameWorld.NewEntity()
                .Replace(new AudioSourceComponent(pickArcherAS));


            var pickMeleeAS = audioSourceParentGO.AddComponent<AudioSource>();
            pickMeleeAS.clip = MainCommonSystem.CommonEnt_ResourcesCommonCom.SoundConfig.PickMeleeAC;
            pickMeleeAS.volume = 0.1f;
            _pickMeleeSoundEnt = gameWorld.NewEntity()
                .Replace(new AudioSourceComponent(pickMeleeAS));


            var buildingAS = audioSourceParentGO.AddComponent<AudioSource>();
            buildingAS.clip = MainCommonSystem.CommonEnt_ResourcesCommonCom.SoundConfig.BuildingAC;
            buildingAS.volume = 0.1f;
            _buildingSoundEnt = gameWorld.NewEntity()
                .Replace(new AudioSourceComponent(buildingAS));


            var settingAS = audioSourceParentGO.AddComponent<AudioSource>();
            settingAS.clip = MainCommonSystem.CommonEnt_ResourcesCommonCom.SoundConfig.SettingUnitAC;
            _settingSoundEnt = gameWorld.NewEntity()
                .Replace(new AudioSourceComponent(settingAS));


            var fireAS = audioSourceParentGO.AddComponent<AudioSource>();
            fireAS.clip = MainCommonSystem.CommonEnt_ResourcesCommonCom.SoundConfig.FireAC;
            fireAS.volume = 0.2f;
            _fireSoundEnt = gameWorld.NewEntity()
                .Replace(new AudioSourceComponent(fireAS));


            var buyAS = audioSourceParentGO.AddComponent<AudioSource>();
            buyAS.clip = MainCommonSystem.CommonEnt_ResourcesCommonCom.SoundConfig.BuyAC;
            buyAS.volume = 0.3f;
            _buySoundEnt = gameWorld.NewEntity()
                .Replace(new AudioSourceComponent(buyAS));


            var meltingAS = audioSourceParentGO.AddComponent<AudioSource>();
            meltingAS.clip = MainCommonSystem.CommonEnt_ResourcesCommonCom.SoundConfig.Melting_Clip;
            meltingAS.volume = 0.3f;
            _meltingSoundEnt = gameWorld.NewEntity()
                .Replace(new AudioSourceComponent(meltingAS));



            var destroyAS = audioSourceParentGO.AddComponent<AudioSource>();
            destroyAS.clip = MainCommonSystem.CommonEnt_ResourcesCommonCom.SoundConfig.Destroy_Clip;
            destroyAS.volume = 0.3f;
            _destroySoundEnt = gameWorld.NewEntity()
                .Replace(new AudioSourceComponent(destroyAS));


            var upgradeUnitMeleeAS = audioSourceParentGO.AddComponent<AudioSource>();
            upgradeUnitMeleeAS.clip = MainCommonSystem.CommonEnt_ResourcesCommonCom.SoundConfig.UpgradeUnitMelee_Clip;
            upgradeUnitMeleeAS.volume = 0.2f;
            _upgradeUnitMeleeSoundEnt = gameWorld.NewEntity()
                .Replace(new AudioSourceComponent(upgradeUnitMeleeAS));


            var seedingAS = audioSourceParentGO.AddComponent<AudioSource>();
            seedingAS.clip = MainCommonSystem.CommonEnt_ResourcesCommonCom.SoundConfig.Seeding_Clip;
            seedingAS.volume = 0.2f;
            _seedingSoundEnt = gameWorld.NewEntity()
                .Replace(new AudioSourceComponent(seedingAS));


            var shiftUnitAS = audioSourceParentGO.AddComponent<AudioSource>();
            shiftUnitAS.clip = MainCommonSystem.CommonEnt_ResourcesCommonCom.SoundConfig.ShiftUnit_Clip;
            shiftUnitAS.volume = 0.6f;
            _shiftUnitSoundEnt = gameWorld.NewEntity()
                .Replace(new AudioSourceComponent(shiftUnitAS));


            var truceAS = audioSourceParentGO.AddComponent<AudioSource>();
            truceAS.clip = MainCommonSystem.CommonEnt_ResourcesCommonCom.SoundConfig.Truce_Clip;
            truceAS.volume = 0.6f;
            _truceSoundEnt = gameWorld.NewEntity()
                .Replace(new AudioSourceComponent(truceAS));
        }
    }
}
