using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public float currentLife;
    public float maxLife;
    
    void Awake ()
    {
        currentLife = maxLife;
    }
    void Start()
    {

    }
    void Update()
    {

    }


    public void SetCurrentLife(float currentLife)
    {
        this.currentLife = currentLife;
    }
    
    public void ResetCurrentLife()
    {
        currentLife = 0;
    }

    public void PlayerHit(int damage)
    {
        this.currentLife -= damage;
        GameCanvasManager.GameManagerInstance.ScreenDamageEffect();
        GameCanvasManager.GameManagerInstance.PlayerHitEffect(currentLife, maxLife);
        if (currentLife <= 0)
        {
            GameOverManager.Instance.GameOver();
        } 
        
    }
}
