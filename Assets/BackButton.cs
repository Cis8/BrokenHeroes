using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButton : MonoBehaviour
{
    [SerializeField]
    GameObject _content;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void DisableContent()
    {
        _content.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
