using UnityEngine;
using UnityEngine.EventSystems;

public class Window : MonoBehaviour, IDragHandler, IBeginDragHandler
{
    public bool enableAtStart = false;
    private RectTransform rectTransform;
    private Vector2 oldLocalPos; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rectTransform = gameObject.GetComponent<RectTransform>();
        gameObject.SetActive(enableAtStart);
    }

    public void SetActive(bool enabled)
    {
        gameObject.SetActive(enabled);
    }

    public void ChangeActive() //Utiliser car le menu peut appeller SetActive avec false si la fenetre a été fermez avec le close button
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform.parent as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out oldLocalPos
        );

        //Debug.Log("Beginning of drag!");
        //Debug.Log("First local position: " + oldLocalPos);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 localMousePos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform.parent as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out localMousePos
        );

        Vector2 delta = localMousePos - oldLocalPos;
        oldLocalPos = localMousePos;

        Vector3 targetPosition = rectTransform.anchoredPosition3D + new Vector3(delta.x, delta.y, 0);
        rectTransform.anchoredPosition3D = targetPosition;

        //Debug.Log("New local position: " + rectTransform.anchoredPosition3D);
    }


}
