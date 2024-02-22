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
        VictoryManager.VictoryManagerInstance.Extinction -= DieVictory;
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

        if (VictoryManager.VictoryManagerInstance != null)
        {
            VictoryManager.VictoryManagerInstance.Extinction += DieVictory;
        }
    }
    void Update()
    {
    }

    public void ApplyDamage(int damage)
    {
        currentLife -= damage;
        //ChangeColor();
        ReactToDamage();
    }

    private void ReactToDamage ()
    {
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
    public void DieVictory()
    {
        currentLife = 0;
        IEnumerator time = TimeToExtinct();
        StartCoroutine(time);
    }

    IEnumerator TimeToExtinct()
    {
        yield return new WaitForSeconds(Random.RandomRange(1, 3));
        ReactToDamage();
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
