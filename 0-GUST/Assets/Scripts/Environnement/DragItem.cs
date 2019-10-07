using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragItem : MonoBehaviour
{

    private Vector2 goal = new Vector2(5, 5);
    public float speed = 0.5f;
    public float attractionRange = 2f;
    public float pickUpRange = 1f;

    public bool pickedUp;

    private AudioSource pickUpSound;

    public GameObject character;
    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        if (character == null)
        {
            character = GameObject.Find("BasicCharacter");
        }

        if (gameManager == null)
        {
            gameManager = GameObject.Find("GameHandler").GetComponent<GameManager>();
        }

        pickUpSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        this.goal = character.transform.position;
        Vector2 direction = (this.goal - (Vector2)this.transform.position);
        float distance = direction.magnitude;
        if (distance < attractionRange)
        {
            if (distance > pickUpRange)
            {
                direction = direction.normalized;
                this.transform.position += new Vector3(direction.x, direction.y, 0) * speed * Time.deltaTime * attractionRange / distance;
            }
            else
            {
                if (!pickedUp)
                {
                    gameManager.PickUp();
                    pickUpSound.Play();
                    StartCoroutine(waitForSound());
                    pickedUp = true;
                    GetComponent<Renderer>().enabled = false;
                }
                

            }
        }
    }

    IEnumerator waitForSound()
    {
        while (pickUpSound.isPlaying)
        {
            yield return null;
        }
        Destroy(gameObject);
    }
}
