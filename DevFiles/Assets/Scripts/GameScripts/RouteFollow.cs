using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteFollow : MonoBehaviour
{
    [SerializeField] private Transform[] routes;
    private int routeToGo;
    private float tParam;
    private Vector3 canoePosition;
    private float speedModifier;
    private bool coroutineAllowed;
    public GameObject canoe;
    public GameObject conoestay;
    public GameObject player;
    public Transform noParent;

    // Start is called before the first frame update
    void Start()
    {
        routeToGo = 0;
        tParam = 0f;
        speedModifier = 0.01f;
        coroutineAllowed = true;
        conoestay.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (coroutineAllowed)
        {
            StartCoroutine(GoByTheRoute(routeToGo));
        }

        
    }

    private IEnumerator GoByTheRoute(int routeNumber)
    {
        coroutineAllowed = false;
        

        Vector3 p0 = routes[routeNumber].GetChild(0).position;
        Vector3 p1 = routes[routeNumber].GetChild(1).position;
        Vector3 p2 = routes[routeNumber].GetChild(2).position;
        Vector3 p3 = routes[routeNumber].GetChild(3).position;

        while (tParam < 1)
        {
            tParam += Time.deltaTime * speedModifier;

            canoePosition = Mathf.Pow(1 - tParam, 3) * p0 + 3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 + 3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 + Mathf.Pow(tParam, 3) * p3;

            canoe.transform.position = canoePosition;

            yield return new WaitForEndOfFrame();
        }

        tParam = 0f;
        conoestay.transform.position = canoe.transform.position;
        conoestay.SetActive(true);
        player.transform.SetParent(noParent);
        
        Destroy(gameObject);
        routeToGo += 1;
       

        coroutineAllowed = true;
    }
}
