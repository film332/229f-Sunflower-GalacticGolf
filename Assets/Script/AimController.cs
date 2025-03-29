using UnityEngine;

public class AimController : MonoBehaviour
{
    public Transform aimDirection;
    public LineRenderer aimLine;
    public float aimLength = 10f;
    public float rotationSpeed = 60f;

    void Update()
    {
        // รับอินพุตจากปุ่มลูกศรซ้ายขวา
        float h = Input.GetAxis("Horizontal");
        if (Mathf.Abs(h) > 0.01f)
        {
            aimDirection.Rotate(Vector3.up, h * rotationSpeed * Time.deltaTime);
        }

        // Raycast เพื่อดูว่าจะยิงไปทางไหน
        Vector3 start = aimDirection.position;
        Vector3 dir = aimDirection.forward;

        Ray ray = new Ray(start, dir);
        RaycastHit hit;
        Vector3 endPoint = start + dir * aimLength;

        if (Physics.Raycast(ray, out hit, aimLength))
        {
            endPoint = hit.point;
        }

        // วาดเส้นเล็ง
        aimLine.SetPosition(0, start);
        aimLine.SetPosition(1, endPoint);
    }
}
