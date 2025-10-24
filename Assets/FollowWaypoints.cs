using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWaypoints : MonoBehaviour
{
    Transform goal;
    float speed = 5.0f;
    float accuracy = 5.0f;
    float rotSpeed = 2.0f;

    public GameObject WPManager;
    GameObject[] WayPoint;
    GameObject currentNode;
    int currentWP = 0;
    Graph g;


    // Start is called before the first frame update
    void Start()
    {
        WayPoint = WPManager.GetComponent<WPManager>().waypoints;
        g = WPManager.GetComponent<WPManager>().graph;
        currentNode = WayPoint[8];

        Invoke("gotoRuim", 1);
    }

    public void GotoHeli()
    {
        g.AStar(currentNode, WayPoint[0]);
        currentWP = 0;
    }
    public void gotoRuin()
    {
        g.AStar(currentNode, WayPoint[11]);
        currentWP = 0;
    }
    // Update is called once per frame
    void LateUpdate()
    {
        if(g.pathList.Count == 0 || currentWP == g.pathList.Count)
        return;

        if(Vector3.Distance(g.pathList[currentWP].getId().transform.position, this.transform.position) < accuracy)
        {
            currentNode = g.pathList[currentWP].getId();
            currentWP++;
        }

        {
            if(currentWP < g.pathList.Count)
            {
                goal = g.pathList[currentWP].getId().transform;
                Vector3 lookAtGoal = new Vector3(goal.position.x, this.transform.position.y, goal.position.z);

                Vector3 direction = lookAtGoal - this.transform.position;
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotSpeed);

                this.transform.Translate(0, 0, speed * Time.deltaTime);
            }
        }
    }
}
