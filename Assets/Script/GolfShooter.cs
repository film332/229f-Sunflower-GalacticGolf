using UnityEngine;
using TMPro;

public class GolfShooter : MonoBehaviour
{
    public int shotCount = 0;  // จำนวนครั้งที่ยิงในด่านนี้

    public Transform aimDirection;      // Drag AimDirection ที่ลูกกอล์ฟมาวาง
    public LineRenderer aimLine;          // Drag AimLine (ที่มี LineRenderer)
    public Rigidbody rb;                // Rigidbody ของลูกกอล์ฟ
    public float maxPower = 10f;          // ค่าแรงสูงสุดที่ชาร์จได้
    public float chargeSpeed = 5f;        // ความเร็วชาร์จแรง
    public float ballMass = 1f;           // มวลลูกกอล์ฟ (ปรับตามดาว)
    public TextMeshProUGUI powerText;     // Drag UI Text สำหรับแสดงค่าแรง
    public TextMeshProUGUI shotText;  // UI สำหรับแสดงจำนวนครั้งที่ยิง


    private float currentPower = 0f;
    private bool isCharging = false;
    private bool isMoving = false;

    void Update()
    {
        // เมื่อบอลไม่เคลื่อนที่ ให้เปิดระบบชาร์จและแสดง UI กับเส้นเล็ง
        if (!isMoving)
        {
            // ชาร์จแรงเมื่อกด Spacebar ค้าง
            if (Input.GetKey(KeyCode.Space))
            {
                isCharging = true;
                currentPower += chargeSpeed * Time.deltaTime;
                currentPower = Mathf.Clamp(currentPower, 0, maxPower);

                // อัปเดต UI แสดงค่าแรง
                if (powerText != null)
                    powerText.text = "Power: " + Mathf.RoundToInt(currentPower);

                // ให้เส้นเล็งแสดงอยู่
                if (aimLine != null && !aimLine.enabled)
                    aimLine.enabled = true;
            }

            // เมื่อปล่อย Spacebar และกำลังชาร์จอยู่ → ยิงลูก
            if (Input.GetKeyUp(KeyCode.Space) && isCharging)
            {
                Shoot();
            }
        }
        else
        {
            // ในขณะที่ลูกเคลื่อนที่ ให้ซ่อน UI (UI จะถูกเคลียร์)
            if (powerText != null)
                powerText.text = "";
        }

        // ตรวจสอบว่าลูกหยุดแล้วหรือยัง (velocity ต่ำมาก)
        if (isMoving && rb.velocity.magnitude < 0.05f)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            isMoving = false;
            currentPower = 0;
        }
    }

    void Shoot()
    {
        // ยิงลูกตามทิศทางของ aimDirection
        Vector3 dir = aimDirection.forward;
        float force = ballMass * currentPower;
        rb.AddForce(dir * force, ForceMode.Impulse);
        isCharging = false;
        isMoving = true;

        // ซ่อนเส้นเล็งและ UI ทันทีเมื่อยิงลูก
        if (aimLine != null)
            aimLine.enabled = false;
        if (powerText != null)
            powerText.text = "";
    }
}
