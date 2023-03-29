import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { AppComponent } from './app.component';
import { Store } from './services/store.service';

import router from './router';

import { AuthActivator } from './services/authActivator.service';
import { FormsModule } from '@angular/forms';
import { OrderService } from './services/order.service';
import { ProductService } from './services/product.service';
import { CommonModule } from '@angular/common';
import ProductListView from './product/productListView.component';
import { CartView } from './cart/cartView.component';
import { ShopPage } from './shop/shopPage.component';
import { Checkout } from './checkout/checkout.component';
import { LoginPage } from './login/loginPage.component';
@NgModule({
  declarations: [
        AppComponent,
        ProductListView,
        CartView,
        ShopPage,
        Checkout,
        LoginPage,
  ],
  imports: [
      BrowserModule,
      HttpClientModule,
      router,
    FormsModule,
    CommonModule
  ],
    providers: [
        Store,
        AuthActivator,
        OrderService,
        ProductService
    ],
  bootstrap: [AppComponent]
})
export class AppModule { }
