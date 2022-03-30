using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Databases/Vehicles", fileName = "Vehicles")]
public class VehicleDatabase : ScriptableObject
{
    [SerializeField, HideInInspector] private List<Vehicle> vehiclesList = new List<Vehicle>();
    public List<Vehicle> VehicleList => vehiclesList;

    [SerializeField] private Vehicle currentVehicle;
    public Vehicle CurrentVehicle => currentVehicle;

    private int currentIndex;
    public int CurrentIndex => currentIndex;

    public void AddElement<T>(T vehicle) where T : Vehicle
    {
        currentVehicle = vehicle;
        vehiclesList.Add(currentVehicle);
        currentIndex = vehiclesList.Count - 1;
    }

    public void RemoveElement()
    {
        if (vehiclesList.Count > 1)
        {
            vehiclesList.RemoveAt(currentIndex);

            if (currentIndex == vehiclesList.Count)
                currentIndex--;

            currentVehicle = vehiclesList[currentIndex];
        }
        else
        {
            RemoveAll();
        }
    }

    public void RemoveAll()
    {
        vehiclesList.Clear();
        currentVehicle = new Vehicle();
        vehiclesList.Add(currentVehicle);
        currentIndex = 0;
    }

    public Vehicle GetNext()
    {
        if (currentIndex < vehiclesList.Count - 1)
            currentIndex++;

        currentVehicle = vehiclesList[currentIndex];

        return currentVehicle;
    }

    public Vehicle GetPrevious()
    {
        if (currentIndex > 0)
            currentIndex--;

        currentVehicle = vehiclesList[currentIndex];

        return currentVehicle;
    }

    public void SetCurrentVehicle(Vehicle vehicle)
    {
        currentVehicle = vehicle;
        currentIndex = vehiclesList.IndexOf(vehicle);
    }

    public void ChangeVehicleType()
    {

    }
}