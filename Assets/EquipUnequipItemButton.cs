using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipUnequipItemButton : MonoBehaviour
{
    [SerializeField]
    GameObject _checked; // tells if the item is currently equipped or not
    FramelessItem _item;

    public void ToggleEquipment()
    {
        if (_checked.activeSelf)
        {
            ((HeroInventoryController)_item.Controller).UnquipItem(_item.Name);   
        } else
        {
            ((HeroInventoryController)_item.Controller).EquipItem(_item.Name);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _item = GetComponent<FramelessItem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
