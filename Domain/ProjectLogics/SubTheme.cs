﻿namespace Domain.ProjectLogics;

public class SubTheme : ITheme
{
    public long Id { get; set; }
    public IEnumerable<Flow> Flows { get; set; }
    public MainTheme MainTheme { get; set; }
}