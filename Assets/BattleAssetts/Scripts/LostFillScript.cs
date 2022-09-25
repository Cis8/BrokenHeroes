using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LostFillScript : MonoBehaviour
{
    public Slider lostHp;
    [SerializeField]
    private float countdownTime;
    private float countdown;
    [SerializeField]
    [Range(0.001f, 10)]
    private float lossSpeed;
    private float amountLossPerUpdate;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if(countdown <= 0)
        {
            if (lostHp.value > 0)
            {
                lostHp.value -= amountLossPerUpdate * Time.deltaTime;
            }
        }

    }

    public void Initialize(int hp)
    {
        lostHp.maxValue = hp;
    }

    public void LostHp(int value)
    {
        countdown = countdownTime;
        lostHp.value = value;
        amountLossPerUpdate = lossSpeed * value;
        ////////////Disabled.Log("Setting loss speed per update at: " + amountLossPerUpdate);
    }
}
