using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnPath : MonoBehaviour
{
    GameObject player; 
    GameObject spawnTrigger;
    void Start()
    {
        player = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        Colour(player, Color.blue);
        player.transform.Translate(0, 1, 0);
        player.AddComponent<Rigidbody>();
        spawnTrigger = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Colour(spawnTrigger, Color.green);
        generatePath(player.transform.localPosition);
    }

    void Colour(GameObject go, Color c) 
    {
        Material mat = new Material(Shader.Find("Diffuse")); 
        mat.color = c; 
        go.GetComponent<Renderer>().material = mat; 
    }

    void generatePath(Vector3 p) 
    {
        int pthLn = 50; 
        GameObject path = GameObject.CreatePrimitive(PrimitiveType.Cube); 
        Colour(path, Color.red); 
        path.transform.localPosition = new Vector3(p.x, 0, p.z + pthLn / 2); 
        path.transform.localScale = new Vector3(10, 1, pthLn); 
        int r = Random.Range(-1, 2); 
        path.transform.RotateAround(p, Vector3.up, r * 90); 
        spawnTrigger.transform.localPosition = new Vector3(p.x, 1, p.z + pthLn); 
        spawnTrigger.transform.RotateAround(p, Vector3.up, r * 90); 
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(x, 0, z) * 20; player.GetComponent<Rigidbody>().AddForce(move);
        Vector3 p = player.transform.localPosition;
        transform.localPosition = new Vector3(p.x, p.y + 10, p.z - 20);
        transform.LookAt(player.transform);
        if (Vector3.Distance(p, spawnTrigger.transform.localPosition) < 5) {
            generatePath(p);
        }
    }
}
