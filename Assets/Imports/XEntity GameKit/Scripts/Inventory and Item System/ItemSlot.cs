using UnityEngine;

namespace XEntity.InventoryItemSystem
{
    //This script is attached to any container slots that can hold items.
    public class ItemSlot : MonoBehaviour
    {
        public Item slotItem;

        public int itemCount;

        public bool IsEmpty { get { return itemCount <= 0; } }

        public UnityEngine.UI.Image iconImage;

        private UnityEngine.UI.Text countText;


        public void Initialize() 
        {
            iconImage = transform.Find("Icon Image").GetComponent<UnityEngine.UI.Image>();
            countText = transform.Find("Icon Image").gameObject.transform.Find("Count Text").GetComponent<UnityEngine.UI.Text>();

            countText.text = string.Empty;
        }

        public bool Add(Item item) 
        {
            if (IsAddable(item))
            {
                slotItem = item;
                itemCount++;


                OnSlotModified();
                return true;
            }
            else return false; 
            
        }

        public void RemoveAndDrop(int amount, Vector3 dropPosition) 
        {
            for (int i = 0; i < amount; i++) 
            {
                if (itemCount > 0)
                {
                    Utils.InstantiateItemCollector(slotItem, dropPosition);
                    itemCount--;
                }
                else break;
            }

            OnSlotModified();
        }

        public void Remove(int amount)
        {
            itemCount -= amount > itemCount ? itemCount : amount;
            OnSlotModified();
        }

        public void Clear() 
        {
            itemCount = 0;
            OnSlotModified();
        }

        public void ClearAndDrop(Vector3 dropPosition) 
        {
            RemoveAndDrop(itemCount, dropPosition);
        }

        private bool IsAddable(Item item)
        {
            if (item != null)
            {
                if (IsEmpty) return true;
                else
                {
                    if (item == slotItem) return true;
                    else return false;
                }
            }
            return false;
        }

        private void OnSlotModified() 
        {
            if (!IsEmpty)
            {
                iconImage.sprite = slotItem.icon;
                countText.text = itemCount.ToString();
                iconImage.gameObject.SetActive(true);
            }
            else 
            {
                itemCount = 0;
                slotItem = null;
                iconImage.sprite = null;
                countText.text = string.Empty;
                //iconImage.gameObject.SetActive(false);
            }
        }

        public void SetData(Item item, int count) 
        {
            slotItem = item;
            itemCount = count;
            OnSlotModified();
        }
    }
}
