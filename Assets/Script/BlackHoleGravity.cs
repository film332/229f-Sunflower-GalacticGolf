using UnityEngine;

public class BlackHoleGravity : MonoBehaviour
{
    public float gravityStrength = 10f;        // ความแรงของแรงดูด
    public float pullRadius = 10f;             // ระยะที่เริ่มมีแรงดูด

    void FixedUpdate()
    {
        // หาวัตถุทั้งหมดในรัศมี
        Collider[] affectedObjects = Physics.OverlapSphere(transform.position, pullRadius);

        foreach (Collider col in affectedObjects)
        {
            Rigidbody rb = col.attachedRigidbody;

            if (rb != null && rb.gameObject.CompareTag("GolfBall")) // ใส่ tag "GolfBall" ให้ลูกด้วยนะ
            {
                // คำนวณทิศทางดูดเข้า
                Vector3 direction = (transform.position - rb.position).normalized;

                // ดึงเข้าไปด้วยแรงตามสูตร Newton
                float distance = Vector3.Distance(transform.position, rb.position);
                float forceMagnitude = gravityStrength / (distance * distance); // F ∝ 1/r^2

                rb.AddForce(direction * forceMagnitude, ForceMode.Acceleration);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        // วาดวงแสดงรัศมีแรงดูดใน Scene
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, pullRadius);
    }
}
