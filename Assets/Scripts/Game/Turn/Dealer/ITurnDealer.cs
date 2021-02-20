using System.Collections.Generic;

public interface ITurnDealer
{
    event System.Action<TurnTypes.Turn> OnTurnChanged;
    void SetCurrentTurn(TurnTypes.Turn newTurn);
    void SetCharacterUnits(List<AUnitController> unitsControllers);
    void SetEnemyUnits(List<AUnitController> unitsControllers);
}