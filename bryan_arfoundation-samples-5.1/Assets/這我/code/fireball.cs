using UnityEngine;

public class fireball : MonoBehaviour
{
    public Transform action;
    public Transform startplace;
    public float speed;
    public float lifetime = 10f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.transform.LookAt(action);
        Destroy(gameObject, lifetime);
        //this.transform.position(startplace);
    }

    // Update is called once per frame
    void Update()
    {
        
        this.transform.Translate(0.0f,0.0f,speed*Time.deltaTime);

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name =="Main Camera")
        {
            GameObject.Find("Level").SendMessage("hurt");
            //Debug.Log("hi" + other);
        
        }
        
    }
}
