using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
    public int health;
    public int armor;
    public float movespeed;
    public NavMeshAgent agent;
    public bool selected;
    public bool hovered;
    public GameObject selectCircle;
    public Material normalMat;
    public Material hoveredMat;
    private MeshRenderer meshR;
    private GameManager manager;

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
    void Update()
    {
        
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
        GameManager.instance.AddToHovored(this);
    }

    public void DeHovor()
    {
        hovered = false;
        meshR.material = normalMat;
        GameManager.instance.DeHovor(this);
    }

    void OnMouseEnter()
    {
        Hovor();
    }

    void OnMouseExit()
    {
        DeHovor();
    }



}
