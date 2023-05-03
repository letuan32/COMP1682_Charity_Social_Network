namespace APIGateway.DTOs.Users;

public class GetUserResponse
{
    public string Id { get; set; }
    public string Email { get; set; }
    public string DisplayName { get; set; }
    public string ProfilePicture { get; set; }
    public int TotalPost { get; set; }
    public bool IsVerify { get; set; }
    public bool IsDisable { get; set; }
}