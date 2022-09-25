using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateModifierIcon : MonoBehaviour
{
    private Fighter parent;
    [SerializeField]
    private StateModifiersEnum type;
    [SerializeField]
    float xOffset = 0;
    [SerializeField]
    float yOffset = 0;

    private Transform childComp;

    public Fighter Parent { get => parent; set => parent = value; }
    public StateModifiersEnum Type { get => type; set => type = value; }
    public float XOffset { get => xOffset; set => xOffset = value; }
    public float YOffset { get => yOffset; set => yOffset = value; }

    // Start is called before the first frame update
    void Start()
    {
        childComp = gameObject.transform.Find("StateModifierCounter");
        BattleEventSystem.current.OnFighterRemoved += (Fighter f) => { if (f == parent) Destroy(this.gameObject); };
    }

    bool fromNowOnIsSet = false;
    // Update is called once per frame
    void Update()
    {
        int remainingTurns = Parent.fighterLogic.StateModifs.GetStateModifierDuration(Type);
        if (fromNowOnIsSet && gameObject.GetComponent<Image>().sprite == null)
            throw new System.Exception("Something erased the sprite");
        if (gameObject.GetComponent<Image>().sprite != null && !fromNowOnIsSet)
            fromNowOnIsSet = true;
        if (remainingTurns > 0)
        {
            gameObject.GetComponent<Image>().enabled = true;
            childComp.GetComponent<Text>().enabled = true;
            childComp.GetComponent<Text>().text = remainingTurns.ToString();
        }
        else
        {
            gameObject.GetComponent<Image>().enabled = false;
            childComp.GetComponent<Text>().enabled = false;
        }

        gameObject.transform.position = Parent.transform.position + new Vector3(XOffset, YOffset, 0);
    }
}
