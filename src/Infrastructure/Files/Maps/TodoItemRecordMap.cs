using System.Globalization;
using Clean_architecture.Application.TodoLists.Queries.ExportTodos;
using CsvHelper.Configuration;

namespace Clean_architecture.Infrastructure.Files.Maps;

public class TodoItemRecordMap : ClassMap<TodoItemRecord>
{
    public TodoItemRecordMap()
    {
        AutoMap(CultureInfo.InvariantCulture);

        Map(m => m.Done).Convert(c => c.Value.Done ? "Yes" : "No");
    }
}
