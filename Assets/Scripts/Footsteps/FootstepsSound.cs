using UnityEngine;

public class FootstepsSound : MonoBehaviour
{
    private static AudioClip[] _footstepsAudio;
    private static int _lenght;

    private void Awake()
    {
        _footstepsAudio = Resources.LoadAll<AudioClip>("Other Assets/Footstep(Snow and Grass)");
        _lenght = _footstepsAudio.Length;
    }

    public static AudioClip GetRandom()
    {
        return _footstepsAudio[Random.Range(0, _lenght)];
    }
}
