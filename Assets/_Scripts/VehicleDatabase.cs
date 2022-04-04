using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Databases/Vehicles", fileName = "Vehicles")]
public class VehicleDatabase : ScriptableObject
{
    [SerializeField] private List<Vehicle> _vehiclesList = new List<Vehicle>();
    public List<Vehicle> VehicleList => _vehiclesList;

    [SerializeField] private Vehicle _currentVehicle;
    public Vehicle CurrentVehicle => _currentVehicle;

    private int _currentIndex;
    public int CurrentIndex => _currentIndex;

    public void AddElement<T>() where T : Vehicle
    {
        T newAsset = CreateAsset<T>();

        _currentVehicle = newAsset;
        _vehiclesList.Add(_currentVehicle);
        _currentIndex = _vehiclesList.Count - 1;
    }

    public void RemoveElement()
    {
        DeleteAsset(_currentVehicle);
        _vehiclesList.RemoveAt(_currentIndex);

        if (_vehiclesList.Count >= 1)
        {
            if (_currentIndex == _vehiclesList.Count)
                _currentIndex--;

            _currentVehicle = _vehiclesList[_currentIndex];
        }
        else
        {
            AddElement<Vehicle>();
        }
    }

    public void RemoveAll()
    {
        int count = _vehiclesList.Count;
        for (int i = 0; i < count; i++)
            RemoveElement();
    }

    public Vehicle GetNext()
    {
        if (_currentIndex < _vehiclesList.Count - 1)
            _currentIndex++;

        _currentVehicle = _vehiclesList[_currentIndex];

        return _currentVehicle;
    }

    public Vehicle GetPrevious()
    {
        if (_currentIndex > 0)
            _currentIndex--;

        _currentVehicle = _vehiclesList[_currentIndex];

        return _currentVehicle;
    }

    public void SelectCurrentVehicle(Vehicle vehicle)
    {
        _currentVehicle = vehicle;
        _currentIndex = _vehiclesList.IndexOf(vehicle);
    }

    public void ConvertCurrentVehicleTo(Type type)
    {
        if (type == typeof(Airplane))
        {
            ConvertCurrentVehicleTo<Airplane>();
            return;
        }

        if (type == typeof(Bike))
        {
            ConvertCurrentVehicleTo<Bike>();
            return;
        }

        if (type == typeof(Car))
        {
            ConvertCurrentVehicleTo<Car>();
            return;
        }

        if (type == typeof(Ship))
        {
            ConvertCurrentVehicleTo<Ship>();
            return;
        }

        throw new NotImplementedException();
    }

    private void ConvertCurrentVehicleTo<T>() where T: Vehicle
    {
        T newAsset = CreateAsset<T>();
        newAsset.CopyInfoFrom(_currentVehicle);
        DeleteAsset(_currentVehicle);

        _currentVehicle = newAsset;
        _vehiclesList[_currentIndex] = _currentVehicle;
    }

    private void DeleteAsset<T>(T asset) where T: Vehicle
    {
        AssetDatabase.RemoveObjectFromAsset(asset);
        AssetDatabase.SaveAssets();
    }

    private T CreateAsset<T>() where T: Vehicle
    {
        T asset = CreateInstance<T>();

        if (!AssetDatabase.Contains(this))
            return null;

        asset.name = $"new{asset.GetType()}";

        AssetDatabase.AddObjectToAsset(asset, this);
        AssetDatabase.SaveAssets();

        return asset;
    }
}