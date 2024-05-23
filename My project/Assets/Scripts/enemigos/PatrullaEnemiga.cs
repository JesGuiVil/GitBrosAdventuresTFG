using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrullaEnemiga : MonoBehaviour
{
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;
    [SerializeField] private Transform enemy;
    [SerializeField] private float speed;
    [SerializeField] private float idleDuration;    
    private float idleTimer;
    private Vector3 initScale;
    private bool movingLeft;
    [SerializeField] private Animator anim;
    private EnemigoBase enemigoScript;

    private AudioSource audioSource;

    [SerializeField] private AudioClip caminarClip;

    // Start is called before the first frame update
    private void Start()
    {
        // Ensure audioSource is set and caminarClip is assigned
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        if (caminarClip != null)
        {
            audioSource.clip = caminarClip;
        }
        else
        {
            Debug.LogWarning("No caminarClip assigned to the patrol script.");
        }
    }
    private void MoveInDirection(int _direction)
    {
        idleTimer = 0;
        anim.SetBool("Movimiento", true);
        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction, initScale.y, initScale.z);
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * speed,enemy.position.y, enemy.position.z);
    }
    private void OnDisable()
    {
        anim.SetBool("Movimiento", false);
        StopWalkingSound();
    }
    private void Update()
    {
        if(!enemigoScript.enemyDead){
            if (movingLeft)
            {
                if (enemy.position.x >= leftEdge.position.x){
                    MoveInDirection(-1);
                }
                else
                {
                    DirectionChange();
                }
            }
            else
            {
                if(enemy.position.x <= rightEdge.position.x){
                    MoveInDirection(1);
                }
                else
                {
                    DirectionChange();
                }
            }

            if (anim.GetBool("Movimiento"))
            {
                PlayWalkingSound();
            }
            else
            {
                StopWalkingSound();
            }
        }
        
    }
    private void PlayWalkingSound()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = caminarClip;
            audioSource.Play();
            audioSource.pitch = 2.0f;
        }
    }

    private void StopWalkingSound()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
            audioSource.pitch = 1.0f;
        }
    }
    private void DirectionChange()
    {
        anim.SetBool("Movimiento", false);
        anim.ResetTrigger("ataqueDistancia");

        idleTimer += Time.deltaTime;
        if(idleTimer > idleDuration)
        movingLeft = !movingLeft;
    }
    private void Awake()
    {
        enemigoScript = GetComponentInChildren<EnemigoBase>();
        initScale = enemy.localScale;
        audioSource = enemigoScript.GetComponent<AudioSource>();
    }
}