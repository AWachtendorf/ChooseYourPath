using UnityEngine;
using UnityEngine.EventSystems;




public class SwipeCard : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    bool DragDone;
    public float Rot = 0;
    // Start is called before the first frame update
    void Start()
    {
        DragDone = true;
    }

    // Update is called once per frame
    public void Update()
    {
        Rot = Rotation();

        if (DragDone)
        {

            if (Rot > 1f && Rot < 180f)
            {

                transform.Rotate(Vector3.back * 0.5f);
            }
            if (Rot < 360 && Rot > 180f)
            {
                transform.Rotate(Vector3.forward * 0.5f);
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        DragDone = false;

    }


    public void OnDrag(PointerEventData eventData)
    {

        if (eventData.delta.x < 0)
        {
            if (Rot < 45f || Rot > 330f) { 
            transform.Rotate(Vector3.forward * (eventData.position.x / 300.0f));
            }
        }

        if (eventData.delta.x > 0)
        {
            if (Rot < 45f || Rot > 330f) { 
            transform.Rotate(Vector3.back * (eventData.position.x / 300.0f));
            }
        }
    }
    public float Rotation()
    {
        float rotation = transform.eulerAngles.z;

        if (rotation < 0)
        {
            rotation += 360;
        }
        if (rotation > 360)
        {
            rotation = 0;
        }
        return rotation;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        DragDone = true;


    }
}

