using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractController : MonoBehaviour
{
    protected Dictionary<string, Item> _inventory;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual void Init()
    {
        _inventory = GameAssets.current.Items;
    }
}
