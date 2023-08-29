using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelBase : MonoBehaviour
{
    [SerializeField]
    GameObject content;

    // Start is called before the first frame update
    void Start()
    {
        HomeEventSystem.current.OnPanelToBeToggled += ToggleContent;
    }

    private void OnDestroy()
    {
        HomeEventSystem.current.OnPanelToBeToggled -= ToggleContent;
    }

    public void ToggleContent(string panelObjectName)
    {
        if(panelObjectName == gameObject.name)
        {
            content.SetActive(!content.activeSelf);
        }
    }
}
