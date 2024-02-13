using UnityEngine;
using UnityEngine.EventSystems;

namespace XEntity.InventoryItemSystem
{
    public class ItemSlotUIEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public static event System.Action OnSlotDrag;
        private static ItemSlot hoveredSlot;
        private ItemSlot mySlot;
        private UnityEngine.UI.Image slotUI;
        private Vector3 dragOffset;
        private Vector3 origin;
        private Color regularColor;
        private Color dragColor;
        private int originalSiblingIndex;

        public bool isBeingDragged { get; private set; } = false;

        private void Awake() 
        {
            mySlot = GetComponent<ItemSlot>();
            slotUI = GetComponent<UnityEngine.UI.Image>();
            originalSiblingIndex = transform.GetSiblingIndex();

            origin = transform.localPosition;
            regularColor = slotUI.color;
            dragColor = new Color(regularColor.r, regularColor.g, regularColor.b, 0.3f);
        }

        private void Update()
        {
            if (isBeingDragged && hoveredSlot != null) 
            {
                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    Utils.TransferItemQuantity(mySlot, hoveredSlot, 1);
                }
                else if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    Utils.TransferItemQuantity(mySlot, hoveredSlot, mySlot.itemCount / 2);
                }
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            hoveredSlot = mySlot;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            hoveredSlot = null;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            slotUI.transform.SetAsLastSibling();
            slotUI.color = dragColor;
            slotUI.raycastTarget = false;
            hoveredSlot = null;
            dragOffset = Input.mousePosition - transform.position;
            isBeingDragged = true;
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = Input.mousePosition - dragOffset;
            OnSlotDrag?.Invoke();
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (hoveredSlot != null) OnDropToSlot();

            transform.SetSiblingIndex(originalSiblingIndex);
            transform.localPosition = origin;
            slotUI.color = regularColor;
            slotUI.raycastTarget = true;
            isBeingDragged = false;
        }

        private void OnDropToSlot() 
        {
            Utils.TransferItem(mySlot, hoveredSlot);
        }
    } 
}
