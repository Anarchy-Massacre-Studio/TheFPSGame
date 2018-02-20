using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class RayCtrl : MonoBehaviour
{
    public GameObject Pointer;

    private RaycastHit hitInfo;
    private LineRenderer lineRenderer;

	void Start ()
    {
        lineRenderer = GetComponentInChildren<LineRenderer>();
        hitInfo = new RaycastHit();
	}
	
	// Update is called once per frame
	void Update ()
    {
        lineRenderer.SetPosition(0, transform.position);

        if(Physics.Raycast(transform.position, - transform.up, out hitInfo))
        {
            lineRenderer.SetPosition(1, hitInfo.point);

            if(Pointer)
            {
                Pointer.SetActive(true);
                Pointer.transform.LookAt(transform);
                Pointer.transform.position = hitInfo.point;
            }
        }
        else
        {
            Pointer.SetActive(false);
            lineRenderer.SetPosition(1, transform.position - transform.up * 10);
        }
    }
}
