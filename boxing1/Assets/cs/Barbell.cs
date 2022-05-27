using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barbell : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	void OnTriggerEnter(Collider col){
		col.gameObject.GetComponent<HPControl>().Damage(100,gameObject);
	}
}
