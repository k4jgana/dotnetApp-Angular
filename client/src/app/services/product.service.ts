import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Product } from "../models/Product";




@Injectable()
export class ProductService {
   
    public products$: Observable<Product[]>;
    private Url = 'http://localhost:8888';

    constructor(private http: HttpClient) { 
        this.products$ = this.loadProducts();
      }
      

    loadProducts(): Observable<Product[]> {
        this.products$ = this.http.get<Product[]>(`${this.Url}/api/products`);
        return this.products$;
      }
}

