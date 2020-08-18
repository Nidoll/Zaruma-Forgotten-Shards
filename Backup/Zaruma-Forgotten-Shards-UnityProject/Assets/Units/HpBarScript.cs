using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBarScript : MonoBehaviour
{
    private Vector2 positionRange;
    private float width;
    public float hight;
    public float y;
    public RectTransform foreground;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ChangeRotation());
        width = foreground.rect.width;
        positionRange.x = 0 - width / 2;
        positionRange.y = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHealthBar(float hpPercent)
    {
        float value = Mathf.Abs(positionRange.x) * hpPercent;
        foreground.rect.Set(value, 0, width * hpPercent, hight);
        
        foreground.sizeDelta = new Vector2(width * hpPercent, hight);
        foreground.localPosition = new Vector3(value, 0, 0);
    }

    IEnumerator ChangeRotation()
    {
        while(true){
            Vector3 cam = Camera.main.transform.position;
            Vector3 pos = transform.position; 
            Vector3 lookDirection = new Vector3(0, cam.y, cam.z) - new Vector3(0, pos.y, pos.z);
            Quaternion rotation = Quaternion.LookRotation(lookDirection);
            transform.rotation = rotation;
            
            yield return new WaitForSeconds(0.01f);    
        }

    }
}
