namespace DailyBoard.Api.DTOs;

public class CardDto
{
    public Guid Id { get; set; }
    public Guid BoardId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int Status { get; set; }
    public DateTime DueDate { get; set; }
    public int Position { get; set; }
}
