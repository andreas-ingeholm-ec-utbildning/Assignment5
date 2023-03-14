﻿using System;
using System.Collections.Generic;
using IssuesClient.Models.Entities;

namespace IssuesClient.Models;

public class Report
{

    public Guid Id { get; set; } = Guid.NewGuid();

    public ReportStatus Status { get; set; }

    public string Title { get; set; } = null!;
    public List<Comment> Comments { get; set; } = new();
    public User User { get; set; } = null!;
    public DateTime Created { get; set; }

}