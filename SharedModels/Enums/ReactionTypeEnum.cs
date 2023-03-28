using System.ComponentModel;

namespace SharedModels.Enums;

public enum ReactionTypeEnum
{
    [Description("Like")]
    Like = 1,
    
    [Description("Dislike")]
    Dislike = 2
}