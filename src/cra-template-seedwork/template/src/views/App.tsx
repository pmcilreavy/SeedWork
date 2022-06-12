import * as React from 'react';
import { IRootStoreModel } from '../domain/RootStoreModel';
import StoreProvider from '../infrastructure/StoreProvider';
import styles from './App.module.scss';
import ListTodos from './ListTodos';

export const App: React.FC<{ store: IRootStoreModel }> = function ({ store }) {
  return (
    <StoreProvider store={store}>
      <div className={styles.root}>Hello, World!</div>
      <ListTodos />
    </StoreProvider>
  );
};

export default App;
