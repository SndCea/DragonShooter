using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace XEntity.InventoryItemSystem
{
    public class ItemOrganizerInventory : MonoBehaviour
    {

        void Start()
        {
            
        }

        void ChangeOrder(int fromIndex, int toIndex)
        {
            // Ensure both indices are within the valid range
            if (fromIndex < 0 || fromIndex >= transform.childCount || toIndex < 0 || toIndex >= transform.childCount)
            {
                return;
            }

            // Change the order by modifying the sibling index
            transform.GetChild(fromIndex).SetSiblingIndex(toIndex);
        }

        void Update()
        {

        }

        private int FirstAvailable()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                ItemSlot itemSlot = transform.GetChild(i).GetComponent<ItemSlot>();
                
                if (itemSlot.IsEmpty)
                {
                    return i;
                }
            }
            return 0;
        }
        public void CheckOrder()
        {
            bool state;
            for (int i = 0; i < transform.childCount; i++)
            {
                ItemSlot itemSlot = transform.GetChild(i).GetComponent<ItemSlot>();

                if (!itemSlot.IsEmpty)
                {
                    int firstEmpty = FirstAvailable();
                    if (i < firstEmpty)
                    {
                        continue;
                    } else
                    {
                        ChangeOrder(i, firstEmpty);
                    }
                }

            }
        }
        //public void CheckOrderrrr()
        //{
        //    bool firstState = false;
        //    bool secondState;
        //    for (int i = 0; i < transform.childCount; i++)
        //    {
        //        ItemSlot itemSlot = transform.GetChild(i).GetComponent<ItemSlot>();
        //        if (i == 0)
        //        {
        //            firstState = itemSlot.IsEmpty;
        //            continue;
        //        }
        //        secondState = itemSlot.IsEmpty;
        //        if (secondState && !firstState)
        //        {
        //            ChangeOrder(i, i - 1);
        //        }

        //    }
        //}
    }
}
