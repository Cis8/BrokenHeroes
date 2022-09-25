using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingScript : MonoBehaviour
{
    [SerializeField] private Transform hpDmgPopup;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //DmgPopup.CreateHpDmgPopup(Vector3.zero, 350);
        }
    }
}
