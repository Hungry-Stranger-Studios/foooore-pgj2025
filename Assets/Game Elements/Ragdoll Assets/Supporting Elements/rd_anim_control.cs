using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class rd_anim_control : MonoBehaviour
{
    Animator an;
    bool choosingAction = false;

    private void Start()
    {
        an = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!choosingAction && an.enabled == true)
            StartCoroutine(ChooseAnimation());

    }

    IEnumerator ChooseAnimation()
    {
        choosingAction = true; 
        yield return new WaitForSeconds(5f);    // Tries all of this every 5 seconds
        int randValue = Random.Range(0, 100);
        if (randValue < 80)
        {
            an.SetInteger("idle_state", 0); // 8/10 chance to play idle
        }
        else if (randValue < 90)
        {
            an.SetInteger("idle_state", 1); // 1/10 chance to play idle act 1
        }
        else
        {
            an.SetInteger("idle_state", 2); // 1/10 chance to play idle act 2
        }
        choosingAction = false;
    }

    public void disableAnimator()
    {
        an.enabled = false;
    }
}
