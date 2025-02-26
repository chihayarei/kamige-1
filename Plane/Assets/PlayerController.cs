using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 2f;
    private Rigidbody2D rb;
    private bool isGrounded;
    private bool gravityInverted = false; //�d�͂����]���Ă��邩�ǂ���

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //���E�ړ�
        float move = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(move * speed, rb.velocity.y);

        //�ʏ�W�����v
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce * (gravityInverted ? -1 : 1), ForceMode2D.Impulse);
        }

        //V�L�[�ŏd�͔��]
        if (Input.GetKeyDown(KeyCode.V))
        {
            gravityInverted = !gravityInverted; //���]�t���O��؂�ւ�
            rb.gravityScale *= -1; //�d�͂𔽓]
            transform.Rotate(90f, 0f, 0f); //�v���C���[���㉺���]
        }


        if (transform.position.y < -10) //��ʊO(y=-10��艺�ɗ�������
        {
            Respawn();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("Spikes"))
        {
            Respawn(); //���X�|�[�����������s
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }


    void Respawn()
    {
        transform.position = new Vector3(0, 0, 0); //�����ʒu�ɖ߂�
        rb.velocity = Vector2.zero; //���x�����Z�b�g
    }
}
