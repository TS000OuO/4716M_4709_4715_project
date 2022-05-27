using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadWorld : MonoBehaviour {
	
	public string scene;
	public GameObject tpBall;
	public float myTimer;

	void OnTriggerStay (Collider col) {
		if(col.gameObject.tag=="Player"){
			if(tpBall.transform.position.y<2.5){
				tpBall.transform.position += new Vector3(0,2*Time.deltaTime,0);
				tpBall.GetComponent<Collider>().attachedRigidbody.useGravity=false;
			}
			
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
			tpBall.GetComponent<Collider>().attachedRigidbody.useGravity=true;
		}
	}
}


