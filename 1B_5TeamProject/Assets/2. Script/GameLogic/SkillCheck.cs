using UnityEngine;
using UnityEngine.UI;

public class SkillCheck : MonoBehaviour
{
    public bool hit = false;

    public float moveSpeed = 1f;

    public Button isSkillCheckBT;
    public Button nopeBT;

    public GameObject skillCheckPanel;

    private Rigidbody2D rb;
    private bool isMovingRight = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        skillCheckPanel.SetActive(false);

        rb = GetComponent<Rigidbody2D>();
        isSkillCheckBT.onClick.AddListener(OpenSkillCheck);
        nopeBT.onClick.AddListener(CloseSkillCheck);
    }

    // Update is called once per frame
    void Update()
    {
        if (isMovingRight)
        {
            rb.linearVelocity = new Vector2(moveSpeed, rb.linearVelocity.y);
        }
        else
        {
            rb.linearVelocity = new Vector2(-moveSpeed, rb.linearVelocity.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isMovingRight = !isMovingRight;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Red"))
        {
            Debug.Log("지금!");
            hit = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Red"))
        {
            Debug.Log("클릭 범위 나감");
            hit = false;
        }
    }

    public void OpenSkillCheck()
    {
        skillCheckPanel.SetActive(true);
    }

    public void CloseSkillCheck()
    {
        skillCheckPanel.SetActive(false);
    }
}
