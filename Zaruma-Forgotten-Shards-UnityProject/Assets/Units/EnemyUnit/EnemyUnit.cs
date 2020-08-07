using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : Unit
{
    public GameObject patrolCube1;
    public GameObject patrolCube2;

    public bool point1;
    public bool stop;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        manager.AddEnemyUnit(this);  
    }

    // Update is called once per frame
    void Update()
    {
        stop = agent.isStopped;

        if(agent.isStopped){
            if(point1){
                agent.SetDestination(patrolCube1.transform.position);
            }else{
                agent.SetDestination(patrolCube2.transform.position);
            }
        }
    }
}
