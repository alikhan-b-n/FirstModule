using System.Collections;
using System.Text.Json.Serialization;
using FirstModule.ThirdTask.Entities;

namespace FirstModule.ThirdTask.Dtos;

public class Root
{
    public List<ResponseDto> response { get; set; }
}
public class ResponseDto
{
    public Date date { get; set; }
    public Teams teams { get; set; }
    public Scores scores { get; set; }
}

public class Date
{
    public DateTime start { get; set; }
}

public class Home
{
    public string? name { get; set; }
    public int? points { get; set; }
}

public class Scores
{
    public Visitors? visitors { get; set; }
    public Home? home { get; set; }
}

public class Teams
{
    public Visitors? visitors { get; set; }
    public Home? home { get; set; }
}

public class Visitors
{
    public string? name { get; set; }
    public int? points { get; set; }
}