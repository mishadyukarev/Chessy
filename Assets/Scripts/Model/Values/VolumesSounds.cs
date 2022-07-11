namespace Chessy.Model.Values
{
    public static class VolumesSounds
    {
        public static float Volume(in ClipTypes clipT, in TestModeTypes testMode)
        {
            switch (clipT)
            {
                case ClipTypes.AttackArcher: return 0.6f;
                case ClipTypes.AttackMelee: return 1;
                case ClipTypes.Building: return 0.1f;
                case ClipTypes.Mistake: return 0.4f;
                case ClipTypes.SoundGoldPack: return 0.3f;
                case ClipTypes.Melting: return 0.3f;
                case ClipTypes.Destroy: return 0.3f;
                case ClipTypes.ClickToTable: return 0.6f;
                case ClipTypes.Truce: return 0.6f;
                case ClipTypes.PickMelee: return 0.1f;
                case ClipTypes.PickArcher: return 0.7f;
                case ClipTypes.WritePensil: return 0.2f;
                case ClipTypes.Leaf: return 0.4f;
                case ClipTypes.KickGround: return 0.1f;
                case ClipTypes.Rock: return 0.2f;
                case ClipTypes.ShortWind: return 0.2f;
                case ClipTypes.ShortRain: return 0.2f;
                case ClipTypes.Music: return testMode == TestModeTypes.Standart ? 0 : 0.2f;
                case ClipTypes.Click: return 0.15f;
                case ClipTypes.SoundRunningUnit: return 0.25f;
                case ClipTypes.SighUnit: return 0.25f;
                case ClipTypes.ExtractAdultForestWithWarrior: return 0.7f;
                case ClipTypes.BuildingWoodcutterWithWarrior: return 0.5f;
                case ClipTypes.AttackAnimal: return 0.4f;

                case ClipTypes.Background1: return 1;
                case ClipTypes.Background2: return 0.6f;

                default: return 1;
            }
        }
        public static float Volume(in AbilityTypes abilityT)
        {
            switch (abilityT)
            {
                case AbilityTypes.KingPassiveNearBonus: return 0.3f;

                case AbilityTypes.DestroyBuilding: return 0.1f;
                case AbilityTypes.SetFarm: return 0.1f;
                case AbilityTypes.Seed: return 0.2f;
                case AbilityTypes.FirePawn: return 0.2f;

                case AbilityTypes.FireArcher: return 0.2f;

                case AbilityTypes.GrowAdultForest: return 0.3f;
                case AbilityTypes.StunElfemale: return 0.3f;
                case AbilityTypes.ChangeDirectionWind: return 0.1f;

                case AbilityTypes.Resurrect: return 0.1f;
                case AbilityTypes.SetTeleport: return 0.1f;
                case AbilityTypes.Teleport: return 0.1f;
                case AbilityTypes.InvokeSkeletons: return 0.1f;

                default: return 1;
            }
        }
    }
}