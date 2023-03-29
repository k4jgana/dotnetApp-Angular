import { Component } from "@angular/core";
import { OrderService } from "../services/order.service";
import { ProductService } from "../services/product.service";

@Component({
    selector: "cart",
    templateUrl: "cartView.component.html",
    styleUrls: ["cartView.component.css"]
})
export class CartView {
    constructor(public orderService:OrderService, public productService:ProductService)
    {

    }
}
