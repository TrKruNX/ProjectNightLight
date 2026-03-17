using UnityEngine;
using UnityEngine.EventSystems; //gotta have this for event interfaces
using UnityEngine.UI; //for refrencing ui components

public class HoverScale : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 originalScale;
    public float scaleMultiplier = 1.1f; //adjust in inspector

    void Awake()
    {
        originalScale = transform.localScale; //store the button's original local scale
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = originalScale * scaleMultiplier; //scale up
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = originalScale; //revert to normal scale
    }


}
