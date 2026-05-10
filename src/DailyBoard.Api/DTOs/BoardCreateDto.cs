namespace DailyBoard.Api.DTOs;

public class BoardCreateDto
{
    public string Name { get; set; }
    public Guid OwnerId { get; set; }
}
