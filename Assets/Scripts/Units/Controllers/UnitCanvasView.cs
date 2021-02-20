using UnityEngine;
using UnityEngine.UI;

public class UnitCanvasView : MonoBehaviour
{
    [SerializeField] private Canvas _myCanvas;
    [SerializeField] private Image _barFill;
    private IUnitHealth _unitHealth;

    public void InjectDependencies(IUnitHealth unitHealth)
    {
        _unitHealth = unitHealth;

        _unitHealth.OnDamaged += UpdateFillAmount;
        _unitHealth.OnDeath += DisableCanvasOnDeath;
    }

    public void ResetOnSpawn()
    {
        UpdateFillAmount(1f);
        _myCanvas.enabled = true;
    }

    public void UnsuscribeFromEvents()
    {
        _unitHealth.OnDamaged -= UpdateFillAmount;
        _unitHealth.OnDeath -= DisableCanvasOnDeath;
    }

    private void UpdateFillAmount(float hpPercentage)
    {
        _barFill.fillAmount = hpPercentage;
    }

    private void DisableCanvasOnDeath(int unitId)
    {
        _myCanvas.enabled = false;
    }
}