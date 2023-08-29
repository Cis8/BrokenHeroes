using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class AmountFramelessItem : MonoBehaviour
{
    TMP_Text _amountText;

    // Start is called before the first frame update
    void Awake()
    {
        _amountText = gameObject.GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetAmount(int amount)
    {
        if(_amountText == null)
        {
            _amountText = gameObject.GetComponent<TMP_Text>();
        }
        _amountText.text = amount.ToString();
    }
}
