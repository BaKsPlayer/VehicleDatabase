using UnityEngine;

[System.Serializable]
public class Car : Vehicle
{
    [SerializeField] protected string _color;
    public string Color => _color;

    public override string GetDescription()
    {
        return $"{base.GetDescription()} \nЦвет: {Color}";
    }
}
