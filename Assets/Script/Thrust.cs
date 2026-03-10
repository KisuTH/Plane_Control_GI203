using UnityEngine;
using UnityEngine.InputSystem;

public class Thrust : MonoBehaviour
{

    public float enginePower = 20f;
    public float liftBooster = 0.5f;
    public float drag = 0.001f;
    public float angularDrag = 0.001f;


    public float yawPower = 50f;// Turn Speed
    public float pitchPower = 50f;
    public float rollPower = 30f;


    private Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //thrust
        if (Keyboard.current.spaceKey.isPressed)

        {
            rb.AddForce(-transform.up * enginePower);
        }
        //Lift
        Vector3 lift = Vector3.Project(rb.linearVelocity, transform.up);
        rb.AddForce(transform.forward * lift.magnitude * liftBooster);

        //Drag
        rb.linearDamping = rb.linearVelocity.magnitude * drag;
        rb.angularDamping = rb.linearVelocity.magnitude * angularDrag;


    }
    void Update()
    {
        var keyboard = Keyboard.current;
        if (keyboard == null) return;

        float yaw = (Keyboard.current.eKey.isPressed ? 1f : 0f) - (Keyboard.current.qKey.isPressed ? 1f : 0f); 
        yaw *= yawPower;

        float pitch = (Keyboard.current.wKey.isPressed ? 1f : 0f) - (Keyboard.current.sKey.isPressed ? 1f : 0f);
        pitch *= pitchPower;

        float roll = (Keyboard.current.dKey.isPressed ? 1f : 0f) - (Keyboard.current.aKey.isPressed ? 1f : 0f);
        roll *= rollPower;

        rb.AddTorque(transform.forward * yaw);
        rb.AddTorque(transform.right * pitch);
        rb.AddTorque(transform.up * roll);
    }
}
