using UnityEngine;

[System.Serializable]
public class Airplane : Vehicle
{
    [SerializeField] protected float _liftingForce;
    public float LiftingForce => _liftingForce;

    public override string GetDescription()
    {
        return $"{base.GetDescription()} \nПодъемная сила: {LiftingForce}";
    }
}
