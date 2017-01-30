using UnityEngine;
using System.Collections;
using System.Collections.Generic; 
public enum TypeScene {
RoomScene, 
kitchenScene, 
RoadScene, 
StoreScene, 
TurtleScene

}
[System.Serializable]
public class Speach
{
    public Sprite spriteSpeach;
    public int id;
    public AudioClip audio;
    public float WaitBeforNext;
    public bool NextIsTriggered; 
	public GameObject responseObjectName;
}
[System.Serializable]
public class RoomExercice {
    public int id;
    public List<Speach>speachExercice;
    public List<ItemShop> itemExercice;
    public int starReward; 
}

[System.Serializable]
public class ItemExplanation
{
    public int id;
    public Sprite ImageItem;
    public List<Haraka> ImageHarakat;
}
[System.Serializable]
public class Haraka {
    public int id; 
    public Transform PositionHarakat;
    public Transform TruePositionItem;

}
[System.Serializable]
public class ExplanationExercice {
    public int id;
    public List<Speach> speachExercice;
    public List<ItemExplanation> itemExercice;
    public int starReward;
    
}
[System.Serializable]
public class Explanation
{
    public int id;
    public TypeScene Scene;
    public List<Speach> speachBeforExercice; 
    public List<ExplanationExercice> Exercices;
    public List<Speach> speachEndExplanation;

}
public class TaskManager : MonoBehaviour {
    public List<Explanation> explanation = new List<Explanation>();
    public static TaskManager Instance; 
}
