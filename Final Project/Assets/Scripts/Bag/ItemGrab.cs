using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemGrab : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject grabParent;
    public GameObject curParent;

    private void Start()
    {
        grabParent = GameObject.Find("Canvas/BagPannel/BagBlock");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        curParent = transform.parent.gameObject;
        transform.GetComponent<CanvasGroup>().blocksRaycasts = false; // 可以進行穿透
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
        transform.SetParent(grabParent.transform);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GameObject curObject = eventData.pointerCurrentRaycast.gameObject;
        if (curObject == null)
        {
            transform.SetParent(curParent.transform);
            transform.GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
        else if ((curObject != null) && (curObject.transform.tag == "BagBlock"))
        {
            transform.SetParent(curObject.transform);
            transform.GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
        else if ((curObject != null) && (curObject.transform.tag != "BagBlock") && (curObject.transform.parent.tag == "物品模板")) 
        {
            var tf = curObject.transform.parent.parent.transform;
            curObject.transform.parent.SetParent(curParent.transform);

            transform.SetParent(curObject.transform.parent.parent.transform);
            transform.GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
        else if ((curObject != null) && (curObject.transform.tag != "BagBlock") && (curObject.transform.parent.tag != "物品模板"))
        {
            transform.SetParent(curParent.transform);
            transform.GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
    }
}
