﻿using System.ComponentModel.DataAnnotations;

namespace Server.Domain.Entities;

public abstract class BaseEntity
{
    [Key]
    public Guid Id { get; set; }

    public DateTime CreationDate { get; set; } = DateTime.Now;

    public Guid? CreatedBy { get; set; }
    public Guid? CreatedByUserId { get; set; }

    public DateTime? ModificationDate { get; set; }

    public Guid? ModificationBy { get; set; }

    public DateTime? DeletionDate { get; set; }

    public Guid? DeleteBy { get; set; }

    public bool IsDeleted { get; set; } = false;
}
