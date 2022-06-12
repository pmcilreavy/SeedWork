import { getAjax } from 'domain/RootStoreModel';
import { cast, flow, Instance, types } from 'mobx-state-tree';

export const TodoModel = types.model('TodoModel', {
  id: types.identifier,
  title: types.string,
  description: types.string,
});

export interface ITodoModel extends Instance<typeof TodoModel> {}
export type TodoModelType = Instance<typeof TodoModel>;

export const TodosRepo = types
  .model('TodosRepo', {
    todos: types.optional(types.array(TodoModel), []),
  })
  .actions((self) => {
    const ajax = getAjax(self);

    const listTodos = flow(function* () {
      console.log('listTodos');

      const response = yield ajax.get('/api/todo/list').then((r) => r.json());

      const data = response;

      self.todos = cast(data as Array<ITodoModel>);
    });

    return { listTodos };
  })
  .actions((self) => {
    const ajax = getAjax(self);

    const createTodo = flow(function* (title: string, description: string) {
      const response = yield ajax.post('/api/todo/create', {
        json: { title: title, description: description },
      });

      self.listTodos();

      console.log(response);
    });

    return { createTodo };
  });
