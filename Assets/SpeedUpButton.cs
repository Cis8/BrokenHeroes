using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpeedUpButton : MonoBehaviour
{
    public enum BattleSpeedEnum { x1, x2, x4 }
    [SerializeField]
    private TextMeshProUGUI speedText;
    [SerializeField]
    PauseButton pauseButton;

    private BattleSpeedEnum speed = BattleSpeedEnum.x1;
    // Start is called before the first frame update
    void Start()
    {
        BattleEventSystem.current.OnBattleEnd += ResetSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ResetSpeed(BattleState endState)
    {
        Time.timeScale = 1f;
    }

    public void ToggleSpeedUp()
    {
        switch (speed)
        {
            case BattleSpeedEnum.x1:
                SetSpeed(2f);
                speed = BattleSpeedEnum.x2;
                break;
            case BattleSpeedEnum.x2:
                SetSpeed(4f);
                speed = BattleSpeedEnum.x4;
                break;
            case BattleSpeedEnum.x4:
                SetSpeed(1f);
                speed = BattleSpeedEnum.x1;
                break;
        }
        speedText.text = speed.ToString();


    }

    private void SetSpeed(float value)
    {
        if (pauseButton.IsPaused)
        {
            pauseButton.OldScale = value;
        }
        else
        {
            Time.timeScale = value;
        }
    }

    
}
