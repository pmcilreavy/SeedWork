import { StoreContext } from 'infrastructure/StoreProvider';
import React from 'react';
import { IRootStoreModel } from '../domain/RootStoreModel';

export const useRootStore = <Selection>(dataSelector: (store: IRootStoreModel) => Selection) =>
  useStoreData(StoreContext, (contextData) => contextData!, dataSelector);

const useStoreData = <Selection, ContextData, Store>(
  context: React.Context<ContextData>,
  storeSelector: (contextData: ContextData) => Store,
  dataSelector: (store: Store) => Selection
) => {
  const value = React.useContext(context);
  const store = storeSelector(value);
  return dataSelector(store);
};
