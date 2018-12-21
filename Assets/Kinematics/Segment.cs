using UnityEngine;

public class Segment : MonoBehaviour
{
    public Transform anchor;

    public Vector3 startPoint;
    public Vector3 endPoint;

    private void Start()
    {
        Refresh();
    }

    public void Follow(Vector3 target)
    {
        transform.LookAt(target);


        //float newRotation = Vector2.SignedAngle(Vector2.right, target - endPoint);
        //transform.localRotation = Quaternion.Euler(0, 0, newRotation);

        transform.position= target;

        //Vector2 newPosition = target - transform.right.xy() * transform.localScale.x / 2;
        //transform.position = newPosition.xyz(0);
        Refresh();
    }

    public void Refresh()
    {
        startPoint = transform.position - transform.forward * transform.localScale.z/1.5f;
        endPoint = transform.position + transform.forward * transform.localScale.z/1.5f ;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(startPoint, 0.2f);
        Gizmos.DrawSphere(endPoint, 0.2f);
    }
}
