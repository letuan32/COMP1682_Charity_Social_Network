using System;
using System.Collections.Generic;

namespace TPostService.Entities;

public class PostEntity : BaseIdEntity<int>, IBaseEntity
{
    public string Content { get; set; }
    
    // TODO: Consider to use one column for media urls (image, url)
    public List<string>? ImagePaths { get; set; }
    public List<string>? VideoPaths { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    public string CreatedBy { get; set; }
    public string UpdatedBy { get; set; }
    public bool IsDeleted { get; set; }
    
    public virtual ICollection<CommentEntity> CommentsEntities { get; set; }
}