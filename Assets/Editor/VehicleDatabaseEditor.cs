using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(VehicleDatabase))]
public class VehicleDatabaseEditor : Editor
{
    private VehicleDatabase database;

    private enum VehicleType
    {
        Airplane, Bike, Car, Ship
    }

    private VehicleType vehicleType;
    private VehicleType prevVehicleType;

    private int _currentPage = 1;
    private int _totalPagesCount => Mathf.CeilToInt((float)database.VehicleList.Count / 10);

    private int _visibleObjectsNumber = 10;

    private void Awake()
    {
        database = (VehicleDatabase)target;
    }

    public override void OnInspectorGUI()
    {
        PrintCurrentVehicle();
        GUILayout.Space(20);

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Add"))
        {
            database.AddElement(new Vehicle());
            _currentPage = _totalPagesCount;
        }

        if (GUILayout.Button("Remove"))
        {
            database.RemoveElement();
        }

        if (GUILayout.Button("<="))
        {
            database.GetPrevious();
        }

        if (GUILayout.Button("=>"))
        {
            database.GetNext();
        }

        GUILayout.EndHorizontal();

        GUILayout.Space(20);
        GUILayout.Label("Vehicle List:");

        var vehicles = database.VehicleList
            .Skip(_visibleObjectsNumber * (_currentPage - 1))
            .Take(_visibleObjectsNumber);
        foreach (var vehicle in vehicles)
        {
            if (GUILayout.Button(vehicle.VehicleName))
                database.SetCurrentVehicle(vehicle);
        }

        GUILayout.Space(5);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("<=="))
        {
            if (_currentPage > 1)
                _currentPage--;
        }

        GUILayout.Space(45);
        GUILayout.Label($"Page({_currentPage}/{_totalPagesCount})");

        if (GUILayout.Button("==>"))
        {
            if (_currentPage < _totalPagesCount)
                _currentPage++;
        }
        GUILayout.EndHorizontal();

        if (GUILayout.Button("RemoveAll"))
        {
            database.RemoveAll();
            _currentPage = 1;
        }
    }

    private void PrintCurrentVehicle()
    {
        EditorGUILayout.LabelField($"Current Vehicle: ({database.CurrentIndex + 1}/{database.VehicleList.Count})");

        GUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("VehicleType:");
        vehicleType = (VehicleType) EditorGUILayout.EnumPopup(vehicleType);
        GUILayout.EndHorizontal();
        ChangeVehicleType();

        serializedObject.Update();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("currentVehicle.icon"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("currentVehicle.vehicleName"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("currentVehicle.weight"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("currentVehicle.capacity"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("currentVehicle.maxSpeed"));
        serializedObject.ApplyModifiedProperties();
    }

    private void ChangeVehicleType()
    {
        if (prevVehicleType != vehicleType)
        {
            prevVehicleType = vehicleType;
            Debug.Log("vehicleType changed: " + vehicleType);

            switch (vehicleType)
            {
                case VehicleType.Bike:

                    break;
            }
        }
    }

}
