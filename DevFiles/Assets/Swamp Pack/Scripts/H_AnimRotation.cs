using UnityEngine;
using System.Collections;

public class H_AnimRotation : MonoBehaviour {
	
	public float SpeedX=0;
	public float SpeedY=0;
	public float SpeedZ=0;
	
	public float RotX=0;
	public float RotY=0;
	public float RotZ=0;

	
	// Update is called once per frame
	void Update () {
		
		RotX+=SpeedX*Time.deltaTime;
		RotY+=SpeedY*Time.deltaTime;
		RotZ+=SpeedZ*Time.deltaTime;

		if (RotX>360) RotX-=360;
		else
		if (RotX<0) RotX+=360;
		
		if (RotY>360) RotY-=360;
		else
		if (RotY<0) RotY+=360;
		
		if (RotZ>360) RotZ-=360;
		else
		if (RotZ<0) RotZ+=360;
		
		transform.localRotation=Quaternion.Euler(RotX,RotY,RotZ);
	}
}
