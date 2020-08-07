using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;


    //stores all Units
    public List<Unit> pUnits;
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
    public GameObject debugCube2;
    
    //Area Selection
    public GameObject selectionSquare;
    private Vector3 selectStart;
    private Vector3 selectP2;
    private Vector3 selectEnd;
    private Vector3 selectP3;
    private bool areaSelect;

    //Mats
    public Material normalMat;
    public Material hoveredMat;

    //Movement
    public float areaTreshhold;
    public float manualClumpTreshhold;
    private Vector3 centerPoint;

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
        RayCast();
        selectEnd = mousePosition;
        DisplaySelectionSquare();

        if(Input.GetMouseButtonUp(0)){
            AreaSelection();

            DeSelectAllUnits();
            foreach(PlayerUnit pUnit in hoveredUnits){
                AddToSelected(pUnit);
            }
            foreach(PlayerUnit pUnit in hoveredUnits){
                pUnit.DeHovor();
            }
        }

        if(Input.GetMouseButtonDown(0)){
            selectStart = mousePosition;
            StartCoroutine(HoldTime());
            areaSelect = true;
            //selectionSquare.SetActive(true);
        }

        if(Input.GetMouseButtonDown(1)){
            MovePlayerUnits();
        }

        if(Input.GetKeyDown(KeyCode.P)){
            CalcDensityOfUnits(selectedUnits, Vector3.zero, areaTreshhold);
        }
    }

    public void MovePlayerUnits()
    {
        List<Vector3> points = new List<Vector3>();
        foreach(Unit unit in selectedUnits){
            points.Add(unit.transform.position);
        }
        centerPoint = CalcCenterPoint(points);

        if(CheckMouseInsideUnits(selectedUnits)){
            ClumpMovement(selectedUnits);
        }else{
            if(CalcDensityOfUnits(selectedUnits, Vector3.zero, areaTreshhold)){
                GroupMovement(selectedUnits);
            }else{
                ClumpMovement(selectedUnits);
            }
        }
        

        /*foreach(PlayerUnit unit in selectedUnits){
            unit.SetMovementDestination(mousePosition);
        }*/
    }

    public bool CheckMouseInsideUnits(List<Unit> units)
    {
        List<Vector3> points = new List<Vector3>();
        foreach(Unit unit in units){
            points.Add(unit.transform.position);
        }

        foreach(Unit unit in units){
            if(Vector3.Distance(centerPoint, mousePosition) <= Vector3.Distance(centerPoint, unit.transform.position)){
                return true;
            }
        }

        return false;
    }

    public void GroupMovement(List<Unit> units)
    {
        //Debug.Log("Group Movement");
        foreach(Unit unit in units){
            unit.MoveToTarget(CalcGroupPoint(unit.transform.position));
        }
    }

    public Vector3 CalcGroupPoint(Vector3 currentPoint)
    {
        Vector3 centerCurrent = currentPoint - centerPoint; 
        return (mousePosition + centerCurrent);
    }

    public void ClumpMovement(List<Unit> units)
    {
        //Debug.Log("Clump Movement");
        foreach(Unit unit in units){
            unit.MoveToTarget(mousePosition);
        }
        StartCoroutine(CheckIfUnitsAreClumped(units, mousePosition));
    }

    IEnumerator CheckIfUnitsAreClumped(List<Unit> units, Vector3 destination)
    {
        List<Unit> removeUnits = new List<Unit>();
        removeUnits.Clear();

        foreach(Unit unit in units){
            if(Vector3.Distance(unit.agent.destination, destination) > 0.1f){
                //Debug.Log(Vector3.Distance(unit.agent.destination, destination));
                removeUnits.Add(unit);
            }
        }

        foreach(Unit unit in removeUnits){
            units.Remove(unit);
        }

        
        while(!CalcDensityOfUnits(units, destination, manualClumpTreshhold)){
            yield return new WaitForSeconds(0.1f);
            //Debug.Log(CalcDensityOfUnits(units, destination, manualClumpTreshhold));
        }

        foreach(Unit unit in units){
            unit.MoveToTarget(unit.transform.position);
        }
    }

    public bool CalcDensityOfUnits(List<Unit> units, Vector3 center, float treshhold)
    {
        float areaOfUnits = 0;
        float areaOfCircle;
        Vector3 centerOfCirlce;
        float radiusOfCircle;

        foreach(Unit unit in units){
            areaOfUnits += Mathf.PI * unit.radius * unit.radius;
        }

        areaOfCircle = areaOfUnits * treshhold;

        radiusOfCircle = Mathf.Sqrt(areaOfCircle / Mathf.PI);

        List<Vector3> unitPoints = new List<Vector3>();

        foreach(Unit unit in units){
            unitPoints.Add(unit.transform.position);
        }

        if(center == Vector3.zero){
            centerOfCirlce = CalcCenterPoint(unitPoints);
        }else{
            centerOfCirlce = center;
        }

        debugCube2.transform.position = centerOfCirlce;

        foreach(Unit unit in units){
            if(Vector3.Distance(unit.transform.position, centerOfCirlce) <= radiusOfCircle){
    
            }else{
                return false;
            }
        }

        return true;

    }

    public Vector3 CalcCenterPoint(List<Vector3> points){

        Vector3 center = Vector3.zero;
        float x = 0;
        float y = 0;
        float z = 0;

        foreach(Vector3 point in points){
            x += point.x;
            y += point.y;
            z += point.z;
        }
        x /= points.Count;
        y /= points.Count;
        z /= points.Count;

        center = new Vector3(x,y,z);

        return center;
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

    public void AreaSelection()
    {
        //StopCoroutine(HoldTime());
        areaSelect = false;
        selectionSquare.SetActive(false);
    }

    public void DisplaySelectionSquare()
    {
        if(areaSelect){
            float width;
            float hight;
            Vector3 center;
            Vector3 p1;
            Vector3 p3;

            p1 = Camera.main.WorldToScreenPoint(selectStart);
            p3 = Camera.main.WorldToScreenPoint(selectEnd);

            center = (p1 + p3) / 2f;

            width = Mathf.Abs(p1.x - p3.x);
            hight = Mathf.Abs(p1.y - p3.y);

            selectionSquare.GetComponent<RectTransform>().position = center;
            selectionSquare.GetComponent<RectTransform>().sizeDelta = new Vector2(width, hight);

            Vector3 p2;
            Vector3 p4;

            Ray p2Ray;
            Ray p4Ray;

            p2 = new Vector3(p3.x,p1.y,0);
            p4 = new Vector3(p1.x,p3.y,0);

            p2Ray = Camera.main.ScreenPointToRay(p2);
            p4Ray = Camera.main.ScreenPointToRay(p4);

            RaycastHit hitInfo2;
            RaycastHit hitInfo3;

            bool ray2 = Physics.Raycast(p2Ray, out hitInfo2, 1000f, LayerMask.GetMask("Map"));
            bool ray3 = Physics.Raycast(p4Ray, out hitInfo3, 1000f, LayerMask.GetMask("Map"));

            selectP2 = hitInfo2.point;
            selectP3 = hitInfo3.point;

            CheckUnitsInSelection();

        }
    }

    public void CheckUnitsInSelection()
    {
        bool check1;
        bool check2;

        foreach(PlayerUnit unit in pUnits){
            check1 = testIfPointInTriangle(unit.transform.position, selectStart, selectP2, selectP3);
            check2 = testIfPointInTriangle(unit.transform.position, selectP2, selectEnd, selectP3);

            if(check1 || check2){
                AddToHovored(unit);
            }else{
                if(!unit.mouseOver){
                    DeHovor(unit);
                }
            }
        }
    }

    public bool testIfPointInTriangle(Vector3 pointToTest, Vector3 p1, Vector3 p2, Vector3 p3)
    {

        float x = pointToTest.x;
        float y = pointToTest.z;
        float x1 = p1.x;
        float y1 = p1.z;
        float x2 = p2.x;
        float y2 = p2.z;
        float x3 = p3.x;
        float y3 = p3.z;

        float denominator = ((y2 - y3)*(x1 - x3) + (x3 - x2)*(y1 - y3));
        float a =  ((y2 - y3)*(x - x3) + (x3 - x2)*(y - y3)) / denominator;
        float b = ((y3 - y1)*(x - x3) + (x1 - x3)*(y - y3)) / denominator;
        float c = 1 - a - b;

        if(0 <= a && a <= 1 && 0 <= b && b <= 1 && 0 <= c && c <= 1){
            return true;
        }else{
            return false;
        }

    }

    IEnumerator HoldTime()
    {
        yield return new WaitForSeconds(0.1f);
        //areaSelect = true;
        if(areaSelect){
            selectionSquare.SetActive(true);
        }
    }

    public void AddToHovored(Unit unit)
    {
        if(!hoveredUnits.Contains(unit)){
            unit.Hovor();
            hoveredUnits.Add(unit);
        }
    }

    public void DeHovor(Unit unit)
    {
        if(hoveredUnits.Contains(unit)){
            unit.DeHovor();
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

    public void AddPlayerUnit(PlayerUnit unit)
    {
        if(!pUnits.Contains(unit)){
            pUnits.Add(unit);
        }
    }

    public void AddEnemyUnit(EnemyUnit unit)
    {
        if(!eUnits.Contains(unit)){
            eUnits.Add(unit);
        }
    }

}
