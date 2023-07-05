using Clean_architecture.Application.Common.Mappings;
using Clean_architecture.Domain.Entities;

namespace Clean_architecture.Application.TodoLists.Queries.ExportTodos;

public class TodoItemRecord : IMapFrom<TodoItem>
{
    public string? Title { get; init; }

    public bool Done { get; init; }
}
