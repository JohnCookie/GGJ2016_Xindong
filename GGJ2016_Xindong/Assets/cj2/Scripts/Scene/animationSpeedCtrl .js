#pragma strict

var playS = 1.0;
var animName1 = "Take 001";
var animName2 = "Take 002";


function Start () {
	if(GetComponent.<Animation>()[animName1] != null)
	GetComponent.<Animation>()[animName1].speed = playS;
	
	if(GetComponent.<Animation>()[animName2] != null)
	GetComponent.<Animation>()[animName2].speed = playS;

}

/*
function Update () {
	if (animation["Take 001"] == null)
		{
			Debug.Log("  ------ null animation in gameobject : " + gameObject.name);
		}
	else
	{
		animation["Take 001"].speed= playS;
	}

}

function Update(){
	animation["Take001"].speed = 0.2;
}
*/