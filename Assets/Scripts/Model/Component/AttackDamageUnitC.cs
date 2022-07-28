namespace Chessy.Model.Component
{
    public class AttackDamageUnitC
    {
        internal double DamageSimpleAttack;
        internal double DamageOnCell;
        internal int CooldownForAttackAnyUnitInSeconds;

        public double DamageSimpleAttackP => DamageSimpleAttack;
        public double DamageOnCellP => DamageOnCell;
        public bool HaveCoolDownForAttackAnyUnit => CooldownForAttackAnyUnitInSeconds > 0;

        internal AttackDamageUnitC Clone()
        {
            var attackC = new AttackDamageUnitC
            {
                DamageSimpleAttack = DamageSimpleAttack,
                DamageOnCell = DamageOnCell,
                CooldownForAttackAnyUnitInSeconds = CooldownForAttackAnyUnitInSeconds
            };

            return attackC;
        }
        internal void Copy(in AttackDamageUnitC newC)
        {
            DamageSimpleAttack = newC.DamageSimpleAttack;
            DamageOnCell = newC.DamageOnCell;
            CooldownForAttackAnyUnitInSeconds = newC.CooldownForAttackAnyUnitInSeconds;
        }
    }
}