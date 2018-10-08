using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrainingNet.Models.DataBase
{
    public class Comment
    {
        public Comment() { }
        
        public Comment(Movie movie, string content)
        {
            this.Movie = movie;
            this.Content = content;
        }

        public int Id { get; set; }
        public string Content { get; set; }
        public virtual Movie Movie { get; set; }
    }
}
