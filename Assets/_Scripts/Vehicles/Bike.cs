using UnityEngine;

public class Bike : Vehicle
{
    [SerializeField] protected Vector3 size;
    public Vector3 Size => size;

    public override string GetDescription()
    {
        return $"{base.GetDescription()} \nГабариты: {size.x}x{size.y}x{size.z}";
    }
}
