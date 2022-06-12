import ky from 'ky';
import { getEnv, Instance, IStateTreeNode, types, addMiddleware } from 'mobx-state-tree';
import { globalErrorHandlerMiddleware } from './middleware/globalErrorHandlerMiddleware';
import { TodosRepo } from './repos/TodoRepo';
import { simpleActionLogger } from 'mst-middlewares';

export interface IRootStoreModel extends Instance<typeof RootStoreModel> {}

export const RootStoreModel = types.model('RootStore', {
  todos: types.optional(TodosRepo, {}),
});

interface IStoreEnvironment {
  ajax: typeof ky;
  // history: H.History;
}

export const getDetaultStore = () => {
  const storeEnv: IStoreEnvironment = {
    ajax: ky.create({ retry: { limit: 1 } }),
    // history: history,
  };
  const store = RootStoreModel.create({}, storeEnv);
  addMiddleware(store, simpleActionLogger);
  addMiddleware(store, globalErrorHandlerMiddleware);
  return store;
};

export function getAjax(model: IStateTreeNode) {
  return (getEnv(model) as IStoreEnvironment).ajax;
}
