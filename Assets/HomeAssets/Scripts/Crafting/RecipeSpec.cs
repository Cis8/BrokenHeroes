using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Crafting Recipe")]
public class RecipeSpec : ScriptableObject
{
    [System.Serializable]
    public class CraftingSpec
    {
        [SerializeField]
        string _itemName;
        [SerializeField]
        int _amount = 1;

        public int Amount { get => _amount; set => _amount = value; }
        public string ItemName { get => _itemName; set => _itemName = value; }
    }

    [SerializeField]
    CraftingSpec _output;
    [SerializeField]
    List<CraftingSpec> _inputs;

    public CraftingSpec Output { get => _output; set => _output = value; }
    public List<CraftingSpec> Inputs { get => _inputs; set => _inputs = value; }
}
