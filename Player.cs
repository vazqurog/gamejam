using Godot;
using System;

public class Player : Area2D
{   
        [Export]
        public int Speed = 200;
        public Vector2 ScreenSize;
        private AnimatedSprite _animationSprite;
        public string facingDirection = "up";
        [Export]
        private int health;
        [Export]
        private int mana;
        private Array inventory;

    // Initializer function
    public override void _Ready()
    {
        ScreenSize = GetViewportRect().Size;
        _animationSprite = GetNode<AnimatedSprite>("animation");
    }

    // main game loop
    public override void _Process(float delta)
    {   
        MovementControls(false, facingDirection, delta);
        CombatControls();
    

    }

    public void MovementControls(bool is_moving, string facingdiretion, float delta)
    {
        var velocity = Vector2.Zero;
        string newAnimation = "";

        // Controller Movement
        if (Input.IsActionPressed("move_left"))
        {   
            velocity.x -= 1;
            is_moving = true;
            facingDirection = "left";
            GD.Print("left");
        }

        else if (Input.IsActionPressed("move_right"))
        {   
            velocity.x += 1;
            is_moving = true;
            facingDirection = "right";
            GD.Print("right");
        }
       
         
        else if (Input.IsActionPressed("move_up"))
        {   
            velocity.y -= 1;
            is_moving = true;
            facingDirection = "up";
            GD.Print("up");
        }
       
        else if (Input.IsActionPressed("move_down"))
        {   
            velocity.y += 1;
            is_moving = true;
            facingDirection = "down";
            GD.Print("down");
        }
        else if (!Input.IsActionPressed("move_down") && !Input.IsActionPressed("move_up") && !Input.IsActionPressed("move_right") && !Input.IsActionPressed("move_left")) 
        {
            velocity.x = 0;
            velocity.y = 0;
        }
        else
        {
            is_moving = false;
        }
        
        if (is_moving)
        {
            newAnimation = "walk-" + facingDirection;
        
        }
        else
        {
            newAnimation = "idle-" + facingDirection;
        }

        if (_animationSprite.Animation != newAnimation)
        {
            _animationSprite.Animation = newAnimation;
            _animationSprite.Play(newAnimation);
        }
        Position += velocity * Speed * delta;

        // Set boundaries 
        Position = new Vector2(
            x:Mathf.Clamp(Position.x, 0 , ScreenSize.x - 10),
            y:Mathf.Clamp(Position.y, 0 , ScreenSize.y - 10)
            );
    }

    public void CombatControls()
    {
        var attack = "";
        if (Input.IsActionPressed("attack") )
        {
            attack = "attack-" + facingDirection;
            _animationSprite.Animation = attack;
            _animationSprite.Play();
        }
    }
}
