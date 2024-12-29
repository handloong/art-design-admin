using System.ComponentModel.DataAnnotations;

namespace ArtAdmin;

/// <summary>
/// 主键Id输入参数
/// </summary>
public class BaseIdInput
{
    /// <summary>
    /// 主键Id
    /// </summary>
    [Required(ErrorMessage = "Id Not Null")]
    public virtual string Id { get; set; }
}

public class BaseIdsInput
{
    /// <summary>
    /// 主键Id
    /// </summary>
    [MinLength(1)]
    public virtual string[] Ids { get; set; }
}