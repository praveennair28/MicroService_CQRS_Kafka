using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Post.Query.Domain.Repositories
{
    [Table("Comment")]
    public class CommentEntity
    {
        [Key]
        public Guid CommentId {get;set;}
        public string UserName {get;set;}
        public DateTime CommentedDate {get;set;}
        public string Comment {get;set;}        
        public bool  Edited {get;set;}
        public Guid PostId {get;set;}
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual PostEntity Post {get;set;}
    }
}