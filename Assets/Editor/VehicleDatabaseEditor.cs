using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(VehicleDatabase))]
public class VehicleDatabaseEditor : Editor
{
    private VehicleDatabase database;

    private int _currentPage = 1;
    private int _totalPagesCount => Mathf.CeilToInt((float)database.VehicleList.Count / 10);

    private int _visibleObjectsNumber = 10;

    private List<Type> _vehicleTypes = new List<Type>();
    private int _selectedTypeIndex;

    private void Awake()
    {
        database = (VehicleDatabase)target;

        Type baseType = typeof(Vehicle);
        _vehicleTypes = Assembly.GetAssembly(baseType).GetTypes().Where(type => type.IsSubclassOf(baseType)).ToList();

        database.VehicleList.RemoveAll(vehicle => vehicle == null);

        if (database.VehicleList.Count == 0)
            database.AddElement<Vehicle>();

        if (database.CurrentVehicle == null)
            database.SelectCurrentVehicle(database.VehicleList[0]);
    }

    public override void OnInspectorGUI()
    {
        if (database.CurrentVehicle == null)
            return;

        _selectedTypeIndex = GetCurrentTypeIndex();

        DrawCurrentVehicle();
        GUILayout.Space(8);

        DrawVehicleSwitches();

        GUILayout.Space(16);

        DrawVehiclesList();

        GUILayout.Space(8);

        DrawVehiclesListSwitches();
    }

    private void DrawVehiclesListSwitches()
    {
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

    private void DrawVehiclesList()
    {
        GUILayout.Label("Vehicle List:");

        var vehicles = database.VehicleList
            .Skip(_visibleObjectsNumber * (_currentPage - 1))
            .Take(_visibleObjectsNumber);

        foreach (var vehicle in vehicles)
            if (GUILayout.Button($"{vehicle.Name} ({vehicle.GetType()})"))
                database.SelectCurrentVehicle(vehicle);
    }

    private void DrawVehicleSwitches()
    {
        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Add"))
        {
            database.AddElement<Vehicle>();
            _selectedTypeIndex = 0;
            _currentPage = _totalPagesCount;
        }

        if (GUILayout.Button("Remove"))
            database.RemoveElement();

        if (GUILayout.Button("<="))
            database.GetPrevious();

        if (GUILayout.Button("=>"))
            database.GetNext();

        GUILayout.EndHorizontal();
    }

    private void DrawCurrentVehicle()
    {
        EditorGUILayout.LabelField($"Current Vehicle: ({database.CurrentIndex + 1}/{database.VehicleList.Count})");

        var vehiclesStringArray = _vehicleTypes.Select(type => type.ToString()).ToArray();
        if (vehiclesStringArray.Length > 0)
        {
            _selectedTypeIndex = EditorGUILayout.Popup("VehicleType:", _selectedTypeIndex, vehiclesStringArray);
            CheckVehicleType();
        }

        var fieldsToDraw = GetCurrentVehicleFields();

        SerializedObject serializedCurrentVehicle = new SerializedObject(database.CurrentVehicle);
        serializedCurrentVehicle.Update();

        foreach (var field in fieldsToDraw)
            EditorGUILayout.PropertyField(serializedCurrentVehicle.FindProperty(field.Name));

        serializedCurrentVehicle.ApplyModifiedProperties();

        database.CurrentVehicle.name = $"{database.CurrentVehicle.Name}";
    }

    private IEnumerable<FieldInfo> GetCurrentVehicleFields()
    {
        var vehicleFields = typeof(Vehicle).GetFields(BindingFlags.Instance | BindingFlags.NonPublic);

        var currentVehicleFields = database.CurrentVehicle.GetType()
            .GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
            .SkipLast(vehicleFields.Count());

        return vehicleFields.Concat(currentVehicleFields);
    }

    private void CheckVehicleType()
    {
        if (database.CurrentVehicle.GetType() != _vehicleTypes[_selectedTypeIndex])
            database.ConvertCurrentVehicleTo(_vehicleTypes[_selectedTypeIndex]);
    }

    private int GetCurrentTypeIndex()
    {
        return Mathf.Clamp(_vehicleTypes.IndexOf(database.CurrentVehicle.GetType()), 0, int.MaxValue);
    }

}
