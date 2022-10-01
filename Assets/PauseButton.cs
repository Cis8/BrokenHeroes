using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    private bool isPaused = false;
    private float oldScale;
    private GameObject pauseText;
    Sprite playSprite, pauseSprite;

    public bool IsPaused { get => isPaused; set => isPaused = value; }
    public float OldScale { get => oldScale; set => oldScale = value; }

    // Start is called before the first frame update
    void Start()
    {
        OldScale = Time.timeScale;
        pauseText = gameObject.transform.Find("PauseText").gameObject;
    }

    private void Awake()
    {
        pauseSprite = GetComponent<Image>().sprite;
        playSprite = Resources.Load<Sprite>("Sprites/Arena/UI/play");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PasuePlayGame()
    {
        if (IsPaused)
        {
            pauseText.SetActive(false);
            GetComponent<Image>().sprite = pauseSprite;
            Time.timeScale = OldScale;
        }
        else
        {
            pauseText.SetActive(true);
            GetComponent<Image>().sprite = playSprite;
            OldScale = Time.timeScale;
            Time.timeScale = 0;
        }
        IsPaused = !IsPaused;
    }
}
