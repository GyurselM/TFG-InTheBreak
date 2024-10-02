using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundManagerMenu : MonoBehaviour
{
    public AudioSource aSource;
    public AudioSource aSource1;
    public AudioClip clickButton;
    public AudioClip hurtSound;
    public AudioClip healSound;
    public AudioClip shieldSound;
    public AudioClip brokenShieldSound;
    public AudioClip poisonSound;
    public AudioClip stunSound;
    public AudioClip sleepSound;
    public AudioClip winSound;
    public AudioClip defeatSound;
    public AudioClip touchCardSound;
    public AudioClip useCardSound;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void onClickButtonReproduce()
    {
        aSource.clip = clickButton;
        aSource.Play();
    }

    public void onHurtSoundReproduce()
    {
        aSource.clip = hurtSound;
        aSource.Play();
    }

    public void onHealSoundReproduce()
    {
        aSource.clip = healSound;
        aSource.Play();
    }

    public void onShieldSoundReproduce()
    {
        aSource.clip = shieldSound;
        aSource.Play();
    }

    public void onBrokenShieldSoundReproduce()
    {
        aSource.clip = brokenShieldSound;
        aSource.Play();
    }

    public void onPoisonSoundReproduce()
    {
        aSource.clip = poisonSound;
        aSource.Play();
    }

    public void onStunSoundReproduce()
    {
        aSource.clip = stunSound;
        aSource.Play();
    }

    public void onSleepSoundReproduce()
    {
        aSource.clip = sleepSound;
        aSource.Play();
    }

    public void onWinSoundReproduce()
    {
        aSource1.clip = winSound;
        aSource1.Play();
    }

    public void onDefeatSoundReproduce()
    {
        aSource1.clip = defeatSound;
        aSource1.Play();
    }

    public void onTouchCardSoundReproduce()
    {
        aSource.clip = touchCardSound;
        aSource.Play();
    }

    public void onUseCardSoundReproduce()
    {
        aSource.clip = useCardSound;
        aSource.Play();
    }
}
