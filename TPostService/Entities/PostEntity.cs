using System;
using System.Collections.Generic;
using SharedModels.Enums;

namespace TPostService.Entities;

public class PostEntity : BaseIdEntity<int>, IBaseEntity
{
    public string Content { get; set; }
    
    public List<string>? MediaUrls { get; set; }
    public List<string>? DocumentUrls { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    public string CreatedBy { get; set; }
    public string UpdatedBy { get; set; }
    public bool IsDeleted { get; set; }
    public int Donations { get; set; }
    public int Views { get; set; }
    public long ExpectedAmount { get; set; }
    public string Location { get; set; }
    public DateTime? ExpectedReceivedDate { get; set; }
    public PostCategoryEnum PostCategoryEnum { get; set; }
    public CurrencyEnum CurrencyEnum { get; set; }
    public PostApproveStatusEnum ApproveStatusEnum { get; set; }

    public virtual ICollection<CommentEntity> CommentsEntities { get; set; }
    public virtual ICollection<ReactionEntity> ReactionEntities { get; set; }

}