using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleResultText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Text>().text += BattleManager.current.state.ToString();
        //BattleEventSystem.current.onBattleEnd += SetText;
    }

    private void SetText(BattleState state)
    {
        //this.gameObject.GetComponent<Text>().text += state.ToString();
        //////////////Disabled.Log("TEXT IS " + this.gameObject.GetComponent<Text>().text);
    }
}
