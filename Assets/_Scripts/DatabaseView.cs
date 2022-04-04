using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class DatabaseView : MonoBehaviour
{
    [SerializeField] private RectTransform _contentTransform;
    [SerializeField] private VehicleDatabase _database;

    [SerializeField] private GameObject _vehicleCard;

    private HorizontalLayoutGroup _contentHorizontalGroup;
    private List<VehicleCard> _cardsPool = new List<VehicleCard>();

    private void Awake()
    {
        _contentHorizontalGroup = _contentTransform.GetComponent<HorizontalLayoutGroup>();

        for (int i = 0; i < _contentTransform.childCount; i++)
        {
            var vehicleCard = _contentTransform.GetChild(i).GetComponent<VehicleCard>();
            _cardsPool.Add(vehicleCard);
        }

        Open();
    }

    public void Open()
    {
        gameObject.SetActive(true);
        Initialize();
    }

    public void Close()
    {
        foreach (var card in _cardsPool)
        {
            card.gameObject.SetActive(false);
        }
        gameObject.SetActive(false);
        _contentTransform.localPosition = new Vector2(0, _contentTransform.localPosition.y);
    }

    private void Initialize()
    {
        float contentWidth = _contentHorizontalGroup.padding.left + _contentHorizontalGroup.padding.right;
        int contentChildsCount = _contentTransform.childCount;

        for (int i = 0; i < _database.VehicleList.Count; i++)
        {
            if (i < contentChildsCount)
            {
                _cardsPool[i].gameObject.SetActive(true);
            }
            else
            {
               var newVehicleCard = Instantiate(_vehicleCard, _contentTransform).GetComponent<VehicleCard>();
                _cardsPool.Add(newVehicleCard);
            }

            RectTransform cardTransform = _cardsPool[i].GetComponent<RectTransform>();
            contentWidth += cardTransform.sizeDelta.x  + _contentHorizontalGroup.spacing;
            _cardsPool[i].Initialize(_database.VehicleList[i]);
        }

        _contentTransform.sizeDelta = new Vector2(contentWidth, _contentTransform.sizeDelta.y);
    }
}
