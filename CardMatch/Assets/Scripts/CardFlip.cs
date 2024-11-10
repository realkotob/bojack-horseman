using UnityEngine;
using UnityEngine.UI;

public class CardFlip : MonoBehaviour
{

    [Header("Settings")]
    [SerializeField]
    private float flipSpeed = 0.5f;

    [Header("References")]
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
        backRotation = transform.rotation;
        frontRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, 180, 0));
        imageFront.enabled = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Debug.Log("MouseButtonDown");
            if (RectTransformUtility.RectangleContainsScreenPoint(GetComponent<RectTransform>(), Input.mousePosition))
            {
                FlipCard();
            }
        }

        ExecuteFlipping();
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
                transform.rotation = Quaternion.RotateTowards(transform.rotation, frontRotation, step * 180);
                var angle = Quaternion.Angle(transform.rotation, frontRotation);
                if (angle < 1f)
                {
                    transform.rotation = frontRotation;
                    isFlipping = false;
                }
                if(angle<90){
                    imageFront.enabled = true;
                    imageBack.enabled = false;
                }
            }
            else
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, backRotation, step * 180);
                var angle = Quaternion.Angle(transform.rotation, backRotation);
                if (angle < 1f)
                {
                    transform.rotation = backRotation;
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
