using System;
using System.Collections.Generic;
using SharedModels.Enums;

namespace TPostService.Entities;

public class ReactionEntity : BaseIdEntity<int>, IBaseEntity
{
    public int PostId { get; set; }
    public string UserId { get; set; }
    public ReactionTypeEnum ReactionTypeEnum { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    public string CreatedBy { get; set; }
    public string UpdatedBy { get; set; }
    public bool IsDeleted { get; set; }
    
    public PostEntity PostEntity { get; set; }

}