import { HttpInterceptorFn } from '@angular/common/http';
import { environment } from '../../environments/environment';

export const ApiPrefixInterceptor: HttpInterceptorFn = (req, next) => {

  const api = environment.api;

  // Clone the request to add the url
  const clonedRequest = req.clone({
    url: `${api}${req.url}`,
  });

  // Pass the cloned request instead of the original request to the next handle
  return next(clonedRequest);
};
