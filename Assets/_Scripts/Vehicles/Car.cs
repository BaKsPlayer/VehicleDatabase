using UnityEngine;

public class Car : Vehicle
{
    [SerializeField] protected string color;
    public string Color => color;

    public override string GetDescription()
    {
        return $"{base.GetDescription()} \nЦвет: {Color}";
    }
}
