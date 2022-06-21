//using Microsoft.EntityFrameworkCore;
//using SeedWork;
//using SeedWork.Command;

//namespace Todo.Domain.Aggregates.Todo.Commands.ListTodos;

//public class ListTodosCommandHandler : ICommandHandler<ListTodosCommand, IReadOnlyCollection<ListTodoDto>>
//{
//    private readonly IReadRepository _reader;

//    public ListTodosCommandHandler(IReadRepository reader) => _reader = reader;

//    public async Task<IReadOnlyCollection<ListTodoDto>> Handle(ListTodosCommand command, CancellationToken cancellationToken)
//    {
//        var results = await _reader.GetTable<Todo>()
//                                   .OrderByDescending(o => o.ModifiedOn)
//                                   .Select(o => new ListTodoDto(o.Id, o.Title, o.Description))
//                                   .ToArrayAsync(cancellationToken);

//        return results;
//    }
//}
