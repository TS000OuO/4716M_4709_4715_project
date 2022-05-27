using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDir : MonoBehaviour
{
	public GameObject Target;
    void Start()
    {
        
    }

    void Update()
    {
        /*Vector3 targetDir = Target.transform.position - transform.position;
		
		float temp = targetDir.x;
		targetDir.x = targetDir.z;
		targetDir.z = -temp;
		
		Vector3 newDir = Vector3.RotateTowards (transform.forward, targetDir, 0.1f, 0.0F);
		transform.rotation = Quaternion.LookRotation (newDir);*/
		
		Vector3 targetDir = Target.transform.position - transform.position;
		
		/*float temp = targetDir.x;
		targetDir.x = targetDir.z;
		targetDir.z = -temp;*/
		
		Vector3 newDir = Vector3.RotateTowards (transform.forward, targetDir, 0.1f, 0.0F);
		newDir.y=0;
		transform.rotation = Quaternion.LookRotation (newDir);	
	    }
}
