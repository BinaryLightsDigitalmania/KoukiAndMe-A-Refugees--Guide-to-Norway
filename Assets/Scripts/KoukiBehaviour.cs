using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum TypeKoukiBehaviour {
SlideEnter, 
Cheer, 
Speak, 
askQuestion, 
EndAskQuestion, 
laugh, 
Wave,
    changeclothes, 
}
public class KoukiBehaviour : MonoBehaviour
{
    public GameObject dialogueBox;
    public float timeToBlink = 0;
    GameObject endEntryPath;
    GameObject startEntryPath;
    bool isInEntryAnimation = false;
    public Sprite spriteBody;
    public Sprite spriteShoe;
    public Sprite spriteHand;
    public Sprite spriteHand2;
    public GameObject Scarf;
    public GameObject Bonnet;
    public Image ImageBody;
    public Image[] ImageShoe;
    public Image[] ImageHand;
    public Image[] ImageHand2;

    public void ChangeClothes()
    {
        Scarf.SetActive(true);
        Bonnet.SetActive(true);
        ImageBody.sprite = spriteBody;
        for (int i = 0; i < ImageShoe.Length; i++)
        {
            ImageShoe[i].sprite = spriteShoe;
        }
        for (int i = 0; i < ImageHand.Length; i++)
        {
            ImageHand[i].sprite = spriteHand;
        }
        for (int i = 0; i < ImageHand2.Length; i++)
        {
            ImageHand2[i].sprite = spriteHand2;
        }
    }

    public void SlideEnter(GameObject ActualEndEntryPath, GameObject ActualStartEntryPath)
    {


        endEntryPath = ActualEndEntryPath;
        startEntryPath = ActualStartEntryPath;
        transform.position = ActualStartEntryPath.transform.position;
        isInEntryAnimation = true;
        GetComponent<Animator>().SetTrigger("Enter");
    }
    public void EndAnimationSlide()
    {
        isInEntryAnimation = false;
        scenar.PlayComportementNext();
    }
    public ScenarioSteps scenar;
    public void InitEntryMotion()
    {

    }
    public void Cheer()
    {
        GetComponent<Animator>().SetTrigger("cheerTrigger");
    }
    public void Speak()
    {
        GetComponent<Animator>().SetTrigger("talkTrigger");
    }
    public void AskQuestion()
    {

        GetComponent<Animator>().SetTrigger("presentQuestion");

    }

    public void EndAsking()
    {
        GetComponent<Animator>().SetTrigger("reversePresentQuestion");
    }

    public void Laugh()
    {
        GetComponent<Animator>().SetTrigger("laughTrigger");

    }
    public void Idle()
    {


    }
    public void WaveAnimation()
    {

        GetComponent<Animator>().SetTrigger("waveTrigger");
    }
    void Start()
    {
        //transform.position = startEntryPath.transform.position;
        timeToBlink = Random.Range(0.5f, 5f);
    }
    void Update()
    {

        if (isInEntryAnimation && endEntryPath != null && startEntryPath != null)
        {
            //transform.position = Vector2.Lerp (startEntryPath.transform.position,endEntryPath.transform.position,Mathf.Abs(startEntryPath.transform.position.x - endEntryPath.transform.position.x) / 100 * 5);
            transform.position = Vector2.MoveTowards(transform.position, endEntryPath.transform.position, Mathf.Abs(startEntryPath.transform.position.x - endEntryPath.transform.position.x) / 100 * 5);
        }

        timeToBlink -= Time.deltaTime;
        if (timeToBlink <= 0)
        {
            gameObject.GetComponent<Animator>().SetTrigger("blinkTrigger");
            timeToBlink = Random.Range(0.5f, 10f);
        }



    }


}
