using System;

namespace TPostService.Entities;

public class CommentEntity : BaseIdEntity<int>, IBaseEntity
{
    public string Content { get; set; }
    public int PostId { get; set; }
    public virtual PostEntity PostEntity { get; set; }
    
    // TODO: Feature - Upload images and videos
    // public List<string>? ImageUrls { get; set; }
    // public List<string>? VideoUrls { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    public string CreatedBy { get; set; }
    public string UpdatedBy { get; set; }
    public bool IsDeleted { get; set; }
}