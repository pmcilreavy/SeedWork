import * as React from 'react';
import { IRootStoreModel } from '../domain/RootStoreModel';

export const StoreContext = React.createContext<IRootStoreModel | null>(null);

export const StoreProvider: React.FC<React.PropsWithChildren<{ store: IRootStoreModel }>> = ({
  children,
  store,
}) => {
  return <StoreContext.Provider value={store}>{children}</StoreContext.Provider>;
};

export default StoreProvider;
