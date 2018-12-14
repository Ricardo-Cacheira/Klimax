using UnityEngine;

public class Segment : MonoBehaviour
{
    public Transform anchor;

    public Vector2 startPoint;
    public Vector2 endPoint;

    private void Start()
    {
        Refresh();
    }

    public void Follow(Vector2 target)
    {
        float newRotation = Vector2.SignedAngle(Vector2.right, target - endPoint);
        transform.localRotation = Quaternion.Euler(0, 0, newRotation);

        Vector2 newPosition = target - transform.right.xy() * transform.localScale.x / 2;
        transform.position = newPosition.xyz(0);
        Refresh();
    }

    public void Refresh()
    {
        startPoint = transform.position - transform.right * transform.localScale.x / 2;
        endPoint = transform.position + transform.right * transform.localScale.x / 2;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(startPoint, 0.2f);
        Gizmos.DrawSphere(endPoint, 0.2f);
    }
}
