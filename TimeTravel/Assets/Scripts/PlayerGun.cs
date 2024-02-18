using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;

    public Camera cam;
    public GameObject gameObject;

    public void Shoot()
    {
        
        RaycastHit hit;
        if (Physics.Raycast(gameObject.transform.position, cam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            Egg egg = hit.transform.GetComponent<Egg>();
            if (egg != null)
            {
                egg.BreakEgg();
            }
        }
    }

    //// Update is called once per frame
    //void Update()
    //{
        
    //}
}
