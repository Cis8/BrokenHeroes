using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeMenuButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenPanel(string panelName)
    {
        HomeEventSystem.current.PanelMustBeToggled(panelName);
    }
}
