﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeCan : Projectile
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void effectRunner(Runner runner) {
        runner.bootsSpeed(intensity);
    }
}