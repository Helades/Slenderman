using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Torchlight : MonoBehaviour
{
    private Light torchlight;
    public GameObject lantern, gameManager;
    public Text uinums, pagenum, textDetector;
    public Image imgbat;
    public Sprite bat1, bat2, bat3, bat4, bat5;
    public CanvasGroup bateryrecharge, noise;
    public RectTransform noiseScale;
    public AudioClip batsound, switchsound, pickup;
    public AudioSource audiosource, staticSound;
    public Camera ca;
    public LayerMask layer;

    public float batery, detectionDistance, hp;
    private float intensity;
    private int baseb, basep;
    public int npage, batnum;
    private int direction = 1;


    // Start is called before the first frame update
    void Start()
    {
        batery = 100f;
        torchlight = GetComponent<Light>();
        intensity = 4;
        npage = 0;
        batnum = 0;
        imgbat.sprite = bat1;
        bateryrecharge.alpha = 0f;
        hp = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (hp >= 100)
        {
            SceneManager.LoadScene("DeathMenu");
        }

        if (baseb < batnum || basep < npage)
        {
            audiosource.clip = pickup;
            audiosource.Play();
            baseb = batnum;
            basep = npage;
        }

        uinums.text = "Bateries: " + batnum;
        pagenum.text = "Pages: " + npage;

        if (Input.GetButtonDown("Fire1"))
        {
            torchlight.enabled = !torchlight.enabled;
            audiosource.clip = switchsound;
            audiosource.Play();
        }

        if (Input.GetButtonDown("recharge") && batnum > 0)
        {
            audiosource.clip = batsound;
            audiosource.Play();
            batnum--;
            batery = 100f;
            bateryrecharge.alpha = 1f;
        }

        if (bateryrecharge.alpha > 0f)
        {
            bateryrecharge.alpha -= 0.5f * Time.deltaTime;
        }

        if (torchlight.enabled)
        {
            if (batery > 0)
                batery -= Time.deltaTime * 0.5f;
        }

        if (batery > 75f)
        {
            imgbat.sprite = bat1;
            torchlight.intensity = 4;
        }
        else if (batery > 50)
        {
            imgbat.sprite = bat2;
            torchlight.intensity = 3;
        }
        else if (batery > 25)
        {
            imgbat.sprite = bat3;
            torchlight.intensity = 2;
        }
        else if (batery > 0)
        {
            imgbat.sprite = bat4;
            torchlight.intensity = 1;
        }
        else if (batery == 0)
        {
            imgbat.sprite = bat5;
            torchlight.intensity = 0;
        }

        Ray ray = new Ray(ca.transform.position, ca.transform.forward);
        RaycastHit hit;

        int layer_mask = LayerMask.GetMask("Default");

        if (Physics.Raycast(ray, out hit, detectionDistance, layer_mask))
        {
            Debug.Log(hit.transform.name);

            if (hit.transform.GetComponent<Item>() != null)
            {
                GameObject objeto = hit.transform.gameObject;
                Item item = hit.transform.GetComponent<Item>();
                textDetector.text = item.nameObject;

                Debug.Log("Detectado objeto IMPORTANTE");

                if (Input.GetButton("Fire2"))
                {
                    textDetector.text = item.descriptionObject;
                }
                else if (item.nameObject == "Batery" && Input.GetButtonDown("Use"))
                {
                    batnum++;
                    Destroy(objeto);
                    textDetector.text = "";
                }
                else if (item.nameObject == "Page" && Input.GetButtonDown("Use"))
                {
                    gameManager.GetComponent<GameManager>().AddNotes();
                    Destroy(objeto);
                    textDetector.text = "";
                }
            }
            else
            {
                textDetector.text = "";
            }
        }
        else
        {
            textDetector.text = "";
        }

        noise.alpha = hp / 100;
        staticSound.volume = hp / 50;

        if (noiseScale.localScale.x < 1.05 && noiseScale.localScale.y < 1.05)
        {
            direction = 1;
        }
        else if (noiseScale.localScale.x > 2 && noiseScale.localScale.y > 2)
        {
            direction = -1;
        }

        noiseScale.localScale = new Vector3(noiseScale.localScale.x + Time.deltaTime * direction * 0.25f, noiseScale.localScale.y + Time.deltaTime * direction * 0.25f, noiseScale.localScale.z);
    }

    public void OnDrawGizmos()
    {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(ca.transform.position, ca.transform.forward * detectionDistance);
    }
}




