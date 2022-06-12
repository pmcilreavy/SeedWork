import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.scss';
import App from './views/App';
import reportWebVitals from './reportWebVitals';
import { getDetaultStore, IRootStoreModel } from './domain/RootStoreModel';
import { onSnapshot } from 'mobx-state-tree';

const defaultStore: IRootStoreModel = getDetaultStore();

onSnapshot(defaultStore, (snapshot) => console.log(JSON.stringify(snapshot)));

const root = ReactDOM.createRoot(document.getElementById('root') as HTMLElement);
root.render(
  <React.StrictMode>
    <App store={defaultStore} />
  </React.StrictMode>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
