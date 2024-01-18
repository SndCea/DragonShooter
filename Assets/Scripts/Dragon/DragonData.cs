using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonData : MonoBehaviour
{
    public Dragon scriptableDragon;
    private float currentLife;
    private int numColors;
    private int indexColor;
    DragonLifeBar dragonLifeBar;
    public MonoBehaviour stateMachine;
    public Material[] meshesColors;
    public bool stop;
    private void OnEnable()
    {
        InicializeDelegates();
    }
    private void OnDisable()
    {
        GameOverManager.Instance.GameOverReleased -= StopGOver;
    }
    void Start()
    {
        InicializeDelegates();
        currentLife = scriptableDragon.maxLife;
        dragonLifeBar = GetComponent<DragonLifeBar>();
        numColors = meshesColors.Length;
        indexColor = 0;
    }
    private void InicializeDelegates()
    {
        if (GameOverManager.Instance != null)
        {
            GameOverManager.Instance.GameOverReleased += StopGOver;
        }
    }
    void Update()
    {
    }

    public void ApplyDamage(int damage)
    {
        currentLife -= damage;
        ChangeColor();
        if (currentLife <= 0)
        {
            if (stateMachine is SmallDragonStateMachine smallStateMachine)
            {
                smallStateMachine.Die();
            }
            if (stateMachine is MediumDragonStateMachine mediumStateMachine)
            {
                mediumStateMachine.Die();
            }
            if (stateMachine is BigDragonStateMachine bigStateMachine)
            {
                bigStateMachine.Die();
            }
        }
        else
        {
            if (stateMachine is SmallDragonStateMachine smallStateMachine)
            {
                smallStateMachine.GetHit();
            }
            if (stateMachine is MediumDragonStateMachine mediumStateMachine)
            {
                mediumStateMachine.GetHit();
            }
            if (stateMachine is BigDragonStateMachine bigStateMachine)
            {
                bigStateMachine.GetHit();
            }
        }
        dragonLifeBar.SetCurrentLife(currentLife);

    }

    public void ChangeColor()
    {
        SkinnedMeshRenderer mesh = GetComponentInChildren<SkinnedMeshRenderer>();
        mesh.material = meshesColors[indexColor];
        indexColor++;

        if (indexColor == numColors)
        {
            indexColor = 0;
        }
    }

    public void StopGOver()
    {
        stop = true;
        Stop(stop);
    }
    public void Stop(bool stop)
    {
        if (stateMachine is SmallDragonStateMachine smallStateMachine)
        {
            smallStateMachine.Stop(stop);
        }
        if (stateMachine is MediumDragonStateMachine mediumStateMachine)
        {
            mediumStateMachine.Stop(stop);
        }
        if (stateMachine is BigDragonStateMachine bigStateMachine)
        {
            bigStateMachine.Stop(stop);
        }
    }
}
