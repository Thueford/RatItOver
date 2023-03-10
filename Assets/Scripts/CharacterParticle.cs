using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterParticle : ParticleBase
{
    public SpriteRenderer sr;
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        var shape = ps.shape;
        shape.spriteRenderer = sr;
        shape.texture = sr.sprite.texture;
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
        var shape = ps.shape;
        shape.texture = sr.sprite.texture;
    }
}
