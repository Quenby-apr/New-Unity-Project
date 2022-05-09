using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 dir;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravity;
    [SerializeField] private GameObject loserPanel;
    [SerializeField] private Score scoreScript;
    
    private int lineToMove = 1;
    public float lineDistance = 4;

    // Start is called before the first frame update
    void Start()
    {
        loserPanel.SetActive(false);
        controller = GetComponent<CharacterController>();
        Time.timeScale = 1;
        StartCoroutine(SpeedIncrease());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (lineToMove > 0)
            {
                lineToMove--;
            }
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            if (lineToMove < 2)
            {
                lineToMove++;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (controller.isGrounded)
                Jump();
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            dir.y = -3 * jumpForce;
        }

        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
        if (lineToMove == 0)
            targetPosition += Vector3.left * lineDistance;
        else if (lineToMove == 2)
            targetPosition += Vector3.right * lineDistance;

        if (transform.position == targetPosition)
            return;
        Vector3 diff = targetPosition - transform.position;
        Vector3 moveDir = diff.normalized * 125 * Time.deltaTime;
        if (moveDir.sqrMagnitude < diff.sqrMagnitude)
            controller.Move(moveDir);
        else
            controller.Move(diff);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        dir.z = speed;
        dir.y += gravity * Time.fixedDeltaTime;
        controller.Move(dir * Time.fixedDeltaTime);
    }
    private void Jump()
    {
        dir.y = jumpForce;
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "obstacle")
        {
            loserPanel.SetActive(true);
            int score = int.Parse(scoreScript.scoreText.text);
            PlayerPrefs.SetInt("lastScore", score);
            Time.timeScale = 0;
        }
    }

    private IEnumerator SpeedIncrease()
    {
        yield return new WaitForSeconds(3);
        speed += 2;
        StartCoroutine(SpeedIncrease());
    }
}
