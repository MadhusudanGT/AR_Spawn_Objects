using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class DataHandler : MonoBehaviour
{
    private GameObject furniture;

    [SerializeField] private ButtonManager buttonPrefab;
    [SerializeField] private GameObject buttonContainer;
    [SerializeField] private List<ItemList> items;

    private static DataHandler instance;
    public static DataHandler Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<DataHandler>();
            }
            return instance;
        }
    }

    private void Start()
    {
        CreateButtons();
    }

    void CreateButtons()
    {
        foreach (ItemList i in items)
        {
            ButtonManager b = Instantiate(buttonPrefab, buttonContainer.transform);
            b.ButtonTexture = i.items.itemImage;
            b.itemType = i.items.itemType;
        }
    }

    public void SetFurniture(Items selectedType)
    {
        ItemList filterItem = items.FirstOrDefault(item => item.items.itemType == selectedType);
        furniture = filterItem.items.itemPrefab;
    }

    public GameObject GetFurniture()
    {
        return furniture;
    }
}

public enum Items
{
    LAMP,
    CHAIR_1,
    SOFA_1,
    SOFA_2,
    SOFA_3,
}

[System.Serializable]
public struct ItemList
{
    public Item items;
}