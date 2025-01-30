using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldShop : MonoBehaviour
{
    [SerializeField] List<GameObject> _shopItemList;
    Shop shop;
    
    private void Start()
    {
        //TODO trouver un moyen de passer des parametre avec les GameEvent (heritage d'un EventParametre?)
        shop = FindObjectOfType<Shop>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            shop.ShowShop(_shopItemList);
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            shop.HideShop();
        }
    }
}
