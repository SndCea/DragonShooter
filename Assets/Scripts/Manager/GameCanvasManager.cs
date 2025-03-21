using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameCanvasManager : MonoBehaviour
{
    [Header("GOs")]
    [SerializeField] GameObject canvasGame;
    [SerializeField] GameObject canvasPointer;
    [SerializeField] GameObject canvasTutorial;

    [Header("Targets")]
    private int numPoints;

    [Header("Shot")]
    public GameObject bulletsPanel;
    public GameObject ammoPanel;

    [Header("Player Hit Effect")]
    [SerializeField] GameObject damageEffect;
    [SerializeField] Image healthBar1;
    [SerializeField] Image healthBar2;

    [Header("Stamina")]
    public Image staminaImage;
    public bool hasStamina = true;
    public float totalStaminaSeconds = 10f;
    public float actualStamina;

    [Header("Power Ups")]
    public TextMeshProUGUI powerUpTitle;
    public TextMeshProUGUI powerUpTimerText;
    private bool usingPowerUp;
    private float powerUpCounter;

    public static GameCanvasManager GameManagerInstance { get; private set; }
    private void Awake()
    {
        if (GameManagerInstance != null && GameManagerInstance != this)
        {
            Destroy(this);
        }
        else
        {
            GameManagerInstance = this;
        }
    }

    public bool GetUsingPowerUpBool()
    {
        return usingPowerUp;
    }

    void Start()
    {
        SetCursorState(true);
        damageEffect.SetActive(false);  
    }

    void Update()
    {
        if (usingPowerUp)
        {
            if (powerUpCounter <= 0)
            {
                powerUpCounter = 0.0f;
                CleanPowerUpCanvas();
                usingPowerUp = false;
            }
            else
            {
                powerUpCounter -= Time.deltaTime;
                powerUpTimerText.text = powerUpCounter.ToString("N0");
            }
            
        }
        
    }
    private void CleanPowerUpCanvas()
    {
        powerUpTitle.text = "";
        powerUpTimerText.text = "";

    }

    public void ScreenDamageEffect()
    {
        damageEffect.SetActive(true);
    }
    public void PlayerHitEffect(float currentPlayerLife, float maxPlayerLife)
    {
        healthBar1.fillAmount = currentPlayerLife / maxPlayerLife;
        if (healthBar2.fillAmount != healthBar1.fillAmount)
        {
            if (healthBar2.fillAmount > healthBar1.fillAmount)
            {
                healthBar2.fillAmount -= 0.1f * 4 * Time.deltaTime;
            }

        }
    }
    public void UsingPowerUp (string title, float lifetime)
    {
        powerUpTitle.text = title;
        if (lifetime > 0)
        {
            powerUpCounter = lifetime;
            powerUpTimerText.text = powerUpCounter.ToString("N0");
            usingPowerUp = true;
        }
    }
    private void SetCursorState(bool newState)
    {
        Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
    }

    public void SetCanvasShots(int numShots, int numMaxShots, Sprite bulletSprite)
    {
        cleanSprites();
        RectTransform bulletsPanelRT = bulletsPanel.GetComponent<RectTransform>();
        int numBulletsLeft = numMaxShots - numShots;
        
        for (int i = 0; i < numBulletsLeft; i++)
        {
            GameObject imgObject = new GameObject("BulletImage");
            RectTransform trans = imgObject.AddComponent<RectTransform>();
            trans.transform.SetParent(bulletsPanel.transform);
            trans.localScale = Vector3.one;

            float panelHeight = bulletsPanelRT.sizeDelta.y * bulletsPanelRT.localScale.y;

            trans.sizeDelta = new Vector2(panelHeight / 3, panelHeight);

            Image image = imgObject.AddComponent<Image>();
            image.sprite = bulletSprite;

        }
    }

    public void SetCanvasAmmo(int numAmmoLeft, Sprite ammoSprite)
    {
        cleanAmmoSprites();
        RectTransform ammoPanelRT = ammoPanel.GetComponent<RectTransform>();

        for (int i = 0; i < numAmmoLeft; i++)
        {
            GameObject imgObject = new GameObject("AmmoImage");
            RectTransform trans = imgObject.AddComponent<RectTransform>();
            trans.transform.SetParent(ammoPanel.transform);
            trans.localScale = Vector3.one;

            float panelHeight = ammoPanelRT.sizeDelta.y * ammoPanelRT.localScale.y;

            trans.sizeDelta = new Vector2(panelHeight / 1.5f, panelHeight / 1.5f);

            Image image = imgObject.AddComponent<Image>();
            image.sprite = ammoSprite;

        }
    }


    private void cleanSprites() 
    { 
        if (bulletsPanel.transform.childCount > 0)
        {
            for (int i = bulletsPanel.transform.childCount - 1; i >= 0; i--)
            {
                Destroy(bulletsPanel.transform.GetChild(i).gameObject);
            }
        }
    }
    private void cleanAmmoSprites()
    {
        if (ammoPanel.transform.childCount > 0)
        {
            for (int i = ammoPanel.transform.childCount - 1; i >= 0; i--)
            {
                Destroy(ammoPanel.transform.GetChild(i).gameObject);
            }
        }
    }

    public void usingStamina()
    {
        if (actualStamina <= totalStaminaSeconds)
        {
            hasStamina = true;
            colorStamina();

            actualStamina += Time.deltaTime * 2f;
        }

        if (actualStamina == totalStaminaSeconds || actualStamina > totalStaminaSeconds)
        {
            hasStamina = false;
            colorStamina();
            actualStamina = totalStaminaSeconds;
        }

        staminaImage.fillAmount = 1 - (actualStamina / totalStaminaSeconds);
    }

    public void winningStamina()
    {
        if (actualStamina > 0f)
        {
            hasStamina = false;
            colorStamina();
            actualStamina -= Time.deltaTime;
        }

        if (actualStamina == 0.0f || actualStamina < 0f)
        {
            hasStamina = true;
            colorStamina();
            actualStamina = 0.0f;
        }

        staminaImage.fillAmount = 1 - (actualStamina / totalStaminaSeconds);
    }
    private void colorStamina()
    {
        if (hasStamina)
        {
            staminaImage.color = Color.yellow;
        }
        else
        {
            staminaImage.color = Color.gray;
        }
    }

    public void EnableCanvasTutorial()
    {
        canvasTutorial.SetActive(!canvasTutorial.activeSelf);
    }
    public void VisibleCanvas (bool b)
    {
        canvasGame.SetActive(b);
        canvasPointer.SetActive(b);
    }
    
}
