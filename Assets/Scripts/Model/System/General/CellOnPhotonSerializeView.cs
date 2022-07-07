using Chessy.Model.Entity;
using Chessy.Model.Values;
using Photon.Pun;
using UnityEngine;

namespace Chessy.Model.System
{
    public sealed class CellOnPhotonSerializeView : MonoBehaviour, IPunObservable
    {
        EntitiesModel _e;
        byte _cellIdx;
        byte _typeUpdate;

        public void GiveData(in byte cellIdx, in byte typeUpdate, in EntitiesModel eM)
        {
            _cellIdx = cellIdx;
            _e = eM;
            _typeUpdate = typeUpdate;
        }


        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (_typeUpdate == 1)
            {
                if (stream.IsWriting)
                {
                    stream.SendNext(_e.UnitT(_cellIdx));
                    stream.SendNext(_e.UnitLevelT(_cellIdx));
                    stream.SendNext(_e.UnitPlayerT(_cellIdx));
                    stream.SendNext(_e.UnitConditionT(_cellIdx));
                    stream.SendNext(_e.IsRightArcherUnit(_cellIdx));
                    stream.SendNext(_e.UnitMainC(_cellIdx).CooldownForAttackAnyUnitInSeconds);


                    stream.SendNext(_e.MainToolWeaponT(_cellIdx));
                    stream.SendNext(_e.MainTWLevelT(_cellIdx));

                    stream.SendNext(_e.ExtraToolWeaponT(_cellIdx));
                    stream.SendNext(_e.ExtraTWLevelT(_cellIdx));
                    stream.SendNext(_e.ExtraTWProtection(_cellIdx));

                    stream.SendNext(_e.HpUnit(_cellIdx));

                    stream.SendNext(_e.YoungForestC(_cellIdx).Resources);
                    stream.SendNext(_e.AdultForestC(_cellIdx).Resources);
                    stream.SendNext(_e.HillC(_cellIdx).Resources);
                    stream.SendNext(_e.MountainC(_cellIdx).Resources);

                    stream.SendNext(_e.HaveFire(_cellIdx));

                }

                else
                {
                    _e.SetUnitOnCellT(_cellIdx, (UnitTypes)stream.ReceiveNext());
                    _e.SetUnitLevelT(_cellIdx, (LevelTypes)stream.ReceiveNext());
                    _e.SetUnitPlayerT(_cellIdx, (PlayerTypes)stream.ReceiveNext());
                    _e.SetUnitConditionT(_cellIdx, (ConditionUnitTypes)stream.ReceiveNext());
                    _e.UnitMainC(_cellIdx).IsArcherDirectedToRight = (bool)stream.ReceiveNext();
                    _e.UnitMainC(_cellIdx).CooldownForAttackAnyUnitInSeconds = (int)stream.ReceiveNext();

                    _e.SetMainToolWeaponT(_cellIdx, (ToolsWeaponsWarriorTypes)stream.ReceiveNext());
                    _e.SetMainTWLevelT(_cellIdx, (LevelTypes)stream.ReceiveNext());

                    _e.SetExtraToolWeaponT(_cellIdx, (ToolsWeaponsWarriorTypes)stream.ReceiveNext());
                    _e.SetExtraTWLevelT(_cellIdx, (LevelTypes)stream.ReceiveNext());
                    _e.SetExtraTWProtection(_cellIdx, (float)stream.ReceiveNext());

                    _e.HpUnitC(_cellIdx).Health = (double)stream.ReceiveNext();



                    _e.YoungForestC(_cellIdx).Resources = (float)stream.ReceiveNext();
                    _e.AdultForestC(_cellIdx).Resources = (float)stream.ReceiveNext();
                    _e.HillC(_cellIdx).Resources = (float)stream.ReceiveNext();
                    _e.MountainC(_cellIdx).Resources = (float)stream.ReceiveNext();

                    _e.EffectE(_cellIdx).HaveFire = (bool)stream.ReceiveNext();

                    _e.NeedUpdateView = true;
                }
            }

            else if (_typeUpdate == 2)
            {
                if (stream.IsWriting)
                {
                    stream.SendNext(_e.UnitMainC(_cellIdx).Possition);
                }

                else
                {
                    _e.UnitMainC(_cellIdx).Possition = (Vector3)stream.ReceiveNext();
                }
            }

            else if (_typeUpdate == 3)
            {
                if (stream.IsWriting)
                {
                    stream.SendNext(_e.StunUnit(_cellIdx));
                    stream.SendNext(_e.ProtectionRainyMagicShield(_cellIdx));
                    stream.SendNext(_e.HaveFrozenArrawArcher(_cellIdx));
                }

                else
                {
                    _e.UnitEffectsC(_cellIdx).StunHowManyUpdatesNeedStay = (float)stream.ReceiveNext();
                    _e.UnitEffectsC(_cellIdx).ProtectionRainyMagicShield = (float)stream.ReceiveNext();
                    _e.UnitEffectsC(_cellIdx).HaveFrozenArrawArcher = (bool)stream.ReceiveNext();

                    _e.NeedUpdateView = true;
                }
            }

            else if (_typeUpdate == 4)
            {
                if (stream.IsWriting)
                {
                    for (var abilityT = (AbilityTypes)1; abilityT < AbilityTypes.End; abilityT++)
                    {
                        stream.SendNext(_e.UnitCooldownAbilitiesC(_cellIdx).Cooldown(abilityT));
                    }
                }

                else
                {
                    for (var abilityT = (AbilityTypes)1; abilityT < AbilityTypes.End; abilityT++)
                    {
                        _e.UnitCooldownAbilitiesC(_cellIdx).Set(abilityT, (int)stream.ReceiveNext());
                    }

                    _e.NeedUpdateView = true;
                }
            }

            else if (_typeUpdate == 5)
            {
                if (stream.IsWriting)
                {
                    stream.SendNext(_e.BuildingOnCellT(_cellIdx));
                    stream.SendNext(_e.BuildingLevelT(_cellIdx));
                    stream.SendNext(_e.BuildingPlayerT(_cellIdx));
                }

                else
                {
                    _e.BuildingC(_cellIdx).BuildingT = (BuildingTypes)stream.ReceiveNext();
                    _e.BuildingC(_cellIdx).LevelT = (LevelTypes)stream.ReceiveNext();
                    _e.BuildingC(_cellIdx).PlayerT = (PlayerTypes)stream.ReceiveNext();

                    _e.NeedUpdateView = true;
                }
            }

            else if (_typeUpdate == 6)
            {
                if (stream.IsWriting)
                {
                    for (var directT = (DirectTypes)1; directT < DirectTypes.End; directT++)
                    {
                        stream.SendNext(_e.HealthTrail(_cellIdx).Health(directT));
                    }
                }

                else
                {
                    for (var directT = (DirectTypes)1; directT < DirectTypes.End; directT++)
                    {
                        _e.HealthTrail(_cellIdx).Set(directT, (float)stream.ReceiveNext());
                    }

                    _e.NeedUpdateView = true;
                }
            }

            else if (_typeUpdate == 7)
            {
                if (stream.IsWriting)
                {
                    stream.SendNext(_e.WaterUnit(_cellIdx));
                }

                else
                {
                    _e.WaterUnitC(_cellIdx).Water = (double)stream.ReceiveNext();

                    _e.NeedUpdateView = true;
                }
            }

            else if (_typeUpdate == 8)
            {
                if (stream.IsWriting)
                {
                    stream.SendNext(_e.WaterOnCellC(_cellIdx).Resources);
                }

                else
                {
                    _e.WaterOnCellC(_cellIdx).Resources = (float)stream.ReceiveNext();

                    _e.NeedUpdateView = true;
                }
            }
            else if (_typeUpdate == 9)
            {
                for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
                {
                    if (stream.IsWriting)
                    {
                        stream.SendNext(_e.WhereUnitCanShiftC(_cellIdx).Can(cellIdxCurrent));
                    }
                    else
                    {
                        _e.WhereUnitCanShiftC(_cellIdx).Set(cellIdxCurrent, (bool)stream.ReceiveNext());

                        _e.NeedUpdateView = true;
                    }
                }
            }
            else if (_typeUpdate == 10)
            {
                for (var buttonT = (ButtonTypes)1; buttonT < ButtonTypes.End; buttonT++)
                {
                    if (stream.IsWriting)
                    {
                        stream.SendNext(_e.UnitButtonAbilitiesC(_cellIdx).Ability(buttonT));
                    }
                    else
                    {
                        _e.UnitButtonAbilitiesC(_cellIdx).SetAbility(buttonT, (AbilityTypes)stream.ReceiveNext());

                        _e.NeedUpdateView = true;
                    }
                }
            }
            else if (_typeUpdate == 11)
            {
                for (var buttonT = (ButtonTypes)1; buttonT < ButtonTypes.End; buttonT++)
                {
                    if (stream.IsWriting)
                    {
                        stream.SendNext(_e.EffectsUnitsRightBarsC(_cellIdx).Effect(buttonT));
                    }
                    else
                    {
                        _e.EffectsUnitsRightBarsC(_cellIdx).Set(buttonT, (EffectTypes)stream.ReceiveNext());

                        _e.NeedUpdateView = true;
                    }
                }
            }
            //else if (_typeUpdate == 12)
            //{
            //    for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            //    {
            //        if (stream.IsWriting)
            //        {
            //            stream.SendNext(_e.HowManyDistanceNeedForShiftingUnitC(_cellIdx).HowMany(cellIdxCurrent));
            //        }
            //        else
            //        {
            //            _e.HowManyDistanceNeedForShiftingUnitC(_cellIdx).Set(cellIdxCurrent, (float)stream.ReceiveNext());

            //            _e.NeedUpdateView = true;
            //        }
            //    }
            //}
            else if (_typeUpdate == 12)
            {
                for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
                {
                    if (stream.IsWriting)
                    {
                        stream.SendNext(_e.WhereUnitCanAttackSimpleAttackToEnemyC(_cellIdx).Can(cellIdxCurrent));
                    }
                    else
                    {
                        _e.WhereUnitCanAttackSimpleAttackToEnemyC(_cellIdx).Set(cellIdxCurrent, (bool)stream.ReceiveNext());

                        _e.NeedUpdateView = true;
                    }
                }
            }
            else if (_typeUpdate == 13)
            {
                for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
                {
                    if (stream.IsWriting)
                    {
                        stream.SendNext(_e.WhereUnitCanAttackUniqueAttackToEnemyC(_cellIdx).Can(cellIdxCurrent));
                    }
                    else
                    {
                        _e.WhereUnitCanAttackUniqueAttackToEnemyC(_cellIdx).Set(cellIdxCurrent, (bool)stream.ReceiveNext());

                        _e.NeedUpdateView = true;
                    }
                }
            }
            else if (_typeUpdate == 14)
            {
                for (var playerT = (PlayerTypes)1; playerT < PlayerTypes.End; playerT++)
                {
                    if (stream.IsWriting)
                    {
                        stream.SendNext(_e.HasKingEffectHereC(_cellIdx).Has(playerT));
                    }
                    else
                    {
                        _e.HasKingEffectHereC(_cellIdx).Set(playerT, (bool)stream.ReceiveNext());

                        _e.NeedUpdateView = true;
                    }
                }
            }
            else if (_typeUpdate == 15)
            {
                for (var playerT = (PlayerTypes)1; playerT < PlayerTypes.End; playerT++)
                {
                    if (stream.IsWriting)
                    {
                        stream.SendNext(_e.UnitVisibleC(_cellIdx).IsVisible(playerT));
                    }
                    else
                    {
                        _e.UnitVisibleC(_cellIdx).Set(playerT, (bool)stream.ReceiveNext());

                        _e.NeedUpdateView = true;
                    }
                }
            }
            else if (_typeUpdate == 16)
            {
                for (var playerT = (PlayerTypes)1; playerT < PlayerTypes.End; playerT++)
                {
                    if (stream.IsWriting)
                    {
                        stream.SendNext(_e.TrailVisibleC(_cellIdx).IsVisible(playerT));
                    }
                    else
                    {
                        _e.TrailVisibleC(_cellIdx).Set(playerT, (bool)stream.ReceiveNext());

                        _e.NeedUpdateView = true;
                    }
                }
            }
            else if (_typeUpdate == 17)
            {
                for (var playerT = (PlayerTypes)1; playerT < PlayerTypes.End; playerT++)
                {
                    if (stream.IsWriting)
                    {
                        stream.SendNext(_e.BuildingVisibleC(_cellIdx).IsVisible(playerT));
                    }
                    else
                    {
                        _e.BuildingVisibleC(_cellIdx).Set(playerT, (bool)stream.ReceiveNext());

                        _e.NeedUpdateView = true;
                    }
                }
            }
        }
    }
}