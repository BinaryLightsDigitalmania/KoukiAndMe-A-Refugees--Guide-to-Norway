var text = "";
var tashkeel = true;
var hinduNumbers = true;

function Start () {
	gameObject.GetComponent(TextMesh).text = ArabicSupport.ArabicFixer.Fix(text, tashkeel, hinduNumbers);
}

function Update () {

}