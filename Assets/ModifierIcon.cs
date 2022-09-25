using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModifierIcon : MonoBehaviour
{
    private Image image;

    private float added_duration;
    private Vector3 added_maxScale;
    private Vector3 added_currentScale;
    private float added_timer;

    private bool isBeingRemoved = false;
    private float drop_duration;
    private float drop_initialYSpeed;
    private float drop_ySpeed;
    private float drop_timer;

    // Start is called before the first frame update
    void Awake()
    {
        added_duration = 1;
        added_maxScale = new Vector3(4f, 4f, 4f);
        added_timer = 0;
        added_currentScale = new Vector3();

        drop_duration = 0.75f;
        drop_initialYSpeed = 1f;
        drop_ySpeed = drop_initialYSpeed;
        drop_timer = 0;
    }

    private void Start()
    {
        image = gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        added_timer -= Time.deltaTime;
        if (added_timer < 0)
            added_timer = 0;
        float value = 1 + (ScaleOffset().x * (added_timer / added_duration));
        added_currentScale.x = value;
        added_currentScale.y = value;
        added_currentScale.z = value;
        gameObject.transform.localScale = added_currentScale;

        if (isBeingRemoved)
        {
            drop_timer -= Time.deltaTime;
            if (drop_timer > 0)
            {
                gameObject.transform.position -= new Vector3(0, drop_ySpeed * Time.deltaTime, 0);
                drop_ySpeed -= (Time.deltaTime / drop_duration) * drop_initialYSpeed;
                Color c = image.color;
                c.a = (drop_timer / drop_duration);
                image.color = c;
            }
            else
            {
                /*Color c = gameObject.GetComponent<Image>().color;
                c.a = 0;
                gameObject.GetComponent<Image>().color = c;*/
                Destroy(gameObject);
            }
        }
    }

    Vector3 ScaleOffset()
    {
        return added_maxScale - new Vector3(1f, 1f);
    }

    public void FlashBigToSmall()
    {
        added_timer = added_duration;
        added_currentScale = added_maxScale;
        //gameObject.transform.localScale = currentScale;
    }

    public void DropIcon()
    {
        isBeingRemoved = true;
        drop_timer = drop_duration;
    }
}
