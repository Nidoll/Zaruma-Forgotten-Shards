using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : Unit
{
    public GameObject patrolCube1;
    public GameObject patrolCube2;

    public bool point1;
    public bool stop;
    public bool visibile = true;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        manager.AddEnemyUnit(this); 
        agent.SetDestination(patrolCube2.transform.position); 
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();

        if(Vector3.Distance(transform.position, patrolCube2.transform.position) <= 0.9f){
            agent.SetDestination(patrolCube1.transform.position);
        }

        if(Vector3.Distance(transform.position, patrolCube1.transform.position) <= 0.9f){
            agent.SetDestination(patrolCube2.transform.position);
        }

    }
}
