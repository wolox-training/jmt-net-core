using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrainingNet.Models.DataBase
{
    public class Comment
    {
        public Comment() { }
        
        public Comment(Movie movie, string content)
        {
            this.movie = movie;
            this.content = content;
        }

        public int Id { get; set; }
        public string content { get; set; }
        public virtual Movie movie { get; set; }
    }
}