using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class comOnOff : MonoBehaviour
{
	Animator anim;
	public GameObject computerObj;
	public GameObject boxingRule;
    // Start is called before the first frame update
	void Start(){
		anim = computerObj.GetComponent<Animator>();
		anim.SetTrigger("comOff");
		boxingRule.gameObject.SetActive(false);
	}
	
	void Update(){
		if(anim.GetCurrentAnimatorStateInfo(0).IsName("comoff Animation")){
			boxingRule.gameObject.SetActive(false);
		}
	}
	
    void OnTriggerEnter (Collider col) {
		anim.SetTrigger("comOn");
	}
	
	void OnTriggerStay (Collider col) {
		if(anim.GetCurrentAnimatorStateInfo(0).IsName("com idle2")){
			boxingRule.gameObject.SetActive(true);
		}
	}
	
	void OnTriggerExit (Collider col) {
		anim.SetTrigger("comOff");
	}
}
