@script ExecuteInEditMode
@script RequireComponent (Camera)
var target : Transform;
function Update () {
    transform.LookAt(target);
}