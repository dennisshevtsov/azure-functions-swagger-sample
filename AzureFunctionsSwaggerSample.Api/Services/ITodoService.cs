

namespace AzureFunctionsSwaggerSample.Api.Services
{
  using System;
  using System.Threading;
  using System.Threading.Tasks;

  using AzureFunctionsSwaggerSample.Api.Dtos;

  public interface ITodoService
  {
    public Task<GetTodoListResponseDto> GetTodoListAsync(Guid todoListId, CancellationToken cancellationToken);

    public Task<GetTodoListsResponseDto> GetTodoListsAsync(CancellationToken cancellationToken);

    public Task<CreateTodoListResponseDto> CreateTodoListAsync(CreateTodoListRequestDto command, CancellationToken cancellationToken);

    public Task UpdateProductAsync(UpdateTodoListRequestDto command, CancellationToken cancellationToken);

    public Task<CreateTodoListTaskResponseDto> CreateTodoListTaskAsync(CreateTodoListTaskRequestDto command, CancellationToken cancellationToken);

    public Task CompleteTodoListTaskAsync(CompleteTodoListTaskRequestDto command, CancellationToken cancellationToken);
  }
}
