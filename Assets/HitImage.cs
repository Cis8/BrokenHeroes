using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitImage : MonoBehaviour
{
    //private HitImageSO hitImageSO;
    private Fighter fighterToFollow;
    private float timer;
    private Image image;
    private float alphaFadeTime;

    //public HitImageSO HitImageSO { get => hitImageSO; set => hitImageSO = value; }

    // Start is called before the first frame update
    void Start()
    {
        timer = 0.5f;
        alphaFadeTime = 0.2f;

        //image.sprite = hitImageSO.Target;
    }

    public void SetSprite(Sprite s)
    {
        image = gameObject.GetComponent<Image>();
        image.sprite = s;
    }

    public void SetFighter(Fighter f)
    {
        fighterToFollow = f;
    }

    // Update is called once per frame
    void Update()
    {
        if(fighterToFollow != null)
        {
            transform.position = fighterToFollow.transform.position;
        }
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            Color c = image.color;
            c.a -= Time.deltaTime / alphaFadeTime;
            image.color = c;
            if (c.a <= 0)
                Destroy(gameObject);
        }
    }
}
