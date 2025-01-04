using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldShop : MonoBehaviour
{
    [SerializeField] List<GameObject> _shopItemList;
    Shop shop;
    // Start is called before the first frame update
    void Start()
    {
        shop = FindObjectOfType<Shop>();
    }

    // Update is called once per frame
    void Update()
    {

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
