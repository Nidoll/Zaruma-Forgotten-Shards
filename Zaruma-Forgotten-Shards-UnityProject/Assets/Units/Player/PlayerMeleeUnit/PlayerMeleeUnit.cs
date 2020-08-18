using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeUnit : PlayerUnit
{
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();

        AttackCalculations();
    }

    public override void AttackTarget(GameObject target)
    {
        attacking = true;
        moveCommand = false;

        this.target = target;

        float stopDistance = target.GetComponent<EnemyUnit>().radius + radius + 0.2f;

        agent.stoppingDistance = stopDistance;
    }

    public void AttackCalculations()
    {
        if(attacking){
            if(target == null){
                if(attackMoveCommand){
                    ResetUnit();
                    attackMoveCommand = true;
                }else{
                    ResetUnit();
                }
                return;
            }
            agent.SetDestination(target.transform.position);
            if(Vector3.Distance(target.transform.position, transform.position) < target.GetComponent<EnemyUnit>().radius + radius + 0.1f){
                if(attackReady){
                    target.GetComponent<Unit>().GetDamage(attackDamage);
                    attackReady = false;
                    StartCoroutine(AttackCooldown());
                }
            }
        }        
    }
}
