import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { map } from "rxjs";
import { LoginRequest } from "../models/LoginRequest";
import {LoginResults } from "../models/LoginResults";
import { Order} from "../models/Order";
import { OrderItem } from "../models/OrderItem";
import { Product } from "../models/Product";




@Injectable()
export class OrderService {

    public order: Order = new Order();
    public items: OrderItem[] = [];
    public token = "";
    private Url = 'http://localhost:8888';
    public expiration = new Date();


    constructor(private http:HttpClient) {
        this.items=this.order.items;
     }

    addToOrder(product: Product) {
       
        let item = this.order.items.find(o => o.productId === product.id);

        if (item) {
            item.quantity++;
        } else {
            item = new OrderItem();
            item.productId = product.id;
            item.productArtist = product.artist;
            item.productCategory = product.category;
            item.productArtId = product.artId;
            item.productTitle = product.title;
            item.productSize = product.size;
            item.unitPrice = product.price;
            item.quantity = 1;
            this.order.items.push(item);
        }
    }

    removeFromOrder(productId: number) {
        const itemIndex = this.order.items.findIndex(o => o.productId === productId);
        if (itemIndex >= 0) {
            const item = this.order.items[itemIndex];
            if (item.quantity > 1) {
                item.quantity--;
            } else {
                this.order.items.splice(itemIndex, 1);
            }
        }
    }

   

    // checkout()
    // {
    //     const headers=new HttpHeaders().set("Authorization",`Bearer ${this.token}`)
    //     return this.http.post(`${this.Url}/api/orders`, this.order,
    //         {
    //             headers:headers
    //         }).pipe(map(()=> {
    //             this.order = new Order();
    //         }));
    // }

    // get loginRequired(): boolean
    // {
    //     return this.token.length===0 || this.expiration>new Date();
    // }

    login(creds: LoginRequest)
    {
        return this.http.post<LoginResults>(`${this.Url}/account/createtoken`, creds)
            .pipe(map(data => {
                this.token = data.token,
                this.expiration=data.expiration
            }));
    }

}


