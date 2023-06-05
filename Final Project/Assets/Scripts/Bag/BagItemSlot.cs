using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Project
{
    /// <summary>
    /// Error: remove item error
    /// HINT: BagDataLoader.UpdateBagItem has logic error for bagItems destory and instantiation
    /// </summary>
    public class BagItemSlot : MonoBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
                useItem();
        }

        public void useItem()
        {
            var childCount = this.transform.childCount;
            //Debug.Log("child Count for SLOT: " + childCount);
            if (childCount > 0)
            {
                var child = this.transform.GetChild(0).gameObject;
                //Debug.Log("child for SLOT: " + child + "   WHAT IS THIS: " + this.transform);

                Item item = new Item();
                item.prefab = child;
                BagDataLoader.AddWeapon(item);
                BagDataLoader.RemoveBagItems(this.transform);

                Destroy(this.transform.GetChild(0).gameObject);            
            }
        }
    }
}

/*
 * 点击bag pannel里的武器: 就可以加到characterPannel里面
 * characterPannel遵循的是Queue FIFO
 * 如果Queue满了, 那就把最开始加进去的移动到bag pannel中
 * 
 * 
 * Destry分情况: 
 *          使用了, 就会destory
 * 面板之间移动: 不需要destroy, 只需要换他的位置
 */