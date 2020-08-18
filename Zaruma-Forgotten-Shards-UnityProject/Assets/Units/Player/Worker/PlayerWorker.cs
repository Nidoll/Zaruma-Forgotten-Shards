using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWorker : PlayerMeleeUnit
{
    private bool mining;
    private bool holdCrystal;
    private GameObject miningTarget;
    public Mesh crystalMesh;
    public Material crystalMat;
    private MeshRenderer meshRend;
    private MeshFilter meshFilter;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        StopCoroutine(CheckForEnemys());
        meshRend = transform.Find("HoldingObject").gameObject.GetComponent<MeshRenderer>();
        meshFilter = transform.Find("HoldingObject").gameObject.GetComponent<MeshFilter>();
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

    public void HoldCrystal()
    {
        meshRend.material = crystalMat;
        meshFilter.mesh = crystalMesh;
    }

    public void HoldNothing()
    {
        meshRend.material = null;
        meshFilter.mesh = null;
    }

    IEnumerator CalcMining(GameObject target)
    {
        bool needOre = holdCrystal;

        while(mining){
            


            yield return new WaitForSeconds(0.1f);
        }

    }
}
