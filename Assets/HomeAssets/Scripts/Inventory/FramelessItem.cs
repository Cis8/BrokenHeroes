using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FramelessItem : FramelessEquippedItem
{
    [SerializeField]
    private AmountFramelessItem _text;
    [SerializeField]
    GameObject _selected;

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
        SetAmount(item.Amount);
    }

    public void SetAmount(int amount)
    {
        if (amount <= 0)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
            _text.SetAmount(amount);
        }
    }

    public void ToggleCheck()
    {
        _selected.SetActive(!_selected.activeSelf);
    }

    public void ActivateCheck()
    {
        _selected.SetActive(true);
    }

    public void DisableCheck()
    {
        _selected.SetActive(false);
    }
}
