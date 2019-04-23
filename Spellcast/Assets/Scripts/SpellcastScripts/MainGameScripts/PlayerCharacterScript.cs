﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using UnityEngine.Networking;

public class PlayerCharacterScript : NetworkBehaviour {

    private FPSViewScript mouseLook;
    private CharacterController player;
    private SpellSystem spellSys;

    private Vector3 move = Vector3.zero;
    private GameObject vrtk, manager, hand;
    public GameObject wand;
    public GameObject wandColor;
    
    private GameObject spawnPoint1, spawnPoint2;
    private Camera fpsCamera;
    private Vector2 moveInput;
    private Vector2 look;
    private bool jumpTick;
    public static bool spawnInUse;
    private KeywordRecognizer recognizer;

    private string[] keywords = new string[] { "fireball", "lightning","shield" };
    public float moveSpeed;
    public float jumpForce;
    public float gravity;
    public float rotateWand;
    public float wandHeight;
    public float playerHealth;

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
        CheckAuthority();
        WandInHandLock();
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


    void CheckAuthority()
    {
        if (hasAuthority)
        {
            Movement();

        }
        else
        {
            return;
        }
    }


    void Movement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        moveInput = new Vector2(horizontal, vertical);

        if (moveInput.sqrMagnitude > 1)
        {
            moveInput.Normalize();
        }

        //     Vector3 moveDirection = fpsCamera.transform.TransformDirection(transform.forward * moveInput.y + transform.right * moveInput.x);

        move.x =/* moveDirection.x */ 0;
        move.z =/* moveDirection.z */ 0;

        if (player.isGrounded)
        {
            move.y = 0;
            jumpTick = false;
        }
        else
        {
            move.y -= gravity * Time.deltaTime;
        }

        if (Input.GetButton("Jump") && jumpTick == false)
        {
            move.y = jumpForce;
            jumpTick = true;
        }

        player.Move(move);
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
                wandColor.GetComponent<Renderer>().material.color = Color.red;
            }
            else if (args.text == "lightning")
            {
                spellSys.LitMode = true;
                spellSys.FireMode = false;
                spellSys.ShieldMode = false;
                wandColor.GetComponent<Renderer>().material.color = Color.yellow;
            }
            else if(args.text == "shield")
            {
                spellSys.ShieldMode = true;
                spellSys.FireMode = false;
                spellSys.LitMode = false;
                wandColor.GetComponent<Renderer>().material.color = Color.green;
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
