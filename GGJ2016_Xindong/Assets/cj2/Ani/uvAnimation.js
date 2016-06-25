#pragma strict

var speed : float = 0.5;
var uAxis : boolean;
var vAxis : boolean;
var xoffset : float;
var yoffset : float;

function Update(){
	if(uAxis == true){
		 xoffset = Time.time * speed;
		 if(null != GetComponent.<Renderer>() && null != GetComponent.<Renderer>().sharedMaterial)
	    	GetComponent.<Renderer>().sharedMaterial.SetTextureOffset("_MainTex",Vector2(xoffset,yoffset));  
	}
	
	 if(vAxis == true) {
		 yoffset = Time.time * speed;
         if(null != GetComponent.<Renderer>() && null != GetComponent.<Renderer>().sharedMaterial)
	    	GetComponent.<Renderer>().sharedMaterial.SetTextureOffset("_MainTex",Vector2(xoffset,yoffset)); 
	}
	


}
