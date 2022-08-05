using System;
using System.Collections.Generic;
using System.Text;

public class PrefixLogger : BaseLogger
{
    public string Prefix { get; set; }
    private const string Format = "[{0}]";
    public override string LogPrefix => string.Format(Format, Prefix);

    public PrefixLogger(string prefix)
    {
        Prefix = prefix;
    }
}