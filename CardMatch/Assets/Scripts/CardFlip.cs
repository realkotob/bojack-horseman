using UnityEngine;
using UnityEngine.UI;

public class CardFlip : MonoBehaviour
{

    [Header("Settings")]
    [SerializeField]
    private float flipSpeed = 0.5f;

    [Header("References")]
    [SerializeField]
    private Transform rotationParent;
    [SerializeField]
    private Image imageBack;
    [SerializeField]
    private Image imageFront;

    private Quaternion frontRotation;
    private Quaternion backRotation;

    private bool isOpen = false;
    private bool isFlipping = false;

    void Start()
    {
        backRotation = rotationParent.rotation;
        frontRotation = Quaternion.Euler(rotationParent.eulerAngles + new Vector3(0, 180, 0));
        imageFront.enabled = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)
        {
            if (Input.touchCount > 0)
            {
                CheckPressed(Input.GetTouch(0).position);
            }
            else
            {
                CheckPressed(Input.mousePosition);
            }
        }

        ExecuteFlipping();
    }

    private void CheckPressed(Vector2 inputPosition)
    {
        if (RectTransformUtility.RectangleContainsScreenPoint(GetComponent<RectTransform>(), inputPosition))
        {
            FlipCard();
        }
    }

    private void FlipCard()
    {
        isOpen = !isOpen;
        isFlipping = true;
        Debug.Log("Flip card to " + isOpen);
    }

    private void ExecuteFlipping()
    {
        if (isFlipping)
        {
            float step = flipSpeed * Time.deltaTime;
            if (isOpen)
            {
                rotationParent.rotation = Quaternion.RotateTowards(rotationParent.rotation, frontRotation, step * 180);
                var angle = Quaternion.Angle(rotationParent.rotation, frontRotation);
                if (angle < 1f)
                {
                    rotationParent.rotation = frontRotation;
                    isFlipping = false;
                }
                if (angle < 90)
                {
                    imageFront.enabled = true;
                    imageBack.enabled = false;
                }
            }
            else
            {
                rotationParent.rotation = Quaternion.RotateTowards(rotationParent.rotation, backRotation, step * 180);
                var angle = Quaternion.Angle(rotationParent.rotation, backRotation);
                if (angle < 1f)
                {
                    rotationParent.rotation = backRotation;
                    isFlipping = false;
                }
                if (angle < 90)
                {
                    imageFront.enabled = false;
                    imageBack.enabled = true;
                }
            }
        }
    }
}
