﻿namespace Server.Logic.Auth.Models;

public class Role
{
    public Role(string name)
    {
        Name = name;
    }

    public string Name { get; set; }
}