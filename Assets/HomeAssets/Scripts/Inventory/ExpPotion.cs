using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/ExpPotion")]
public class ExpPotion : Item
{
    [SerializeField]
    private int _grantedExp;

    public int GrantedExp { get => _grantedExp; set => _grantedExp = value; }
}
