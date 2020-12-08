using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{

    private Vector3 mOffset;

    private float mZCoord;
    void OnMouseDown()
    {
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

        //Store offset = gameobject world pos - mouse world pos
        mOffset = gameObject.transform.position - GetMouseWorldPos();
    }

    private Vector3 GetMouseWorldPos()
    {
        //pixel coordiates (x,y)
        Vector3 mousePoint = Input.mousePosition;

        //z coordinates of game object on screen
        mousePoint.z = mZCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    void OnMouseDrag()
    {
        transform.position = GetMouseWorldPos() + mOffset;


        //Attempt to fix it so it only draws one line
        //var ChildSCriptOff = GetComponentInChildren<CollisionChecker>();
        //ChildSCriptOff.enabled = false;
    }

    /*
    void ChildSCriptOff()
    {
        GameObject..enabled = false;
    }
    */
}
