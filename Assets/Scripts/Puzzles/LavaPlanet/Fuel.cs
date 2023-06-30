using UnityEngine;


public class Fuel : MonoBehaviour
{
    [SerializeField]
    float burningTime;
    public float BurningTime => burningTime;

    public bool IsInfinite => BurningTime < 0;

}
