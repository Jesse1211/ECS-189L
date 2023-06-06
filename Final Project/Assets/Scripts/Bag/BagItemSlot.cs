using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Project
{
    /// <summary>
    /// Right click to utilize the item
    /// </summary>
    public class BagItemSlot : MonoBehaviour, IPointerClickHandler
    {
        public GameObject Bag;
        private BagManager BagManager;
        private void Start()
        {
            BagManager = Bag.GetComponent<BagManager>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
                BagManager.useItem(this.transform);
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

/*
 * 1: player捡起GO (GO: 已经Instantiate了)
 * 2: 怎么体现他捡到了: 
 *          - 装到bagItem 从playerController
 * 3: 背包显示: 
 *          - 从bagItems里面提取到item 是为了拿到prefab
 *          - 点击互动: bag 和 weapon 之间的关联: 调整item的location / parent
 */