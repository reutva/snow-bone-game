using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drawLines : MonoBehaviour
{
    public List<GameObject> connectObj; //the objects the obj will be connected
    public GameObject empty; //just an instantiate has nothing important
    public List<GameObject> createdObjs; //we need them to have more than 1 line
    public List<LineRenderer> lines; //save the lines so they wont disappear

    bool isIntersect = false;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject g in connectObj) //init createdObjs and lines
        {
            GameObject obj = Instantiate(empty, transform.position, Quaternion.identity);
            obj.transform.parent = gameObject.transform;
            LineRenderer lineRenderer = obj.AddComponent<LineRenderer>();
            lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
            lineRenderer.widthMultiplier = 0.2f;
            lineRenderer.SetPosition(0, gameObject.transform.position);
            lineRenderer.SetPosition(1, g.transform.position);
            createdObjs.Add(obj);
            lines.Add(lineRenderer);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(data.CurrentObj.ToString() == gameObject.name)//init data
        {
            data.objects.Add(gameObject);
            data.connectedObjectsList.Add(connectObj);
            data.hasIntersection.Add(false);
            data.CurrentObj += 1;
        }

        for(int i = 0; i < connectObj.Count; i++) //draw the lines between 2 objects
        {
            lines[i].SetPosition(0, gameObject.transform.position);
            lines[i].SetPosition(1, connectObj[i].transform.position);
        }

        if (data.CurrentObj > int.Parse(gameObject.name)) //start do the logic just after init finish
        {
            for (int i = 0; i < data.objects.Count; i++)
            {
                if (data.objects[i].name != gameObject.name)
                {
                    for (int x = 0; x < data.connectedObjectsList[int.Parse(gameObject.name) - 1].Count; x++)
                    {
                        for (int j = 0; j < data.connectedObjectsList[i].Count; j++)
                        {
                            //checks if one of the points is a strat point of 2 lines or end point of 2 points
                            if (transform.position != data.connectedObjectsList[int.Parse(gameObject.name) - 1][x].transform.position && transform.position != data.objects[i].transform.position && transform.position != data.connectedObjectsList[i][j].transform.position
                                && data.connectedObjectsList[int.Parse(gameObject.name) - 1][x].transform.position != data.objects[i].transform.position && data.connectedObjectsList[int.Parse(gameObject.name) - 1][x].transform.position != data.connectedObjectsList[i][j].transform.position
                                && data.objects[i].transform.position != data.connectedObjectsList[i][j].transform.position)
                            {
                                isIntersect = doIntersect(transform.position, data.connectedObjectsList[int.Parse(gameObject.name) - 1][x].transform.position, data.objects[i].transform.position, data.connectedObjectsList[i][j].transform.position);
                                data.hasIntersection[int.Parse(gameObject.name)-1] = isIntersect;
                                if (isIntersect) //if there is intersection color by red the line and break the loop
                                {
                                    lines[x].SetColors(Color.red, Color.red);
                                    break;
                                }
                                else
                                {
                                    lines[x].SetColors(Color.green, Color.green);
                                }
                            }
                        }
                        if(isIntersect)
                        {
                            break;
                        }
                    }
                    if(isIntersect)
                    {
                        break;
                    }
                }
                if(isIntersect)
                {
                    break;
                }
            }
        }
        isIntersect = false;
    }


    //stole from internet - check if 2 lines are intersected

    static bool onSegment(Vector2 p, Vector2 q, Vector2 r)
    {
        if (q.x <= Mathf.Max(p.x, r.x) && q.x >= Mathf.Min(p.x, r.x) &&
            q.y <= Mathf.Max(p.y, r.y) && q.y >= Mathf.Min(p.y, r.y))
            return true;

        return false;
    }

    // To find orientation of ordered triplet (p, q, r). 
    // The function returns following values 
    // 0 --> p, q and r are colinear 
    // 1 --> Clockwise 
    // 2 --> Counterclockwise 
    static int orientation(Vector2 p, Vector2 q, Vector2 r)
    {
        // See https://www.geeksforgeeks.org/orientation-3-ordered-points/ 
        // for details of below formula. 
        float val = (q.y - p.y) * (r.x - q.x) - (q.x - p.x) * (r.y - q.y);

        if (val == 0) return 0; // colinear 

        return (val > 0) ? 1 : 2; // clock or counterclock wise 
    }

    // The main function that returns true if line segment 'p1q1' 
    // and 'p2q2' intersect. 
    static bool doIntersect(Vector2 p1, Vector2 q1, Vector2 p2, Vector2 q2)
    {
        // Find the four orientations needed for general and 
        // special cases 
        int o1 = orientation(p1, q1, p2);
        int o2 = orientation(p1, q1, q2);
        int o3 = orientation(p2, q2, p1);
        int o4 = orientation(p2, q2, q1);

        // General case 
        if (o1 != o2 && o3 != o4)
            return true;

        // Special Cases 
        // p1, q1 and p2 are colinear and p2 lies on segment p1q1 
        if (o1 == 0 && onSegment(p1, p2, q1)) return true;

        // p1, q1 and q2 are colinear and q2 lies on segment p1q1 
        if (o2 == 0 && onSegment(p1, q2, q1)) return true;

        // p2, q2 and p1 are colinear and p1 lies on segment p2q2 
        if (o3 == 0 && onSegment(p2, p1, q2)) return true;

        // p2, q2 and q1 are colinear and q1 lies on segment p2q2 
        if (o4 == 0 && onSegment(p2, q1, q2)) return true;

        return false; // Doesn't fall in any of the above cases 
    }

}
