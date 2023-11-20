using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_TG : Block_TG, Interactive_TG
{
    private Transform rotate_point_tr;
    private Quaternion original_rotation;
    [SerializeField] private List<AudioClip> clip_list; //{open, close}
    private AudioSource audio_source;

    private void Awake()
    {
        TryGetComponent<AudioSource>(out audio_source);
    }
    private void OnEnable()
    {
        
        is_open = false;
        //transform.rotation = Quaternion.identity;
        original_rotation = transform.rotation;
        rotate_point_tr = transform.GetChild(0);
    }
    public void react()
    {
        //Debug.Log("react");

        transform.RotateAround(rotate_point_tr.position, rotate_point_tr.up, 90f * (is_open ? 1:-1));
        if (is_open)
        {
            audio_source.PlayOneShot(clip_list[1]);
        }
        else { 
            audio_source.PlayOneShot(clip_list[0]);
        }
        is_open = !is_open;

    }

    public override void init(bool is_open_)
    {
        transform.rotation = original_rotation;
        if (is_open_) {
            transform.RotateAround(rotate_point_tr.position, rotate_point_tr.up, -90f);
        }        
        is_open = is_open_;
    }
}
