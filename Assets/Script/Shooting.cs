using System.Collections;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bullet;
    private bool onMouse = true;
    private int move;
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (onMouse)
            {
                onMouse = false;
                if (move<3) {
                    StartCoroutine(OnMouse());
                    move++;
                }
                else
                {
                    StartCoroutine(Recharge());
                }

            }
        }
    }

    IEnumerator OnMouse()
    {
        yield return new WaitForSeconds(1f);
        Instantiate(bullet, transform.position, transform.rotation);
        onMouse = true;
    } 

    IEnumerator Recharge()
    {
        yield return new WaitForSeconds(2f);
        onMouse = true;
        move = 0;
    }
}
