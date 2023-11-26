using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragonLifeBar : MonoBehaviour
{
    public Image lifeBar1;
    public Image lifeBar2;

    //futuro quitart el public
    public float maxLife;
    public float currentLife;
    void Start()
    {
        maxLife = GetComponent<DragonData>().scriptableDragon.maxLife;
        currentLife = maxLife;
    }

    void Update()
    {
        lifeBar1.fillAmount = currentLife / maxLife;
        if (lifeBar2.fillAmount != lifeBar1.fillAmount )
        {
            if (lifeBar2.fillAmount > lifeBar1.fillAmount)
            {
                lifeBar2.fillAmount -= 0.1f * 4 * Time.deltaTime;
            }
            
        }
    }
    public void SetMaxLife (float maxLife)
    {
        this.maxLife = maxLife;
    }

    public void SetCurrentLife(float currentLife)
    {
        this.currentLife = currentLife;
        lifeBar1.fillAmount = currentLife / maxLife;
    }

}
