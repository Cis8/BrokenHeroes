using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionSquaredItem : SquaredItem
{

    int _grantedExp;

    public int GrantedExp { get => _grantedExp; set => _grantedExp = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetItem(ExpPotion item)
    {
        base.SetItem(item);
        _grantedExp = item.GrantedExp;
    }
}
