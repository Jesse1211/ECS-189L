using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Project
{
    public class ItemSlot : MonoBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
                useItem();
        }

        public void useItem()
        {
            var child = this.transform.GetChild(0).gameObject;
            if (child)
            {
                DataLoader.RemoveBagItems(this.transform);
                Destroy(this.transform.GetChild(0).gameObject);
            }
        }
    }
}

/*
 * 点击bag pannel里的武器: 就可以加到characterPannel里面
 * characterPannel遵循的是Queue FIFO
 * 如果Queue满了, 那就把最开始加进去的移动到bag pannel中
 */