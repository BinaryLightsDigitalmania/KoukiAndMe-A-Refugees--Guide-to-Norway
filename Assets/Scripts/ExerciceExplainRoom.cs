using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]
public class ExerciceExplainRoomItem {
    public GameObject Name;
    public GameObject PanelHarakatInPosition;
    public GameObject PanelHarakatRandom;
    public GameObject [] HarakatInPosition;
    public Image [] HarakatInPositionBorder;
    public GameObject [] HarakatRandom;
    public void InitGame() {
      Name.SetActive(true);
      PanelHarakatInPosition.SetActive(true);
      PanelHarakatRandom.SetActive(false);
        for (int i = 0; i < HarakatInPosition.Length; i++)
        {
            HarakatInPosition[i].SetActive(true);
        }
        for (int i = 0; i < HarakatInPositionBorder.Length; i++)
        {
            HarakatInPositionBorder[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < HarakatRandom.Length; i++)
        {
            HarakatRandom[i].gameObject.SetActive(false);
        }
      //  HarakatRandom;

}
    public void StartGame() {
        Name.SetActive(true);
        PanelHarakatInPosition.SetActive(true);
        PanelHarakatRandom.SetActive(true);
        for (int i = 0; i < HarakatInPosition.Length; i++)
        {
            HarakatInPosition[i].SetActive(true);
        }
        for (int i = 0; i < HarakatInPositionBorder.Length; i++)
        {
            HarakatInPositionBorder[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < HarakatRandom.Length; i++)
        {
            HarakatRandom[i].gameObject.SetActive(false);
        }

    }
}
public class ExerciceExplainRoom : MonoBehaviour
{
    int idWord;
    int idHarakaClicked;
    int idPositionHarakaClicked;
    int indexHarakaClicked;
    int indexPositionHarakaClicked;
    bool FocusOnPosition;
    public List<ExerciceExplainRoomItem> listItems; 
    public void ClickOnRandomHaraka(int _idHarakaClicked, int _indexHarakaClicked)
    {

        if (FocusOnPosition)
        {
            if (idHarakaClicked == idPositionHarakaClicked)
            {
                listItems[idWord].HarakatInPosition[indexHarakaClicked].SetActive(false);
                listItems[idWord].HarakatInPositionBorder[indexPositionHarakaClicked].gameObject.SetActive(false);
                listItems[idWord].HarakatInPosition[indexPositionHarakaClicked].SetActive(true); 
            }
            else
            {
                FocusOnPosition = false;
                listItems[idWord].HarakatInPositionBorder[indexPositionHarakaClicked].color = Color.blue; 
            }
            idHarakaClicked = -1;
            idPositionHarakaClicked = -1;
            indexHarakaClicked = -1;
            indexPositionHarakaClicked = -1;
        }
        else
        {
            FocusOnPosition = false;
           
            idHarakaClicked = _idHarakaClicked;
            indexHarakaClicked = _indexHarakaClicked;
        }


    }
    public void ClickOnPositionHaraka(int _indexPositionHarakaClicked, int _idPositionHarakaClicked)
    {
        if (!FocusOnPosition)
        {
            FocusOnPosition = true;
            if (idHarakaClicked != -1)
            {
                if (idHarakaClicked == idPositionHarakaClicked)
                {
                    listItems[idWord].HarakatInPosition[indexHarakaClicked].SetActive(false);
                    listItems[idWord].HarakatInPositionBorder[indexPositionHarakaClicked].gameObject.SetActive(false);
                    listItems[idWord].HarakatInPosition[indexPositionHarakaClicked].SetActive(true);
                }
                else
                {
                    FocusOnPosition = false;
                    listItems[idWord].HarakatInPositionBorder[indexPositionHarakaClicked].color = Color.grey;
                }
                idHarakaClicked = -1;
                idPositionHarakaClicked = -1;
                indexHarakaClicked = -1;
                indexPositionHarakaClicked = -1;
            }
            else
            {
                if (indexPositionHarakaClicked != -1)
                {
                    listItems[idWord].HarakatInPositionBorder[indexPositionHarakaClicked].color = Color.grey;
                }
                indexPositionHarakaClicked = _indexPositionHarakaClicked;
                idPositionHarakaClicked = _idPositionHarakaClicked;
                listItems[idWord].HarakatInPositionBorder[indexPositionHarakaClicked].color = Color.blue;

            }
        }
        else
        {
            FocusOnPosition = true;
            indexPositionHarakaClicked = _indexPositionHarakaClicked;
            idPositionHarakaClicked = _idPositionHarakaClicked;
            listItems[idWord].HarakatInPositionBorder[indexPositionHarakaClicked].color = Color.blue;
        }


    }
    public void CheckHarakat()
    {


    }
}
