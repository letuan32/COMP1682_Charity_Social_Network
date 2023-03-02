namespace T_PostService.Entities;

public interface IBaseEntity
{
    int Id { get; set; }
    DateTime CreatedDate { get; set; } 
    DateTime UpdatedDate { get; set; } 
    string CreatedBy { get; set; }
    string UpdatedBy { get; set; }
    bool IsDeleted { get; set; }
}