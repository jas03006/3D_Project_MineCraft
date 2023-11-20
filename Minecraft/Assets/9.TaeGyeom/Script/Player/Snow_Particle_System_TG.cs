using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snow_Particle_System_TG : MonoBehaviour
{
    [SerializeField] private int trigger_cnt = 0;
    private ParticleSystem ps;
    private BoxCollider col;

    private void Awake()
    {
        gameObject.TryGetComponent<ParticleSystem>(out ps);
        gameObject.TryGetComponent(out col);
        trigger_cnt = 0;
    }
    private void Update()
    {
        if (trigger_cnt <= 0 && ps.isStopped)
        {
            trigger_cnt = 0;
            ps.Play();
        }
        else if(trigger_cnt > 0 && !ps.isStopped) {
            ps.Stop();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        trigger_cnt++;
    }
    private void OnTriggerExit(Collider other)
    {
        trigger_cnt--;
    }
    public void reset_cnt() {
        trigger_cnt = 0;

    }

    public void set_size(float width_, float height_, int max_cnt) {
        col.size = new Vector3(width_, col.size.y, height_);
        var shape = ps.shape;
        shape.scale = new Vector3(width_, height_, shape.scale.z);

        var m_ps = ps.main;
        m_ps.maxParticles = max_cnt;
        if (m_ps.maxParticles == 0)
        {
            m_ps.maxParticles = 1;
        }

        var emit = ps.emission;
        emit.rateOverTime = max_cnt / 20;
        if (emit.rateOverTime.constant == 0) {
            emit.rateOverTime = 1;
        }
        //Debug.Log(emit.rateOverTime);
    }
}
