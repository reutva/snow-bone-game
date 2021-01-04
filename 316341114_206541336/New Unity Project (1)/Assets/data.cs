using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class data : MonoBehaviour
{
    public static int CurrentObj = 1; //for initialization

    public static List<GameObject> objects; //the objects by their order
    public static List<List<GameObject>> connectedObjectsList; //list of connection object for each object


    public static List<bool> hasIntersection = new List<bool>(); //for each object check if has intercetions

    private void Start()
    {
        objects = new List<GameObject>(); //init
        connectedObjectsList = new List<List<GameObject>>(); //init
        hasIntersection = new List<bool>(); // init
    }

}
