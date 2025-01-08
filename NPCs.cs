using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCs : MonoBehaviour
{

    private Transform NPC;
    public Transform banana;
    public TextMeshProUGUI NPCText;
    private Banana bananaScript;

    // Start is called before the first frame update
    void Start()
    {
        NPC = GetComponent<Transform>();
        bananaScript = FindObjectOfType<Banana>();
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 distance = banana.transform.position - NPC.transform.position;

        if (distance.magnitude < 5)
        {

            Vector3 direction = (banana.transform.position - NPC.transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            lookRotation.x = 0f;
            lookRotation.z = 0f;
            NPC.transform.rotation = lookRotation;

            if (gameObject.CompareTag("NPC1"))
                {
                NPCText.text = ("I'll give ya 10 dollars if you can KICKFLIP between those 2 rails");
                if (bananaScript.Trick1)
                    NPCText.text = ("Rad!");
                }
            if (gameObject.CompareTag("NPC2"))
            {
                NPCText.text = ("I'll give ya 10 dollars if you can do a POP SHOVE IT over each rail");
                if (bananaScript.Trick2)
                    NPCText.text = ("Dope!");
            }
            if (gameObject.CompareTag("NPC3"))
            {
                NPCText.text = ("I'll give ya 10 dollars if you can do ALL 3 TRICKS on top of the highest rail above me");
                if (bananaScript.Trick3)
                    NPCText.text = ("Gnarly!");
            }
            if (gameObject.CompareTag("NPC4"))
            {
                NPCText.text = ("I'll give ya 10 dollars if you can do a VARIAL FLIP over me");
                if (bananaScript.Trick4)
                    NPCText.text = ("Sick!");
            }
            if (gameObject.CompareTag("Bouncer"))
            {
                NPCText.text = ("You need $40 to get in BananaMan");
            }

        }
    }
}
