using UnityEngine;

namespace XEntity.InventoryItemSystem
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Item")]
    public class Item : ScriptableObject
    {
        #region Essential
        public ItemType type;
        public string itemName;
        public Sprite icon;
        public GameObject prefab;
        public float lifetime;

        [TextArea]
        public string itemInformation;

        #endregion
    }
}
