using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    private Vector2 mousePosition;
    private Vector2 screenSpace;
    public float speed;
    public float offset;
    private Vector2 direction;
    private Vector2 directionKeyBoard;

    public float minRotation;
    public float maxRotation;
    public float minHight;
    public float maxHight;

    private bool mouseMovement = false;


    private float mouseWheel;
    public float scrollSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M)){
            mouseMovement = !mouseMovement;
        }

        checkMousePositionToScreen();

        KeyBoardControlls();
        
        CalcRotation();

    }

    void FixedUpdate()
    {
        if(mouseMovement){
            Camera.main.transform.position += new Vector3(direction.x,0,direction.y) * speed * Time.fixedDeltaTime;
        }

        Camera.main.transform.position += new Vector3(directionKeyBoard.x,0,directionKeyBoard.y) * speed * Time.fixedDeltaTime;
    }

    void checkMousePositionToScreen()
    {
        mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        screenSpace = new Vector2(Screen.width, Screen.height);

        if(mousePosition.x >= screenSpace.x - offset){
            direction = new Vector2(1, direction.y);
        }else if(mousePosition.x <= 0 + offset){
            direction = new Vector2(-1, direction.y);
        }else{
            direction = new Vector2(0,direction.y);
        }

        if(mousePosition.y >= screenSpace.y - offset){
            direction = new Vector2(direction.x, 1);
        }else if(mousePosition.y <= 0 + offset){
            direction = new Vector2(direction.x, -1);
        }else{
            direction = new Vector2(direction.x, 0);
        }
    }

    void KeyBoardControlls()
    {
        if(Input.GetKey(KeyCode.W)){
            directionKeyBoard = new Vector2(directionKeyBoard.x, 1);
        }else if(Input.GetKey(KeyCode.S)){
            directionKeyBoard = new Vector2(directionKeyBoard.x, -1);
        }else{
            directionKeyBoard = new Vector2(directionKeyBoard.x, 0);
        }

        if(Input.GetKey(KeyCode.A)){
            directionKeyBoard = new Vector2(-1, directionKeyBoard.y);
        }else if(Input.GetKey(KeyCode.D)){
            directionKeyBoard = new Vector2(1, directionKeyBoard.y);
        }else{
            directionKeyBoard = new Vector2(0, directionKeyBoard.y);
        }
    }

    void CalcRotation()
    {
        mouseWheel = Input.mouseScrollDelta.y;
        transform.position = new Vector3(transform.position.x, 
                                         Mathf.Clamp(transform.position.y + mouseWheel * scrollSpeed * -10 * Time.fixedDeltaTime, minHight, maxHight), 
                                         transform.position.z);
        
        float deltaRotation = maxRotation - minRotation;
        float deltaHight = maxHight - minHight;
        float currentHight = maxHight - transform.position.y;
        float percent = currentHight / deltaHight;
        transform.eulerAngles = new Vector3(minRotation + deltaRotation * (1-percent), transform.eulerAngles.y, transform.eulerAngles.z);
    }
}
