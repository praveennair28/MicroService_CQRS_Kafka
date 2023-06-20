using CQRS.Core.Commands;

namespace Post.Cmd.Api.Command{
    public class EditCommentCommand : BaseCommand
    {
        public Guid CommentID {get;set;} 
        public string Comment {get;set;}
        public string UserName {get;set;}

    }
}