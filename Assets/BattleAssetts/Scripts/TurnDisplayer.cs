using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnDisplayer : MonoBehaviour
{
    private void Start()
    {
        BattleEventSystem.current.OnTurnStarted += UpdateTurn;
        ////////////Disabled.Log("Turn display subscribed");
    }

    public void UpdateTurn(int t)
    {
        ////////////Disabled.Log("Turn updated in the display");
        GetComponent<Text>().text = "turn " + t;
    }
}
