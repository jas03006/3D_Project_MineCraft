using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum Sound_Id { 
    None=0,
    exp=1,
    eat=2,
    attack =3,
    attacked = 4,
    step = 5
    //dig´Â µû·Î
}

public class Audio_Manager_TG : MonoBehaviour
{
    public static Audio_Manager_TG instance = null;
    [SerializeField] private AudioSource audio_source;

    public int sound_cnt_per_id = 3;
    private Dictionary<Sound_Id, int> SoundID2index_dict;
    [SerializeField] private List<AudioClip> audio_clip_list;
    public List<Sound_Id> clip_id_list =
        new List<Sound_Id>() {
            Sound_Id.exp,
            Sound_Id.eat,
            Sound_Id.attack,
            Sound_Id.attacked,
            Sound_Id.step
        };

    public int dig_sound_cnt_per_id = 3;
    private Dictionary<Item_ID_TG, int> ID2index_dict;
    [SerializeField] private List<AudioClip> dig_audio_clip_list;
    public List<Item_ID_TG> dig_clip_id_list =
        new List<Item_ID_TG>() {
            Item_ID_TG.stone,
            Item_ID_TG.grass,
            Item_ID_TG.dirt,
            Item_ID_TG.board,
            Item_ID_TG.bedrock,
            Item_ID_TG.coal,
            Item_ID_TG.iron,
            Item_ID_TG.tree,
            Item_ID_TG.leaf,
            Item_ID_TG.diamond,
            Item_ID_TG.box,
            Item_ID_TG.craft_box,
            Item_ID_TG.furnace,
            Item_ID_TG.door,
            Item_ID_TG.bed
        };
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else {
            Destroy(this.gameObject);
        }

        ID2index_dict = new Dictionary<Item_ID_TG, int>();
        int i = 0;
        foreach (Item_ID_TG e in dig_clip_id_list)
        {
            ID2index_dict[e] = i;
            i++;
        }

        SoundID2index_dict = new Dictionary<Sound_Id, int>();
        i = 0;
        foreach (Sound_Id e in clip_id_list)
        {
            SoundID2index_dict[e] = i;
            i++;
        }
    }
    public AudioSource get_audio_player() {
        return audio_source;
    }
    public void play_random_sound(Sound_Id id_) {
        if (!SoundID2index_dict.ContainsKey(id_))
        {
            return;
        }
        get_audio_player().PlayOneShot(audio_clip_list[SoundID2index_dict[id_]*sound_cnt_per_id + Random.Range(0, sound_cnt_per_id)]);
    }
    public void play_random_dig_sound(Item_ID_TG id_)
    {
        if (!ID2index_dict.ContainsKey(id_)) {
            return;
        }
        get_audio_player().PlayOneShot(dig_audio_clip_list[ID2index_dict[id_] * dig_sound_cnt_per_id + Random.Range(0, dig_sound_cnt_per_id)]);
    }
}
