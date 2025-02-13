using UnityEngine;

public class AudioManager : MonoBehaviour
{

    /// <summary>
    /// to use it , first make an instancr of class 
    /// AudioManager audioManager; 
    /// 
    /// " Add a tag Audio to the empty game object that you will assign this scrit to 
    /// then identify it in start function 
    /// audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    /// 
    /// then to call it for die ex: audioManager.PlayeSFX(audioManager.DieClip);
    /// 
    /// </summary>
    /// 

    

    [Header("Audio Source")]

    [SerializeField] public AudioSource backgroundMusic;
    
    [SerializeField] AudioSource SFX;
     [SerializeField] AudioSource SFX2;


    [Header("Audio Clip")]
    [SerializeField] AudioClip backgroundMusicClip;
   // public AudioClip slimeClip;
    public AudioClip DieClip;
    public AudioClip AirPush;
    public AudioClip AirPush1;
    public AudioClip AirPush2;
    public AudioClip colide ;
    public AudioClip Fire ;
    public AudioClip ShipHit ;
    private bool hasPlayed = false;
    private bool hasPlayed2 = false;

    
     

    void Start()
    {
        //Play background music  with low volume
        backgroundMusic.clip = backgroundMusicClip ;
        backgroundMusic.volume = 0.03f ;
        //backgroundMusic.Play();

    }

    public void StopBk(){
        backgroundMusic.Stop();
    }

   public void PlayeSFX (AudioClip clip){

        // only playes effects 
        SFX.volume = 0.1f ;
       
        SFX.PlayOneShot(clip);
        

   }
   
   public void PlayeSFX2 (AudioClip clip){

        // only playes effects 
        SFX2.volume = 0.1f ;
        SFX2.PlayOneShot(clip);
        

   }


      public void PlayOnce(AudioClip clip)
    {
        if (!hasPlayed)
        {
            SFX.PlayOneShot(clip);
            hasPlayed = true;
        }
      
    }

    public void PlayOnce2(AudioClip clip)
    {
        if (!hasPlayed2)
        {
            SFX.PlayOneShot(clip);
            hasPlayed2 = true;
        }
      
    }

    public void ResetPlayOnce()
    {
        hasPlayed = false;
    }

}
