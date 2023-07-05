using Clean_architecture.Application.TodoLists.Queries.ExportTodos;

namespace Clean_architecture.Application.Common.Interfaces;

public interface ICsvFileBuilder
{
    byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
}
