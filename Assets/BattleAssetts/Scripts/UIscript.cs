using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIscript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        BattleEventSystem.current.OnBattleEnd += ShowEndBattleScreen;
    }

    private void ShowEndBattleScreen(BattleState state)
    {
        this.transform.Find("BattleEndPanel").gameObject.SetActive(true);
    }
}
