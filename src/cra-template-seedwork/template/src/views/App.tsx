import * as React from 'react';
import { IRootStoreModel } from '../domain/RootStoreModel';
import StoreProvider from '../infrastructure/StoreProvider';
import ListTodos from './ListTodos';

export const App: React.FC<{ store: IRootStoreModel }> = function ({ store }) {
  return (
    <StoreProvider store={store}>
      <div className="max-w-screen-md bg-gray-200 my-10 mx-auto p-10 rounded-3xl">
        <ListTodos />
      </div>
    </StoreProvider>
  );
};

export default App;
