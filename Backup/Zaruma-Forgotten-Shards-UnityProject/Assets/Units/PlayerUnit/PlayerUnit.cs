using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : Unit
{

    public float normalStopDistance;
    public GameObject target;
    // Start is called before the first frame update
    public new void Start()
    {
        base.Start();
        manager.AddPlayerUnit(this);
        StartCoroutine(CheckForEnemys());
    }

    // Update is called once per frame
    public new void Update()
    {
       base.Update();
    }

    public IEnumerator CheckForEnemys()
    {
        Collider[] colliders;

        while(aggroCheck){
            if(attackMoveCommand){
                if(Vector3.Distance(transform.position, attackMoveTarget) <= 1){
                    attackMoveCommand = false;
                    agent.destination = transform.position;
                }
            }

            if((!moveCommand && !attacking) || attackMoveCommand){
                colliders = Physics.OverlapSphere(transform.position, aggroRange, LayerMask.GetMask("Enemys"));
                if(colliders.Length >= 1){
                    for(int i = 0; i < colliders.Length; i++){
                        if(colliders[i].GetComponent<EnemyUnit>().visibile){
                            AttackTarget(colliders[i].gameObject);
                            break;
                        }
                    }
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void ResetUnit()
    {
        moveCommand = false;
        attacking = false;
        agent.stoppingDistance = normalStopDistance;
        agent.destination = transform.position;

        if(attackMoveCommand){
            agent.destination = attackMoveTarget; 
        }
    }

    
}
