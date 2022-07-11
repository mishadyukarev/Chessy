using Chessy.Model.Entity;
using Chessy.Model.Values;
using Photon.Pun;
using System;
using UnityEngine;

namespace Chessy.Model.System
{
    public sealed class CellOnPhotonSerializeView : MonoBehaviour, IPunObservable
    {
        EntitiesModel _e;
        byte _cellIdx;
        SyncCellTypes _typeUpdate;

        public void GiveData(in byte cellIdx, in SyncCellTypes syncCellT, in EntitiesModel eM)
        {
            _cellIdx = cellIdx;
            _e = eM;
            _typeUpdate = syncCellT;
        }


        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            switch (_typeUpdate)
            {
                case SyncCellTypes.Main:
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
                    break;

                case SyncCellTypes.EffectsUnit:
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
                    break;

                case SyncCellTypes.CooldownsUnit:
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
                    break;

                case SyncCellTypes.Building:
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
                    break;

                case SyncCellTypes.TrailHealth:
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
                    break;

                case SyncCellTypes.WaterUnit:
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
                    break;

                case SyncCellTypes.WaterOnCell:
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
                    break;

                case SyncCellTypes.WhereUnitCanShift:
                    {
                        for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
                        {
                            if (stream.IsWriting)
                            {
                                stream.SendNext(_e.WhereUnitCanShiftC(_cellIdx).CanShiftHere(cellIdxCurrent));
                            }
                            else
                            {
                                _e.WhereUnitCanShiftC(_cellIdx).Set(cellIdxCurrent, (bool)stream.ReceiveNext());

                                _e.NeedUpdateView = true;
                            }
                        }
                    }
                    break;

                case SyncCellTypes.UnitButtonAbilities:
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
                    break;

                case SyncCellTypes.EffectsBarsUnitsRight:
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
                    break;

                case SyncCellTypes.WhereUnitCanAttackSimple:
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
                    break;

                case SyncCellTypes.WhereUnitCanAttackUnique:
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
                    break;

                case SyncCellTypes.HasKingEffectHere:
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
                    break;

                case SyncCellTypes.UnitVisible:
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
                    break;

                case SyncCellTypes.TrailVisible:
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
                    break;

                case SyncCellTypes.BuildingVisible:
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
                    break;

                case SyncCellTypes.SkinUnit:
                    {
                        for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
                        {
                            if (stream.IsWriting)
                            {
                                stream.SendNext(_e.SkinInfoUnitC(_cellIdx).ViewIdxCell);
                                stream.SendNext(_e.SkinInfoUnitC(_cellIdx).DataIdxCell);
                            }
                            else
                            {
                                _e.SkinInfoUnitC(_cellIdx).ViewIdxCell = (byte)stream.ReceiveNext();
                                _e.SkinInfoUnitC(_cellIdx).DataIdxCell = (byte)stream.ReceiveNext();

                                _e.NeedUpdateView = true;
                            }
                        }
                    }
                    break;

                case SyncCellTypes.DamageUnit:
                    {

                        if (stream.IsWriting)
                        {
                            stream.SendNext(_e.DamageSimpleAttack(_cellIdx));
                            stream.SendNext(_e.DamageOnCell(_cellIdx));
                        }
                        else
                        {
                            _e.UnitMainC(_cellIdx).DamageSimpleAttack = (double)stream.ReceiveNext();
                            _e.UnitMainC(_cellIdx).DamageOnCell = (double)stream.ReceiveNext();

                            _e.NeedUpdateView = true;
                        }

                    }
                    break;

                case SyncCellTypes.WhereArcherCanBurnForest:
                    {
                        for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
                        {
                            if (stream.IsWriting)
                            {
                                stream.SendNext(_e.WhereUnitCanFireAdultForestC(_cellIdx).Can(cellIdxCurrent));
                            }
                            else
                            {
                                _e.WhereUnitCanFireAdultForestC(_cellIdx).Can(cellIdxCurrent) = (bool)stream.ReceiveNext();

                                _e.NeedUpdateView = true;
                            }
                        }
                    }
                    break;

                case SyncCellTypes.EveryTime:
                    {
                        if (stream.IsWriting)
                        {
                            stream.SendNext(_e.CloudPossitionC(_cellIdx).Position);
                            stream.SendNext(_e.CloudC(_cellIdx).IsCenter);
                            stream.SendNext(_e.CloudWhereViewDataOnCellC(_cellIdx).DataIdxCell);
                            stream.SendNext(_e.CloudWhereViewDataOnCellC(_cellIdx).ViewIdxCell);

                            stream.SendNext(_e.UnitPossitionOnCell(_cellIdx));
                        }
                        else
                        {
                            _e.CloudPossitionC(_cellIdx).Position = (Vector3)stream.ReceiveNext();
                            _e.CloudC(_cellIdx).IsCenter = (bool)stream.ReceiveNext();
                            _e.CloudWhereViewDataOnCellC(_cellIdx).DataIdxCell = (byte)stream.ReceiveNext();
                            _e.CloudWhereViewDataOnCellC(_cellIdx).ViewIdxCell = (byte)stream.ReceiveNext();

                            _e.UnitPossitionOnCellC(_cellIdx).Position = (Vector3)stream.ReceiveNext();

                            _e.NeedUpdateView = true;
                        }
                    }
                    break;

                default: throw new Exception();
            }
        }
    
    }

    public enum SyncCellTypes
    {
        None,

        Main,
        EffectsUnit,
        CooldownsUnit,
        Building,
        TrailHealth,
        WaterUnit,
        WaterOnCell,
        WhereUnitCanShift,
        UnitButtonAbilities,
        EffectsBarsUnitsRight,
        WhereUnitCanAttackSimple,
        WhereUnitCanAttackUnique,
        HasKingEffectHere,
        UnitVisible,
        TrailVisible,
        BuildingVisible,
        SkinUnit,
        DamageUnit,
        WhereArcherCanBurnForest,
        EveryTime,

        End,
    }
}