using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToolTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    GameObject toolTipPrefab;

    GameObject toolTipObject;

    public void OnPointerEnter(PointerEventData eventData)
    {
        toolTipObject = Instantiate(toolTipPrefab, Input.mousePosition, toolTipPrefab.transform.rotation, transform);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Invoke("DestroyObject", 0.3f);
    }

    void DestroyObject()
    {
        Destroy(toolTipObject);
    }
}
