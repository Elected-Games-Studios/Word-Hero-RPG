using UnityEngine;

public class UnlockRegions : MonoBehaviour
{
    public void Unlock()
    {
        PlayerStats.Instance.regionProgress = 4;
    }
}
