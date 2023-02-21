using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BugReportClient.Models.Entities;

public class AddressEntity
{

    [Required]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public string StreetName { get; set; } = null!;

    [Required]
    public int? StreetNumber { get; set; }

    [Required]
    [Column(TypeName = "char(6)")]
    public string PostalCode { get; set; } = null!;

    [Required]
    public string City { get; set; } = null!;

    public ICollection<UserEntity> Users { get; set; } = new HashSet<UserEntity>();

}
