 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb2d;

    private bool Rightwall = false;

    [SerializeField] private float score;

    [SerializeField] private TextMeshProUGUI scoretxt;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            rb2d.velocity = new Vector2(0, 5);
        }

        // if right wall is hit move left else move right
        if (Rightwall)
        {
            transform.Translate(Vector2.left * (3 * Time.deltaTime));
        }
        else
        {
            transform.Translate(Vector2.right * (3 * Time.deltaTime));
        }

        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "rightwall")
        {
            Rightwall = true;
            transform.localScale = new Vector3 (-0.8f, 0.8f, 0.8f);
            score += 1;
            scoretxt.text = "" + score;
        }

        if (collision.tag == "leftwall")
        {
            Rightwall = false;
            transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            score += 1;
            scoretxt.text = "" + score;
        }

        if (collision.tag == "spike")
        {
            gameObject.SetActive(false);
        }
    }
}
