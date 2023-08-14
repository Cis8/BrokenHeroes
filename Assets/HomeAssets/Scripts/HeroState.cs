using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HeroState
{
    // Serialize the fields that are required on the home scene to display the main properties with the UI
    [SerializeField]
    public string Name;
    [SerializeField]
    public int Level;
    [SerializeField]
    public ClassEnum Class;
}
