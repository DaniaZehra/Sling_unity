using UnityEngine;

public class Float_Player : MonoBehaviour
{
    public float bounceHeight = 0.05f; // How high the object should bounce
    public float bounceSpeed = 1.5f; // Speed of the bounce
    public float returnSpeed = 1.5f; // Speed of returning to original position
    public float bounceDuration = 1f; // Duration of the bounce before returning

    private Vector3 originalPosition;
    private float bounceStartTime;
    private bool isBouncing = true;

    void Start()
    {
        originalPosition = transform.position;
        bounceStartTime = Time.time;
    }

    void Update()
    {
        if (isBouncing)
        {
            float elapsedTime = Time.time - bounceStartTime;
            float bounceOffset = Mathf.Sin(elapsedTime * bounceSpeed) * bounceHeight;

            transform.position = new Vector3(
                originalPosition.x,
                originalPosition.y + bounceOffset,
                originalPosition.z
            );
            if (elapsedTime >= bounceDuration)
            {
                isBouncing = false;
                Debug.Log("Finished bouncing");
            }
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, originalPosition, returnSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, originalPosition) < 0.01f)
            {
                transform.position = originalPosition;
                enabled = false; 
            }
        }
    }
}
