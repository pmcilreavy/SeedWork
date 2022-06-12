import { IMiddlewareHandler } from 'mobx-state-tree';

export const globalErrorHandlerMiddleware: IMiddlewareHandler = (call, next, abort) => {
  // TODO implement global error handler
  next(call);
};
