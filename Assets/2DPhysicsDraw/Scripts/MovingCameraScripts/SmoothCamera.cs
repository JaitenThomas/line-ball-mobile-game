using UnityEngine;
using System.Collections;

public class SmoothCamera : MonoBehaviour {

    public Transform target;
	public Vector3 offset;
   

	void Start(){
		offset = target.transform.position;
	}

    void Update()
    {
        if (target)
		{
			
			transform.position = target.transform.position - offset;
        }

    }
}
