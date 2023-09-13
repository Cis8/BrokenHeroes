using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    FramelessItem _framelessItem;

    public void OnPointerEnter(PointerEventData eventData)
    {
        TooltipSystem.Show(_framelessItem.Image.sprite, _framelessItem.Rarity, _framelessItem.Name);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipSystem.Hide();
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
