using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class logic : MonoBehaviour
{

    public Text txt;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 1; i <= transform.childCount; i++)
        {
            if(transform.GetChild(i-1).name != "Text")
            {
                transform.GetChild(i-1).name = i.ToString();
            }
        }
        txt.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < data.hasIntersection.Count; i++)
        {
            if(data.hasIntersection[i])
            {
                txt.gameObject.SetActive(false);
                break;
            }
            if(!data.hasIntersection[i] && i == data.objects.Count-1)
            {
                txt.gameObject.SetActive(true);
            }
            else
            {
                txt.gameObject.SetActive(false);
            }
        }
    }
}
