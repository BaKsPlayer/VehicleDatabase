using UnityEngine;

[System.Serializable]
public class Bike : Vehicle
{
    [SerializeField] protected Vector3 _size;
    public Vector3 Size => _size;

    public override string GetDescription()
    {
        return $"{base.GetDescription()} \nГабариты: {_size.x}x{_size.y}x{_size.z}";
    }
}
