﻿namespace ITHealth.Data.Entities;

public class BaseEntity
{
    public int Id { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}