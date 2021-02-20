using UnityEngine;

public class AnimatorParams
{
    private static AnimatorParams _instance;
    public static AnimatorParams Instance 
    {
        get
        {
            if (_instance == null)
            {
                _instance = new AnimatorParams();
            }
            return _instance;
        }
    }

    public readonly int UNIT_FX_DAMAGED;
    public readonly int UNIT_FX_DEATH;

    public AnimatorParams()
    {
        UNIT_FX_DAMAGED = Animator.StringToHash("Damaged");
        UNIT_FX_DEATH = Animator.StringToHash("Death");
    }
}