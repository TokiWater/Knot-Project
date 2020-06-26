using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour {

    public GameObject[] vertices = new GameObject[80];
	public GameObject[] vessels = new GameObject[79];
    LineRenderer line;
	float x = 0.0f;
    float z = 0.0f;
    float spaceX = 0.3f;
	float randRadTheta = 0.0f;
	float randRadPhi = 0.0f;
	float objX = 0.0f;
	float objY = 0.0f;
    float objZ = 0.0f;
	float mag;
	float thrust = 300.0f;
	Rigidbody rb;
	CapsuleCollider cc;
	Vector3 vector = Vector3.zero;
	Vector3 vectorStart;
	Vector3 vectorEnd;
	Vector3 center = new Vector3(4.0f, 0.0f, 0.0f);
	Vector3 pull  = new Vector3(0.1f, 0.0f, 0.0f);
    Vector3 prev;
	Vector3 ppei = Vector3.zero;
    Vector3 moveDirection = Vector3.zero;
 	Vector3 check = Vector3.zero;
    Vector3 pullVectorF;
	Vector3 pullVectorB;
	Vector3[] vectorStartPrev = new Vector3[79];
	Vector3[] vectorEndPrev = new Vector3[79];
	int time = 0;
	
	
	//int spaceZ = 1;
	
    void Awake(){
        line = GetComponent<LineRenderer>();
        line.material =  new Material(Shader.Find("Unlit/Color"));
        line.positionCount = vertices.Length;
		
		int idx = 0;

        foreach (GameObject v in vertices)
        {      
	 
		 objX = -2;
		 objY = -2;
         objZ = -2; 
	    prev  = new Vector3(objX, 0, objZ);
		check = prev - center;
		 while(objY < -1 || check.magnitude > 4 || check.magnitude < 3.5 || Vector3.Dot(ppei, moveDirection) > 0 ){
		 
		// while(objX < -1 || objX > 5 ||  objY < -1){	
		   // randRadTheta = Random.Range(0, 2.0f * Mathf.PI);
           // randRadPhi = Random.Range(0, 3.0f * Mathf.PI / 4.0f);   
		   
		       randRadTheta = Random.Range(0, 2.0f * Mathf.PI);
               randRadPhi = Random.Range(0,  Mathf.PI ); 
		    
           // v.GetComponent<MeshRenderer> ().enabled = false;
		   moveDirection = new   Vector3(Mathf.Cos(randRadPhi),  Mathf.Sin(randRadPhi) * Mathf.Sin(randRadTheta),  Mathf.Sin(randRadPhi) * Mathf.Cos(randRadTheta));
			v.transform.position = vector +  spaceX * moveDirection;
			objX = v.transform.position.x;
		    objY = v.transform.position.y;
            objZ = v.transform.position.z; 	
			prev = new Vector3(objX, 0, objZ);
			ppei = vector - center;
			check = prev - center; 
			moveDirection.y = 0;
			
		}
			
			//v.GetComponent<MeshRenderer> ().enabled = false;
			if(idx != 0){
			   vectorStartPrev[idx - 1] = vector;
			   vectorEndPrev[idx - 1] = v.transform.position;
		    }
			vector = v.transform.position;
			idx++;
		  //  x += spaceX;
			//z += spaceZ; 
   		}
	
	}


    void Start () 
    {		
      //  line = GetComponent<LineRenderer>();
      //  line.material =  new Material(Shader.Find("Unlit/Color"));
     //   line.positionCount = vertices.Length;

     //   foreach (GameObject v in vertices)
      //  {            
            //v.GetComponent<MeshRenderer> ().enabled = false;
	//		v.transform.position = new Vector3(x, z, 0);
	//	    x += spaceX;
	//		z += spaceZ; 
   	//	}
    }
	
    void Update () 
    {
        int idx = 0;
        foreach (GameObject v in vertices)
        {   
		   if(idx == 0 && time > 100){
			 rb = v.GetComponent<Rigidbody>();
		   //  v.transform.position = pull + v.transform.position;
		   //  if(time == 101){
			 //   pullVectorF = v.transform.forward;
				//}
			  rb.AddForce(-transform.forward * thrust);
		   }
	       if(idx == 79 && time > 100){
			   GameObject finalV = vertices[idx];
			  rb = v.GetComponent<Rigidbody>();
			 //  v.transform.position = pull + v.transform.position;
			// if(time == 101){
			  //   pullVectorB = v.transfrom.forward;
			//}
			  rb.AddForce(transform.forward * thrust);
		   }
		
		    if(idx == 0){
			     vectorStart = v.transform.position;
			     
			}
   			vectorEnd = v.transform.position;
		    //vectorEndPrev = v.transform.position;
			if(idx != 0){
		        createVessel(vectorStart, vectorEnd, vectorStartPrev[idx - 1], vectorEndPrev[idx - 1], idx - 1);
				vectorEndPrev[idx - 1] = vectorEnd;
			    vectorStartPrev[idx - 1] = vectorStart;
				
		    }

			vectorStart = v.transform.position;
         //  line.SetPosition(idx, v.transform.position);
            idx++;
		 
        }
		time++;
    }
	
	void createVessel(Vector3 p1, Vector3 p2, Vector3 pp1, Vector3 pp2, int num){
        Vector3 pos = Vector3.Lerp(p1, p2, 0.5f);
		Vector3 ppos = Vector3.Lerp(pp1, pp2, 0.5f);
		Vector3 diff = p2 - p1;
		mag = diff.magnitude;
		GameObject segObj = vessels[num];
	//	Rigidbody rb = segObj.GetComponent<Rigidbody> ();
		segObj.transform.position = pos;
		segObj.transform.up = diff;
		segObj.transform.localScale = new Vector3(0.3f, mag / 2.0f, 0.3f);
		//cc = segObj.GetComponent<CapsuleCollider> ();
		//cc.height = mag / 2.0f;
		//rb.velocity = ppos - pos;
    }
}
