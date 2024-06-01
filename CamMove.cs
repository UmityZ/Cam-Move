using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMove : MonoBehaviour
{
    [SerializeField] Camera Cam;
    private Vector3 dragOrigin;

    [SerializeField] SpriteRenderer MapRender;
    private float mapMinX, mapMaxX, mapMinY, mapMaxY;

    [SerializeField]private float Stepsize, minCamsize, MaxCamsize;
    void Awake()
    {
        mapMinX = MapRender.transform.position.x - MapRender.bounds.size.x /2f;
        mapMaxX = MapRender.transform.position.x + MapRender.bounds.size.x /2f;

        mapMinY = MapRender.transform.position.y - MapRender.bounds.size.y /2f;
        mapMaxY = MapRender.transform.position.y + MapRender.bounds.size.y /2f;
    }

    // Update is called once per frame
    void Update()
    {
        PanCamera();
        if (Input.mouseScrollDelta.y<0)
        {
            ZoomOut();
        }
        if (Input.mouseScrollDelta.y > 0)
        {
            ZoomIn();
        }
    }
    void PanCamera()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Cam.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.GetMouseButton(0))
        {
            Vector3 difference = dragOrigin - Cam.ScreenToWorldPoint(Input.mousePosition);
            
            
            Cam.transform.position = ClampCam(Cam.transform.position+difference);
        }
    }
    private Vector3 ClampCam(Vector3 TargetPosition)
    {
        float camheight=Cam.orthographicSize;
        float camwidth = Cam.orthographicSize * Cam.aspect;

        float MinX = mapMinX + camwidth;
        float MaxX = mapMaxX - camwidth;
        float MinY = mapMinY + camheight;
        float MaxY = mapMaxY - camheight;

        float newX = Mathf.Clamp(TargetPosition.x, MinX, MaxX);
        float newY = Mathf.Clamp(TargetPosition.y, MinY, MaxY);
        return new Vector3(newX,newY,TargetPosition.z);
    }
    private void ZoomIn()
    {
        float newSize = Cam.orthographicSize - Stepsize;
        Cam.orthographicSize = Mathf.Clamp(newSize, minCamsize, MaxCamsize);
        Cam.transform.position = ClampCam(Cam.transform.position);
    }
    private void ZoomOut()
    {
        float newSize = Cam.orthographicSize + Stepsize;
        Cam.orthographicSize = Mathf.Clamp(newSize, minCamsize, MaxCamsize);
        Cam.transform.position = ClampCam(Cam.transform.position);
    }
}
