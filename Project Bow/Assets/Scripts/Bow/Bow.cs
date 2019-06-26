using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bow : MonoBehaviour
{
    public GameManager gameManager;

    // ads = Aim Down Sights
    [SerializeField]
    private Vector3 ads;
    [SerializeField]
    private Vector3 adsRotation;

    [SerializeField]
    private Vector3 hipfire;
    [SerializeField]
    private Vector3 hipfireRotation;

    [SerializeField]
    private float smoothTime = 50f;
    [SerializeField]
    private Image Crosshair;

    public float Charge = 0;
    public float ChargeTime = 10;

    public GameObject arrowPlaceholder;
    public GameObject arrowPrefab;
    public Transform ArrowFirePoint;

    private void Start() {
        GameObject gameManagerObj = GameObject.FindGameObjectWithTag("GameController");
        gameManager = gameManagerObj.GetComponent<GameManager>();
    }

    void Update()
    {
        // Bow Zooming
        if (Input.GetMouseButton(1)) {
            gameManager.isADS = true;
            Crosshair.enabled = !gameManager.isADS;

            Vector3 SmoothedPos = Vector3.Lerp(transform.localPosition, ads, smoothTime * Time.deltaTime);
            transform.localPosition = SmoothedPos;
            transform.localRotation = Quaternion.Euler(adsRotation);

        } else {
            gameManager.isADS = false;
            Crosshair.enabled = !gameManager.isADS;

            Vector3 SmoothedPos = Vector3.Lerp(transform.localPosition, hipfire, smoothTime * Time.deltaTime);
            transform.localPosition = SmoothedPos;
            transform.localRotation = Quaternion.Euler(hipfireRotation);
        }


        // Charge Bow
        if (Input.GetMouseButton(0))
        {
            arrowPlaceholder.SetActive(true);

            float SmoothedCharge = Mathf.Lerp(Charge, 1, ChargeTime * Time.deltaTime);
            Charge = SmoothedCharge;

            
        }
        else if (Input.GetMouseButtonUp(0))
        {
            arrowPlaceholder.SetActive(false);
            
            GameObject arrowObj = Instantiate(arrowPrefab, ArrowFirePoint.position, ArrowFirePoint.transform.rotation);

            BowShoot arrow = arrowObj.GetComponent<BowShoot>();
            arrow.AddTrust(Charge);
            Charge = 0;
        }
    }
}
