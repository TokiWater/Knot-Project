using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    public float coefficient;
	public Vector3 velocity;
	Rigidbody rb;
	
	
	void OnTriggerStay(Collider col){
	   rb = col.GetComponent<Rigidbody>();
	   if ( rb == null){
	       return;
	   }
	   
	   var relativeVelocity = velocity - rb.velocity;
	   
	   rb.AddForce(coefficient * relativeVelocity);
    }
}
