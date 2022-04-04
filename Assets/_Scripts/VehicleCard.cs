using UnityEngine;
using UnityEngine.UI;

public class VehicleCard : MonoBehaviour
{
    [SerializeField] private Text _titleText;
    [SerializeField] private Image _iconImage;
    [SerializeField] private Text _descriptionText;

    public void Initialize(Vehicle vehicle)
    {
        _titleText.text = vehicle.Name;
        _iconImage.sprite = vehicle.Icon;
        _descriptionText.text = vehicle.GetDescription();
    }
}
