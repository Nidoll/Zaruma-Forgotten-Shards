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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
