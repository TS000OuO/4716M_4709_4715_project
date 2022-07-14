using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDir : MonoBehaviour
{
	public GameObject Target;
	public GameObject[] testTarget;
    void Start()
    {
        testTarget=GameObject.FindGameObjectsWithTag("Player");
    }

    void Update()
    {
		testTarget=GameObject.FindGameObjectsWithTag("Player");
		Target=testTarget[0];
		for(int i = 0 ; i < testTarget.Length ; i++){
			if(Vector3.Distance(testTarget[i].transform.position,gameObject.transform.position)<Vector3.Distance(Target.transform.position,gameObject.transform.position)){
				Target=testTarget[i];
			}
		}
		
		//Target
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
