using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AddExpButton : MonoBehaviour
{
    [SerializeField]
    Character _currentHero;
    int _expAmount;
    PotionSquaredItem _potion;
    [SerializeField]
    PotionsController _controller;

    // Start is called before the first frame update
    void Start()
    {
        _potion = gameObject.GetComponent<PotionSquaredItem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddExp()
    {
        if(_potion.Amount > 0)
        {
            _potion.DecreaseAmount(1);
            _currentHero.AddExp(_potion.GrantedExp);
            _controller.DecreaseItemData(_potion.Name, 1);
        }
    }
}
