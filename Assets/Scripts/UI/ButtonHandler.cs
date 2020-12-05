using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class ButtonHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    
    [SerializeField] private GameObject costObject; 
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        costObject.SetActive(true);
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        costObject.SetActive(false);
    }
    
}
