using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrainingNet.Models.DataBase
{
    public class Comment
    {
        public Comment() { }
        public Comment(Movie movie, string content)
        {
            Movie = movie;
            Content = content;
        }
        public int Id { get; set; }
        public string Content { get; set; }
        public Movie Movie { get; set; }
    }
}