using System;

namespace Game.Game
{
    public readonly struct Systems
    {
        public Systems(ref ActionC update, ref ActionC fixedUpdate, in EntitiesModel ents, in Action updateView, in Action updateUI, out Action runAfterDoing)
        {
            update.Action +=
                (Action)
                new InputS(ents).Run
                + new RayS(ents).Run
                + new SelectorS(ents, updateView, updateUI).Run;


            ents.AttackShieldE = new AttackShieldE(new AttackShieldS(ents).Run);
            ents.UnitAttackE = new UnitAttackUnitE(new UnitAttackUnitS(ents).Run);


            runAfterDoing =
                (Action)
                new GetCurentPlayerS(ents).Run
                + new CityBuildingGetCellsS(ents).Run
                + new SetIdxsBuildingsS(ents).Run
                + new VisibElseS(ents).Run
                + new AbilitySyncS(ents).Run
                + new GetDamageUnitsS(ents).Run
                + new GetUnitTypeS(ents).Run
                + new DeleteTrailsS(ents).Run

                + new PawnExtractAdultForestGetCellsS(ents).Run
                + new PawnExtractHillS(ents).Run
                + new WoodcutterExtractGetCellsS(ents).Run
                + new FarmExtractGetCellsS(ents).Run

                + new GetCellsForSetUnitS(ents).Run
                + new GetCellsForShiftUnitS(ents).Run
                + new GetCellsForArsonArcherS(ents).Run

                + new GetAttackMeleeCellsS(ents).Run
                + new GetCellsForAttackArcherS(ents).Run;


            new SystemsMaster(ents);
            new SystemsOther(ents);
        }
    }
}