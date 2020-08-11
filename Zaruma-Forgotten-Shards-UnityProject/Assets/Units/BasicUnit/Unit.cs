using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    public int health;
    public int armor;
    public float radius;
    public float movespeed;
    public NavMeshAgent agent;
    public bool selected;
    public bool hovered;
    public GameObject selectCircle;
    public Material normalMat;
    public Material hoveredMat;
    private MeshRenderer meshR;
    protected GameManager manager;
    public bool mouseOver;
    public bool clumpMove;
    public bool moveCommand;
    public float aggroRange;
    public bool aggroCheck = true;
    public bool debugUnit;
    public bool attacking;

    // Start is called before the first frame update
    public void Start()
    {
        manager = GameManager.instance;
        agent = GetComponent<NavMeshAgent>();
        meshR = GetComponent<MeshRenderer>();
        selectCircle = transform.Find("SelectCircle").gameObject;
        normalMat = manager.normalMat;
        hoveredMat = manager.hoveredMat;

        

    }

    // Update is called once per frame
    public void Update()
    { 
        if(Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(agent.destination.x, agent.destination.z)) <= 0.1f){
            moveCommand = false;
        }  
    }

    public void SelectUnit()
    {
        selected = true;
        selectCircle.SetActive(true);
    }

    public void DeSelect()
    {
        selected = false;
        selectCircle.SetActive(false);
    }

    public void Hovor()
    {
        hovered = true;
        meshR.material = hoveredMat;
    }

    public void DeHovor()
    {
        hovered = false;
        meshR.material = normalMat;
    }

    void OnMouseEnter()
    {
        mouseOver = true;
        manager.AddToHovored(this);
    }

    void OnMouseExit()
    {
        mouseOver = false;
        manager.DeHovor(this);
    }

    public void SetMovementDestination(Vector3 destination)
    {
        agent.SetDestination(destination);
    }

    public void AttackTarget(Transform target)
    {   
        Debug.Log("Attack Enemy");
        attacking = true;
    }

    public void MoveToTarget(Vector3 target)
    {
        moveCommand = true;
        agent.SetDestination(target);
    }

    




}
