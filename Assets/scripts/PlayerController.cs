using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f; // скорость движения игрока
    public float jumpForce = 5f; // сила прыжка
    private Animator anim; // ссылка на компонент Animator
    private bool isGrounded = true; // проверка на землю

    // Функция Start вызывается перед первым обновлением кадра
    void Start()
    {
        // Получаем ссылку на компонент Animator
        anim = GetComponent<Animator>();
        // Получаем Rigidbody2D персонажа
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        // Блокируем поворот по оси Z
        rb.freezeRotation = true;
    }

    // Функция Update вызывается каждый кадр
    void Update()
    {
        // Получаем текущую вертикальную скорость игрока
        float verticalVelocity = GetComponent<Rigidbody2D>().velocity.y;

        // Если игрок падает, активируем параметр аниматора "fall"
        if (verticalVelocity < -0.1f)
        {
            anim.SetBool("fall", true);
        }
        else
        {
            anim.SetBool("fall", false);
        }

        // Если игрок прыгает, активируем параметр аниматора "jump"
        if (verticalVelocity > 0.1f)
        {
            anim.SetBool("jump", true);
        }
        else
        {
            anim.SetBool("jump", false);
        }



        // движение влево при нажатии клавиши A или стрелки влево
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            anim.SetBool("walk_left", true); // устанавливаем параметр аниматора "walk" в true
        }
        else
        {
            anim.SetBool("walk_left", false); // если игрок не движется вправо, устанавливаем параметр аниматора "walk" в false
        }

        // движение вправо при нажатии клавиши D или стрелки вправо
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            anim.SetBool("walk", true); // устанавливаем параметр аниматора "walk" в true
        }
        else
        {
            anim.SetBool("walk", false); // если игрок не движется вправо, устанавливаем параметр аниматора "walk" в false
        }

        // прыжок при нажатии клавиши Space, если игрок стоит на земле
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false; // устанавливаем флаг на false, чтобы не позволить игроку прыгать в воздухе
            //anim.SetBool("jump", true); // устанавливаем параметр аниматора "jump" в true
        }
    }

    // Функция OnCollisionEnter2D вызывается при столкновении с другим объектом
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Если игрок столкнулся с землей, устанавливаем флаг на true и сбрасываем параметр аниматора "jump" в false
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}