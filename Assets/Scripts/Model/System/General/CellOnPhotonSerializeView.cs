using Chessy.Model.Entity;
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
                            //stream.SendNext(_unitCs[_cellIdx));
                            //stream.SendNext(_e.UnitLevelT(_cellIdx));
                            //stream.SendNext(_unitCs[_cellIdx));
                            //stream.SendNext(_unitCs[_cellIdx));
                            //stream.SendNext(_e.IsRightArcherUnit(_cellIdx));
                            //stream.SendNext(_unitCs[_cellIdx).CooldownForAttackAnyUnitInSeconds);


                            //stream.SendNext(_mainTWC[_cellIdx));
                            //stream.SendNext(_mainTWC[_cellIdx));

                            //stream.SendNext(_extraTWC[_cellIdx));
                            //stream.SendNext(_extraTWC[_cellIdx));
                            //stream.SendNext(_e.ExtraTWProtection(_cellIdx));

                            //stream.SendNext(_e.HpUnit(_cellIdx));

                            //stream.SendNext(_e.YoungForestC(_cellIdx).Resources);
                            //stream.SendNext(_environmentCs[_cellIdx).Resources);
                            //stream.SendNext(_environmentCs[_cellIdx).Resources);
                            //stream.SendNext(_e.MountainC(_cellIdx).Resources);

                            //stream.SendNext(_fireCs[_cellIdx));


                            //stream.SendNext(_e.ProtectionRainyMagicShield(_cellIdx));
                            //stream.SendNext(_e.HaveFrozenArrawArcher(_cellIdx));

                            //stream.SendNext(_buildingCs[_cellIdx));
                            //stream.SendNext(_e.BuildingLevelT(_cellIdx));
                            //stream.SendNext(_e.BuildingPlayerT(_cellIdx));

                            //for (var buttonT = (ButtonTypes)1; buttonT < ButtonTypes.End; buttonT++)
                            //{
                            //    stream.SendNext(_e.UnitButtonAbilitiesC(_cellIdx).Ability(buttonT));
                            //    stream.SendNext(_effectsUnitsRightBarsCs[_cellIdx).Effect(buttonT));
                            //}

                            //for (var playerT = (PlayerTypes)1; playerT < PlayerTypes.End; playerT++)
                            //{
                            //    stream.SendNext(_e.HasKingEffectHereC(_cellIdx).Has(playerT));
                            //    stream.SendNext(_unitVisibleCs[_cellIdx).IsVisible(playerT));
                            //    stream.SendNext(_e.TrailVisibleC(_cellIdx).IsVisible(playerT));
                            //    stream.SendNext(_visibleBuildingCs[_cellIdx).IsVisible(playerT));
                            //}

                            //stream.SendNext(_e.DamageSimpleAttack(_cellIdx));
                            //stream.SendNext(_e.DamageOnCell(_cellIdx));

                            //stream.SendNext(_unitWhereViewDataCs[_cellIdx).ViewIdxCell);
                            //stream.SendNext(_unitWhereViewDataCs[_cellIdx).DataIdxCell);

                            //for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
                            //{
                            //    stream.SendNext(_e.WhereUnitCanAttackSimpleAttackToEnemyC(_cellIdx).Can(cellIdxCurrent));
                            //    stream.SendNext(_e.WhereUnitCanAttackUniqueAttackToEnemyC(_cellIdx).Can(cellIdxCurrent));

                            //    stream.SendNext(_e.WhereUnitCanShiftC(_cellIdx).CanShiftHere(cellIdxCurrent));

                            //    stream.SendNext(_e.WhereUnitCanFireAdultForestC(_cellIdx).Can(cellIdxCurrent));
                            //}
                        }

                        else
                        {
                            //_e.SetUnitOnCellT(_cellIdx, (UnitTypes)stream.ReceiveNext());
                            //_e.SetUnitLevelT(_cellIdx, (LevelTypes)stream.ReceiveNext());
                            //_e.SetUnitPlayerT(_cellIdx, (PlayerTypes)stream.ReceiveNext());
                            //_e.SetUnitConditionT(_cellIdx, (ConditionUnitTypes)stream.ReceiveNext());
                            //_unitCs[_cellIdx).IsArcherDirectedToRight = (bool)stream.ReceiveNext();
                            //_unitCs[_cellIdx).CooldownForAttackAnyUnitInSeconds = (int)stream.ReceiveNext();

                            //_e.SetMainToolWeaponT(_cellIdx, (ToolsWeaponsWarriorTypes)stream.ReceiveNext());
                            //_e.SetMainTWLevelT(_cellIdx, (LevelTypes)stream.ReceiveNext());

                            //_e.SetExtraToolWeaponT(_cellIdx, (ToolsWeaponsWarriorTypes)stream.ReceiveNext());
                            //_e.SetExtraTWLevelT(_cellIdx, (LevelTypes)stream.ReceiveNext());
                            //_e.SetExtraTWProtection(_cellIdx, (float)stream.ReceiveNext());

                            //_hpUnitCs[_cellIdx).Health = (double)stream.ReceiveNext();



                            //_e.YoungForestC(_cellIdx).Resources = (float)stream.ReceiveNext();
                            //_environmentCs[_cellIdx).Resources = (float)stream.ReceiveNext();
                            //_environmentCs[_cellIdx).Resources = (float)stream.ReceiveNext();
                            //_e.MountainC(_cellIdx).Resources = (float)stream.ReceiveNext();

                            //_e.EffectE(_cellIdx).HaveFire = (bool)stream.ReceiveNext();


                            //_effectsUnitCs[_cellIdx).ProtectionRainyMagicShield = (float)stream.ReceiveNext();
                            //_effectsUnitCs[_cellIdx).HaveFrozenArrawArcher = (bool)stream.ReceiveNext();

                            //_e.BuildingC(_cellIdx).BuildingT = (BuildingTypes)stream.ReceiveNext();
                            //_e.BuildingC(_cellIdx).LevelT = (LevelTypes)stream.ReceiveNext();
                            //_e.BuildingC(_cellIdx).PlayerT = (PlayerTypes)stream.ReceiveNext();


                            //for (var buttonT = (ButtonTypes)1; buttonT < ButtonTypes.End; buttonT++)
                            //{
                            //    _e.UnitButtonAbilitiesC(_cellIdx).SetAbility(buttonT, (AbilityTypes)stream.ReceiveNext());
                            //    _effectsUnitsRightBarsCs[_cellIdx).Set(buttonT, (EffectTypes)stream.ReceiveNext());
                            //}

                            //for (var playerT = (PlayerTypes)1; playerT < PlayerTypes.End; playerT++)
                            //{
                            //    _e.HasKingEffectHereC(_cellIdx).Set(playerT, (bool)stream.ReceiveNext());
                            //    _unitVisibleCs[_cellIdx).Set(playerT, (bool)stream.ReceiveNext());
                            //    _e.TrailVisibleC(_cellIdx).Set(playerT, (bool)stream.ReceiveNext());
                            //    _visibleBuildingCs[_cellIdx).Set(playerT, (bool)stream.ReceiveNext());
                            //}

                            //_unitCs[_cellIdx).DamageSimpleAttack = (double)stream.ReceiveNext();
                            //_unitCs[_cellIdx).DamageOnCell = (double)stream.ReceiveNext();

                            //_unitWhereViewDataCs[_cellIdx).ViewIdxCell = (byte)stream.ReceiveNext();
                            //_unitWhereViewDataCs[_cellIdx).DataIdxCell = (byte)stream.ReceiveNext();


                            //for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
                            //{


                            //    _e.WhereUnitCanAttackSimpleAttackToEnemyC(_cellIdx).Set(cellIdxCurrent, (bool)stream.ReceiveNext());
                            //    _e.WhereUnitCanAttackUniqueAttackToEnemyC(_cellIdx).Set(cellIdxCurrent, (bool)stream.ReceiveNext());

                            //    _e.WhereUnitCanShiftC(_cellIdx).Set(cellIdxCurrent, (bool)stream.ReceiveNext());

                            //    _e.WhereUnitCanFireAdultForestC(_cellIdx).Can(cellIdxCurrent) = (bool)stream.ReceiveNext();
                            //}

                            //_updateAllViewC.NeedUpdateView = true;
                        }
                    }
                    break;

                case SyncCellTypes.EveryTime:
                    {
                        if (stream.IsWriting)
                        {
                            //stream.SendNext(_e.CloudPossitionC(_cellIdx).Position);
                            //stream.SendNext(_e.CloudC(_cellIdx).IsCenter);
                            //stream.SendNext(_e.CloudWhereViewDataOnCellC(_cellIdx).DataIdxCell);
                            //stream.SendNext(_e.CloudWhereViewDataOnCellC(_cellIdx).ViewIdxCell);

                            //stream.SendNext(_e.UnitPossitionOnCell(_cellIdx));

                            //stream.SendNext(_e.WaterUnit(_cellIdx));
                            //stream.SendNext(_environmentCs[_cellIdx).Resources);

                            //for (var directT = (DirectTypes)1; directT < DirectTypes.End; directT++)
                            //{
                            //    stream.SendNext(_hpTrailCs[_cellIdx).Health(directT));
                            //}

                            //stream.SendNext(_e.StunUnit(_cellIdx));

                            //for (var abilityT = (AbilityTypes)1; abilityT < AbilityTypes.End; abilityT++)
                            //{
                            //    stream.SendNext(_cooldownAbilityCs[_cellIdx).Cooldown(abilityT));
                            //}
                        }
                        else
                        {
                            //_e.CloudPossitionC(_cellIdx).Position = (Vector3)stream.ReceiveNext();
                            //_e.CloudC(_cellIdx).IsCenter = (bool)stream.ReceiveNext();
                            //_e.CloudWhereViewDataOnCellC(_cellIdx).DataIdxCell = (byte)stream.ReceiveNext();
                            //_e.CloudWhereViewDataOnCellC(_cellIdx).ViewIdxCell = (byte)stream.ReceiveNext();

                            //_e.UnitPossitionOnCellC(_cellIdx).Position = (Vector3)stream.ReceiveNext();

                            //_unitWaterCs[_cellIdx).Water = (double)stream.ReceiveNext();
                            //_environmentCs[_cellIdx).Resources = (float)stream.ReceiveNext();

                            //for (var directT = (DirectTypes)1; directT < DirectTypes.End; directT++)
                            //{
                            //    _hpTrailCs[_cellIdx).Set(directT, (float)stream.ReceiveNext());
                            //}

                            //_effectsUnitCs[_cellIdx).StunHowManyUpdatesNeedStay = (float)stream.ReceiveNext();

                            //for (var abilityT = (AbilityTypes)1; abilityT < AbilityTypes.End; abilityT++)
                            //{
                            //    _cooldownAbilityCs[_cellIdx).Set(abilityT, (int)stream.ReceiveNext());
                            //}

                            //_updateAllViewC.NeedUpdateView = true;
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
        EveryTime,

        End,
    }
}