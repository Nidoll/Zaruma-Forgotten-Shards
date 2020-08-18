using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KrystalScript : MonoBehaviour
{
    public int crystalAmount;
    public Mesh emptyRepo;

    public int RemoveCrystal(int Amount)
    {
        int returnAmount = 0;

        if(crystalAmount < Amount){
            returnAmount = crystalAmount;
        }else{
            returnAmount = Amount;
        }

        crystalAmount -= Amount;
        DestroyCrystal();
        
        return returnAmount;
    }

    public void DestroyCrystal()
    {
        GetComponent<MeshFilter>().mesh = emptyRepo;
    }

}
