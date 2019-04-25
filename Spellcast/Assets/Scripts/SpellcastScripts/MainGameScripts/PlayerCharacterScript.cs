using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using UnityEngine.Networking;

public class PlayerCharacterScript : NetworkBehaviour {

    private FPSViewScript mouseLook;
    private CharacterController player;
    private SpellSystem spellSys;

    public static bool spawnInUse;

    public GameObject wand, wandColor, tipColor, model;
    public float moveSpeed, jumpForce, gravity, rotateWand, wandHeight, playerHealth;
    public bool isReady;

    private Vector3 move = Vector3.zero;
    private GameObject vrtk, manager, hand, spawnPoint1, spawnPoint2;    
    private Camera fpsCamera;
    private Vector2 moveInput, look;
    private bool jumpTick;
    private KeywordRecognizer recognizer;
    private string[] keywords = new string[] { "fireball", "lightning", "shield" };


    private void Awake()
    {
        manager = GameObject.Find("Manager");
    }
    public override void OnStartAuthority()
    {
        player = gameObject.GetComponent<CharacterController>();
        spellSys = gameObject.GetComponentInChildren<SpellSystem>();
        vrtk = GameObject.FindGameObjectWithTag("VRKit");
        fpsCamera = Camera.main;
        spawnPoint1 = manager.GetComponent<VariableStorageMainGame>().spawn1;
        spawnPoint2 = manager.GetComponent<VariableStorageMainGame>().spawn2;



        if (transform.parent != isLocalPlayer)
        {
            fpsCamera.enabled = false;
            vrtk.gameObject.SetActive(false);
        }
        else
        {                     
            vrtk.gameObject.SetActive(true);
            vrtk.transform.parent = this.transform;
            vrtk.transform.position = player.transform.position;
           
            recognizer = new KeywordRecognizer(keywords);
            recognizer.OnPhraseRecognized += SpeechRecognition;
            recognizer.Start();
            AttachWandToHand();

            if (gameObject.name.Contains("One"))
            {
                transform.position = spawnPoint1.transform.position;
                transform.rotation = spawnPoint1.transform.rotation;
                vrtk.transform.rotation = transform.rotation;
            }
            if (gameObject.name.Contains("Two"))
            {
                player.transform.position = spawnPoint2.transform.position;
                player.transform.rotation = spawnPoint2.transform.rotation;
                vrtk.transform.rotation = transform.rotation;
            }
            return;
        }
    }


    void Update()
    {
        WandInHandLock();
        CooldownSet();
        Vector3 mScale = manager.GetComponent<VariableStorageMainGame>().head.transform.position;
        model.transform.localScale = new Vector3(model.transform.localScale.x, mScale.y * .5f, model.transform.localScale.z);
        model.transform.position = manager.GetComponent<VariableStorageMainGame>().head.transform.position - new Vector3(0, 0.9f, 0);
    }

    void AttachWandToHand()
    {
        hand  = manager.GetComponent<VariableStorageMainGame>().hand;
        wand.transform.position = hand.transform.position + new Vector3(0, wandHeight, 0);
        wand.transform.eulerAngles = hand.transform.eulerAngles - new Vector3(rotateWand, 0, 0);
       
    }

    void WandInHandLock()
    {
        if (hand !=null)
        {
            wand.transform.parent = hand.transform;
        }
        
    }

    void CooldownSet()
    {
        if(spellSys.castCounter == 0)
            tipColor.GetComponent<Renderer>().material.color = Color.white;
    }

    void SpeechRecognition(PhraseRecognizedEventArgs args)
    {
        if (isLocalPlayer)
        {
            print(args.text);
      
            if (args.text == "fireball")
            {
                spellSys.FireMode = true;
                spellSys.LitMode = false;
                spellSys.ShieldMode = false;
                spellSys.castCounter = 4;
                wandColor.GetComponent<Renderer>().material.color = Color.red;
                tipColor.GetComponent<Renderer>().material.color = Color.red;
            }
            else if (args.text == "lightning")
            {
                spellSys.LitMode = true;
                spellSys.FireMode = false;
                spellSys.ShieldMode = false;
                spellSys.castCounter = 4;
                wandColor.GetComponent<Renderer>().material.color = Color.yellow;
                tipColor.GetComponent<Renderer>().material.color = Color.yellow;
            }
            else if(args.text == "shield")
            {
                spellSys.ShieldMode = true;
                spellSys.FireMode = false;
                spellSys.LitMode = false;
                spellSys.castCounter = 1;
                wandColor.GetComponent<Renderer>().material.color = Color.green;
                tipColor.GetComponent<Renderer>().material.color = Color.green;
            }
        }
    }


    void OnGUI()
    {
        if (hasAuthority)
        {
            GUI.Box(new Rect(Screen.width / 2, Screen.height / 2, 5, 5), "");
        }
        
    }
}
