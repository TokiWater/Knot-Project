using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Rope : MonoBehaviour {

    public GameObject[] vertices = new GameObject[84];
	public GameObject[] vessels = new GameObject[83];
	public GameObject[] physicalEffects = new GameObject[1];

    LineRenderer line;
	int flag = 0;
	int flagS = 0;
	int flagE = 0;
	float x = 0.0f;
    float z = 0.0f;
    float spaceX = 0.3f;
	float randRadTheta = 0.0f;
	float randRadPhi = 0.0f;
	float objX = 0.0f;
	float objY = 0.0f;
    float objZ = 0.0f;
	float mag;
	float startPos;
	float endPos;
	float thrust = 400.0f;
	float test;
	Rigidbody rb;
	Wind wd;
	int startTime = 0;
	CapsuleCollider cc;
	Vector3 vector = Vector3.zero;
	Vector3 vectorStart;
	Vector3 vectorEnd;
	Vector3 center = new Vector3(1.0f, 0.0f, 0.0f);
	Vector3 end = new Vector3(2.5f, 0.0f, 0.0f);
	Vector3 pull  = new Vector3(0.1f, 0.0f, 0.0f);
    Vector3 prev;
	Vector3 ppei = Vector3.zero;
    Vector3 moveDirection = Vector3.zero;
 	Vector3 check = Vector3.zero;
    Vector3 pullVectorF;
	Vector3 pullVectorB;
	Vector3 posVec;
	Vector3 thrustVec = new Vector3(1.0f, 0.4f, 0.0f);
	Vector3 thrustMVec = new Vector3(-1.0f, 0.4f, 0.0f);
	Vector3 thrustVecStart = new Vector3(0.0f, 1.0f, 0.0f);
	Vector3[] vectorStartPrev = new Vector3[79];
	Vector3[] vectorEndPrev = new Vector3[79];
	int time = 0;
	TextAsset csvFile;
	List<string[]> csvDatas = new List<string[]>();
	
	
	
	
	//int spaceZ = 1;
	
    void Awake(){
		
		int idx = 0;

        foreach (GameObject v in vertices)
        {      
	 
		 objX = -2;
		 objY = -2;
         objZ = -2; 
	    prev  = new Vector3(objX, 0, objZ);
		check = prev - center;
		if(idx < 80){
		 while(objY > 1 || objY < -1 || check.magnitude > 1.3 || check.magnitude < 0.5 || Vector3.Cross(ppei, moveDirection).y > 0 ){
		 
		// while(objX < -1 || objX > 5 ||  objY < -1){	
		   // randRadTheta = Random.Range(0, 2.0f * Mathf.PI);
           // randRadPhi = Random.Range(0, 3.0f * Mathf.PI / 4.0f);   
		   
		       randRadTheta = Random.Range(0, 2.0f * Mathf.PI);
			   
			   test = Random.value;
			    //randRadPhi = Random.Range(- Mathf.PI / 3.0f,  Mathf.PI / 3.0f ); 
			  // if(test > 0.5f ){
              //    randRadPhi = Random.Range(0,  Mathf.PI / 3.0f ); 
			 //  }else{
			//	   randRadPhi = Random.Range(Mathf.PI - Mathf.PI / 3.0f,  Mathf.PI); 
			 //  }
		    
           // v.GetComponent<MeshRenderer> ().enabled = false;
		   // moveDirection = new   Vector3(Mathf.Cos(randRadPhi),  Mathf.Sin(randRadPhi) * Mathf.Sin(randRadTheta),  Mathf.Sin(randRadPhi) * Mathf.Cos(randRadTheta));
	        moveDirection = new Vector3( Mathf.Cos(randRadPhi) * Mathf.Cos(randRadTheta),  Mathf.Sin(randRadPhi),  Mathf.Cos(randRadPhi) * Mathf.Sin(randRadTheta));
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
			//if(idx != 0){
			//   vectorStartPrev[idx - 1] = vector;
			//   vectorEndPrev[idx - 1] = v.transform.position;
		  //  }
			vector = v.transform.position;
			
			}
			
			if(idx > 79){
                moveDirection = end - vector;
				v.transform.position = vector + moveDirection.normalized * spaceX;
				
				vector = v.transform.position;
		    }
			
			
			
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
		      if(flagS == 0){	
			     rb.AddForce(thrustMVec * thrust);
			  }
			  startPos = v.transform.position.x;
		   }
	       if(idx ==  83 && time > 100){
			   GameObject finalV = vertices[idx];
			  rb = v.GetComponent<Rigidbody>();
			 //  v.transform.position = pull + v.transform.position;
			// if(time == 101){
			  //   pullVectorB = v.transfrom.forward;
			//}
			  if(flagE == 0){
			     rb.AddForce(thrustVec * thrust);
		      }
			  endPos = v.transform.position.x;
		   }
		
		    if(idx == 0){
			     vectorStart = v.transform.position;
			     
			}
   			vectorEnd = v.transform.position;
		    //vectorEndPrev = v.transform.position;
			//if(idx != 0){
		   //     createVessel(vectorStart, vectorEnd, idx - 1);
			//	vectorEndPrev[idx - 1] = vectorEnd;
			//    vectorStartPrev[idx - 1] = vectorStart;
				
		   // }

			vectorStart = v.transform.position;
         //  line.SetPosition(idx, v.transform.position);
            idx++;
		 
        }
		
		idx = 0;
		
		foreach (GameObject v in vertices)
		{
		    
		    if(idx == 0){
				vectorStart = v.transform.position;
		    }
		    if(idx != 0){
			   vectorEnd = v.transform.position;
			   createVessel(vectorStart, vectorEnd, idx - 1);
			   vectorStart = v.transform.position;
		    }
			idx++;
		}
		
		if (flag == 1 && startTime == 200){
		   int idnum = 0;
		   Debug.Log("100達成");
		   string filePath = "seed.csv";
		   
		   foreach(GameObject v in vertices){
			   float fileX = v.transform.position.x;
			   float fileY = v.transform.position.y;
			   float fileZ = v.transform.position.z;
		       string myString = "#" + idnum + "#" + "," + fileX + "," + fileY + "," + fileZ + "\n";
			   File.AppendAllText(filePath, myString);	
               idnum++;	   
		   }
	   
			foreach(GameObject v in physicalEffects){
			   wd = v.GetComponent<Wind>();
			   randRadTheta = Random.Range(0, 2.0f * Mathf.PI); 
			   
			   wd.velocity.x = 5 * Mathf.Cos(randRadTheta);
			   wd.velocity.z = 5 * Mathf.Sin(randRadTheta);
			   wd.velocity.y = 5;
			}
			
		   // foreach(GameObject v in vertices){
			 //  rb = v.GetComponent<Rigidbody>();
			
			
			  //if(idnum == 0 || idnum == 83){
			    // rb.mass = 30;
			  // }
			   

			   //idnum++;
			//}
			flag = 2;
	    }
		
		if (startTime == 300){
			foreach(GameObject v in physicalEffects){
			   wd = v.GetComponent<Wind>();
			   wd.velocity.x = 0;
			   wd.velocity.y = 0;
			   wd.velocity.z = 0;
			}
					
		}
		if (startTime == 500){
		   int idnum = 0;
		   csvFile = Resources.Load("xSeed1") as TextAsset;
		   StringReader reader = new StringReader(csvFile.text);
		   while (reader.Peek() != -1){
		       string line = reader.ReadLine();
			   csvDatas.Add(line.Split(','));
		   }
		   Debug.Log("#" + csvDatas[0][1] + "#" + csvDatas[0][2] + "#" + csvDatas[1][0]);
		   foreach(GameObject v in vertices){
		      posVec = new Vector3(float.Parse(csvDatas[idnum][1]), float.Parse(csvDatas[idnum][2]), float.Parse(csvDatas[idnum][3]));
			  v.transform.position = posVec;
			  idnum++;
		   }	
		}
		
		if (startTime == 600){
		    
			foreach(GameObject v in physicalEffects){
			   wd = v.GetComponent<Wind>();
			   int objNum = Random.Range(3, 80);
			   GameObject obj =  vertices[objNum];
			   
			   float windX = obj.transform.position.x;
			   float windY = 0;
			   float windZ = obj.transform.position.z;
			   Vector3 windVector = new Vector3(windX, windY, windZ);
			   v.transform.position = windVector;
			   
			   if(windX < 1){
			      randRadTheta = Random.Range( - Mathf.PI / 2.0f, Mathf.PI / 2.0f); 
			   }else{
				  randRadTheta = Random.Range(  Mathf.PI / 2.0f,  3.0f * Mathf.PI / 2.0f); 
			   }
			   
			   wd.velocity.x = 5 * Mathf.Cos(randRadTheta);
			   wd.velocity.z = 5 * Mathf.Sin(randRadTheta);
			   wd.velocity.y = 5;
			}
					
		}	
		if (startTime == 700){
		   foreach(GameObject v in physicalEffects){
		      wd = v.GetComponent<Wind>();
			  wd.velocity.x = 0;
		      wd.velocity.z = 0;
		      wd.velocity.y = 0;	  
		   }
		   
		   startTime = 400
		   ;
	    }
		
		if(endPos > 3.8){
		   flagE = 1;
	    }
		
		if(startPos < -2){
           flagS = 1;
	    }
		
		if(flagE == 1 && flagS == 1 && flag == 0){
		   flag = 1;
	    }
		
		if(flag == 1 || flag == 2){
		   startTime++;
	   }
		
		//if(time == 1000){
		 // int idnum = 0;
		//   csvFile = Resources.Load("xSeed1") as TextAsset;
		//   StringReader reader = new StringReader(csvFile.text);
		//   while (reader.Peek() != -1){
		//       string line = reader.ReadLine();
		//	   csvDatas.Add(line.Split(','));
		//   }
		//   Debug.Log("#" + csvDatas[0][1] + "#" + csvDatas[0][2] + "#" + csvDatas[1][0]);
		//   foreach(GameObject v in vertices){
		//      posVec = new Vector3(float.Parse(csvDatas[idnum][1]), float.Parse(csvDatas[idnum][2]), float.Parse(csvDatas[idnum][3]));
		//	  v.transform.position = posVec;
		//	  idnum++;
		 //  }
		   
		 //  flag = 1;
	     
       // }
		
		time++;
    }
	
	void createVessel(Vector3 p1, Vector3 p2, int num){
        Vector3 pos = Vector3.Lerp(p1, p2, 0.5f);
		//Vector3 ppos = Vector3.Lerp(pp1, pp2, 0.5f);
		Vector3 diff = p2 - p1;
		mag = diff.magnitude;
		GameObject segObj = vessels[num];
	//	Rigidbody rb = segObj.GetComponent<Rigidbody> ();
		segObj.transform.position = pos;
		segObj.transform.up = diff;
		segObj.transform.localScale = new Vector3(0.2f, mag / 2.0f, 0.2f);
		cc = segObj.GetComponent<CapsuleCollider> ();
		cc.height = 0.1f + mag / 2.0f ;
		//rb.velocity = ppos - pos;
    }
}
