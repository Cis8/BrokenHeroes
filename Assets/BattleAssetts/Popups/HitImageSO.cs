using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class HitImageSO : ScriptableObject
{
    [SerializeField]
    private Sprite target, self;

    public Sprite Target { get => target; set => target = value; }
    public Sprite Self { get => self; set => self = value; }

    /*public void InitializeHitImages(Fighter trg, Fighter slf = null)
    {
        if(Self != null && slf != null)
        {
            //Instantiate<HitImage>()
        }
    }*/
}
