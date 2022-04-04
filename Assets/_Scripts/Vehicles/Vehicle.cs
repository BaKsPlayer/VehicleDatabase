using System.IO;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class Vehicle : ScriptableObject
{
    [SerializeField] protected Sprite _icon;
    public Sprite Icon => _icon;

    [SerializeField] protected string _name = "new";
    public string Name => _name;

    [SerializeField] protected float _weight;
    public float Weight => _weight;

    [SerializeField] protected int _capacity;
    public int Capacity => _capacity;

    [SerializeField] protected float _maxSpeed;
    public float MaxSpeed => _maxSpeed;

    public virtual string GetDescription()
    {
        return $"Масса: {Weight}\nВместимость: {Capacity}\nМакс. скорость: {MaxSpeed}";
    }

    public void CopyInfoFrom<T>(T vehicle) where T: Vehicle
    {
        _icon = vehicle.Icon;
        _name = vehicle.Name;
        _weight = vehicle.Weight;
        _capacity = vehicle.Capacity;
        _maxSpeed = vehicle.MaxSpeed;
    }
}
