using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatModificationDescription : MonoBehaviour
{
    [SerializeField]
    TMP_Text _statisticName;
    [SerializeField]
    TMP_Text _value;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetupText(string statName, int amount, bool isPercentual)
    {
        _statisticName.text = statName;
        _value.text = $"{(amount > 0 ? "+" : "-")}{Math.Abs(amount)}{(isPercentual ? "%" : "")}";
    }
}
