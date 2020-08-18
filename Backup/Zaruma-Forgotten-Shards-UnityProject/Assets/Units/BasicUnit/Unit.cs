using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    public int health;
    private int maxHealth;
    public int armor;
    public float radius;
    public float movespeed;
    public float attackSpeed;
    public int attackDamage;
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
    public bool attackReady = true;
    public bool attackMove;
    private HpBarScript hpBar;

    //Attack Move
    public Vector3 attackMoveTarget;
    public bool attackMoveCommand;

    // Start is called before the first frame update
    public void Start()
    {
        maxHealth = health;
        manager = GameManager.instance;
        agent = GetComponent<NavMeshAgent>();
        meshR = GetComponent<MeshRenderer>();
        selectCircle = transform.Find("SelectCircle").gameObject;
        normalMat = manager.normalMat;
        hoveredMat = manager.hoveredMat;

        hpBar = GetComponentInChildren<HpBarScript>();

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

    public virtual void AttackTarget(GameObject target)
    {   
        Debug.Log("Attack Enemy");
    }

    public void MoveToTarget(Vector3 target)
    {
        moveCommand = true;
        attacking = false;
        attackMoveCommand = false;
        agent.SetDestination(target);
    }

    public IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(1f / attackSpeed);
        attackReady = true;
    }

    public void GetDamage(int amount)
    {
        int realDamage = amount - armor;

        if(realDamage <= 0){
            realDamage = 0;
        }

        if(health - realDamage <= 0){
            Destroy(gameObject);
        }else{
            health -= realDamage;
        }

        hpBar.UpdateHealthBar((float)health / (float)maxHealth);
    }

    public void Die()
    {
        GameManager.instance.RemoveDeadUnit(this);
        Destroy(gameObject);
    }

    public void AttackMove(Vector3 target)
    {
       attackMoveCommand = true;
       attackMoveTarget = target;
       moveCommand = false;
       attacking = false;
       agent.destination = target; 
    }

    




}
