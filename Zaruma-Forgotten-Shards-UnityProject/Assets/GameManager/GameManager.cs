using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;


    //stores all Units
    public List<Unit> fUnits;
    public List<Unit> eUnits;
    public List<Unit> nUnits;

    //stores hovered/selected
    public List<Unit> hoveredUnits;
    public List<Unit> selectedUnits;

    //Controlls
    private RaycastHit hitInfo;
    private Ray ray;
    private bool rayCast;
    public Vector3 mousePosition;
    public GameObject debugCube;
    public GameObject selectionSquare;

    //Mats
    public Material normalMat;
    public Material hoveredMat;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }else{
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonUp(0)){
            DeSelectAllUnits();
            foreach(PlayerUnit pUnit in hoveredUnits){
                AddToSelected(pUnit);
            }
        }

        if(Input.GetMouseButtonDown(0)){
            
        }

        RayCast();

    }

    public void RayCast()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 50, Color.red);
        rayCast = Physics.Raycast(ray, out hitInfo, 50f, LayerMask.GetMask("Map"));

        if(rayCast){
            mousePosition = hitInfo.point;
            debugCube.transform.position = mousePosition;
        }
    }

    public void AddToHovored(Unit unit)
    {
        if(!hoveredUnits.Contains(unit)){
            hoveredUnits.Add(unit);
        }
    }

    public void DeHovor(Unit unit)
    {
        if(hoveredUnits.Contains(unit)){
            hoveredUnits.Remove(unit);
        }
    }

    public void AddToSelected(PlayerUnit pUnit)
    {
        if(!selectedUnits.Contains(pUnit)){
            pUnit.SelectUnit();
            selectedUnits.Add(pUnit);
        }
    }

    public void DeSelectAllUnits()
    {
        foreach(Unit unit in selectedUnits){
            unit.DeSelect();
        }
        selectedUnits.Clear();
    }

    void GetMousePositionOnMap()
    {

    }

}
