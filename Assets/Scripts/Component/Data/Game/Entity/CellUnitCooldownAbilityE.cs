using ECS;
using System;

namespace Game.Game
{
    public sealed class CellUnitCooldownAbilityE : EntityAbstract
    {
        readonly AbilityTypes _ability;
        ref AmountC CooldownRef => ref Ent.Get<AmountC>();
        public AmountC Cooldown => Ent.Get<AmountC>();

        internal CellUnitCooldownAbilityE(in AbilityTypes ability, in EcsWorld gameW) : base(gameW)
        {
            _ability = ability;
        }

        internal void SetNew()
        {
            CooldownRef.Amount = 0;
        }
        internal void Shift(in CellUnitCooldownAbilityE cooldownE_from)
        {
            CooldownRef = cooldownE_from.Cooldown;
            cooldownE_from.CooldownRef.Amount = 0;
        }

        public void SetAfterAbility()
        {
            switch (_ability)
            {
                case AbilityTypes.CircularAttack:
                    CooldownRef.Amount = 2;
                    break;

                case AbilityTypes.BonusNear:
                    CooldownRef.Amount = 3;
                    break;

                case AbilityTypes.GrowAdultForest:
                    CooldownRef.Amount = 5;
                    break;

                case AbilityTypes.StunElfemale:
                    CooldownRef.Amount = 5;
                    break;

                case AbilityTypes.ChangeDirectionWind:
                    CooldownRef.Amount = 6;
                    break;

                case AbilityTypes.IceWall:
                    CooldownRef.Amount = 5;
                    break;

                default: throw new Exception();
            }
        }
        public void TakeAfterUpdate()
        {
            CooldownRef.Amount--;
        }
        public void SyncRpc(in int cooldown)
        {
            CooldownRef.Amount = cooldown;
        }
    }
}