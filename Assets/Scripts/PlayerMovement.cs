using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rigidbody;
    public float moveSpeed;
    private bool onWater = false;
    public GameObject miniMapCamera;
    public TextMeshProUGUI warningText;
    private float waterTimer = 10;
    public GameObject sceneTransition;
    private GameObject selectedRuin;
    public GameObject levelController;

    void Start()
    {
        rigidbody = GetComponentInChildren<Rigidbody>();
    }

    private void Update()
    {
        miniMapCamera.transform.position = new Vector3(transform.position.x, 150, transform.position.z);

        if (onWater && waterTimer >= 0)
        {
            waterTimer -= Time.deltaTime;
            warningText.text = "Get out of water before " + (int)waterTimer + "!";
        }

        if (!onWater)
        {
            waterTimer = 10;
        }

        if (onWater && waterTimer < 0)
        {
            waterTimer = -0.25f;
            StartCoroutine(LoadAgain());
        }

        if (selectedRuin != null && Input.GetKeyDown(KeyCode.E))
        {
            selectedRuin.transform.GetChild(0).GetComponent<ParticleSystem>().loop = false;
            selectedRuin.transform.GetChild(0).GetChild(0).GetComponent<ParticleSystem>().loop = false;
            selectedRuin.transform.GetChild(0).GetChild(1).GetComponent<ParticleSystem>().loop = false;
            selectedRuin.transform.GetChild(1).gameObject.SetActive(true);
            selectedRuin.transform.GetChild(2).gameObject.SetActive(true);
            selectedRuin.transform.tag = null;
            selectedRuin = null;
            levelController.GetComponent<LevelController>().cleanedRuinCount =
                levelController.GetComponent<LevelController>().cleanedRuinCount + 1;
        }
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rigidbody.AddForce(movement * moveSpeed);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            onWater = true;
            warningText.gameObject.SetActive(true);
        }
        
        if (other.CompareTag("Ruin"))
        {
            selectedRuin = other.gameObject;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            onWater = false;
            warningText.gameObject.SetActive(false);
        }
        
        if (other.CompareTag("Ruin") && selectedRuin != null)
        {
            selectedRuin = null;
        }
    }

    public IEnumerator LoadAgain()
    {
        sceneTransition.GetComponent<Animator>().SetTrigger("Start");
        yield return new WaitForSeconds(1.25f);
        SceneManager.LoadScene(1);
    }
}
