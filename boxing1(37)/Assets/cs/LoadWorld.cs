using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadWorld : MonoBehaviour {
	
	public string scene;
	public GameObject tpBall;
	public float myTimer;
	float timer;

    void Start()
    {
        timer = myTimer;
		
	}

    void OnTriggerStay (Collider col) {


			if (col.gameObject.tag=="Player"){
			
				tpBall.transform.position += new Vector3(0,0, 2 * Time.deltaTime);
			
			if(myTimer>0){
				myTimer-=1*Time.deltaTime;
			}
			else{
				SceneManager.LoadScene(scene);
			}
		}
	}
	
	void OnTriggerExit (Collider col) {
		if(col.gameObject.tag=="Player"){
			tpBall.transform.position = new Vector3((float)2.81, (float)2.6, (float)0.7);
			myTimer = timer; 
		}
	}
}


