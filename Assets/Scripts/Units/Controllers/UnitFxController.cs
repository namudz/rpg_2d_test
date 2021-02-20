using UnityEngine;

public class UnitFxController : MonoBehaviour
{
    [SerializeField] private Animator _fxAnimator;
    [SerializeField] private SpriteRenderer _unitSprite;
    private IUnitHealth _unitHealth;

    public void InjectDependencies(IUnitHealth unitHealth)
    {
        _unitHealth = unitHealth;
        
        _unitHealth.OnDamaged += UpdateFillAmount;
        _unitHealth.OnDeath += PlayDeathFx;
    }

    public void ResetOnSpawn()
    {
        _unitSprite.enabled = true;
        _fxAnimator.Rebind();
    }

    public void UnsuscribeFromEvents()
    {
        _unitHealth.OnDamaged -= UpdateFillAmount;
        _unitHealth.OnDeath -= PlayDeathFx;
    }

    private void UpdateFillAmount(float hpPercentage)
    {
        _fxAnimator.SetTrigger(AnimatorParams.Instance.UNIT_FX_DAMAGED);
    }

    private void PlayDeathFx(int unitId)
    {
        _fxAnimator.SetTrigger(AnimatorParams.Instance.UNIT_FX_DEATH);
        _unitSprite.enabled = false;
    }
}