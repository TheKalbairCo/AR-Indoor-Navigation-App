using UnityEngine;
using UnityEngine.EventSystems;

public class ImageZoom : MonoBehaviour, IPointerDownHandler, IScrollHandler
{
    public float zoomSpeed = 0.1f;
    private RectTransform rectTransform;
    private Vector2 originalSizeDelta;
    private bool isPinching;
    private float initialDistance;
    private Vector2 initialSize;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        originalSizeDelta = rectTransform.sizeDelta;
    }

    void Update()
    {
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            if (!isPinching)
            {
                isPinching = true;
                initialDistance = Vector2.Distance(touchZero.position, touchOne.position);
                initialSize = rectTransform.sizeDelta;
            }
            else
            {
                float currentDistance = Vector2.Distance(touchZero.position, touchOne.position);
                float factor = currentDistance / initialDistance;
                rectTransform.sizeDelta = initialSize * factor;

                rectTransform.sizeDelta = new Vector2(
                    Mathf.Clamp(rectTransform.sizeDelta.x, originalSizeDelta.x / 2, originalSizeDelta.x * 3),
                    Mathf.Clamp(rectTransform.sizeDelta.y, originalSizeDelta.y / 2, originalSizeDelta.y * 3)
                );
            }
        }
        else
        {
            isPinching = false;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Necessary for IPointerDownHandler, but not used in this example.
    }

    public void OnScroll(PointerEventData eventData)
    {
        float scroll = eventData.scrollDelta.y;
        rectTransform.sizeDelta += Vector2.one * scroll * zoomSpeed;
        rectTransform.sizeDelta = new Vector2(
            Mathf.Clamp(rectTransform.sizeDelta.x, originalSizeDelta.x / 2, originalSizeDelta.x * 3),
            Mathf.Clamp(rectTransform.sizeDelta.y, originalSizeDelta.y / 2, originalSizeDelta.y * 3)
        );
    }
}
