using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {
    public Blink lvlupblink;
    public float lvlIncreaseTime = 30f;
    public float difficulty = 1f;
    public SpriteRenderer[] Hearts;
    public int health;
    public float interval = 0.1f;
    public float shieldHealInterval = 2f;
    public int blockScore = 100;
    public int hitScore = 200;
    public int dodgeScore = 10;
    public float attackTimeout = 0.2f;
    public float hurtTimeout = 1f;

    public GameObject SoundOrigin;
    public GameObject LvlUpSound;
    public GameObject StepSound;
    public GameObject ShieldHitSound;
    public GameObject SwordHitSound;
    public GameObject DamagedSound;
    public GameObject GameOverSound;
    private static PlayerManager _Instance = null;
    public static PlayerManager Instance
    {
        get
        {
            if (_Instance == null)
            {

                _Instance = FindObjectOfType<PlayerManager>();
            }
            return _Instance;
        }
    }
    [Range(0,1)]
    public int slot = 1;

    public bool CanAttack { get { return !blocking && !attacking && attackTimeout + lastAttack < Time.time; } }
    public bool CanBlock { get { return !attacking; /*&& shieldHealth > 0;*/ } }
    public int shieldHealth = 3;

    public bool attacking = false;
    public bool blocking = false;
    public bool playing = false;

    [Header("Knight Left")]
    public GameObject knight0;

    public GameObject knight0SwordUp;
    public GameObject knight0SwordDown;
    public GameObject knight0SwordIdle;

    public GameObject knight0Shields;
    public GameObject knight0ShieldFull;
    public GameObject knight0ShieldHalf;
    public GameObject knight0ShieldEmpty;
    ////////////////////////////////
    [Header("Knight Right")]
    public GameObject knight1;

    public GameObject knight1SwordUp;
    public GameObject knight1SwordDown;
    public GameObject knight1SwordIdle;

    public GameObject knight1Shields;
    public GameObject knight1ShieldFull;
    public GameObject knight1ShieldHalf;
    public GameObject knight1ShieldEmpty;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < Hearts.Length; i++)
        {
            if (i > health - 1)
            {
                Hearts[i].gameObject.SetActive(false);
            }
            else
            {
                Hearts[i].gameObject.SetActive(true);
            }
        }
        if (!playing)
        {
            if (Input.GetButton("Start"))
            {
                if (!playing)
                {
                    playing = true;
                    // play sound
                    // notify game manager
                    Score.Instance.showHighscore = false;
                    health = 6;
                    slot = 1;
                    Score.Instance.score = 0;
                    difficulty = 1f;
                    nextLevelUp = Time.time + lvlIncreaseTime;
                    Instantiate(LvlUpSound, SoundOrigin.transform);
                }
                else
                {
                    playing = false;
                    Score.Instance.showHighscore = true;
                    Debug.Log("Reset");
                }
            }
            knight0.SetActive(true);

            knight0SwordUp.SetActive(true);
            knight0SwordDown.SetActive(true);
            knight0SwordIdle.SetActive(true);

            knight0Shields.SetActive(true);
            knight0ShieldFull.SetActive(true);
            knight0ShieldHalf.SetActive(true);
            knight0ShieldEmpty.SetActive(true);
            knight1.SetActive(true);

            knight1SwordUp.SetActive(true);
            knight1SwordDown.SetActive(true);
            knight1SwordIdle.SetActive(true);

            knight1Shields.SetActive(true);
            knight1ShieldFull.SetActive(true);
            knight1ShieldHalf.SetActive(true);
            knight1ShieldEmpty.SetActive(true);
            return;
        }
        else
        {
            if (nextLevelUp < Time.time && difficulty > 0.3f)
            {
                Instantiate(LvlUpSound, SoundOrigin.transform);
                difficulty -= 0.07f;
                nextLevelUp = Time.time + lvlIncreaseTime;
                StartCoroutine(lvlupblink.DoBlink());           
                // play sound
            }

            if (Input.GetButtonDown("Attack") && CanAttack)
            {

                StartCoroutine("Attack");
                Instantiate(StepSound, SoundOrigin.transform);
            }
            else if (Input.GetButtonDown("Block") && CanBlock)
            {
                blocking = true;
                Instantiate(StepSound, SoundOrigin.transform);
                lastShieldUp = Time.time;
            }
            else if (Input.GetButtonUp("Block") /*|| shieldHealth <= 0*/)
            {
                blocking = false;
            }

            if (blocking)
            {

                knight1Shields.SetActive(true);
                knight0Shields.SetActive(true);
                knight1ShieldFull.SetActive(true);
                knight0ShieldFull.SetActive(true);
                knight1ShieldHalf.SetActive(false);
                knight1ShieldEmpty.SetActive(false);
                knight0ShieldHalf.SetActive(false);
                knight0ShieldEmpty.SetActive(false);
                knight1SwordIdle.SetActive(false);
                knight0SwordIdle.SetActive(false);
                //    }
                //    else if(shieldHealth == 2)
                //    {

                //        knight1ShieldHalf.SetActive(true);
                //        knight0ShieldHalf.SetActive(true);
                //        knight1ShieldFull.SetActive(false);
                //        knight1ShieldEmpty.SetActive(false);
                //        knight0ShieldFull.SetActive(false);
                //        knight0ShieldEmpty.SetActive(false);

                //    }
                //    else
                //    {

                //        knight1ShieldEmpty.SetActive(true);
                //        knight0ShieldEmpty.SetActive(true);
                //        knight1ShieldFull.SetActive(false);
                //        knight1ShieldHalf.SetActive(false);
                //        knight0ShieldFull.SetActive(false);
                //        knight0ShieldHalf.SetActive(false);
                //    }

                //}
                //else
                //{

                //    // blink shield once
                //    // play deny sound
                //}
            }

            if (!attacking && !blocking)
            {
                knight0Shields.SetActive(false);
                knight1Shields.SetActive(false);
                knight0SwordUp.SetActive(false);
                knight1SwordUp.SetActive(false);
                knight0SwordDown.SetActive(false);
                knight1SwordDown.SetActive(false);
                knight1Shields.SetActive(false);
                knight0SwordIdle.SetActive(true);
                knight1SwordIdle.SetActive(true);
            }

            //if(shieldHealth < 2 && lastShieldHeal < Time.time-shieldHealInterval)
            //{
            //    shieldHealth++;
            //    // play sound
            //    lastShieldHeal = Time.time;
            //}
            if ((Input.GetButtonDown("Left") || Input.GetAxis("Stick") < -0.5f || Input.GetAxis("DPad") < -0.5f) && slot == 1 && !blocking && !attacking)
            {
                slot = 0;
                Instantiate(StepSound, SoundOrigin.transform);
            }
            else if ((Input.GetButtonDown("Right") || Input.GetAxis("Stick") > 0.5 || Input.GetAxis("DPad") > 0.5f) && slot == 0 && CanBlock && !blocking && !attacking)
            {
                slot = 1;
                Instantiate(StepSound, SoundOrigin.transform);
                // enable render in slot 1, disable slot 2
            }

            if (lastHurt + hurtTimeout > Time.time)
            {
                knight1.SetActive(slot == 1 && Time.time % 0.2 > 0.1);
                knight0.SetActive(slot == 0 && Time.time % 0.2 > 0.1);
            }
            else
            {
                knight1.SetActive(slot == 1);
                knight0.SetActive(slot == 0);
            }
        }
    }

    float lastShieldHeal = 0;
    float lastShieldUp = 0;
    public void Damage(int targetSlot, bool ignoreShield = true, bool damageShield = true)
    {
        if(slot != targetSlot)
        {
            Score.Instance.score += dodgeScore * (int)(1f/PlayerManager.Instance.difficulty);
            return;
        }
        if (lastHurt + hurtTimeout > Time.time)
            {
                return;
            }
        if(!ignoreShield && blocking)
        {

            //if (damageShield)
            //{
                //shieldHealth--;
                // update shield graphics
                // play sound
                //add score
                Score.Instance.score += blockScore * (int)(1f / PlayerManager.Instance.difficulty);
                Instantiate(ShieldHitSound, SoundOrigin.transform);
            //}
            return;
        }
        if(slot == targetSlot)
        {
            health--;
            Instantiate(DamagedSound, SoundOrigin.transform);
            lastHurt = Time.time;
            //play sound
        }
       
        if (health <= 0)
        {
            // play game over sound
            // notify game manager
            Instantiate(GameOverSound, SoundOrigin.transform);
            playing = false;
            Score.Instance.showHighscore = false;
        }
    }
    public float lastHurt = -10f;

    public IEnumerator Attack()
    {
        attacking = true;
        //play sound
        knight0SwordUp.SetActive(true);
        knight1SwordUp.SetActive(true);
        yield return new WaitForSeconds(interval);
        knight0SwordUp.SetActive(false);
        knight0SwordDown.SetActive(true);
        knight1SwordUp.SetActive(false);
        knight1SwordDown.SetActive(true);
        //show attack 2
        if (slot == 1 && Bat.Instance.Damage())
        {
            Score.Instance.score += hitScore * (int)(1f / PlayerManager.Instance.difficulty);
            Instantiate(SwordHitSound, SoundOrigin.transform);
        }
        if (slot == 1 && Spider.Instance.Damage())
        {
            Score.Instance.score += hitScore * (int)(1f / PlayerManager.Instance.difficulty);
            Instantiate(SwordHitSound, SoundOrigin.transform);
        }
        if (slot == 0 && Skeleton.Instance.Damage())
        {
            Score.Instance.score += hitScore * (int)(1f / PlayerManager.Instance.difficulty);
            Instantiate(SwordHitSound, SoundOrigin.transform);
        }
        //add score if hit
        //add speed bonus
        yield return new WaitForSeconds(interval);
        knight0SwordDown.SetActive(false);
        knight0SwordIdle.SetActive(true);
        knight1SwordDown.SetActive(false);
        knight1SwordIdle.SetActive(true);

        lastAttack = Time.time;
        attacking = false;
    }
    float lastAttack = -10;
    float nextLevelUp;
}
