using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISwitch : MonoBehaviour
{
    private bool isUIActive = false;
    private GameObject leftMenu;
    private GameObject rightMenu;

    // Start is called before the first frame update
    void Start()
    {
        SwapScale();
        leftMenu = gameObject.transform.Find("LeftMenu").gameObject;
        rightMenu = gameObject.transform.Find("RightMenu").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchUI()
    {
        isUIActive = !isUIActive;
        leftMenu.SetActive(isUIActive);
        rightMenu.SetActive(isUIActive);
        SwapScale();
    }

    private void SwapScale()
    {
        Vector3 scale = gameObject.GetComponent<SpriteRenderer>().transform.localScale;
        scale = new Vector3(-1 * scale.x, scale.y, scale.z);
        gameObject.GetComponent<SpriteRenderer>().transform.localScale = scale;
    }
}
