using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : Unit
{
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        manager.AddPlayerUnit(this);
        StartCoroutine(CheckForEnemys());
    }

    // Update is called once per frame
    new void Update()
    {
       base.Update();
    }

    IEnumerator CheckForEnemys()
    {
        Collider[] colliders;

        while(aggroCheck){
            if(!moveCommand && !attacking){
                colliders = Physics.OverlapSphere(transform.position, aggroRange, LayerMask.GetMask("Enemys"));
                if(colliders.Length >= 1){
                    AttackTarget(colliders[0].transform);
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}
