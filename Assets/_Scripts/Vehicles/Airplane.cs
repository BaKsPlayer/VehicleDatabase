using UnityEngine;

public class Airplane : Vehicle
{
    [SerializeField] protected float liftingForce;
    public float LiftingForce => liftingForce;

    public override string GetDescription()
    {
        return $"{base.GetDescription()} \nПодъемная сила: {LiftingForce}";
    }
}
