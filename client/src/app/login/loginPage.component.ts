import { Component } from "@angular/core";
import { Router } from "@angular/router";
import { LoginRequest } from "../models/LoginRequest";
import { Store } from "../services/store.service";


@Component({
    selector: "login-page",
    templateUrl:"loginPage.component.html"
})
export class LoginPage
{
    constructor(private store: Store, private router: Router) { }

    public creds:LoginRequest =
        {
            username:"",
            password:""
        };



    public errorMessage = "";


    onLogin()
    {
        this.store.login(this.creds)
            .subscribe(() => {
                if (this.store.order.items.length > 0) {
                    this.router.navigate(["checkout"]);
                } else {
                    this.router.navigate([""]);
                }
            }, err => { this.errorMessage = "failed to log in"; })
    }


/*    onLogin() {
        this.store.login(this.creds).subscribe(
            () => {
                this.router.navigate(["checkout"]);
            },
            (err) => {
                this.errorMessage = "Failed to log in";
            }
        );
    }*/



    
}