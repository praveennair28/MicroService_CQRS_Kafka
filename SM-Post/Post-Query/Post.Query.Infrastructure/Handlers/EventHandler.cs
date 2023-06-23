using Post.Common.Events;
using Post.Query.Domain.Repositories;

namespace Post.Query.Infrastructure.Handlers 
{


    public class EventHandler : IEventHandler
    {
        private readonly IPostRepository _postRepository;
        private readonly ICommentRepository _commentRepository;

        public EventHandler(IPostRepository postRepository,ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
            _postRepository = postRepository;
        }

        public async Task On(PostCreatedEvent @event)
        {
            var post = new PostEntity(){
                PostId = @event.Id,
                Author = @event.Author,
                DatePosted = @event.DatePosted,
                Message = @event.Message
            };
            await _postRepository.CreateAsync(post);
        }

        public async Task On(MessageUpdatedEvent @event)
        {
            var post = await _postRepository.GetByIdAsync(@event.Id);

            if(post == null) return;

            post.Message = @event.Message;
            await _postRepository.UpdateAsync(post);
        }

        public async Task On(PostLikedEvent @event)
        {
            var post = await _postRepository.GetByIdAsync(@event.Id);
            if(post == null) return;

            post.Likes++;
            await _postRepository.UpdateAsync(post);
        }

        public async Task On(CommentAddedEvent @event)
        {
            var comment = new CommentEntity(){
                Comment = @event.Comment,
                CommentedDate = @event.CommentedDate,
                CommentId = @event.CommentId,
                PostId = @event.Id,
                UserName = @event.UserName,
                Edited = false
            };
            await _commentRepository.CreateAsync(comment);
        }

        public async Task On(CommentUpdatedEvent @event)
        {
            var comment = await _commentRepository.GetByIdAsync(@event.CommentId);
            if(comment == null) return;

            comment.Comment = @event.Comment;
            comment.Edited = true;
            comment.CommentedDate = @event.EditDate;

            await _commentRepository.UpdateAsync(comment);

        }

        public async Task On(CommentRemovedEvent @event)
        {
           await _commentRepository.DeleteAsync(@event.CommentId);
        }

        public async Task On(PostRemovedEvent @event)
        {
            await _postRepository.DeleteAsync(@event.Id);
        }
    }
}