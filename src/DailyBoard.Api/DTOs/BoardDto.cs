namespace DailyBoard.Api.DTOs;

public class BoardDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid OwnerId { get; set; }
    public CardDto[] Cards { get; set; }
}
