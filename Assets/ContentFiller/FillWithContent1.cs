using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class FillWithContent1 : MonoBehaviour
{
    GameObject HeadBox, HeaderShort, HeaderLong, CardOne, CardOneContent, CardTwo, CardTwoContent;
    Text HeaderTextShort, HeaderTextLong, CardOneText, CardTwoText;
    Canvas CardOneOrder, CardTwoOrder;
    Setup activesetup;
    Setup[] allSetups;
    public TextAsset jsonData;

    int activeOption = 1;
    bool CardActive = true;
    bool HeadOpen = false;
    RectTransform HeadBoxTransForm;
    RectTransform HeaderShortTextTransform;
    RectTransform HeaderLongTextTransform;


    // Start is called before the first frame update
    void Start()
    {
        HeadOpen = false;
        HeadBox = GameObject.Find("UI/Head");
        HeadBoxTransForm = HeadBox.GetComponent<RectTransform>();

        HeaderShort = GameObject.Find("UI/Head/TextShort");
        HeaderTextShort = HeaderShort.GetComponent<Text>();
        HeaderShortTextTransform = HeaderShort.GetComponent<RectTransform>();

        HeaderLong = GameObject.Find("UI/Head/TextLong");
        HeaderTextLong = HeaderLong.GetComponent<Text>();
        HeaderLongTextTransform = HeaderTextLong.GetComponent<RectTransform>();

        LeanTween.alphaText(HeaderLongTextTransform, 0, 0f);


        CardOne = GameObject.Find("UI/CardOptionOne");
        CardOneOrder = CardOne.GetComponent<Canvas>();
        CardOneContent = GameObject.Find("UI/CardOptionOne/Text");
        CardOneText = CardOneContent.GetComponent<Text>();

        CardTwo = GameObject.Find("UI/CardOptionTwo");
        CardTwoOrder = CardTwo.GetComponent<Canvas>();
        CardTwoContent = GameObject.Find("UI/CardOptionTwo/Text");
        CardTwoText = CardTwoContent.GetComponent<Text>();

        CardActive = true;

        allSetups = JsonHelper.FromJson<Setup>(jsonData.text);

        activesetup = FindContent(allSetups, activeOption);
        BringActiveCardToFront(CardOneOrder, CardTwoOrder);
        Animatecard(CardOne, CardTwo);
    }

    // Update is called once per frame
    void Update()
    {


        HeaderTextShort.text = activesetup.headShort;
        HeaderTextLong.text = activesetup.headOpen;
        CardOneText.text = activesetup.cardOne;
        CardTwoText.text = activesetup.cardTwo;

        if (SwipeManager.SwipeDirection == Swipe.Down)
        {
            if (!HeadOpen) { 
            OpenHead(HeadBox);
            }
        }

        if (SwipeManager.SwipeDirection == Swipe.Up)
        {
            if (HeadOpen) { 
            CloseHead(HeadBox);
            }
        }

        if (!HeadOpen)
        {
            if (SwipeManager.SwipeDirection == Swipe.Right)
            {
                if (CardActive)
                {
                    activesetup = FindContent(allSetups, activesetup.nextOptionOne);
                }
                else
                {
                    activesetup = FindContent(allSetups, activesetup.nextOptionTwo);
                }

                OpenHead(HeadBox);

            }


            if (SwipeManager.SwipeDirection == Swipe.Left)
            {
                CardActive = !CardActive;


                if (CardActive)
                {
                    BringActiveCardToFront(CardOneOrder, CardTwoOrder);
                    Animatecard(CardOne, CardTwo);
                }
                else
                {
                    BringActiveCardToFront(CardTwoOrder, CardOneOrder);
                    Animatecard(CardTwo, CardOne);
                }
            }
        }
    }


    public Setup FindContent(Setup[] s, int neededEntry)
    {
        for (int i = 0; i < s.Length; i++)
        {
            if (s[i].option == neededEntry)
            {
                return s[i];

            }
        }
        return null;
    }
    public void BringActiveCardToFront(Canvas cardInfront, Canvas cardInBackground)
    {
        cardInfront.sortingOrder = 3;
        cardInBackground.sortingOrder = 2;

    }

    public void Animatecard(GameObject cardInfront, GameObject cardInBackground)
    {
        LeanTween.scale(cardInfront, new Vector2(1.3f, 1.3f), 0.2f).setEase(LeanTweenType.once);
        LeanTween.rotateZ(cardInfront, 18f, 0.5f).setEase(LeanTweenType.once);
        LeanTween.moveLocalX(cardInfront, 340f, 0.5f);

        LeanTween.scale(cardInBackground, new Vector2(1.0f, 1.0f), 0.2f).setEase(LeanTweenType.once);
        LeanTween.rotateZ(cardInBackground, -18f, 0.5f).setEase(LeanTweenType.once);
        LeanTween.moveLocalX(cardInBackground, 457f, 0.5f);
    }

    public void OpenHead(GameObject header)
    {

        HeadOpen = true;
    }

    public void CloseHead(GameObject header)
    {

        HeadOpen = false;
    }

}


