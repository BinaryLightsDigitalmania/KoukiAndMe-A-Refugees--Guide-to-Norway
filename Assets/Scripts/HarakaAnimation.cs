using UnityEngine;
using System.Collections;

public class HarakaAnimation : MonoBehaviour {
	 Animator anim; 
	public void DesactiveBool(){
		anim = GetComponent<Animator> (); 
		anim.SetBool ("HarakaHint", false); 
	
	}
}
