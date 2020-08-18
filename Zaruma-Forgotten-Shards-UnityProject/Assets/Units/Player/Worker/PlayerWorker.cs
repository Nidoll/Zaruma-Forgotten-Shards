using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWorker : PlayerMeleeUnit
{
    private bool mining;
    private bool holdCrystal;
    private GameObject miningTarget;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        StopCoroutine(CheckForEnemys());
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
    }

    public void StartMining(GameObject target)
    {
        Debug.Log("Start Mining");
        ResetUnit();
        agent.SetDestination(target.transform.position);
    }
}
