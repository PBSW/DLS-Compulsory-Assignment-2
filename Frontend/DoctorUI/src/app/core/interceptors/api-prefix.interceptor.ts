import { HttpInterceptorFn } from '@angular/common/http';
import { environment } from '../../../environments/environment.development';

export const ApiPrefixInterceptor: HttpInterceptorFn = (req, next) => {

  const baseUrl = environment.baseUrl;

  if (!req.url.startsWith('http')) {
    req = req.clone({ url: baseUrl + req.url });
  }

  return next(req);
};
