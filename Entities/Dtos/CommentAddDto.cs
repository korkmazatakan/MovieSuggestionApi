namespace Entities.Dtos
{
    public class CommentAddDto
    {
        public string Username { get; set; }
        public string Text { get; set; }
        public int MovieId { get; set; }
        public int SubCommentOf { get; set; }
    }
}