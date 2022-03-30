using UnityEngine;

public class Ship : Vehicle
{
    [SerializeField] protected float displacement;
    public float Displacement => displacement;

    public override string GetDescription()
    {
        return $"{base.GetDescription()} \nВодоизмещение: {Displacement}";
    }
}
