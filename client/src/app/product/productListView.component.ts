
// import { Component, OnInit } from "@angular/core";
// import { OrderService } from "../services/order.service";
// import { ProductService } from "../services/product.service";
// import { Product } from "../shared/Product";
// // import { Store } from "../services/store.service";

// @Component({
//     selector: "product-list",
//     templateUrl: "productListView.component.html",
//     styleUrls: ["productListView.component.css"]
    
// })
// export default class ProductListView implements OnInit{

//     products:Product[]=[];



//     constructor(public orderService:OrderService,public productService: ProductService) {

//     }

//     ngOnInit(): void {
//         this.productService.loadProducts().subscribe()
//     }
// }



import { Component, OnInit } from "@angular/core";
import { Observable } from "rxjs";
import { OrderService } from "../services/order.service";
import { ProductService } from "../services/product.service";
import { Product } from "../models/Product";

@Component({
    selector: "product-list",
    templateUrl: "productListView.component.html",
    styleUrls: ["productListView.component.css"]
})
export default class ProductListView implements OnInit{

    public products$!: Observable<Product[]>;

    constructor(
        public orderService: OrderService,
        public productService: ProductService
    ) {}

    ngOnInit(): void {
        this.products$ = this.productService.loadProducts();
    }
}

