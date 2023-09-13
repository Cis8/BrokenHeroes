using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipSystem : MonoBehaviour
{
    private static TooltipSystem _current;

    [SerializeField]
    private ItemInfo tootltip;

    public static TooltipSystem Current { get => _current; private set => _current = value; }

    private void Awake()
    {
        _current = this;
    }

    public static void Show(Sprite icon, RarityEnum rarity, string name)
    {
        _current.tootltip.Show();
        _current.tootltip.SetupFramelessItem(icon, rarity, name);
    }

    public static void Hide()
    {
        _current.tootltip.Hide();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
