using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(InputHandler))]
public class TopDownCharacterMover : MonoBehaviour
{
    private InputHandler _input;

    [SerializeField]
    private bool RotateTowardMouse;

    public float health;
    [Header("Text")]
    //[SerializeField]
    //private Text healthText;
    [SerializeField]
    private Text speedText;

    public float MovementSpeed;
    [SerializeField]
    private float RotationSpeed;

    [SerializeField]
    private Camera Camera;

    [SerializeField]
    private Vector3 cursorWorld;

    [SerializeField]
    private Animator anim;

    public float damage;

    private void Awake()
    {
        _input = GetComponent<InputHandler>();
    }

    
    void Update()
    {
        

        var targetVector = new Vector3(_input.InputVector.x, 0, _input.InputVector.y);

        
        var movementVector = MoveTowardTarget(targetVector);

       

        if (!RotateTowardMouse)
        {
            RotateTowardMovementVector(movementVector);
        }
        if (RotateTowardMouse)
        {
            RotateFromMouseVector();
        }
        Mouse();
        anim.SetBool("speed", targetVector.magnitude > 0);
    }

    private void RotateFromMouseVector()
    {
        Ray ray = Camera.ScreenPointToRay(_input.MousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, maxDistance: 300f))
        {
            var target = hitInfo.point;
            target.y = transform.position.y;
            transform.LookAt(target);
        }
    }

    private Vector3 MoveTowardTarget(Vector3 targetVector)
    {
        var speed = MovementSpeed * Time.deltaTime;
        // transform.Translate(targetVector * (MovementSpeed * Time.deltaTime)); Demonstrate why this doesn't work
        //transform.Translate(targetVector * (MovementSpeed * Time.deltaTime), Camera.gameObject.transform);

        targetVector = Quaternion.Euler(0, Camera.gameObject.transform.rotation.eulerAngles.y, 0) * targetVector;
        var targetPosition = transform.position + targetVector * speed;
        transform.position = targetPosition;
        return targetVector;
    }

    private void RotateTowardMovementVector(Vector3 movementDirection)
    {
        if (movementDirection.magnitude == 0) { return; }
        var rotation = Quaternion.LookRotation(movementDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, RotationSpeed);
    }

    public void Mouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            cursorWorld = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 17.720f));
            anim.SetTrigger("hitting");
        }

    }
    public void IncreaseSpeed()
    {
        MovementSpeed += 10;
    }
}