using CQRS.Core.Commands;

namespace Post.Cmd.Api.Command{
    public class AddCommentCommand : BaseCommand
    {
        public string UserName {get;set;} 
        public string Comment {get;set;}

    }
}