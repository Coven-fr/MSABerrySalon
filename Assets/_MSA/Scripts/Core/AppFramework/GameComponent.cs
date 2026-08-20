using UnityEngine;

public abstract class GameComponent : MonoBehaviour, ISwitchable
{
    protected bool isActive = false;

    public void Activate()
    {
        isActive = true;
    }

    public void Deactivate()
    {
        isActive = false;
    }
}
