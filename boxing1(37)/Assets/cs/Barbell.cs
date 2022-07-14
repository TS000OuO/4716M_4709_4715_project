using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barbell : MonoBehaviour
{
	public GameObject atker;
    // Start is called before the first frame update
    void Start()
    {
        /*if(WorldController1.worldNo==2){
			Destroy(gameObject,3);
		}*/
		if(atker.tag=="Player"||atker.tag=="Player2"){
			if(WorldController1.worldNo==2){
				Destroy(gameObject,5);
			}
			else{
				Destroy(gameObject,3);
			}
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	void OnTriggerEnter(Collider col){
		if(col.gameObject!=atker){
			col.gameObject.GetComponent<HPControl>().Damage(25,atker);
		}
	}
}
