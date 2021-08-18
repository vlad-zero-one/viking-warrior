using UnityEngine;
using UnityEngine.Events;

public class Options : MonoBehaviour
{
    public static bool FootstepsOn = true;
    public static bool FootstepsSoundOn = true;

    public void SwitchOnFootsteps()
    {
        FootstepsOn = true;
    }

    public void SwitchOffFootsteps()
    {
        FootstepsOn = false;
    }

    public void SwitchOnFootstepsSound()
    {
        FootstepsSoundOn = true;
    }

    public void SwitchOffFootstepsSound()
    {
        FootstepsSoundOn = false;
    }

    private void OnEnable()
    {
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
    }
}
