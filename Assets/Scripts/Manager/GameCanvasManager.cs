using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.UI;

public class GameCanvasManager : MonoBehaviour
{
    [Header("GOs")]
    [SerializeField] GameObject canvasGame;
    [SerializeField] GameObject canvasPointer;

    [Header("Targets")]
    private int numPoints;

    [Header("Shot")]
    public GameObject bulletsPanel;

    int bulletsMargin = 40;

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
    public Text powerUpTitle;
    public Text powerUpCounterText;
    public bool usingPowerUp;
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
                powerUpCounterText.text = powerUpCounter.ToString("N0");
            }
            
        }
        
    }
    private void CleanPowerUpCanvas()
    {
        powerUpTitle.text = "";
        powerUpCounterText.text = "";

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
            powerUpCounterText.text = powerUpCounter.ToString("N0");
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
            trans.transform.SetParent(bulletsPanel.transform); // setting parent
            trans.localScale = Vector3.one;

            float panelHeight = bulletsPanelRT.sizeDelta.y * bulletsPanelRT.localScale.y;

            trans.sizeDelta = new Vector2(panelHeight / 3, panelHeight);

            Image image = imgObject.AddComponent<Image>();
            image.sprite = bulletSprite;

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

    public void VisibleCanvas (bool b)
    {
        canvasGame.SetActive(b);
        canvasPointer.SetActive(b);
    }
    
}
