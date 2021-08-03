using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class AnimateFields : MonoBehaviour
{
    GameObject HeadBox, HeaderShort, HeaderLong, CardOne,CardTwo;
    Text HeaderTextShort, HeaderTextLong;
    Canvas CardOneOrder, CardTwoOrder;
   
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
            
        CardTwo = GameObject.Find("UI/CardOptionTwo");
        CardTwoOrder = CardTwo.GetComponent<Canvas>();
             
        CardActive = true;

        BringActiveCardToFront(CardOneOrder, CardTwoOrder);
        Animatecard(CardOne, CardTwo);
    }

    // Update is called once per frame
    void Update()
    {

        if (SwipeManager.SwipeDirection == Swipe.Down)
        {
            if (!HeadOpen)
            {
                OpenHead(HeadBox);
            }
        }

        if (SwipeManager.SwipeDirection == Swipe.Up)
        {
            if (HeadOpen)
            {
                CloseHead(HeadBox);
            }
        }

        if (!HeadOpen)
        {
    
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
            if (SwipeManager.SwipeDirection == Swipe.Right)
            {
           
                OpenHead(HeadBox);

            }
        }
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
        LeanTween.moveLocalY(header, 672f, 0.5f).setEase(LeanTweenType.once).setEase(LeanTweenType.easeOutSine);

        LeanTween.size(header.GetComponent<RectTransform>(), new Vector2(780f, 1375.942f), 0.5f).setEase(LeanTweenType.easeOutSine);

        LeanTween.alphaText(HeaderShortTextTransform, 0, 0.1f);

        LeanTween.alphaText(HeaderLongTextTransform, 1, 1.0f);
 
        LeanTween.size(HeaderShortTextTransform, new Vector2(730f, 1250.0f), 0.5f);
        LeanTween.size(HeaderLongTextTransform, new Vector2(730f, 1250.0f), 0.5f);

        HeadOpen = true;
    }

    public void CloseHead(GameObject header)
    {

        LeanTween.alphaText(HeaderShortTextTransform, 1, 1f);

        LeanTween.alphaText(HeaderLongTextTransform, 0, 0.2f);

        LeanTween.moveLocalY(header, 1220f, 0.5f).setEase(LeanTweenType.once);

        LeanTween.size(header.GetComponent<RectTransform>(), new Vector2(780f, 235.0f), 0.5f);

        LeanTween.size(HeaderShortTextTransform, new Vector2(730f, 129.0f), 0.3f);
        LeanTween.size(HeaderLongTextTransform, new Vector2(730f, 129.0f), 0.3f);

        HeadOpen = false;
    }

}


