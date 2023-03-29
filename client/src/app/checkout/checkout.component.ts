﻿ import { Component } from "@angular/core";
import { Router } from "@angular/router";
import { OrderService } from "../services/order.service";
import { Store } from '../services/store.service';

@Component({
  selector: "checkout",
  templateUrl: "checkout.component.html",
  styleUrls: ['checkout.component.css']
})
export class Checkout {

    public errorMessage = "";

    constructor(public store: Store, private router: Router,public orderService: OrderService) {
  }

    onCheckout() {

        this.errorMessage = "";  
        this.store.checkout().subscribe(() =>
        {
            this.router.navigate(["/"]);
        }, err =>
        {
            this.errorMessage = `Failed to checkout: ${err}`;
        })
  }
}