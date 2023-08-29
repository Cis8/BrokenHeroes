using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipFrameEmpty : MonoBehaviour
{
    [SerializeField]
    FramelessEquippedItem _managedEquipment;

    public FramelessEquippedItem ManagedEquipment { get => _managedEquipment; private set => _managedEquipment = value; }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetupEquipment(Item item, AbstractController controller)
    {
        ManagedEquipment.Init(item, controller);
        ManagedEquipment.gameObject.SetActive(true);
    }

    public void DisableEquipment()
    {
        ManagedEquipment.gameObject.SetActive(false);
    }

    public bool IsItemWithName(string name)
    {
        return ManagedEquipment.Name == name;
    }
}
