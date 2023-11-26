using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using XEntity.InventoryItemSystem;
using Item = XEntity.InventoryItemSystem.Item;

public class InventoryManager : MonoBehaviour
{
    //Target Prefab es solo la parte visual, allí tiene Item, que es el que se añade al inventario, e Item tiene prefab,
    //que es el prefab con los códigos de la funcionalidad, es decir, será prefab de Damage
    //lo que haré al usarlo será instantiate el prefab de ese item, que se cree en esa escena, este es un GO vacio con 
    //solo codigos, asi que se creará en la escena y él mismo se borrará al cabo de un tiempo
    public static InventoryManager InventoryManagerInstance { get; private set; }
    public GameObject mainUI;
    public ItemSlot[] slots;
    public List<Item> inventoryStock = new List<Item>();
    public Item[] totalItems;

    public Transform mainContainerUI;
    protected Transform containerPanel;
    public TextMeshProUGUI descriptionText;
    public GameObject panelSelectedData;
    private ItemSlot selectedItemSlot;

    protected virtual void Awake()
    {
        if (InventoryManagerInstance != null && InventoryManagerInstance != this)
        {
            Destroy(this);
        }
        else
        {
            InventoryManagerInstance = this;
        }

        //The container is initilized on awake.
        InitializeContainer();

        foreach (ItemSlot slot in slots)
        {
            slot.Initialize();
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        MetePowerUps();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            mainUI.SetActive(!mainUI.activeSelf);
        }
    }
    void MetePowerUps()
    {
        foreach(Item item in inventoryStock)
        {
            AddItem(item);
        }
    }
    

    protected virtual void InitializeContainer()
    {
        IntialzieMainUI(transform);
    }

    protected void IntialzieMainUI(Transform containerPanel)
    {
        Transform slotHolder = mainContainerUI.Find("Slot Holder");
        slots = new ItemSlot[slotHolder.childCount];
        for (int i = 0; i < slots.Length; i++)
        {
            ItemSlot slot = slotHolder.GetChild(i).GetComponent<ItemSlot>();
            slots[i] = slot;

            Button slotButton = slot.GetComponent<Button>();
            slotButton.onClick.RemoveAllListeners();
            slotButton.onClick.AddListener(delegate { OnSlotClicked(slot); });
        }
        mainContainerUI.gameObject.SetActive(false);
    }
    protected void OnSlotClicked(ItemSlot slot)
    {
        if (slot.IsEmpty) return;

        ShowItemDescription(slot.slotItem.itemInformation);
        selectedItemSlot = slot;
    }
    public bool AddItem(Item item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].Add(item))
            {
                //Durante pruebas desactivo esto porque lo estoy metiendo en stock manualmente
                //y llamando a esta funcion en AMeter()
                //inventoryStock.Add(item);
                return true;
            }
        } 
        return false;

    }

    protected void ToggleUI()
    {
        if (mainContainerUI.gameObject.activeSelf)
        {
            StartCoroutine(Utils.TweenScaleOut(mainContainerUI.gameObject, 50, false));
        }
        
    }
    private void ShowItemDescription (string description)
    {
        descriptionText.text = description;
        panelSelectedData.SetActive(true);
    }

    public void UseItem()
    {
        Item selectedItem = selectedItemSlot.slotItem;
        GameObject powerUpPrefab = selectedItem.prefab;
        if (PowerUpsInScene() && selectedItem.type == ItemType.PowerUp)
        {
            ShowItemDescription("I'm sorry, you can't use a Power Up if another one is being used.");

        } else
        {
            Instantiate(powerUpPrefab);
            GameCanvasManager.GameManagerInstance.UsingPowerUp(selectedItem.name, selectedItem.lifetime);
            selectedItemSlot.Remove(1);
            mainUI.SetActive(false);
        }
    }

    private bool PowerUpsInScene()
    {
        return GameObject.FindGameObjectsWithTag("PowerUp").Length > 0;
    }

}
