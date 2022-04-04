using UnityEngine;

[System.Serializable]
public class Ship : Vehicle
{

    [SerializeField] protected float _displacement;
    public float Displacement => _displacement;

    public override string GetDescription()
    {
        return $"{base.GetDescription()} \nВодоизмещение: {Displacement}";
    }
}
