using UnityEngine;

[System.Serializable]
public class Vehicle
{
    [SerializeField] protected Sprite icon;
    public Sprite Icon => icon;

    [SerializeField] protected string vehicleName = "newVehicle";
    public string VehicleName => vehicleName;

    [SerializeField] protected float weight;
    public float Weight => weight;

    [SerializeField] protected int capacity;
    public int Capacity => capacity;

    [SerializeField] protected float maxSpeed;
    public float MaxSpeed => maxSpeed;

    public virtual string GetDescription()
    {
        return $"Масса: {Weight}\nВместимость: {Capacity}\nМакс. скорость: {MaxSpeed}";
    }
}
