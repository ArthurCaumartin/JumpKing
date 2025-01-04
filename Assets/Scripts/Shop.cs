using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    List<Button> _currentButton = new List<Button>();
    [SerializeField] FloatReference piece;
    [SerializeField] Button _buttonPrefab;

    private bool CanBuy(float price)
    {
        return piece.Value > price;
    }

    public void Buy(float price)
    {
        if (!CanBuy(price))
        {
            return;
        }
    }

    public void ShowShop(List<GameObject> ItemList)
    {
        ClearButtons();
        gameObject.SetActive(true);
        if(ItemList != null)
        {
            print(ItemList.Count);
            for(int i = 0; i < ItemList.Count; i++)
            {
                _currentButton.Add(Instantiate(_buttonPrefab, transform));
                _currentButton[i].onClick.AddListener(() => print(ItemList[i].name));
            }
        }
    }

    public void HideShop()
    {
        ClearButtons();
        gameObject.SetActive(false);
    }

    public void ClearButtons()
    {
        for(int i = 0; i < _currentButton.Count; i++)
        {
            Destroy(_currentButton[i].gameObject);
        }
        _currentButton.Clear();
    }
}
