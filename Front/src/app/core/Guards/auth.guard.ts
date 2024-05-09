import {ActivatedRouteSnapshot, CanActivateFn, Router, RouterStateSnapshot, UrlTree} from '@angular/router';
import {Observable} from "rxjs";
import {inject} from "@angular/core";
import {StoreService} from "../services/store/store.service";

export const authGuard: CanActivateFn = (  route: ActivatedRouteSnapshot,
                                           state: RouterStateSnapshot):boolean|UrlTree|Observable<boolean|UrlTree> => {

  return inject(StoreService).isLogged() ? true : inject(Router).createUrlTree(['Login']);
};
