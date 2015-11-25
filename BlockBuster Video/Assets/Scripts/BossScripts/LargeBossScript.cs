using UnityEngine;
using System.Collections;

public class LargeBossScript : PhysicsBlockScript {

    // Use this for initialization
    public override void Start()
    {
        base.Start();

        health = 50;

        transform.localScale = new Vector3(250, 250, 0);
        maxSize = new Vector3(250, 250, 0);

        bossAttackTime = Time.time + bossAttack;

        currentTime = Time.time;

    }
        // Update is called once per frame
        public override void Update () {
        base.Update();
        attack();
	}

    public override void attack()
    {
        if (!frozen)
        {
            chargeRedBoss();
            if (Time.time >= bossAttackTime)
            {
                GameControllerScript.health = 0;
            }
        }
    }
}
