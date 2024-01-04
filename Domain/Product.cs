using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace B2S_REST_API.Domain;

public partial class Product
{
    public int PrdId { get; set; }

    public string? PrdName { get; set; }

    public string PrdProductNumber { get; set; } = null!;

    public string? PrdTypeNumber { get; set; }

    public string? PrdEanGlr { get; set; }

    public string? PrdUpc { get; set; }

    public string? PrdProductText { get; set; }

    public int? GrpId { get; set; }

    public int? BrdId { get; set; }

    public string? SpecJson { get; set; }

    [JsonIgnore]
    public virtual Brand? Brd { get; set; }
    [JsonIgnore]
    public virtual ItemGroup? Grp { get; set; }
}
