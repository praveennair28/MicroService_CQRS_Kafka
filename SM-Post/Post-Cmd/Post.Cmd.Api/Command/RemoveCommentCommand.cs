using CQRS.Core.Commands;

namespace Post.Cmd.Api.Command{
    public class RemoveCommentCommand : BaseCommand
    {
        public Guid CommentID {get;set;} 
        public string UserName {get;set;}
    }
}