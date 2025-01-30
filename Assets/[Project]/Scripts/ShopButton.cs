using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
    [SerializeField] Button button;
    Shop shop;

    private void Start()
    {
        shop = GetComponentInParent<Shop>();
        button.onClick.AddListener(() => shop.Buy(5));
    }
}
