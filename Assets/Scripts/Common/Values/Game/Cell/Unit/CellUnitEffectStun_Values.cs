namespace Chessy.Game
{
    public static class CellUnitEffectStun_Values
    {
        public static int ForStunAfterAbility(in AbilityTypes ability)
        {
            switch (ability)
            {
                case AbilityTypes.StunElfemale: return 4;
                case AbilityTypes.ActiveAroundBonusSnowy: return 2;
                case AbilityTypes.DirectWave: return 2;
                default: throw new System.Exception();
            }
        }


    }
}