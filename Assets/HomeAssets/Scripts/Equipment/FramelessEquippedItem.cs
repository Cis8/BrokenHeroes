using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FramelessEquippedItem : FramelessItem
{
    EquipmentPieceEnum _kind;

    public EquipmentPieceEnum Kind { get => _kind; private set => _kind = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Init(Item item, AbstractController controller)
    {
        base.Init(item, controller);
        _kind = ((Equipment)item).PieceKind;
    }

    public void UnequipItem()
    {
        ((EquippedItemsController)Controller).RemoveItem(Name);
    }
}
