import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from "@angular/router";
import { JwtHelperService } from "@auth0/angular-jwt";
import { Observable } from "rxjs";

@Injectable()
export class AuthGuard implements CanActivate{

    constructor(private router: Router, private JwtHelper: JwtHelperService){

    }
    canActivate(){
        const token = localStorage.getItem("Jwt");
        if (token && !this.JwtHelper.isTokenExpired(token)){
            return true;
        }
        this.router.navigate([''])
        return false;
    }
}