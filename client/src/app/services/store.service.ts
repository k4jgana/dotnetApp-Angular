import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { map, Observable } from "rxjs";
import { LoginRequest } from "../models/LoginRequest";
import {  LoginResults } from "../models/LoginResults";
import { Order } from "../models/Order";
import { Product } from "../models/Product";
import { OrderService } from "./order.service";


@Injectable()
export class Store {
    constructor(private http: HttpClient,private orderService:OrderService) { }

    public products: Product[] = [];

    public order: Order = new Order();

    public token = "";
    private Url = 'http://localhost:8888';
    public expiration = new Date();

    get loginRequired(): boolean
    {
        return this.token.length===0 || this.expiration>new Date();
    }

    login(creds: LoginRequest)
    {
        return this.http.post<LoginResults>(`${this.Url}/account/createtoken`, creds)
            .pipe(map(data => {
                this.token = data.token,
                this.expiration=data.expiration
            }));
    }

      checkout()
    {
        const headers=new HttpHeaders().set("Authorization",`Bearer ${this.token}`)
        return this.http.post(`${this.Url}/api/orders`, this.orderService.order,
            {
                headers:headers
            }).pipe(map(()=> {
                this.order = new Order();
            }));
    }

   


    // addToOrder(product: Product) {
    //     let item = this.order.items.find(o => o.productId === product.id);

    //     if (item) {
    //         item.quantity++;
    //     } else {
    //         item = new OrderItem();
    //         item.productId = product.id;
    //         item.productArtist = product.artist;
    //         item.productCategory = product.category;
    //         item.productArtId = product.artId;
    //         item.productTitle = product.title;
    //         item.productSize = product.size;
    //         item.unitPrice = product.price;
    //         item.quantity = 1;
    //         this.order.items.push(item);
    //     }
    // }

  

    // removeFromOrder(productId: number) {
    //     const itemIndex = this.order.items.findIndex(o => o.productId === productId);
    //     if (itemIndex >= 0) {
    //         const item = this.order.items[itemIndex];
    //         if (item.quantity > 1) {
    //             item.quantity--;
    //         } else {
    //             this.order.items.splice(itemIndex, 1);
    //         }
    //     }
    // }





    // loadProducts(): Observable<void> {
    //     return this.http.get<[]>(`${this.Url}/api/products`).pipe(
    //         map((data) => {
    //             this.products = data;
    //             return;
    //         })
    //     );
    // }
}
